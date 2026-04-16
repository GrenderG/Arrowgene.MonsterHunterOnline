using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for player report data.
    /// C++ Reader: crygame.dll+sub_10184760 (UnkTlv0149)
    /// C++ Printer: crygame.dll+sub_10184E30
    /// </summary>
    public class TlvPlayerReportData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxReports = 10;

        /// <summary>
        /// Last report time.
        /// Field ID: 1
        /// </summary>
        public int LastReportTime { get; set; }

        /// <summary>
        /// Today report times (byte).
        /// Field ID: 2
        /// </summary>
        public byte TodayReportTimes { get; set; }

        /// <summary>
        /// Report other player num (derived from arrays).
        /// Field ID: 3
        /// </summary>
        public int ReportOtherPlayerNum => OtherPlayerDBID?.Length ?? 0;

        /// <summary>
        /// Other player DB IDs (long array).
        /// Field ID: 4
        /// </summary>
        public long[] OtherPlayerDBID { get; set; }

        /// <summary>
        /// Report other player times (int array).
        /// Field ID: 5
        /// </summary>
        public int[] ReportOtherPlayerTime { get; set; }

        /// <summary>
        /// Today hang up times (byte).
        /// Field ID: 6
        /// </summary>
        public byte TodayHangUpTimes { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((OtherPlayerDBID?.Length ?? 0) > MaxReports)
                throw new InvalidDataException($"[TlvPlayerReportData] OtherPlayerDBID exceeds the maximum of {MaxReports} elements.");
            if ((ReportOtherPlayerTime?.Length ?? 0) > MaxReports)
                throw new InvalidDataException($"[TlvPlayerReportData] ReportOtherPlayerTime exceeds the maximum of {MaxReports} elements.");

            WriteTlvInt32(buffer, 1, LastReportTime);
            WriteTlvByte(buffer, 2, TodayReportTimes);
            WriteTlvInt32(buffer, 3, ReportOtherPlayerNum);
            WriteTlvInt64Arr(buffer, 4, OtherPlayerDBID);
            WriteTlvInt32Arr(buffer, 5, ReportOtherPlayerTime);
            WriteTlvByte(buffer, 6, TodayHangUpTimes);
        }
    }
}
