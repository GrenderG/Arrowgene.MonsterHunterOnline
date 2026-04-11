using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for commerce info (ID + guild ID).
    /// C++ Reader: crygame.dll+sub_1011A260 (inner of UnkTlv0018)
    /// C++ Printer: crygame.dll+sub_1011A400
    /// </summary>
    public class TlvCommerceInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: CommerceId = reader.ReadInt(); break;
                    case 2: OwnGuildId = reader.ReadLong(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, CommerceId);
            WriteTlvLong(writer, 2, OwnGuildId);
        }
    }
}
