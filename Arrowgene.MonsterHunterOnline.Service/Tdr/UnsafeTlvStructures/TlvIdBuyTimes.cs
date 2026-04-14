using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for ID with buy times.
    /// C++ Reader: crygame.dll+sub_10236220 (UnkTlv0270)
    /// C++ Printer: crygame.dll+sub_10236300
    /// </summary>
    public class TlvIdBuyTimes : Structure, ITlvStructure
    {
        /// <summary>
        /// This ID.
        /// Field ID: 1
        /// </summary>
        public int ThisId { get; set; }

        /// <summary>
        /// Buy times.
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
