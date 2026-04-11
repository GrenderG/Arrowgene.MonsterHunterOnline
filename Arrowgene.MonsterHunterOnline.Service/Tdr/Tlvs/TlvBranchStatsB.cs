using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for branch stats (variant B with short level).
    /// C++ Reader: crygame.dll+sub_10216470 (inner of UnkTlv0236)
    /// C++ Printer: crygame.dll+sub_102166E0
    /// </summary>
    public class TlvBranchStatsB : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Branch type.
        /// Field ID: 1
        /// </summary>
        public byte BranchType { get; set; }

        /// <summary>
        /// Branch level.
        /// Field ID: 2
        /// </summary>
        public short BranchLevel { get; set; }

        /// <summary>
        /// Branch total score.
        /// Field ID: 3
        /// </summary>
        public uint BranchAllScore { get; set; }

        /// <summary>
        /// Branch daily score.
        /// Field ID: 4
        /// </summary>
        public uint BranchDayScore { get; set; }

        /// <summary>
        /// Branch record card level-up times.
        /// Field ID: 5
        /// </summary>
        public int BranchRecordCardLevelUpTimes { get; set; }

        /// <summary>
        /// Branch challenge score.
        /// Field ID: 6
        /// </summary>
        public uint BranchChallengeScore { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: BranchType = reader.ReadByte(); break;
                    case 2: BranchLevel = reader.ReadShort(); break;
                    case 3: BranchAllScore = (uint)reader.ReadInt(); break;
                    case 4: BranchDayScore = (uint)reader.ReadInt(); break;
                    case 5: BranchRecordCardLevelUpTimes = reader.ReadInt(); break;
                    case 6: BranchChallengeScore = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, BranchType);
            WriteTlvShort(writer, 2, BranchLevel);
            WriteTlvInt(writer, 3, (int)BranchAllScore);
            WriteTlvInt(writer, 4, (int)BranchDayScore);
            WriteTlvInt(writer, 5, BranchRecordCardLevelUpTimes);
            WriteTlvInt(writer, 6, (int)BranchChallengeScore);
        }
    }
}
