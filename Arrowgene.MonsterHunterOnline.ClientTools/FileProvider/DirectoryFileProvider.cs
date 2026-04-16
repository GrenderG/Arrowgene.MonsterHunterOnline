using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Arrowgene.MonsterHunterOnline.ClientTools.FileProvider;

public sealed class DirectoryFileProvider : IFileProvider
{
    private readonly string _root;

    public DirectoryFileProvider(string rootDirectory)
    {
        _root = rootDirectory;
    }

    public string Root => _root;

    public bool Exists(string relativePath)
    {
        return File.Exists(Resolve(relativePath));
    }

    public byte[] ReadAllBytes(string relativePath)
    {
        return File.ReadAllBytes(Resolve(relativePath));
    }

    public Stream OpenRead(string relativePath)
    {
        return File.OpenRead(Resolve(relativePath));
    }

    public IEnumerable<string> EnumerateFiles(string relativeDir, string pattern)
    {
        string dir = Resolve(relativeDir);
        if (!Directory.Exists(dir))
            return [];
        return Directory.GetFiles(dir, pattern)
            .Select(f => Path.GetRelativePath(_root, f).Replace('\\', '/'));
    }

    public IEnumerable<string> EnumerateDirectories(string relativeDir)
    {
        string dir = Resolve(relativeDir);
        if (!Directory.Exists(dir))
            return [];
        return Directory.GetDirectories(dir)
            .Select(d => Path.GetRelativePath(_root, d).Replace('\\', '/'));
    }

    private string Resolve(string relativePath)
    {
        return Path.Combine(_root, relativePath.Replace('/', Path.DirectorySeparatorChar));
    }
}
