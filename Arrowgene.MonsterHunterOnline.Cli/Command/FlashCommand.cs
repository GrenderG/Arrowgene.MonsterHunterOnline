using System;
using System.IO;
using System.Linq;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.ClientTools.Flash;

namespace Arrowgene.MonsterHunterOnline.Cli.Command;

public sealed class FlashCommand : ICommand
{
    private static readonly ILogger Logger = LogProvider.Logger(typeof(FlashCommand));

    public string Key => "flash";
    public string Description => "Reads and extracts FLA/SWF files";

    public CommandResultType Run(CommandParameter parameter)
    {
        if (parameter.Arguments.Count >= 2 &&
            string.Equals(parameter.Arguments[0], "info", StringComparison.OrdinalIgnoreCase))
        {
            return ShowInfo(parameter.Arguments[1]);
        }

        if (parameter.Arguments.Count >= 3 &&
            string.Equals(parameter.Arguments[0], "extract", StringComparison.OrdinalIgnoreCase))
        {
            return Extract(parameter.Arguments[1], parameter.Arguments[2]);
        }

        Logger.Info("Usage: flash info <path>");
        Logger.Info("Usage: flash extract <path> <outDir>");
        return CommandResultType.Completed;
    }

    public void Shutdown()
    {
    }

    private CommandResultType ShowInfo(string path)
    {
        if (!File.Exists(path))
        {
            Logger.Error($"Flash file does not exist: {path}");
            return CommandResultType.Completed;
        }

        if (SwfFile.IsSwf(path))
        {
            SwfFile swf = SwfFile.Open(path);
            Logger.Info($"Type: SWF");
            Logger.Info($"Compression: {swf.Compression}");
            Logger.Info($"Version: {swf.Version}");
            Logger.Info($"Declared Length: {swf.DeclaredFileLength}");
            Logger.Info($"Actual Length: {swf.ActualFileLength}");
            Logger.Info(
                $"Frame Size: {swf.FrameSize.WidthPixels:0.##}x{swf.FrameSize.HeightPixels:0.##} px ({swf.FrameSize.WidthTwips}x{swf.FrameSize.HeightTwips} twips)");
            Logger.Info($"Frame Rate: {swf.FrameRate:0.###}");
            Logger.Info($"Frame Count: {swf.FrameCount}");
            Logger.Info($"Tags: {swf.Tags.Count}");
            Logger.Info($"Exports: {swf.ExportNames.Count}");
            Logger.Info($"Symbol Classes: {swf.SymbolClassNames.Count}");
            return CommandResultType.Completed;
        }

        if (FlaArchive.IsFla(path))
        {
            FlaArchive fla = FlaArchive.Open(path);
            Logger.Info("Type: FLA");
            Logger.Info($"Entries: {fla.Entries.Count}");
            Logger.Info($"Files: {fla.Entries.Count(entry => !entry.IsDirectory)}");
            Logger.Info($"Directories: {fla.Entries.Count(entry => entry.IsDirectory)}");
            Logger.Info($"Has DOMDocument.xml: {fla.TryGetEntry("DOMDocument.xml", out _)}");
            return CommandResultType.Completed;
        }

        Logger.Error($"Unsupported flash file format: {path}");
        return CommandResultType.Completed;
    }

    private CommandResultType Extract(string path, string outputDirectory)
    {
        if (!File.Exists(path))
        {
            Logger.Error($"Flash file does not exist: {path}");
            return CommandResultType.Completed;
        }

        if (SwfFile.IsSwf(path))
        {
            SwfFile swf = SwfFile.Open(path);
            swf.ExtractAll(outputDirectory);
            Logger.Info($"Extracted SWF to: {outputDirectory}");
            return CommandResultType.Completed;
        }

        if (FlaArchive.IsFla(path))
        {
            FlaArchive fla = FlaArchive.Open(path);
            fla.ExtractAll(outputDirectory);
            Logger.Info($"Extracted FLA to: {outputDirectory}");
            return CommandResultType.Completed;
        }

        Logger.Error($"Unsupported flash file format: {path}");
        return CommandResultType.Completed;
    }
}
