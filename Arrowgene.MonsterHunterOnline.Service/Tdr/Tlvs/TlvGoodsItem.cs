using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for goods inventory item.
    /// C++ Reader: crygame.dll+sub_1012EFA0 (UnkTlv0041)
    /// C++ Printer: crygame.dll+sub_1012F0F0
    /// </summary>
    public class TlvGoodsItem : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Item identifier.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Goods number/quantity.
        /// Field ID: 2
        /// </summary>
        public uint GoodsNumber { get; set; }

        /// <summary>
        /// Last change time.
        /// Field ID: 3
        /// </summary>
        public uint LastChangeTime { get; set; }

        /// <summary>
        /// Is owned flag.
        /// Field ID: 4
        /// </summary>
        public byte IsOwned { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = reader.ReadInt(); break;
                    case 2: GoodsNumber = (uint)reader.ReadInt(); break;
                    case 3: LastChangeTime = (uint)reader.ReadInt(); break;
                    case 4: IsOwned = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Id);
            WriteTlvInt(writer, 2, (int)GoodsNumber);
            WriteTlvInt(writer, 3, (int)LastChangeTime);
            WriteTlvByte(writer, 4, IsOwned);
        }
    }
}
