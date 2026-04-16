using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for date day with higher value.
    /// C++ Reader: crygame.dll+sub_101A5800 (UnkTlv0184)
    /// C++ Printer: crygame.dll+sub_101A58E0
    /// </summary>
    public class TlvDateDayHigher : Structure, ITlvStructure
    {
        /// <summary>
        /// Date day.
        /// Field ID: 1
        /// </summary>
        public int DateDay { get; set; }

        /// <summary>
        /// Current higher value.
        /// Field ID: 2
        /// </summary>
        public int CurHigher { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, DateDay);
            WriteTlvInt32(buffer, 2, CurHigher);
        }
    }
}
