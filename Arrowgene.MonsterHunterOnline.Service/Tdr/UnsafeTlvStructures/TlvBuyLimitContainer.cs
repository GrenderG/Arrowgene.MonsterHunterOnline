using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for daily/week/month/forever buy limit data.
    /// C++ Reader: crygame.dll+sub_10182870 (UnkTlv0147)
    /// C++ Printer: crygame.dll+sub_10183570
    /// </summary>
    public class TlvBuyLimitContainer : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((DailyLimitType?.Length ?? 0) > MaxLimits) throw new InvalidDataException($"[TlvBuyLimitContainer] DailyLimitType exceeds {MaxLimits}.");
            if ((WeekLimitType?.Length ?? 0) > MaxLimits) throw new InvalidDataException($"[TlvBuyLimitContainer] WeekLimitType exceeds {MaxLimits}.");
            if ((MonthLimitType?.Length ?? 0) > MaxLimits) throw new InvalidDataException($"[TlvBuyLimitContainer] MonthLimitType exceeds {MaxLimits}.");
            if ((ForeverLimitType?.Length ?? 0) > MaxLimits) throw new InvalidDataException($"[TlvBuyLimitContainer] ForeverLimitType exceeds {MaxLimits}.");

            WriteTlvInt32(buffer, 1, DailyLimitCnt);
            WriteTlvInt32Arr(buffer, 2, DailyLimitType);
            WriteTlvInt32Arr(buffer, 3, DailyLimitData);
            WriteTlvInt32(buffer, 4, WeekLimitCnt);
            WriteTlvInt32Arr(buffer, 5, WeekLimitType);
            WriteTlvInt32Arr(buffer, 6, WeekLimitData);
            WriteTlvInt32(buffer, 7, MonthLimitCnt);
            WriteTlvInt32Arr(buffer, 8, MonthLimitType);
            WriteTlvInt32Arr(buffer, 9, MonthLimitData);
            WriteTlvInt32(buffer, 10, ForeverLimitCnt);
            WriteTlvInt32Arr(buffer, 11, ForeverLimitType);
            WriteTlvInt32Arr(buffer, 12, ForeverLimitData);
            WriteTlvInt32(buffer, 13, LastDailyTm);
            WriteTlvInt32(buffer, 14, LastWeekTm);
            WriteTlvInt32(buffer, 15, LastMonthTm);
        }
    }
}
