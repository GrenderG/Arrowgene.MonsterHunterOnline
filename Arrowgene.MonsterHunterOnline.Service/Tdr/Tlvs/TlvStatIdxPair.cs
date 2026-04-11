using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for stat index pair (short fields).
    /// C++ Reader: crygame.dll+sub_1021A880 (UnkTlv0241)
    /// C++ Printer: crygame.dll+sub_1021AB50
    /// </summary>
    public class TlvStatIdxPair : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Stat index.
        /// Field ID: 1
        /// </summary>
        public short StatIdx { get; set; }

        /// <summary>
        /// Stat value.
        /// Field ID: 2
        /// </summary>
        public short StatValue { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: StatIdx = reader.ReadShort(); break;
                    case 2: StatValue = reader.ReadShort(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvShort(writer, 1, StatIdx);
            WriteTlvShort(writer, 2, StatValue);
        }
    }
}
