using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for guild timestamp with commerce ID.
    /// C++ Reader: crygame.dll+sub_1016F8B0 (UnkTlv0119)
    /// C++ Printer: crygame.dll+sub_1016F9D0
    /// </summary>
    public class TlvGuildTimestamp : Structure, ITlvStructure
    {
        /// <summary>
        /// Guild ID.
        /// Field ID: 1
        /// </summary>
        public long GuildId { get; set; }

        /// <summary>
        /// Timestamp.
        /// Field ID: 2
        /// </summary>
        public int Timestamp { get; set; }

        /// <summary>
        /// Commerce ID.
        /// Field ID: 3
        /// </summary>
        public int CommerceId { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt64(buffer, 1, GuildId);
            WriteTlvInt32(buffer, 2, Timestamp);
            WriteTlvInt32(buffer, 3, CommerceId);
        }
    }
}
