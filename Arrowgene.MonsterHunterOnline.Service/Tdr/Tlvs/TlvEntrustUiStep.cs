using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for entrust UI step.
    /// C++ Reader: crygame.dll+sub_1019BC10 (UnkTlv0172)
    /// C++ Printer: crygame.dll+sub_1019BE00
    /// </summary>
    public class TlvEntrustUiStep : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Entrust UI step.
        /// Field ID: 1
        /// </summary>
        public int EntrustUiStep { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: EntrustUiStep = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, EntrustUiStep);
        }
    }
}
