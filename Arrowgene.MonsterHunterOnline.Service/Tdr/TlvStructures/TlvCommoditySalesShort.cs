using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for commodity sales (short fields).
    /// C++ Reader: crygame.dll+sub_10212770 (UnkTlv0229)
    /// C++ Printer: crygame.dll+sub_10212A40
    /// </summary>
    public class TlvCommoditySalesShort : Structure, ITlvStructure
    {
        /// <summary>
        /// Commodity ID.
        /// Field ID: 1
        /// </summary>
        public short Commodity { get; set; }

        /// <summary>
        /// Sold count.
        /// Field ID: 2
        /// </summary>
        public short SaledCount { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt16(buffer, 1, Commodity);
            WriteTlvInt16(buffer, 2, SaledCount);
        }
    }
}
