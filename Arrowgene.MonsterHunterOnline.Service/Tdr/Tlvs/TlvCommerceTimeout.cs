using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for commerce timeout tracking.
    /// C++ Reader: crygame.dll+sub_1012F1C0 (UnkTlv0042)
    /// C++ Printer: crygame.dll+sub_1012F2A0
    /// </summary>
    public class TlvCommerceTimeout : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Commerce identifier.
        /// Field ID: 1
        /// </summary>
        public int CommerceId { get; set; }

        /// <summary>
        /// Timeout date.
        /// Field ID: 2
        /// </summary>
        public uint TimeoutDate { get; set; }

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
                    case 2: TimeoutDate = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, CommerceId);
            WriteTlvInt(writer, 2, (int)TimeoutDate);
        }
    }
}
