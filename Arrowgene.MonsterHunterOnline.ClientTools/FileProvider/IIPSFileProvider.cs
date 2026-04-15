using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Arrowgene.MonsterHunterOnline.ClientTools.IIPS;

namespace Arrowgene.MonsterHunterOnline.ClientTools.FileProvider;

public sealed class IIPSFileProvider : IFileProvider
{
    private readonly IIPSUnifiedArchive _archive;
    private readonly Dictionary<string, IIPSArchiveEntry> _lookup;

    public IIPSFileProvider(IIPSUnifiedArchive archive)
    {
        _archive = archive;
        _lookup = new Dictionary<string, IIPSArchiveEntry>(StringComparer.OrdinalIgnoreCase);
        foreach (IIPSArchiveEntry entry in archive.MergedEntries)
        {
            if (entry.Exists && !string.IsNullOrEmpty(entry.ArchivePath))
            {
                string key = Normalize(entry.ArchivePath!);
                _lookup[key] = entry;
            }
        }
    }

    public IIPSUnifiedArchive Archive => _archive;

    public bool Exists(string relativePath)
    {
        return _lookup.ContainsKey(Normalize(relativePath));
    }

    public byte[] ReadAllBytes(string relativePath)
    {
        if (_lookup.TryGetValue(Normalize(relativePath), out IIPSArchiveEntry? entry))
            return entry.ReadAllBytes();
        throw new FileNotFoundException($"Entry not found in archive: {relativePath}");
    }

    public Stream OpenRead(string relativePath)
    {
        return new MemoryStream(ReadAllBytes(relativePath), writable: false);
    }

    public IEnumerable<string> EnumerateFiles(string relativeDir, string pattern)
    {
        string prefix = Normalize(relativeDir);
        if (!prefix.EndsWith('/'))
            prefix += '/';

        // Convert simple glob pattern (e.g. "*.dat", "quest_*.dat") to a match function
        string ext = Path.GetExtension(pattern);
        string namePrefix = pattern.Contains('*') ? pattern[..pattern.IndexOf('*')] : pattern;

        return _lookup.Keys
            .Where(k =>
            {
                if (!k.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    return false;
                // Only direct children (no deeper subdirectories)
                string remainder = k[prefix.Length..];
                if (remainder.Contains('/'))
                    return false;
                // Match pattern
                if (!string.IsNullOrEmpty(namePrefix) && !remainder.StartsWith(namePrefix, StringComparison.OrdinalIgnoreCase))
                    return false;
                if (!string.IsNullOrEmpty(ext) && !remainder.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                    return false;
                return true;
            });
    }

    public IEnumerable<string> EnumerateDirectories(string relativeDir)
    {
        string prefix = Normalize(relativeDir);
        if (!prefix.EndsWith('/'))
            prefix += '/';

        HashSet<string> dirs = new(StringComparer.OrdinalIgnoreCase);
        foreach (string key in _lookup.Keys)
        {
            if (!key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                continue;
            string remainder = key[prefix.Length..];
            int sep = remainder.IndexOf('/');
            if (sep > 0)
            {
                dirs.Add(prefix + remainder[..sep]);
            }
        }

        return dirs;
    }

    private static string Normalize(string path)
    {
        return path.Replace('\\', '/');
    }
}
