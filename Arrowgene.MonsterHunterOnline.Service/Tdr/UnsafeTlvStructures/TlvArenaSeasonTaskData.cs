using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for arena/zodiac season task data.
    /// C++ Reader: crygame.dll+sub_1015B590 (UnkTlv0095)
    /// C++ Printer: crygame.dll+sub_1015C6C0
    /// </summary>
    public class TlvArenaSeasonTaskData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxStarBits = 10;
        public const int MaxTasks = 10;
        public const int MaxCompleteTasks = 100;

        /// <summary>Field ID: 1</summary>
        public int AnsCnt { get; set; }

        /// <summary>Star bit count (derived from array). Field ID: 2</summary>
        public int StarBitCount => StarBit?.Length ?? 0;

        /// <summary>Star bit array. Field ID: 3</summary>
        public byte[] StarBit { get; set; }

        /// <summary>Field ID: 4</summary>
        public int RefreshTime { get; set; }

        /// <summary>Field ID: 5</summary>
        public byte ZodiacLightenCnt { get; set; }

        /// <summary>Field ID: 6</summary>
        public int Activity { get; set; }

        /// <summary>Field ID: 7</summary>
        public int CanResetTimes { get; set; }

        /// <summary>Field ID: 8</summary>
        public int UsedResetTimes { get; set; }

        /// <summary>Task count (derived from arrays). Field ID: 9</summary>
        public int TaskCountVal => Tasks?.Length ?? 0;

        /// <summary>Task IDs. Field ID: 10</summary>
        public int[] Tasks { get; set; }

        /// <summary>Prize IDs (same count as Tasks). Field ID: 11</summary>
        public int[] Prizes { get; set; }

        /// <summary>Complete task count (derived). Field ID: 12</summary>
        public int CompleteTaskCount => CompleteTasks?.Length ?? 0;

        /// <summary>Complete task IDs. Field ID: 13</summary>
        public int[] CompleteTasks { get; set; }

        /// <summary>Field ID: 14</summary>
        public int TaskRefreshTimes { get; set; }

        /// <summary>Field ID: 15</summary>
        public int LastSubmitTaskTime { get; set; }

        /// <summary>Field ID: 16</summary>
        public int TaskDoDayNum { get; set; }

        /// <summary>Field ID: 17</summary>
        public int TaskBuyDayNum { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((StarBit?.Length ?? 0) > MaxStarBits) throw new InvalidDataException($"[TlvArenaSeasonTaskData] StarBit exceeds {MaxStarBits}.");
            if ((Tasks?.Length ?? 0) > MaxTasks) throw new InvalidDataException($"[TlvArenaSeasonTaskData] Tasks exceeds {MaxTasks}.");
            if ((CompleteTasks?.Length ?? 0) > MaxCompleteTasks) throw new InvalidDataException($"[TlvArenaSeasonTaskData] CompleteTasks exceeds {MaxCompleteTasks}.");

            WriteTlvInt32(buffer, 1, AnsCnt);
            WriteTlvInt32(buffer, 2, StarBitCount);
            WriteTlvByteArr(buffer, 3, StarBit);
            WriteTlvInt32(buffer, 4, RefreshTime);
            WriteTlvByte(buffer, 5, ZodiacLightenCnt);
            WriteTlvInt32(buffer, 6, Activity);
            WriteTlvInt32(buffer, 7, CanResetTimes);
            WriteTlvInt32(buffer, 8, UsedResetTimes);
            WriteTlvInt32(buffer, 9, TaskCountVal);
            WriteTlvInt32Arr(buffer, 10, Tasks);
            WriteTlvInt32Arr(buffer, 11, Prizes);
            WriteTlvInt32(buffer, 12, CompleteTaskCount);
            WriteTlvInt32Arr(buffer, 13, CompleteTasks);
            WriteTlvInt32(buffer, 14, TaskRefreshTimes);
            WriteTlvInt32(buffer, 15, LastSubmitTaskTime);
            WriteTlvInt32(buffer, 16, TaskDoDayNum);
            WriteTlvInt32(buffer, 17, TaskBuyDayNum);
        }
    }
}
