using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (Player Social / Guild / Team Info).
    /// C++ Reader: crygame.dll+sub_10115620
    /// C++ Printer: crygame.dll+sub_10115840
    /// </summary>
    public class TlvSocialInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        public int TeamId { get; set; }
        public int GuildId { get; set; }
        public int IsGuildLeader { get; set; } // Represented as Int32 in the C++ structure
        public byte IsClanLeader { get; set; } // Represented as Byte in the C++ structure

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1:
                        TeamId = reader.ReadInt();
                        break;

                    case 2:
                        GuildId = reader.ReadInt();
                        break;

                    case 3:
                        IsGuildLeader = reader.ReadInt();
                        break;

                    case 4:
                        IsClanLeader = reader.ReadByte();
                        break;

                    default:
                        SkipTlvField(reader, wireType);
                        break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, TeamId);
            WriteTlvInt(writer, 2, GuildId);
            WriteTlvInt(writer, 3, IsGuildLeader);
            WriteTlvByte(writer, 4, IsClanLeader);
        }
    }
}