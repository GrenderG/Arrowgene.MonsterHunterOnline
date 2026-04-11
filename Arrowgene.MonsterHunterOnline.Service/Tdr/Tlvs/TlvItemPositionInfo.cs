using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for item position info.
    /// C++ Reader: crygame.dll+sub_10191590 (UnkTlv0166)
    /// C++ Printer: crygame.dll+sub_10191700
    /// </summary>
    public class TlvItemPositionInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Position.
        /// Field ID: 1
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Item ID.
        /// Field ID: 2
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Item number.
        /// Field ID: 3
        /// </summary>
        public int ItemNum { get; set; }

        /// <summary>
        /// Quality.
        /// Field ID: 4
        /// </summary>
        public int Quality { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Position = reader.ReadInt(); break;
                    case 2: ItemId = reader.ReadInt(); break;
                    case 3: ItemNum = reader.ReadInt(); break;
                    case 4: Quality = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Position);
            WriteTlvInt(writer, 2, ItemId);
            WriteTlvInt(writer, 3, ItemNum);
            WriteTlvInt(writer, 4, Quality);
        }
    }
}
