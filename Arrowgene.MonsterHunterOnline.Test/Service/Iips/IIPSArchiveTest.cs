#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Text;
using Arrowgene.MonsterHunterOnline.ClientTools.IIPS;
using Xunit;

namespace Arrowgene.MonsterHunterOnline.Test.Service.Iips;

public class IIPSArchiveTest
{
    [Fact]
    public void CryptoHelpersRoundTrip()
    {
        byte[] sectionData = Enumerable.Range(0, 32).Select(i => (byte)i).ToArray();
        byte[] encryptedSection = (byte[])sectionData.Clone();
        IIPSArchiveCrypto.IfsSectionEncrypt(encryptedSection);
        IIPSArchiveCrypto.IfsSectionDecrypt(encryptedSection);
        Assert.Equal(sectionData, encryptedSection);

        byte[] blockData = Enumerable.Range(0, 64).Select(i => (byte)(255 - i)).ToArray();
        byte[] encryptedBlock = (byte[])blockData.Clone();
        IIPSArchiveCrypto.MpqEncryptBlock(encryptedBlock, 0x12345678);
        IIPSArchiveCrypto.MpqDecryptBlock(encryptedBlock, 0x12345678);
        Assert.Equal(blockData, encryptedBlock);
    }

    [Fact]
    public void CanCreateArchiveAndRoundTripEntries()
    {
        string tempDirectory = CreateTempDirectory();
        try
        {
            string archivePath = Path.Combine(tempDirectory, "created.ifs");
            byte[] plain = Encoding.UTF8.GetBytes("plain archive entry");
            byte[] compressed = Encoding.UTF8.GetBytes(new string('A', 5000));
            byte[] encryptedSector = Enumerable.Range(0, 9000).Select(i => (byte)(i % 251)).ToArray();

            using IIPSArchive archive = IIPSArchive.CreateNew();
            archive.Add("plain.txt", plain);
            archive.Add("folder\\compressed.txt", compressed, new IIPSArchiveEntryOptions
            {
                StorageMode = IIPSArchiveStorageMode.SingleUnit,
                Compress = true,
            });
            archive.Add("folder\\encrypted.bin", encryptedSector, new IIPSArchiveEntryOptions
            {
                StorageMode = IIPSArchiveStorageMode.SectorBased,
                Compress = true,
                Encrypt = true,
                UseFixedKey = true,
            });

            archive.Save(archivePath);

            using IIPSArchive reopened = IIPSArchive.Open(archivePath);
            Assert.Equal(plain, reopened.Extract("plain.txt"));
            Assert.Equal(compressed, reopened.Extract("folder\\compressed.txt"));
            Assert.Equal(encryptedSector, reopened.Extract("folder\\encrypted.bin"));

            Assert.True(reopened.TryGetEntry("(listfile)", out IIPSArchiveEntry? listFile));
            Assert.NotNull(listFile);
            string listFileContent = Encoding.UTF8.GetString(reopened.Extract(listFile!));
            Assert.Contains("plain.txt", listFileContent);
            Assert.Contains("folder\\compressed.txt", listFileContent);
            Assert.Contains("folder\\encrypted.bin", listFileContent);
        }
        finally
        {
            Directory.Delete(tempDirectory, recursive: true);
        }
    }

    [Fact]
    public void CanAddModifyAndRemoveAcrossSaves()
    {
        string tempDirectory = CreateTempDirectory();
        try
        {
            string baseArchivePath = Path.Combine(tempDirectory, "base.ifs");
            string modifiedArchivePath = Path.Combine(tempDirectory, "modified.ifs");

            byte[] preservedEncrypted = Enumerable.Range(0, 4096).Select(i => (byte)(i % 197)).ToArray();
            byte[] originalText = Encoding.UTF8.GetBytes("before");
            byte[] replacementText = Encoding.UTF8.GetBytes("after");
            byte[] newEntryContent = Enumerable.Range(0, 1536).Select(i => (byte)(i % 223)).ToArray();

            using (IIPSArchive initial = IIPSArchive.CreateNew())
            {
                initial.Add("keep\\fixed.bin", preservedEncrypted, new IIPSArchiveEntryOptions
                {
                    StorageMode = IIPSArchiveStorageMode.SectorBased,
                    Compress = true,
                    Encrypt = true,
                    UseFixedKey = true,
                });
                initial.Add("replace.txt", originalText);
                initial.Add("remove.txt", Encoding.UTF8.GetBytes("remove me"));
                initial.Save(baseArchivePath);
            }

            using IIPSArchive archive = IIPSArchive.Open(baseArchivePath);
            archive.Modify("replace.txt", replacementText, new IIPSArchiveEntryOptions
            {
                StorageMode = IIPSArchiveStorageMode.SingleUnit,
                Compress = true,
                Encrypt = true,
            });
            archive.Add("new\\entry.bin", newEntryContent, new IIPSArchiveEntryOptions
            {
                StorageMode = IIPSArchiveStorageMode.SectorBased,
                Compress = true,
            });
            Assert.True(archive.Remove("remove.txt"));
            archive.Save(modifiedArchivePath);

            using IIPSArchive reopened = IIPSArchive.Open(modifiedArchivePath);
            Assert.Equal(preservedEncrypted, reopened.Extract("keep\\fixed.bin"));
            Assert.Equal(replacementText, reopened.Extract("replace.txt"));
            Assert.Equal(newEntryContent, reopened.Extract("new\\entry.bin"));
            Assert.False(reopened.TryGetEntry("remove.txt", out _));
        }
        finally
        {
            Directory.Delete(tempDirectory, recursive: true);
        }
    }

    [Fact]
    public void AddDirectoryPreservesArchiveStructureOnExtract()
    {
        string tempDirectory = CreateTempDirectory();
        try
        {
            string sourceRoot = Path.Combine(tempDirectory, "source");
            string nestedDirectory = Path.Combine(sourceRoot, "nested");
            string archivePath = Path.Combine(tempDirectory, "structured.ifs");
            string extractDirectory = Path.Combine(tempDirectory, "extract");

            Directory.CreateDirectory(nestedDirectory);
            File.WriteAllText(Path.Combine(sourceRoot, "root.txt"), "root");
            File.WriteAllText(Path.Combine(nestedDirectory, "child.txt"), "child");

            using (IIPSArchive archive = IIPSArchive.CreateNew())
            {
                archive.AddDirectory(sourceRoot, archiveRoot: "assets");
                archive.Save(archivePath);
            }

            using IIPSArchive reopened = IIPSArchive.Open(archivePath);
            Assert.Contains("assets\\root.txt", reopened.ArchivePaths);
            Assert.Contains("assets\\nested\\child.txt", reopened.ArchivePaths);

            reopened.ExtractAll(extractDirectory);

            Assert.Equal("root", File.ReadAllText(Path.Combine(extractDirectory, "assets", "root.txt")));
            Assert.Equal("child", File.ReadAllText(Path.Combine(extractDirectory, "assets", "nested", "child.txt")));
            Assert.False(Directory.Exists(Path.Combine(extractDirectory, "_unnamed")));
        }
        finally
        {
            Directory.Delete(tempDirectory, recursive: true);
        }
    }

    private static string CreateTempDirectory()
    {
        string path = Path.Combine(Path.GetTempPath(), $"iips-tests-{Guid.NewGuid():N}");
        Directory.CreateDirectory(path);
        return path;
    }
}
