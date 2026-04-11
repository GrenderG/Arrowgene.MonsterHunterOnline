using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for arena/zodiac season task data.
    /// C++ Reader: crygame.dll+sub_1015B590 (UnkTlv0095)
    /// C++ Printer: crygame.dll+sub_1015C6C0
    /// </summary>
    public class TlvArenaSeasonTaskData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: AnsCnt = reader.ReadInt(); break;
                    case 2: reader.ReadInt(); break;
                    case 3:
                        int len3 = reader.ReadInt();
                        if (len3 > 0 && len3 <= MaxStarBits)
                            StarBit = reader.ReadBytes(len3);
                        break;
                    case 4: RefreshTime = reader.ReadInt(); break;
                    case 5: ZodiacLightenCnt = reader.ReadByte(); break;
                    case 6: Activity = reader.ReadInt(); break;
                    case 7: CanResetTimes = reader.ReadInt(); break;
                    case 8: UsedResetTimes = reader.ReadInt(); break;
                    case 9: reader.ReadInt(); break;
                    case 10: Tasks = ReadTlvIntArray(reader); break;
                    case 11: Prizes = ReadTlvIntArray(reader); break;
                    case 12: reader.ReadInt(); break;
                    case 13: CompleteTasks = ReadTlvIntArray(reader); break;
                    case 14: TaskRefreshTimes = reader.ReadInt(); break;
                    case 15: LastSubmitTaskTime = reader.ReadInt(); break;
                    case 16: TaskDoDayNum = reader.ReadInt(); break;
                    case 17: TaskBuyDayNum = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((StarBit?.Length ?? 0) > MaxStarBits) throw new InvalidDataException($"[TlvArenaSeasonTaskData] StarBit exceeds {MaxStarBits}.");
            if ((Tasks?.Length ?? 0) > MaxTasks) throw new InvalidDataException($"[TlvArenaSeasonTaskData] Tasks exceeds {MaxTasks}.");
            if ((CompleteTasks?.Length ?? 0) > MaxCompleteTasks) throw new InvalidDataException($"[TlvArenaSeasonTaskData] CompleteTasks exceeds {MaxCompleteTasks}.");

            WriteTlvInt(writer, 1, AnsCnt);
            WriteTlvInt(writer, 2, StarBitCount);
            WriteTlvByteArray(writer, 3, StarBit, StarBitCount);
            WriteTlvInt(writer, 4, RefreshTime);
            WriteTlvByte(writer, 5, ZodiacLightenCnt);
            WriteTlvInt(writer, 6, Activity);
            WriteTlvInt(writer, 7, CanResetTimes);
            WriteTlvInt(writer, 8, UsedResetTimes);
            WriteTlvInt(writer, 9, TaskCountVal);
            WriteTlvIntArray(writer, 10, Tasks, TaskCountVal);
            WriteTlvIntArray(writer, 11, Prizes, TaskCountVal);
            WriteTlvInt(writer, 12, CompleteTaskCount);
            WriteTlvIntArray(writer, 13, CompleteTasks, CompleteTaskCount);
            WriteTlvInt(writer, 14, TaskRefreshTimes);
            WriteTlvInt(writer, 15, LastSubmitTaskTime);
            WriteTlvInt(writer, 16, TaskDoDayNum);
            WriteTlvInt(writer, 17, TaskBuyDayNum);
        }
    }
}
