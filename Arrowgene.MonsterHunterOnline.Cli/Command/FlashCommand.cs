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

        if (parameter.Arguments.Count >= 3 &&
            string.Equals(parameter.Arguments[0], "render-minimaps", StringComparison.OrdinalIgnoreCase))
        {
            return RenderMinimaps(parameter.Arguments[1], parameter.Arguments[2]);
        }

        Logger.Info("Usage: flash info <path>");
        Logger.Info("Usage: flash extract <path> <outDir>");
        Logger.Info("Usage: flash render-minimaps <minimapDir> <outDir>");
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

    private CommandResultType RenderMinimaps(string minimapDir, string outputDir)
    {
        if (!Directory.Exists(minimapDir))
        {
            Logger.Error($"Minimap directory does not exist: {minimapDir}");
            return CommandResultType.Completed;
        }

        Directory.CreateDirectory(outputDir);

        string[] swfFiles = Directory.GetFiles(minimapDir, "*.swf");
        string[] flaFiles = Directory.GetFiles(minimapDir, "*.fla");

        Logger.Info($"Found {swfFiles.Length} SWF files and {flaFiles.Length} FLA files in {minimapDir}");

        int rendered = 0, failed = 0;

        foreach (string swfPath in swfFiles.OrderBy(f => f, StringComparer.OrdinalIgnoreCase))
        {
            string name = Path.GetFileNameWithoutExtension(swfPath);
            string outPath = Path.Combine(outputDir, $"{name}.png");

            try
            {
                SwfFile swf = SwfFile.Open(swfPath);
                byte[]? png = SwfSceneRenderer.RenderFirstFrame(swf);
                if (png != null && png.Length > 0)
                {
                    File.WriteAllBytes(outPath, png);
                    Logger.Info($"[SWF OK] {name} ({swf.FrameSize.WidthPixels:0}x{swf.FrameSize.HeightPixels:0}) -> {outPath} ({png.Length} bytes)");
                    rendered++;
                }
                else
                {
                    Logger.Error($"[SWF EMPTY] {name} - renderer produced no output");
                    failed++;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[SWF FAIL] {name} - {ex.Message}");
                failed++;
            }
        }

        Logger.Info($"Done. Rendered: {rendered}, Failed: {failed}, Total: {swfFiles.Length}");
        return CommandResultType.Exit;
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
