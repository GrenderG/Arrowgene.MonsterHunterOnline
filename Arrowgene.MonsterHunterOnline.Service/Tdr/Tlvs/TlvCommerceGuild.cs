using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for commerce and guild ownership.
    /// C++ Reader: crygame.dll+sub_1011A0D0 (UnkTlv0015)
    /// C++ Printer: crygame.dll+sub_1011A2B0
    /// </summary>
    public class TlvCommerceGuild : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: CommerceId = (uint)reader.ReadInt(); break;
                    case 2: OwnGuildId = (ulong)reader.ReadLong(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, (int)CommerceId);
            WriteTlvLong(writer, 2, (long)OwnGuildId);
        }
    }
}
