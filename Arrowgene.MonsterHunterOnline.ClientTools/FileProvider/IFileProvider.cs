using System.Collections.Generic;
using System.IO;

namespace Arrowgene.MonsterHunterOnline.ClientTools.FileProvider;

public interface IFileProvider
{
    bool Exists(string relativePath);
    byte[] ReadAllBytes(string relativePath);
    Stream OpenRead(string relativePath);
    IEnumerable<string> EnumerateFiles(string relativeDir, string pattern);
    IEnumerable<string> EnumerateDirectories(string relativeDir);
}
