using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for weekly award count with reset time.
    /// C++ Reader: crygame.dll+sub_10159E40 (UnkTlv0092)
    /// C++ Printer: crygame.dll+sub_1015A0E0
    /// </summary>
    public class TlvWeeklyAwardTime : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Weekly award count.
        /// Field ID: 1
        /// </summary>
        public short WeeklyAwardCnt { get; set; }

        /// <summary>
        /// Last reset time.
        /// Field ID: 3
        /// </summary>
        public uint LastResetTm { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: WeeklyAwardCnt = reader.ReadShort(); break;
                    case 3: LastResetTm = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvShort(writer, 1, WeeklyAwardCnt);
            WriteTlvInt(writer, 3, (int)LastResetTm);
        }
    }
}
