using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for daily/week/month/forever buy limit data.
    /// C++ Reader: crygame.dll+sub_10182870 (UnkTlv0147)
    /// C++ Printer: crygame.dll+sub_10183570
    /// </summary>
    public class TlvBuyLimitContainer : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxLimits = 20;

        /// <summary>Daily limit count (derived). Field ID: 1</summary>
        public int DailyLimitCnt => DailyLimitType?.Length ?? 0;

        /// <summary>Daily limit types. Field ID: 2</summary>
        public int[] DailyLimitType { get; set; }

        /// <summary>Daily limit data. Field ID: 3</summary>
        public int[] DailyLimitData { get; set; }

        /// <summary>Week limit count (derived). Field ID: 4</summary>
        public int WeekLimitCnt => WeekLimitType?.Length ?? 0;

        /// <summary>Week limit types. Field ID: 5</summary>
        public int[] WeekLimitType { get; set; }

        /// <summary>Week limit data. Field ID: 6</summary>
        public int[] WeekLimitData { get; set; }

        /// <summary>Month limit count (derived). Field ID: 7</summary>
        public int MonthLimitCnt => MonthLimitType?.Length ?? 0;

        /// <summary>Month limit types. Field ID: 8</summary>
        public int[] MonthLimitType { get; set; }

        /// <summary>Month limit data. Field ID: 9</summary>
        public int[] MonthLimitData { get; set; }

        /// <summary>Forever limit count (derived). Field ID: 10</summary>
        public int ForeverLimitCnt => ForeverLimitType?.Length ?? 0;

        /// <summary>Forever limit types. Field ID: 11</summary>
        public int[] ForeverLimitType { get; set; }

        /// <summary>Forever limit data. Field ID: 12</summary>
        public int[] ForeverLimitData { get; set; }

        /// <summary>Last daily time. Field ID: 13</summary>
        public int LastDailyTm { get; set; }

        /// <summary>Last week time. Field ID: 14</summary>
        public int LastWeekTm { get; set; }

        /// <summary>Last month time. Field ID: 15</summary>
        public int LastMonthTm { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // dailyLimitCnt, derived
                    case 2: DailyLimitType = ReadTlvIntArray(reader); break;
                    case 3: DailyLimitData = ReadTlvIntArray(reader); break;
                    case 4: reader.ReadInt(); break; // weekLimitCnt, derived
                    case 5: WeekLimitType = ReadTlvIntArray(reader); break;
                    case 6: WeekLimitData = ReadTlvIntArray(reader); break;
                    case 7: reader.ReadInt(); break; // monthLimitCnt, derived
                    case 8: MonthLimitType = ReadTlvIntArray(reader); break;
                    case 9: MonthLimitData = ReadTlvIntArray(reader); break;
                    case 10: reader.ReadInt(); break; // foreverLimitCnt, derived
                    case 11: ForeverLimitType = ReadTlvIntArray(reader); break;
                    case 12: ForeverLimitData = ReadTlvIntArray(reader); break;
                    case 13: LastDailyTm = reader.ReadInt(); break;
                    case 14: LastWeekTm = reader.ReadInt(); break;
                    case 15: LastMonthTm = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((DailyLimitType?.Length ?? 0) > MaxLimits) throw new InvalidDataException($"[TlvBuyLimitContainer] DailyLimitType exceeds {MaxLimits}.");
            if ((WeekLimitType?.Length ?? 0) > MaxLimits) throw new InvalidDataException($"[TlvBuyLimitContainer] WeekLimitType exceeds {MaxLimits}.");
            if ((MonthLimitType?.Length ?? 0) > MaxLimits) throw new InvalidDataException($"[TlvBuyLimitContainer] MonthLimitType exceeds {MaxLimits}.");
            if ((ForeverLimitType?.Length ?? 0) > MaxLimits) throw new InvalidDataException($"[TlvBuyLimitContainer] ForeverLimitType exceeds {MaxLimits}.");

            WriteTlvInt(writer, 1, DailyLimitCnt);
            WriteTlvIntArray(writer, 2, DailyLimitType, DailyLimitCnt);
            WriteTlvIntArray(writer, 3, DailyLimitData, DailyLimitCnt);
            WriteTlvInt(writer, 4, WeekLimitCnt);
            WriteTlvIntArray(writer, 5, WeekLimitType, WeekLimitCnt);
            WriteTlvIntArray(writer, 6, WeekLimitData, WeekLimitCnt);
            WriteTlvInt(writer, 7, MonthLimitCnt);
            WriteTlvIntArray(writer, 8, MonthLimitType, MonthLimitCnt);
            WriteTlvIntArray(writer, 9, MonthLimitData, MonthLimitCnt);
            WriteTlvInt(writer, 10, ForeverLimitCnt);
            WriteTlvIntArray(writer, 11, ForeverLimitType, ForeverLimitCnt);
            WriteTlvIntArray(writer, 12, ForeverLimitData, ForeverLimitCnt);
            WriteTlvInt(writer, 13, LastDailyTm);
            WriteTlvInt(writer, 14, LastWeekTm);
            WriteTlvInt(writer, 15, LastMonthTm);
        }
    }
}
