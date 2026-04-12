using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for goods inventory item.
    /// C++ Reader: crygame.dll+sub_1012EFA0 (UnkTlv0041)
    /// C++ Printer: crygame.dll+sub_1012F0F0
    /// </summary>
    public class TlvGoodsItem : Structure, ITlvStructure
    {
        /// <summary>
        /// Item identifier.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Goods number/quantity.
        /// Field ID: 2
        /// </summary>
        public uint GoodsNumber { get; set; }

        /// <summary>
        /// Last change time.
        /// Field ID: 3
        /// </summary>
        public uint LastChangeTime { get; set; }

        /// <summary>
        /// Is owned flag.
        /// Field ID: 4
        /// </summary>
        public byte IsOwned { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvInt32(buffer, 2, (int)GoodsNumber);
            WriteTlvInt32(buffer, 3, (int)LastChangeTime);
            WriteTlvByte(buffer, 4, IsOwned);
        }
    }
}
