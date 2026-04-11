using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for ID with buy times.
    /// C++ Reader: crygame.dll+sub_10236220 (UnkTlv0270)
    /// C++ Printer: crygame.dll+sub_10236300
    /// </summary>
    public class TlvIdBuyTimes : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// This ID.
        /// Field ID: 1
        /// </summary>
        public int ThisId { get; set; }

        /// <summary>
        /// Buy times.
        /// Field ID: 2
        /// </summary>
        public int BuyTimes { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: ThisId = reader.ReadInt(); break;
                    case 2: BuyTimes = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, ThisId);
            WriteTlvInt(writer, 2, BuyTimes);
        }
    }
}
