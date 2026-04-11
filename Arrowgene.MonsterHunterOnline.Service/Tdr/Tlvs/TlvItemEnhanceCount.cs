using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for item enhance count.
    /// C++ Reader: crygame.dll+sub_10185370 (UnkTlv0150)
    /// C++ Printer: crygame.dll+sub_10185450
    /// </summary>
    public class TlvItemEnhanceCount : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Item ID.
        /// Field ID: 1
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Enhance count.
        /// Field ID: 2
        /// </summary>
        public uint EnhanceCount { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: ItemId = reader.ReadInt(); break;
                    case 2: EnhanceCount = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, ItemId);
            WriteTlvInt(writer, 2, (int)EnhanceCount);
        }
    }
}
