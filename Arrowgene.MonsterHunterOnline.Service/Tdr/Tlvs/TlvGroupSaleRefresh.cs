using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for group sale with refresh time.
    /// C++ Reader: crygame.dll+sub_10210AE0 (UnkTlv0227)
    /// C++ Printer: crygame.dll+sub_10210DC0
    /// </summary>
    public class TlvGroupSaleRefresh : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Group.
        /// Field ID: 1
        /// </summary>
        public short Group { get; set; }

        /// <summary>
        /// Sold count.
        /// Field ID: 2
        /// </summary>
        public int SaledCount { get; set; }

        /// <summary>
        /// Refresh time.
        /// Field ID: 3
        /// </summary>
        public uint RefreshTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Group = reader.ReadShort(); break;
                    case 2: SaledCount = reader.ReadInt(); break;
                    case 3: RefreshTime = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvShort(writer, 1, Group);
            WriteTlvInt(writer, 2, SaledCount);
            WriteTlvInt(writer, 3, (int)RefreshTime);
        }
    }
}
