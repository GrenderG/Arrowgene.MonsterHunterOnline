#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Arrowgene.MonsterHunterOnline.ClientTools.IIPS;

public sealed class IIPSFileList
{
    public string? ReleaseName { get; private set; }
    public IReadOnlyList<IIPSFileListVersion> Versions => _versions;

    private readonly List<IIPSFileListVersion> _versions = [];

    public static IIPSFileList Parse(string path)
    {
        return Parse(File.ReadAllLines(path));
    }

    public static IIPSFileList Parse(string[] lines)
    {
        IIPSFileList fileList = new IIPSFileList();
        string? currentSection = null;
        IIPSFileListVersion? currentVersion = null;

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                string sectionName = line[1..^1].ToLowerInvariant();

                if (sectionName == "subversion")
                {
                    currentVersion = new IIPSFileListVersion();
                    fileList._versions.Add(currentVersion);
                }

                currentSection = sectionName;
                continue;
            }

            int eqIndex = line.IndexOf('=');
            if (eqIndex < 0)
            {
                continue;
            }

            string key = line[..eqIndex].Trim().ToLowerInvariant();
            string value = line[(eqIndex + 1)..].Trim();

            if (currentSection == "releasename")
            {
                if (key == "name")
                {
                    fileList.ReleaseName = value;
                }
            }
            else if (currentSection == "subversion" && currentVersion != null)
            {
                switch (key)
                {
                    case "version":
                        currentVersion.Version = value;
                        break;
                    case "patch":
                        currentVersion.PatchFile = value;
                        break;
                    case "base":
                        currentVersion.BaseFiles = ParseFileList(value);
                        break;
                    case "high":
                        currentVersion.HighFiles = ParseFileList(value);
                        break;
                }
            }
        }

        return fileList;
    }

    /// <summary>
    /// Returns the final resolved set of IFS files that represent the complete client state.
    /// Base/high files use the last version that defines them, patches accumulate from that point.
    /// </summary>
    public IIPSFileListResolvedState Resolve()
    {
        List<string> baseFiles = [];
        List<string> highFiles = [];
        List<string> patchFiles = [];
        int baseDefinedAt = -1;
        int highDefinedAt = -1;

        // Walk forward to find the last version that defines base/high
        for (int i = _versions.Count - 1; i >= 0; i--)
        {
            if (baseDefinedAt < 0 && _versions[i].BaseFiles.Count > 0)
            {
                baseDefinedAt = i;
                baseFiles.AddRange(_versions[i].BaseFiles);
            }

            if (highDefinedAt < 0 && _versions[i].HighFiles.Count > 0)
            {
                highDefinedAt = i;
                highFiles.AddRange(_versions[i].HighFiles);
            }

            if (baseDefinedAt >= 0 && highDefinedAt >= 0)
            {
                break;
            }
        }

        // Collect patches after the last base rebuild
        int patchStart = Math.Max(baseDefinedAt, highDefinedAt);
        for (int i = patchStart + 1; i < _versions.Count; i++)
        {
            if (!string.IsNullOrEmpty(_versions[i].PatchFile))
            {
                patchFiles.Add(_versions[i].PatchFile!);
            }
        }

        string? latestVersion = _versions.Count > 0 ? _versions[^1].Version : null;

        return new IIPSFileListResolvedState(latestVersion, baseFiles, highFiles, patchFiles);
    }

    /// <summary>
    /// Returns all IFS filenames referenced across the entire file list (every version).
    /// </summary>
    public List<string> GetAllReferencedFiles()
    {
        HashSet<string> seen = new(StringComparer.OrdinalIgnoreCase);
        List<string> result = [];

        foreach (IIPSFileListVersion version in _versions)
        {
            foreach (string f in version.BaseFiles)
            {
                if (seen.Add(f)) result.Add(f);
            }

            foreach (string f in version.HighFiles)
            {
                if (seen.Add(f)) result.Add(f);
            }

            if (!string.IsNullOrEmpty(version.PatchFile) && seen.Add(version.PatchFile!))
            {
                result.Add(version.PatchFile!);
            }
        }

        return result;
    }

    private static List<string> ParseFileList(string value)
    {
        return value
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList();
    }
}

public sealed class IIPSFileListVersion
{
    public string? Version { get; internal set; }
    public string? PatchFile { get; internal set; }
    public List<string> BaseFiles { get; internal set; } = [];
    public List<string> HighFiles { get; internal set; } = [];

    public bool HasBaseFiles => BaseFiles.Count > 0;
    public bool HasHighFiles => HighFiles.Count > 0;
    public bool HasPatchFile => !string.IsNullOrEmpty(PatchFile);
}

public sealed class IIPSFileListResolvedState
{
    public IIPSFileListResolvedState(
        string? version,
        List<string> baseFiles,
        List<string> highFiles,
        List<string> patchFiles)
    {
        Version = version;
        BaseFiles = baseFiles;
        HighFiles = highFiles;
        PatchFiles = patchFiles;
    }

    public string? Version { get; }
    public List<string> BaseFiles { get; }
    public List<string> HighFiles { get; }
    public List<string> PatchFiles { get; }

    /// <summary>
    /// All IFS files in load order: base first, then high, then patches chronologically.
    /// </summary>
    public IEnumerable<string> AllFilesInOrder =>
        BaseFiles.Concat(HighFiles).Concat(PatchFiles);
}
