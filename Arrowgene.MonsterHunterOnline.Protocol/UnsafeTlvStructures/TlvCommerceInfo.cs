using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for commerce info (ID + guild ID).
    /// C++ Reader: crygame.dll+sub_1011A260 (inner of UnkTlv0018)
    /// C++ Printer: crygame.dll+sub_1011A400
    /// </summary>
    public class TlvCommerceInfo : Structure, ITlvStructure
    {
        /// <summary>
        /// Commerce ID.
        /// Field ID: 1
        /// </summary>
        public int CommerceId { get; set; }

        /// <summary>
        /// Own guild ID.
        /// Field ID: 2
        /// </summary>
        public long OwnGuildId { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, CommerceId);
            WriteTlvInt64(buffer, 2, OwnGuildId);
        }
    }
}
