using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for commodity sales (short fields).
    /// C++ Reader: crygame.dll+sub_10212770 (UnkTlv0229)
    /// C++ Printer: crygame.dll+sub_10212A40
    /// </summary>
    public class TlvCommoditySalesShort : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Commodity ID.
        /// Field ID: 1
        /// </summary>
        public short Commodity { get; set; }

        /// <summary>
        /// Sold count.
        /// Field ID: 2
        /// </summary>
        public short SaledCount { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Commodity = reader.ReadShort(); break;
                    case 2: SaledCount = reader.ReadShort(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvShort(writer, 1, Commodity);
            WriteTlvShort(writer, 2, SaledCount);
        }
    }
}
