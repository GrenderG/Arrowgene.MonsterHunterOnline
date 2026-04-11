using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for storage sizes.
    /// C++ Reader: crygame.dll+sub_10164390 (UnkTlv0101)
    /// C++ Printer: crygame.dll+sub_101644B0
    /// </summary>
    public class TlvStoreSizes : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Store size.
        /// Field ID: 2
        /// </summary>
        public short StoreSize { get; set; }

        /// <summary>
        /// Normal size.
        /// Field ID: 3
        /// </summary>
        public short NormalSize { get; set; }

        /// <summary>
        /// Material store size.
        /// Field ID: 4
        /// </summary>
        public short MaterialStoreSize { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: StoreSize = reader.ReadShort(); break;
                    case 3: NormalSize = reader.ReadShort(); break;
                    case 4: MaterialStoreSize = reader.ReadShort(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvShort(writer, 2, StoreSize);
            WriteTlvShort(writer, 3, NormalSize);
            WriteTlvShort(writer, 4, MaterialStoreSize);
        }
    }
}
