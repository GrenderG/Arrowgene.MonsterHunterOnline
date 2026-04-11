using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for sculpture round tracking.
    /// C++ Reader: crygame.dll+sub_10141F90 (UnkTlv0062)
    /// C++ Printer: crygame.dll+sub_10142050
    /// </summary>
    public class TlvSculptureRound : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Sculpture identifier.
        /// Field ID: 1
        /// </summary>
        public int Sculpture { get; set; }

        /// <summary>
        /// Round value.
        /// Field ID: 2
        /// </summary>
        public int Round { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Sculpture = reader.ReadInt(); break;
                    case 2: Round = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Sculpture);
            WriteTlvInt(writer, 2, Round);
        }
    }
}
