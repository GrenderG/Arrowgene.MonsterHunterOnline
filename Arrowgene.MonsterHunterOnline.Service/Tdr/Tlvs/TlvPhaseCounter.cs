using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for phase counter tracking.
    /// C++ Reader: crygame.dll+sub_10154020 (UnkTlv0083)
    /// C++ Printer: crygame.dll+sub_10154120
    /// </summary>
    public class TlvPhaseCounter : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Identifier.
        /// Field ID: 1
        /// </summary>
        public byte Id { get; set; }

        /// <summary>
        /// Phase value.
        /// Field ID: 2
        /// </summary>
        public byte Phase { get; set; }

        /// <summary>
        /// Counter value.
        /// Field ID: 3
        /// </summary>
        public uint Counter { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = reader.ReadByte(); break;
                    case 2: Phase = reader.ReadByte(); break;
                    case 3: Counter = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, Id);
            WriteTlvByte(writer, 2, Phase);
            WriteTlvInt(writer, 3, (int)Counter);
        }
    }
}
