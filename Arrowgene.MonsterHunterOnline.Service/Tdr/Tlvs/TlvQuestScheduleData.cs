using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for quest/task schedule data (lib, group, tasks, content, completion).
    /// C++ Reader: crygame.dll+sub_10126040 (UnkTlv0031)
    /// C++ Printer: crygame.dll+sub_10127330
    /// </summary>
    public class TlvQuestScheduleData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxTasks = 64;
        public const int MaxContent = 256;
        public const int MaxCompleteBit = 200;
        public const int MaxComplete = 1024;

        /// <summary>Field ID: 1</summary>
        public byte OpenFlag { get; set; }

        /// <summary>Field ID: 2</summary>
        public int Lib { get; set; }

        /// <summary>Field ID: 3</summary>
        public int Group { get; set; }

        /// <summary>Field ID: 4</summary>
        public int RefreshTime { get; set; }

        /// <summary>Field ID: 5</summary>
        public int CurLibFinishCount { get; set; }

        /// <summary>Task count (derived). Field ID: 6</summary>
        public int TaskCount => Task?.Count ?? 0;

        /// <summary>Task entries. Field ID: 7</summary>
        public List<TlvIdState> Task { get; set; }

        /// <summary>Content count (derived). Field ID: 8</summary>
        public int ContentCount => Content?.Count ?? 0;

        /// <summary>Content entries. Field ID: 9</summary>
        public List<TlvTaskState> Content { get; set; }

        /// <summary>Complete bit count (derived). Field ID: 10</summary>
        public int CompleteBitCount => CompleteBit?.Length ?? 0;

        /// <summary>Complete bit data. Field ID: 11</summary>
        public byte[] CompleteBit { get; set; }

        /// <summary>Complete count (derived). Field ID: 12</summary>
        public int CompleteCount => Complete?.Count ?? 0;

        /// <summary>Complete entries. Field ID: 13</summary>
        public List<TlvTaskCount> Complete { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: OpenFlag = reader.ReadByte(); break;
                    case 2: Lib = reader.ReadInt(); break;
                    case 3: Group = reader.ReadInt(); break;
                    case 4: RefreshTime = reader.ReadInt(); break;
                    case 5: CurLibFinishCount = reader.ReadInt(); break;
                    case 6: reader.ReadInt(); break;
                    case 7: Task = ReadTlvList<TlvIdState>(reader); break;
                    case 8: reader.ReadInt(); break;
                    case 9: Content = ReadTlvList<TlvTaskState>(reader); break;
                    case 10: reader.ReadInt(); break;
                    case 11:
                        int len11 = reader.ReadInt();
                        if (len11 > 0 && len11 <= MaxCompleteBit)
                            CompleteBit = reader.ReadBytes(len11);
                        break;
                    case 12: reader.ReadInt(); break;
                    case 13: Complete = ReadTlvList<TlvTaskCount>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((Task?.Count ?? 0) > MaxTasks) throw new InvalidDataException($"[TlvQuestScheduleData] Task exceeds {MaxTasks}.");
            if ((Content?.Count ?? 0) > MaxContent) throw new InvalidDataException($"[TlvQuestScheduleData] Content exceeds {MaxContent}.");
            if ((CompleteBit?.Length ?? 0) > MaxCompleteBit) throw new InvalidDataException($"[TlvQuestScheduleData] CompleteBit exceeds {MaxCompleteBit}.");
            if ((Complete?.Count ?? 0) > MaxComplete) throw new InvalidDataException($"[TlvQuestScheduleData] Complete exceeds {MaxComplete}.");

            WriteTlvByte(writer, 1, OpenFlag);
            WriteTlvInt(writer, 2, Lib);
            WriteTlvInt(writer, 3, Group);
            WriteTlvInt(writer, 4, RefreshTime);
            WriteTlvInt(writer, 5, CurLibFinishCount);
            WriteTlvInt(writer, 6, TaskCount);
            WriteTlvList(writer, 7, Task);
            WriteTlvInt(writer, 8, ContentCount);
            WriteTlvList(writer, 9, Content);
            WriteTlvInt(writer, 10, CompleteBitCount);
            WriteTlvByteArray(writer, 11, CompleteBit, CompleteBitCount);
            WriteTlvInt(writer, 12, CompleteCount);
            WriteTlvList(writer, 13, Complete);
        }
    }
}
