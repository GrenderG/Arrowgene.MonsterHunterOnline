using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for element slot (item ID + enhance count).
    /// C++ Reader: crygame.dll+sub_101853A0 (inner of UnkTlv0151)
    /// C++ Printer: crygame.dll+sub_10185540
    /// </summary>
    public class TlvElementSlot : TlvStructure
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
        public int EnchanseCount { get; set; }

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
                    case 2: EnchanseCount = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, ItemId);
            WriteTlvInt(writer, 2, EnchanseCount);
        }
    }
}
