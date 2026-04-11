using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for tips check with refresh time.
    /// C++ Reader: crygame.dll+sub_1016A190 (UnkTlv0110)
    /// C++ Printer: crygame.dll+sub_1016A460
    /// </summary>
    public class TlvTipsRefresh : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Tips check flag.
        /// Field ID: 1
        /// </summary>
        public byte TipsCheck { get; set; }

        /// <summary>
        /// Refresh time.
        /// Field ID: 2
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
                    case 1: TipsCheck = reader.ReadByte(); break;
                    case 2: RefreshTime = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, TipsCheck);
            WriteTlvInt(writer, 2, (int)RefreshTime);
        }
    }
}
