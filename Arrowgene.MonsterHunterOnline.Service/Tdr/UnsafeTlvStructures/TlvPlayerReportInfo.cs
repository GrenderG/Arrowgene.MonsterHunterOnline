using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for player report info.
    /// C++ Reader: crygame.dll+sub_10183530 (UnkTlv0148)
    /// C++ Printer: crygame.dll+sub_10183610
    /// </summary>
    public class TlvPlayerReportInfo : Structure, ITlvStructure
    {
        /// <summary>
        /// Other player database ID.
        /// Field ID: 1
        /// </summary>
        public ulong OtherPlayerDbId { get; set; }

        /// <summary>
        /// Report other player time.
        /// Field ID: 2
        /// </summary>
        public int ReportOtherPlayerTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt64(buffer, 1, (long)OtherPlayerDbId);
            WriteTlvInt32(buffer, 2, ReportOtherPlayerTime);
        }
    }
}
