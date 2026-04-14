using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for commodity sales count.
    /// C++ Reader: crygame.dll+sub_10205830 (UnkTlv0225)
    /// C++ Printer: crygame.dll+sub_10205910
    /// </summary>
    public class TlvCommoditySales : Structure, ITlvStructure
    {
        /// <summary>
        /// Commodity ID.
        /// Field ID: 1
        /// </summary>
        public int Commodity { get; set; }

        /// <summary>
        /// Sold count.
        /// Field ID: 2
        /// </summary>
        public int SaledCount { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Commodity);
            WriteTlvInt32(buffer, 2, SaledCount);
        }
    }
}
