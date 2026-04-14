using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for quest/task schedule data (lib, group, tasks, content, completion).
    /// C++ Reader: crygame.dll+sub_10126040 (UnkTlv0031)
    /// C++ Printer: crygame.dll+sub_10127330
    /// </summary>
    public class TlvQuestScheduleData : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((Task?.Count ?? 0) > MaxTasks) throw new InvalidDataException($"[TlvQuestScheduleData] Task exceeds {MaxTasks}.");
            if ((Content?.Count ?? 0) > MaxContent) throw new InvalidDataException($"[TlvQuestScheduleData] Content exceeds {MaxContent}.");
            if ((CompleteBit?.Length ?? 0) > MaxCompleteBit) throw new InvalidDataException($"[TlvQuestScheduleData] CompleteBit exceeds {MaxCompleteBit}.");
            if ((Complete?.Count ?? 0) > MaxComplete) throw new InvalidDataException($"[TlvQuestScheduleData] Complete exceeds {MaxComplete}.");

            WriteTlvByte(buffer, 1, OpenFlag);
            WriteTlvInt32(buffer, 2, Lib);
            WriteTlvInt32(buffer, 3, Group);
            WriteTlvInt32(buffer, 4, RefreshTime);
            WriteTlvInt32(buffer, 5, CurLibFinishCount);
            WriteTlvInt32(buffer, 6, TaskCount);
            WriteTlvSubStructureList(buffer, 7, Task.Count, Task);
            WriteTlvInt32(buffer, 8, ContentCount);
            WriteTlvSubStructureList(buffer, 9, Content.Count, Content);
            WriteTlvInt32(buffer, 10, CompleteBitCount);
            WriteTlvByteArr(buffer, 11, CompleteBit);
            WriteTlvInt32(buffer, 12, CompleteCount);
            WriteTlvSubStructureList(buffer, 13, Complete.Count, Complete);
        }
    }
}
