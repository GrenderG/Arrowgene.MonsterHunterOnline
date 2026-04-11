using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for daily stats tracking.
    /// C++ Reader: crygame.dll+sub_10181510 (UnkTlv0146)
    /// C++ Printer: crygame.dll+sub_101817B0
    /// </summary>
    public class TlvDailyStats : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: DateDay = reader.ReadInt(); break;
                    case 2: CurHigher = reader.ReadInt(); break;
                    case 3: MeetTime = reader.ReadInt(); break;
                    case 4: GiantTime = reader.ReadInt(); break;
                    case 5: DailyRewardFlag = reader.ReadInt(); break;
                    case 6: RewardFlag = reader.ReadInt(); break;
                    case 7: Activity = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, DateDay);
            WriteTlvInt(writer, 2, CurHigher);
            WriteTlvInt(writer, 3, MeetTime);
            WriteTlvInt(writer, 4, GiantTime);
            WriteTlvInt(writer, 5, DailyRewardFlag);
            WriteTlvInt(writer, 6, RewardFlag);
            WriteTlvInt(writer, 7, Activity);
        }
    }
}
