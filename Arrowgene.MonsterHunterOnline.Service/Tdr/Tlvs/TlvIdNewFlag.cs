using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for ID with new flag.
    /// C++ Reader: crygame.dll+sub_101A2660 (UnkTlv0180)
    /// C++ Printer: crygame.dll+sub_101A2740
    /// </summary>
    public class TlvIdNewFlag : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// ID value.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// New flag.
        /// Field ID: 2
        /// </summary>
        public byte NewFlag { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = reader.ReadInt(); break;
                    case 2: NewFlag = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Id);
            WriteTlvByte(writer, 2, NewFlag);
        }
    }
}
