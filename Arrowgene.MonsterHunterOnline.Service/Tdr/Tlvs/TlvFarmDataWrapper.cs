using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for farm data wrapper.
    /// C++ Reader: crygame.dll+sub_10206340 (UnkTlv0218)
    /// C++ Printer: crygame.dll+sub_10206660
    /// </summary>
    public class TlvFarmDataWrapper : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Farm data. Field ID: 2</summary>
        public TlvFarmData Farm { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: Farm = ReadTlvObject<TlvFarmData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvObject(writer, 2, Farm);
        }
    }
}
