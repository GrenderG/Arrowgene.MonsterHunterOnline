using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for commerce and guild ownership.
    /// C++ Reader: crygame.dll+sub_1011A0D0 (UnkTlv0015)
    /// C++ Printer: crygame.dll+sub_1011A2B0
    /// </summary>
    public class TlvCommerceGuild : Structure, ITlvStructure
    {
        /// <summary>
        /// Commerce identifier.
        /// Field ID: 1
        /// </summary>
        public uint CommerceId { get; set; }

        /// <summary>
        /// Own guild identifier.
        /// Field ID: 2
        /// </summary>
        public ulong OwnGuildId { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)CommerceId);
            WriteTlvInt64(buffer, 2, (long)OwnGuildId);
        }
    }
}
