using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for task reset data with ratios and levels.
    /// C++ Reader: crygame.dll+sub_1015DC60 (UnkTlv0097)
    /// C++ Printer: crygame.dll+sub_1015E6B0
    /// </summary>
    public class TlvTaskResetData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: ResetTimes = reader.ReadInt(); break;
                    case 2: reader.ReadInt(); break; // taskCount, derived
                    case 3: Tasks = ReadTlvIntArray(reader); break;
                    case 4: Ratios = ReadTlvFloatArray(reader); break;
                    case 5: reader.ReadInt(); break; // completeTaskCount, derived
                    case 6: CompleteTasks = ReadTlvIntArray(reader); break;
                    case 7: reader.ReadInt(); break; // levelCount, derived
                    case 8: Levels = ReadTlvIntArray(reader); break;
                    case 9: MaxTaskCount = reader.ReadInt(); break;
                    case 10: BTaskMoreThan100 = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Tasks?.Length ?? 0) > MaxTasks)
                throw new InvalidDataException($"[TlvTaskResetData] Tasks exceeds the maximum of {MaxTasks} elements.");
            if ((CompleteTasks?.Length ?? 0) > MaxCompleteTasks)
                throw new InvalidDataException($"[TlvTaskResetData] CompleteTasks exceeds the maximum of {MaxCompleteTasks} elements.");
            if ((Levels?.Length ?? 0) > MaxLevels)
                throw new InvalidDataException($"[TlvTaskResetData] Levels exceeds the maximum of {MaxLevels} elements.");

            WriteTlvInt(writer, 1, ResetTimes);
            WriteTlvInt(writer, 2, TaskCount);
            WriteTlvIntArray(writer, 3, Tasks, TaskCount);
            WriteTlvFloatArray(writer, 4, Ratios);
            WriteTlvInt(writer, 5, CompleteTaskCount);
            WriteTlvIntArray(writer, 6, CompleteTasks, CompleteTaskCount);
            WriteTlvInt(writer, 7, LevelCount);
            WriteTlvIntArray(writer, 8, Levels, LevelCount);
            WriteTlvInt(writer, 9, MaxTaskCount);
            WriteTlvInt(writer, 10, BTaskMoreThan100);
        }
    }
}
