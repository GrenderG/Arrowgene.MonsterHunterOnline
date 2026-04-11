using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for prize ID with state.
    /// C++ Reader: crygame.dll+sub_10172170 (UnkTlv0123)
    /// C++ Printer: crygame.dll+sub_10172250
    /// </summary>
    public class TlvPrizeIdState : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Prize ID.
        /// Field ID: 1
        /// </summary>
        public int PrizeId { get; set; }

        /// <summary>
        /// State value.
        /// Field ID: 2
        /// </summary>
        public byte State { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: PrizeId = reader.ReadInt(); break;
                    case 2: State = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, PrizeId);
            WriteTlvByte(writer, 2, State);
        }
    }
}
