using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for task/quest system data.
    /// C++ Reader: crygame.dll+sub_10225D00 (UnkTlv0257)
    /// C++ Printer: crygame.dll+sub_10226430
    /// </summary>
    public class TlvQuestSystemData : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((Content?.Count ?? 0) > MaxContent) throw new InvalidDataException($"[TlvQuestSystemData] Content exceeds {MaxContent}.");
            if ((CompleteBit?.Count ?? 0) > MaxCompleteBit) throw new InvalidDataException($"[TlvQuestSystemData] CompleteBit exceeds {MaxCompleteBit}.");
            if ((TaskBytes?.Length ?? 0) > MaxTaskBytes) throw new InvalidDataException($"[TlvQuestSystemData] TaskBytes exceeds {MaxTaskBytes}.");
            if ((Reset?.Count ?? 0) > MaxReset) throw new InvalidDataException($"[TlvQuestSystemData] Reset exceeds {MaxReset}.");

            WriteTlvVarInt32(buffer, 1, TaskCount);
            WriteTlvSubStructureList(buffer, 2, Content.Count, Content);
            WriteTlvVarInt32(buffer, 3, CompleteBitCount);
            WriteTlvSubStructureList(buffer, 4, CompleteBit.Count, CompleteBit);
            WriteTlvVarInt32(buffer, 5, TaskBytesCount);
            WriteTlvByteArr(buffer, 6, TaskBytes);
            WriteTlvSubStructure(buffer, 13, Daily);
            WriteTlvSubStructure(buffer, 14, Schedule);
            WriteTlvInt32(buffer, 15, XDailyCount);
            WriteTlvSubStructureList(buffer, 16, Reset.Count, Reset);
            WriteTlvSubStructure(buffer, 17, Trace);
            WriteTlvSubStructure(buffer, 18, Complete);
            WriteTlvSubStructure(buffer, 19, XDaily);
        }
    }
}
