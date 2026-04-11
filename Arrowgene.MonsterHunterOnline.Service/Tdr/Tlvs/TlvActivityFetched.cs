using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for activity with fetched flag.
    /// C++ Reader: crygame.dll+sub_1015A4D0 (UnkTlv0093)
    /// C++ Printer: crygame.dll+sub_1015A7A0
    /// </summary>
    public class TlvActivityFetched : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Activity value.
        /// Field ID: 1
        /// </summary>
        public int Activity { get; set; }

        /// <summary>
        /// Fetched flag.
        /// Field ID: 2
        /// </summary>
        public byte Fetched { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Activity = reader.ReadInt(); break;
                    case 2: Fetched = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Activity);
            WriteTlvByte(writer, 2, Fetched);
        }
    }
}
