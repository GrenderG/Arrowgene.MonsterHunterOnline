using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (Player Social / Guild / Team Info).
    /// C++ Reader: crygame.dll+sub_10115620
    /// C++ Printer: crygame.dll+sub_10115840
    /// </summary>
    public class TlvSocialInfo : Structure, ITlvStructure
    {
        public int TeamId { get; set; }
        public int GuildId { get; set; }
        public int IsGuildLeader { get; set; } // Represented as Int32 in the C++ structure
        public byte IsClanLeader { get; set; } // Represented as Byte in the C++ structure

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, TeamId);
            WriteTlvInt32(buffer, 2, GuildId);
            WriteTlvInt32(buffer, 3, IsGuildLeader);
            WriteTlvByte(buffer, 4, IsClanLeader);
        }
    }
}