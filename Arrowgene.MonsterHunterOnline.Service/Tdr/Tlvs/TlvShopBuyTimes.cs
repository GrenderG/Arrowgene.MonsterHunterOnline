using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for shop buy times tracking.
    /// C++ Reader: crygame.dll+sub_1022E350 (UnkTlv0270)
    /// C++ Printer: crygame.dll+sub_1022E410
    /// </summary>
    public class TlvShopBuyTimes : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Item or shop ID.
        /// Field ID: 1
        /// </summary>
        public int ThisId { get; set; }

        /// <summary>
        /// Number of times bought.
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
