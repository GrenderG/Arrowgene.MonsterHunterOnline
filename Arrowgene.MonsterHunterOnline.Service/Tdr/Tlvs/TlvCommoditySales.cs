using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for commodity sales count.
    /// C++ Reader: crygame.dll+sub_10205830 (UnkTlv0225)
    /// C++ Printer: crygame.dll+sub_10205910
    /// </summary>
    public class TlvCommoditySales : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Commodity ID.
        /// Field ID: 1
        /// </summary>
        public int Commodity { get; set; }

        /// <summary>
        /// Sold count.
        /// Field ID: 2
        /// </summary>
        public int SaledCount { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Commodity = reader.ReadInt(); break;
                    case 2: SaledCount = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Commodity);
            WriteTlvInt(writer, 2, SaledCount);
        }
    }
}
