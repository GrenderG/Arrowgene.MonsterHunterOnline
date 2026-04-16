using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for silver currency tracking.
    /// C++ Reader: crygame.dll+sub_10180D20 (UnkTlv0145)
    /// C++ Printer: crygame.dll+sub_10180E80
    /// </summary>
    public class TlvSilverStats : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, SilverCount);
            WriteTlvInt32(buffer, 2, WeekFreeFetchTimes);
            WriteTlvInt32(buffer, 3, WeekBuyFetchTimes);
            WriteTlvInt32(buffer, 4, EnlargeTimes);
        }
    }
}
