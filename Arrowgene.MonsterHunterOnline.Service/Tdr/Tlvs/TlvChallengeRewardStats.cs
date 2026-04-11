using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for challenge reward tracking.
    /// C++ Reader: crygame.dll+sub_1014E170 (UnkTlv0078)
    /// C++ Printer: crygame.dll+sub_1014E300
    /// </summary>
    public class TlvChallengeRewardStats : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: LastResetTime = (uint)reader.ReadInt(); break;
                    case 2: GainChallengeRewardTimes = reader.ReadInt(); break;
                    case 3: GainSuccessRewardTimes = reader.ReadInt(); break;
                    case 4: GainVipChallengeRewardTimes = reader.ReadInt(); break;
                    case 5: GainVipSuccessRewardTimes = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, (int)LastResetTime);
            WriteTlvInt(writer, 2, GainChallengeRewardTimes);
            WriteTlvInt(writer, 3, GainSuccessRewardTimes);
            WriteTlvInt(writer, 4, GainVipChallengeRewardTimes);
            WriteTlvInt(writer, 5, GainVipSuccessRewardTimes);
        }
    }
}
