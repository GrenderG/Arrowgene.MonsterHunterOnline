using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for guild commerce activity info.
    /// C++ Reader: crygame.dll+sub_10119C20 (UnkTlv0014)
    /// C++ Printer: crygame.dll+sub_10119DC0
    /// </summary>
    public class TlvGuildCommerceActivity : Structure, ITlvStructure
    {
        /// <summary>
        /// Guild ID.
        /// Field ID: 1
        /// </summary>
        public ulong GuildId { get; set; }

        /// <summary>
        /// Commerce ID.
        /// Field ID: 2
        /// </summary>
        public int CommerceId { get; set; }

        /// <summary>
        /// Activity start time.
        /// Field ID: 3
        /// </summary>
        public int ActivityStartTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvUInt64(buffer, 1, GuildId);
            WriteTlvInt32(buffer, 2, CommerceId);
            WriteTlvInt32(buffer, 3, ActivityStartTime);
        }
    }
}
