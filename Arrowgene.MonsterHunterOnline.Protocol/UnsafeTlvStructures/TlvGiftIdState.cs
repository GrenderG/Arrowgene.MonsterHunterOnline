using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for gift ID with state.
    /// C++ Reader: crygame.dll+sub_1024AED0 (UnkTlv0296)
    /// C++ Printer: crygame.dll+sub_1024B1A0
    /// </summary>
    public class TlvGiftIdState : Structure, ITlvStructure
    {
        /// <summary>
        /// Gift ID.
        /// Field ID: 1
        /// </summary>
        public int GiftId { get; set; }

        /// <summary>
        /// Gift state.
        /// Field ID: 2
        /// </summary>
        public byte GiftState { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, GiftId);
            WriteTlvByte(buffer, 2, GiftState);
        }
    }
}
