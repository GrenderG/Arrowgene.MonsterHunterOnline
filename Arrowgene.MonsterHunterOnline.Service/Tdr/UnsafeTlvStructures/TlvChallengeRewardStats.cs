using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for challenge reward tracking.
    /// C++ Reader: crygame.dll+sub_1014E170 (UnkTlv0078)
    /// C++ Printer: crygame.dll+sub_1014E300
    /// </summary>
    public class TlvChallengeRewardStats : Structure, ITlvStructure
    {
        /// <summary>
        /// Last reset time.
        /// Field ID: 1
        /// </summary>
        public uint LastResetTime { get; set; }

        /// <summary>
        /// Gain challenge reward times.
        /// Field ID: 2
        /// </summary>
        public int GainChallengeRewardTimes { get; set; }

        /// <summary>
        /// Gain success reward times.
        /// Field ID: 3
        /// </summary>
        public int GainSuccessRewardTimes { get; set; }

        /// <summary>
        /// Gain VIP challenge reward times.
        /// Field ID: 4
        /// </summary>
        public int GainVipChallengeRewardTimes { get; set; }

        /// <summary>
        /// Gain VIP success reward times.
        /// Field ID: 5
        /// </summary>
        public int GainVipSuccessRewardTimes { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)LastResetTime);
            WriteTlvInt32(buffer, 2, GainChallengeRewardTimes);
            WriteTlvInt32(buffer, 3, GainSuccessRewardTimes);
            WriteTlvInt32(buffer, 4, GainVipChallengeRewardTimes);
            WriteTlvInt32(buffer, 5, GainVipSuccessRewardTimes);
        }
    }
}
