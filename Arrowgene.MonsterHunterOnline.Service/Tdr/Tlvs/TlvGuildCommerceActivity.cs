using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guild commerce activity info.
    /// C++ Reader: crygame.dll+sub_10119C20 (UnkTlv0014)
    /// C++ Printer: crygame.dll+sub_10119DC0
    /// </summary>
    public class TlvGuildCommerceActivity : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: GuildId = (ulong)reader.ReadLong(); break;
                    case 2: CommerceId = reader.ReadInt(); break;
                    case 3: ActivityStartTime = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvULong(writer, 1, GuildId);
            WriteTlvInt(writer, 2, CommerceId);
            WriteTlvInt(writer, 3, ActivityStartTime);
        }
    }
}
