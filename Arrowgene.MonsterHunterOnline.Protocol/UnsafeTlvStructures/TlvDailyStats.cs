using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for daily stats tracking.
    /// C++ Reader: crygame.dll+sub_10181510 (UnkTlv0146)
    /// C++ Printer: crygame.dll+sub_101817B0
    /// </summary>
    public class TlvDailyStats : Structure, ITlvStructure
    {
        /// <summary>
        /// Date day.
        /// Field ID: 1
        /// </summary>
        public int DateDay { get; set; }

        /// <summary>
        /// Current higher.
        /// Field ID: 2
        /// </summary>
        public int CurHigher { get; set; }

        /// <summary>
        /// Meet time.
        /// Field ID: 3
        /// </summary>
        public int MeetTime { get; set; }

        /// <summary>
        /// Giant time.
        /// Field ID: 4
        /// </summary>
        public int GiantTime { get; set; }

        /// <summary>
        /// Daily reward flag.
        /// Field ID: 5
        /// </summary>
        public int DailyRewardFlag { get; set; }

        /// <summary>
        /// Reward flag.
        /// Field ID: 6
        /// </summary>
        public int RewardFlag { get; set; }

        /// <summary>
        /// Activity value.
        /// Field ID: 7
        /// </summary>
        public int Activity { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, DateDay);
            WriteTlvInt32(buffer, 2, CurHigher);
            WriteTlvInt32(buffer, 3, MeetTime);
            WriteTlvInt32(buffer, 4, GiantTime);
            WriteTlvInt32(buffer, 5, DailyRewardFlag);
            WriteTlvInt32(buffer, 6, RewardFlag);
            WriteTlvInt32(buffer, 7, Activity);
        }
    }
}
