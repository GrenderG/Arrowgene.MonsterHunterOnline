using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Arrowgene.MonsterHunterOnline.ClientTools.FileProvider;

/// <summary>
/// Chains multiple file providers. First provider that has a file wins.
/// </summary>
public sealed class CompositeFileProvider : IFileProvider
{
    private readonly IFileProvider[] _providers;

    public CompositeFileProvider(params IFileProvider[] providers)
    {
        _providers = providers;
    }

    public bool Exists(string relativePath)
    {
        foreach (IFileProvider p in _providers)
            if (p.Exists(relativePath)) return true;
        return false;
    }

    public byte[] ReadAllBytes(string relativePath)
    {
        foreach (IFileProvider p in _providers)
            if (p.Exists(relativePath)) return p.ReadAllBytes(relativePath);
        throw new FileNotFoundException($"Not found in any provider: {relativePath}");
    }

    public Stream OpenRead(string relativePath)
    {
        foreach (IFileProvider p in _providers)
            if (p.Exists(relativePath)) return p.OpenRead(relativePath);
        throw new FileNotFoundException($"Not found in any provider: {relativePath}");
    }

    public IEnumerable<string> EnumerateFiles(string relativeDir, string pattern)
    {
        HashSet<string> seen = new(System.StringComparer.OrdinalIgnoreCase);
        foreach (IFileProvider p in _providers)
            foreach (string f in p.EnumerateFiles(relativeDir, pattern))
                if (seen.Add(f)) yield return f;
    }

    public IEnumerable<string> EnumerateDirectories(string relativeDir)
    {
        HashSet<string> seen = new(System.StringComparer.OrdinalIgnoreCase);
        foreach (IFileProvider p in _providers)
            foreach (string d in p.EnumerateDirectories(relativeDir))
                if (seen.Add(d)) yield return d;
    }
}
