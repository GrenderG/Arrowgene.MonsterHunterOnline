using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for shop sale info.
    /// C++ Reader: crygame.dll+sub_1020DF50 (UnkTlv0223)
    /// C++ Printer: crygame.dll+sub_1020E2E0
    /// </summary>
    public class TlvShopSaleInfo : Structure, ITlvStructure
    {
        /// <summary>
        /// Shop type.
        /// Field ID: 1
        /// </summary>
        public byte ShopType { get; set; }

        /// <summary>
        /// Shop ID.
        /// Field ID: 2
        /// </summary>
        public uint ShopId { get; set; }

        /// <summary>
        /// Sale ID.
        /// Field ID: 3
        /// </summary>
        public uint SaleId { get; set; }

        /// <summary>
        /// Buy count.
        /// Field ID: 4
        /// </summary>
        public uint BuyCount { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, ShopType);
            WriteTlvInt32(buffer, 2, (int)ShopId);
            WriteTlvInt32(buffer, 3, (int)SaleId);
            WriteTlvInt32(buffer, 4, (int)BuyCount);
        }
    }
}
