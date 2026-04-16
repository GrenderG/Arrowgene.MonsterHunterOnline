using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for task reset data with ratios and levels.
    /// C++ Reader: crygame.dll+sub_1015DC60 (UnkTlv0097)
    /// C++ Printer: crygame.dll+sub_1015E6B0
    /// </summary>
    public class TlvTaskResetData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxTasks = 5;
        public const int MaxCompleteTasks = 20;
        public const int MaxLevels = 250;

        /// <summary>
        /// Reset times.
        /// Field ID: 1
        /// </summary>
        public int ResetTimes { get; set; }

        /// <summary>
        /// Task count (derived from array).
        /// Field ID: 2
        /// </summary>
        public int TaskCount => Tasks?.Length ?? 0;

        /// <summary>
        /// Task IDs (int array).
        /// Field ID: 3
        /// </summary>
        public int[] Tasks { get; set; }

        /// <summary>
        /// Task ratios (float array, same count as Tasks).
        /// Field ID: 4
        /// </summary>
        public float[] Ratios { get; set; }

        /// <summary>
        /// Complete task count (derived from array).
        /// Field ID: 5
        /// </summary>
        public int CompleteTaskCount => CompleteTasks?.Length ?? 0;

        /// <summary>
        /// Completed task IDs (int array).
        /// Field ID: 6
        /// </summary>
        public int[] CompleteTasks { get; set; }

        /// <summary>
        /// Level count (derived from array).
        /// Field ID: 7
        /// </summary>
        public int LevelCount => Levels?.Length ?? 0;

        /// <summary>
        /// Level IDs (int array).
        /// Field ID: 8
        /// </summary>
        public int[] Levels { get; set; }

        /// <summary>
        /// Max task count.
        /// Field ID: 9
        /// </summary>
        public int MaxTaskCount { get; set; }

        /// <summary>
        /// Whether task count exceeds 100.
        /// Field ID: 10
        /// </summary>
        public int BTaskMoreThan100 { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Tasks?.Length ?? 0) > MaxTasks)
                throw new InvalidDataException($"[TlvTaskResetData] Tasks exceeds the maximum of {MaxTasks} elements.");
            if ((CompleteTasks?.Length ?? 0) > MaxCompleteTasks)
                throw new InvalidDataException($"[TlvTaskResetData] CompleteTasks exceeds the maximum of {MaxCompleteTasks} elements.");
            if ((Levels?.Length ?? 0) > MaxLevels)
                throw new InvalidDataException($"[TlvTaskResetData] Levels exceeds the maximum of {MaxLevels} elements.");

            WriteTlvInt32(buffer, 1, ResetTimes);
            WriteTlvInt32(buffer, 2, TaskCount);
            WriteTlvInt32Arr(buffer, 3, Tasks);
            WriteTlvFloatArr(buffer, 4, Ratios);
            WriteTlvInt32(buffer, 5, CompleteTaskCount);
            WriteTlvInt32Arr(buffer, 6, CompleteTasks);
            WriteTlvInt32(buffer, 7, LevelCount);
            WriteTlvInt32Arr(buffer, 8, Levels);
            WriteTlvInt32(buffer, 9, MaxTaskCount);
            WriteTlvInt32(buffer, 10, BTaskMoreThan100);
        }
    }
}
