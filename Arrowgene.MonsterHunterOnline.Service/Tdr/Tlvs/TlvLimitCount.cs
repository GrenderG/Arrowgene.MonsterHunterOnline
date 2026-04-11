using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for limit count tracking.
    /// C++ Reader: crygame.dll+sub_10178400 (UnkTlv0132)
    /// C++ Printer: crygame.dll+sub_101784C0
    /// </summary>
    public class TlvLimitCount : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Limit identifier.
        /// Field ID: 1
        /// </summary>
        public int LimitId { get; set; }

        /// <summary>
        /// Count value.
        /// Field ID: 2
        /// </summary>
        public int Count { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: LimitId = reader.ReadInt(); break;
                    case 2: Count = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, LimitId);
            WriteTlvInt(writer, 2, Count);
        }
    }
}
