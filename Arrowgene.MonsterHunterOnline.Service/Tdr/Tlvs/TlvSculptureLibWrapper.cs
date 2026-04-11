using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for sculpture lib wrapper (single nested lib field).
    /// C++ Reader: crygame.dll+sub_10141750 (UnkTlv0061)
    /// C++ Printer: crygame.dll+sub_10141AD0
    /// </summary>
    public class TlvSculptureLibWrapper : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Libs data. Field ID: 1</summary>
        public TlvSculptureLibData Libs { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Libs = ReadTlvObject<TlvSculptureLibData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvObject(writer, 1, Libs);
        }
    }
}
