using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for weekly award count with reset time.
    /// C++ Reader: crygame.dll+sub_10159E40 (UnkTlv0092)
    /// C++ Printer: crygame.dll+sub_1015A0E0
    /// </summary>
    public class TlvWeeklyAwardTime : Structure, ITlvStructure
    {
        /// <summary>
        /// Weekly award count.
        /// Field ID: 1
        /// </summary>
        public short WeeklyAwardCnt { get; set; }

        /// <summary>
        /// Last reset time.
        /// Field ID: 3
        /// </summary>
        public uint LastResetTm { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt16(buffer, 1, WeeklyAwardCnt);
            WriteTlvInt32(buffer, 3, (int)LastResetTm);
        }
    }
}
