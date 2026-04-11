using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for player report info.
    /// C++ Reader: crygame.dll+sub_10183530 (UnkTlv0148)
    /// C++ Printer: crygame.dll+sub_10183610
    /// </summary>
    public class TlvPlayerReportInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Other player database ID.
        /// Field ID: 1
        /// </summary>
        public ulong OtherPlayerDbId { get; set; }

        /// <summary>
        /// Report other player time.
        /// Field ID: 2
        /// </summary>
        public int ReportOtherPlayerTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: OtherPlayerDbId = (ulong)reader.ReadLong(); break;
                    case 2: ReportOtherPlayerTime = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvLong(writer, 1, (long)OtherPlayerDbId);
            WriteTlvInt(writer, 2, ReportOtherPlayerTime);
        }
    }
}
