using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for level time tracking with layer.
    /// C++ Reader: crygame.dll+sub_10144CF0 (UnkTlv0067)
    /// C++ Printer: crygame.dll+sub_10144E00
    /// </summary>
    public class TlvLevelTimeLayer : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Level ID.
        /// Field ID: 1
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// Seconds value.
        /// Field ID: 2
        /// </summary>
        public short Seconds { get; set; }

        /// <summary>
        /// Layer value.
        /// Field ID: 3
        /// </summary>
        public short Layer { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: LevelId = reader.ReadInt(); break;
                    case 2: Seconds = reader.ReadShort(); break;
                    case 3: Layer = reader.ReadShort(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, LevelId);
            WriteTlvShort(writer, 2, Seconds);
            WriteTlvShort(writer, 3, Layer);
        }
    }
}
