using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for shop buy times tracking.
    /// C++ Reader: crygame.dll+sub_1022E350 (UnkTlv0270)
    /// C++ Printer: crygame.dll+sub_1022E410
    /// </summary>
    public class TlvShopBuyTimes : Structure, ITlvStructure
    {
        /// <summary>
        /// Item or shop ID.
        /// Field ID: 1
        /// </summary>
        public int ThisId { get; set; }

        /// <summary>
        /// Number of times bought.
        /// Field ID: 2
        /// </summary>
        public int BuyTimes { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, ThisId);
            WriteTlvInt32(buffer, 2, BuyTimes);
        }
    }
}
