using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for credit.
    /// C++ Reader: crygame.dll+sub_1016E000 (UnkTlv0116)
    /// C++ Printer: crygame.dll+sub_1016E1F0
    /// </summary>
    public class TlvCredit : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Credit value.
        /// Field ID: 1
        /// </summary>
        public int Credit { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Credit = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Credit);
        }
    }
}
