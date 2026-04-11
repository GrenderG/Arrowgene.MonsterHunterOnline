using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for hide/enable flags.
    /// C++ Reader: crygame.dll+sub_10163B00 (UnkTlv0099)
    /// C++ Printer: crygame.dll+sub_10163BE0
    /// </summary>
    public class TlvHideEnable : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Hide flag.
        /// Field ID: 1
        /// </summary>
        public int Hide { get; set; }

        /// <summary>
        /// Enable flag.
        /// Field ID: 2
        /// </summary>
        public int Enable { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Hide = reader.ReadInt(); break;
                    case 2: Enable = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Hide);
            WriteTlvInt(writer, 2, Enable);
        }
    }
}
