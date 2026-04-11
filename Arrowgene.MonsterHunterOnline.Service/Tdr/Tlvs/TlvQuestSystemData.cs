using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for task/quest system data.
    /// C++ Reader: crygame.dll+sub_10225D00 (UnkTlv0257)
    /// C++ Printer: crygame.dll+sub_10226430
    /// </summary>
    public class TlvQuestSystemData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        public const int MaxContent = 128;
        public const int MaxCompleteBit = 256;
        public const int MaxTaskBytes = 1280;
        public const int MaxReset = 32;

        /// <summary>Task count (derived). Field ID: 1</summary>
        public int TaskCount => Content?.Count ?? 0;

        /// <summary>Content entries. Field ID: 2</summary>
        public List<TlvTaskStateVarEntry> Content { get; set; }

        /// <summary>Complete bit count (derived). Field ID: 3</summary>
        public int CompleteBitCount => CompleteBit?.Count ?? 0;

        /// <summary>Complete bit entries. Field ID: 4</summary>
        public List<TlvTaskCompleteBitEntry> CompleteBit { get; set; }

        /// <summary>Task bytes count (derived). Field ID: 5</summary>
        public int TaskBytesCount => TaskBytes?.Length ?? 0;

        /// <summary>Task byte data. Field ID: 6</summary>
        public byte[] TaskBytes { get; set; }

        /// <summary>Daily task stats. Field ID: 13</summary>
        public TlvDailyTaskStats Daily { get; set; } = new();

        /// <summary>Schedule refresh time. Field ID: 14</summary>
        public TlvRefreshTimeOnly Schedule { get; set; } = new();

        /// <summary>XDaily count (derived). Field ID: 15</summary>
        public int XDailyCount => Reset?.Count ?? 0;

        /// <summary>Reset entries. Field ID: 16</summary>
        public List<TlvLibRefreshCount> Reset { get; set; }

        /// <summary>Trace data. Field ID: 17</summary>
        public TlvResetTaskTime Trace { get; set; } = new();

        /// <summary>Complete data. Field ID: 18</summary>
        public TlvTraceTaskTime Complete { get; set; } = new();

        /// <summary>XDaily data. Field ID: 19</summary>
        public TlvCompleteTaskCount XDaily { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadVarInt(); break;
                    case 2: Content = ReadTlvList<TlvTaskStateVarEntry>(reader); break;
                    case 3: reader.ReadVarInt(); break;
                    case 4: CompleteBit = ReadTlvList<TlvTaskCompleteBitEntry>(reader); break;
                    case 5: reader.ReadVarInt(); break;
                    case 6: { int len = reader.ReadInt(); if (len > 0 && len <= MaxTaskBytes) TaskBytes = reader.ReadBytes(len); } break;
                    case 13: Daily = ReadTlvObject<TlvDailyTaskStats>(reader); break;
                    case 14: Schedule = ReadTlvObject<TlvRefreshTimeOnly>(reader); break;
                    case 15: reader.ReadInt(); break;
                    case 16: Reset = ReadTlvList<TlvLibRefreshCount>(reader); break;
                    case 17: Trace = ReadTlvObject<TlvResetTaskTime>(reader); break;
                    case 18: Complete = ReadTlvObject<TlvTraceTaskTime>(reader); break;
                    case 19: XDaily = ReadTlvObject<TlvCompleteTaskCount>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((Content?.Count ?? 0) > MaxContent) throw new InvalidDataException($"[TlvQuestSystemData] Content exceeds {MaxContent}.");
            if ((CompleteBit?.Count ?? 0) > MaxCompleteBit) throw new InvalidDataException($"[TlvQuestSystemData] CompleteBit exceeds {MaxCompleteBit}.");
            if ((TaskBytes?.Length ?? 0) > MaxTaskBytes) throw new InvalidDataException($"[TlvQuestSystemData] TaskBytes exceeds {MaxTaskBytes}.");
            if ((Reset?.Count ?? 0) > MaxReset) throw new InvalidDataException($"[TlvQuestSystemData] Reset exceeds {MaxReset}.");

            WriteTlvVarInt(writer, 1, TaskCount);
            WriteTlvList(writer, 2, Content);
            WriteTlvVarInt(writer, 3, CompleteBitCount);
            WriteTlvList(writer, 4, CompleteBit);
            WriteTlvVarInt(writer, 5, TaskBytesCount);
            WriteTlvByteArray(writer, 6, TaskBytes, TaskBytesCount);
            WriteTlvObject(writer, 13, Daily);
            WriteTlvObject(writer, 14, Schedule);
            WriteTlvInt(writer, 15, XDailyCount);
            WriteTlvList(writer, 16, Reset);
            WriteTlvObject(writer, 17, Trace);
            WriteTlvObject(writer, 18, Complete);
            WriteTlvObject(writer, 19, XDaily);
        }
    }
}
