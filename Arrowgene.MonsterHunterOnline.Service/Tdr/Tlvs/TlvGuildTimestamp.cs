using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guild timestamp with commerce ID.
    /// C++ Reader: crygame.dll+sub_1016F8B0 (UnkTlv0119)
    /// C++ Printer: crygame.dll+sub_1016F9D0
    /// </summary>
    public class TlvGuildTimestamp : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: GuildId = reader.ReadLong(); break;
                    case 2: Timestamp = reader.ReadInt(); break;
                    case 3: CommerceId = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvLong(writer, 1, GuildId);
            WriteTlvInt(writer, 2, Timestamp);
            WriteTlvInt(writer, 3, CommerceId);
        }
    }
}
