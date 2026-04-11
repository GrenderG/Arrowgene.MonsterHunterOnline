using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for shop sale info.
    /// C++ Reader: crygame.dll+sub_1020DF50 (UnkTlv0223)
    /// C++ Printer: crygame.dll+sub_1020E2E0
    /// </summary>
    public class TlvShopSaleInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Shop type.
        /// Field ID: 1
        /// </summary>
        public byte ShopType { get; set; }

        /// <summary>
        /// Shop ID.
        /// Field ID: 2
        /// </summary>
        public uint ShopId { get; set; }

        /// <summary>
        /// Sale ID.
        /// Field ID: 3
        /// </summary>
        public uint SaleId { get; set; }

        /// <summary>
        /// Buy count.
        /// Field ID: 4
        /// </summary>
        public uint BuyCount { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: ShopType = reader.ReadByte(); break;
                    case 2: ShopId = (uint)reader.ReadInt(); break;
                    case 3: SaleId = (uint)reader.ReadInt(); break;
                    case 4: BuyCount = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, ShopType);
            WriteTlvInt(writer, 2, (int)ShopId);
            WriteTlvInt(writer, 3, (int)SaleId);
            WriteTlvInt(writer, 4, (int)BuyCount);
        }
    }
}
