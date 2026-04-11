using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for card star/stat system data.
    /// C++ Reader: crygame.dll+sub_1021D840 (UnkTlv0245)
    /// C++ Printer: crygame.dll+sub_1021DD80
    /// </summary>
    public class TlvCardStarSystemData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        public const int MaxNewStat = 3000;

        /// <summary>Field ID: 2</summary>
        public int OpenFlag { get; set; }

        /// <summary>Field ID: 3</summary>
        public int ResetTime { get; set; }

        /// <summary>Field ID: 4</summary>
        public int RecordCardLevelUpTimes { get; set; }

        /// <summary>Field ID: 5</summary>
        public int StarScore { get; set; }

        /// <summary>Level info. Field ID: 6</summary>
        public TlvBranchStatsB LevelInfo { get; set; } = new();

        /// <summary>Card info. Field ID: 7</summary>
        public TlvUnlockCompleteBits CardInfo { get; set; } = new();

        /// <summary>Stat info. Field ID: 8</summary>
        public TlvStatData StatInfo { get; set; } = new();

        /// <summary>Track cards. Field ID: 9</summary>
        public TlvCountCards TrackCards { get; set; } = new();

        /// <summary>Field ID: 10</summary>
        public int WeeklyRefreshTime { get; set; }

        /// <summary>New stat count (derived). Field ID: 11</summary>
        public int NewStatCount => NewStatVals?.Length ?? 0;

        /// <summary>New stat IDs. Field ID: 12</summary>
        public short[] NewStatIds { get; set; }

        /// <summary>New stat values. Field ID: 13</summary>
        public int[] NewStatVals { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: OpenFlag = reader.ReadInt(); break;
                    case 3: ResetTime = reader.ReadInt(); break;
                    case 4: RecordCardLevelUpTimes = reader.ReadInt(); break;
                    case 5: StarScore = reader.ReadInt(); break;
                    case 6: LevelInfo = ReadTlvObject<TlvBranchStatsB>(reader); break;
                    case 7: CardInfo = ReadTlvObject<TlvUnlockCompleteBits>(reader); break;
                    case 8: StatInfo = ReadTlvObject<TlvStatData>(reader); break;
                    case 9: TrackCards = ReadTlvObject<TlvCountCards>(reader); break;
                    case 10: WeeklyRefreshTime = reader.ReadInt(); break;
                    case 11: reader.ReadInt(); break;
                    case 12: NewStatIds = ReadTlvShortArray(reader); break;
                    case 13: NewStatVals = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((NewStatIds?.Length ?? 0) > MaxNewStat) throw new InvalidDataException($"[TlvCardStarSystemData] NewStatIds exceeds {MaxNewStat}.");
            if ((NewStatVals?.Length ?? 0) > MaxNewStat) throw new InvalidDataException($"[TlvCardStarSystemData] NewStatVals exceeds {MaxNewStat}.");

            WriteTlvInt(writer, 2, OpenFlag);
            WriteTlvInt(writer, 3, ResetTime);
            WriteTlvInt(writer, 4, RecordCardLevelUpTimes);
            WriteTlvInt(writer, 5, StarScore);
            WriteTlvObject(writer, 6, LevelInfo);
            WriteTlvObject(writer, 7, CardInfo);
            WriteTlvObject(writer, 8, StatInfo);
            WriteTlvObject(writer, 9, TrackCards);
            WriteTlvInt(writer, 10, WeeklyRefreshTime);
            WriteTlvInt(writer, 11, NewStatCount);
            WriteTlvShortArray(writer, 12, NewStatIds);
            WriteTlvIntArray(writer, 13, NewStatVals, NewStatCount);
        }
    }
}
