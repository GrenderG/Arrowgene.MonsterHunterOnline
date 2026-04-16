using System.Collections.Generic;
using System.IO;
using System.Linq;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.ClientTools.Dat;
using Arrowgene.MonsterHunterOnline.ClientTools.IIPS;

namespace Arrowgene.MonsterHunterOnline.Cli.Command
{
    public class IIPSCommand : ICommand
    {
        private static readonly ILogger Logger = LogProvider.Logger(typeof(IIPSCommand));


        public string Key => "iips";
        public string Description => "iips";


        public IIPSCommand()
        {
        }

        public CommandResultType Run(CommandParameter parameter)
        {
            if (parameter.Arguments.Count >= 3 && parameter.Arguments[0] == "dat")
            {
                string inDir = parameter.Arguments[1];
                string outDir = parameter.Arguments[2];

                if (!Directory.Exists(inDir))
                {
                    Logger.Error($"Input directory does not exist: {inDir}");
                    return CommandResultType.Completed;
                }

                if (!Directory.Exists(outDir))
                {
                    Directory.CreateDirectory(outDir);
                }

                DatFile df = new DatFile();
                List<string> files = new List<string>(Directory.GetFiles(inDir));
                files.Sort();
                foreach (string staticFile in files)
                {
                    if (staticFile.EndsWith(".dat"))
                    {
                        FileInfo fi = new FileInfo(staticFile);
                        df.Open(staticFile);
                        if (df.ContentType == DatFile.DatContentType.TSV)
                        {
                            foreach (TsvSheet sheet in df.Sheets)
                            {
                                string outPath = Path.Combine(outDir, $"{fi.Name}_{sheet.Name}.csv");
                                File.WriteAllText(outPath, sheet.ToCsv());
                            }
                        }
                        else
                        {
                            string outPath = Path.Combine(outDir, $"{fi.Name}.txt");
                            File.WriteAllText(outPath, df.Content);
                        }
                    }
                }

                return CommandResultType.Completed;
            }

            if (parameter.Arguments.Count >= 2 && parameter.Arguments[0] == "ifs")
            {
                string inDir = parameter.Arguments[1];

                if (!Directory.Exists(inDir))
                {
                    Logger.Error($"Input directory does not exist: {inDir}");
                    return CommandResultType.Completed;
                }


                List<string> files = new List<string>(Directory.GetFiles(inDir));
                files = files.OrderBy(f =>
                {
                    string fileName = Path.GetFileName(f);
                    if (fileName.StartsWith("base_")) return 0;
                    if (fileName.StartsWith("patch_")) return 1;
                    return 2;
                }).ThenBy(f => f).ToList();
                string outDir = parameter.Arguments.Count >= 3 ? parameter.Arguments[2] : null;

                foreach (string staticFile in files)
                {
                    if (staticFile.EndsWith(".ifs"))
                    {
                        using IIPSArchive archive = IIPSArchive.Open(staticFile);

                        Logger.Info($"Archive: {Path.GetFileName(staticFile)}");
                        Logger.Info($"  Entries: {archive.Entries.Count}, ArchivePaths: {archive.ArchivePaths.Count}");

                        int named = 0;
                        foreach (var entry in archive.Entries)
                        {
                            if (!string.IsNullOrEmpty(entry.ArchivePath)) named++;
                        }
                        Logger.Info($"  Named entries: {named}");

                        if (outDir != null)
                        {
                            archive.ExtractAll(outDir);
                        }
                    }
                }

                return CommandResultType.Completed;
            }

            Logger.Info("Usage: iips dat <inDir> <outDir>");
            Logger.Info("Usage: iips ifs <inDir> [outDir]");

            return CommandResultType.Completed;
        }

        public void Shutdown()
        {
        }
    }
}
