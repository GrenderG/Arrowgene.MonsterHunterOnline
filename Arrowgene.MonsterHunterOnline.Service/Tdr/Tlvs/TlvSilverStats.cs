using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for silver currency tracking.
    /// C++ Reader: crygame.dll+sub_10180D20 (UnkTlv0145)
    /// C++ Printer: crygame.dll+sub_10180E80
    /// </summary>
    public class TlvSilverStats : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Silver count.
        /// Field ID: 1
        /// </summary>
        public int SilverCount { get; set; }

        /// <summary>
        /// Weekly free fetch times.
        /// Field ID: 2
        /// </summary>
        public int WeekFreeFetchTimes { get; set; }

        /// <summary>
        /// Weekly buy fetch times.
        /// Field ID: 3
        /// </summary>
        public int WeekBuyFetchTimes { get; set; }

        /// <summary>
        /// Enlarge times.
        /// Field ID: 4
        /// </summary>
        public int EnlargeTimes { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: SilverCount = reader.ReadInt(); break;
                    case 2: WeekFreeFetchTimes = reader.ReadInt(); break;
                    case 3: WeekBuyFetchTimes = reader.ReadInt(); break;
                    case 4: EnlargeTimes = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, SilverCount);
            WriteTlvInt(writer, 2, WeekFreeFetchTimes);
            WriteTlvInt(writer, 3, WeekBuyFetchTimes);
            WriteTlvInt(writer, 4, EnlargeTimes);
        }
    }
}
