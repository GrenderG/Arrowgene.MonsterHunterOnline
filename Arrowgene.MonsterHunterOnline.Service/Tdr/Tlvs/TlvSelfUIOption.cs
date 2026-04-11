using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for self UI option data.
    /// C++ Reader: crygame.dll+sub_1019BD60 (inner of UnkTlv0173)
    /// C++ Printer: crygame.dll+sub_1019BEB0
    /// </summary>
    public class TlvSelfUIOption : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Entrust UI step.
        /// Field ID: 1
        /// </summary>
        public int EntrustUIStep { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: EntrustUIStep = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, EntrustUIStep);
        }
    }
}
