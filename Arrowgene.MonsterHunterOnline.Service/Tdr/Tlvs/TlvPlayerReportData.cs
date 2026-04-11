using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for player report data.
    /// C++ Reader: crygame.dll+sub_10184760 (UnkTlv0149)
    /// C++ Printer: crygame.dll+sub_10184E30
    /// </summary>
    public class TlvPlayerReportData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: LastReportTime = reader.ReadInt(); break;
                    case 2: TodayReportTimes = reader.ReadByte(); break;
                    case 3: reader.ReadInt(); break; // reportOtherPlayerNum, derived
                    case 4: OtherPlayerDBID = ReadTlvLongArray(reader); break;
                    case 5: ReportOtherPlayerTime = ReadTlvIntArray(reader); break;
                    case 6: TodayHangUpTimes = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((OtherPlayerDBID?.Length ?? 0) > MaxReports)
                throw new InvalidDataException($"[TlvPlayerReportData] OtherPlayerDBID exceeds the maximum of {MaxReports} elements.");
            if ((ReportOtherPlayerTime?.Length ?? 0) > MaxReports)
                throw new InvalidDataException($"[TlvPlayerReportData] ReportOtherPlayerTime exceeds the maximum of {MaxReports} elements.");

            WriteTlvInt(writer, 1, LastReportTime);
            WriteTlvByte(writer, 2, TodayReportTimes);
            WriteTlvInt(writer, 3, ReportOtherPlayerNum);
            WriteTlvLongArray(writer, 4, OtherPlayerDBID);
            WriteTlvIntArray(writer, 5, ReportOtherPlayerTime);
            WriteTlvByte(writer, 6, TodayHangUpTimes);
        }
    }
}
