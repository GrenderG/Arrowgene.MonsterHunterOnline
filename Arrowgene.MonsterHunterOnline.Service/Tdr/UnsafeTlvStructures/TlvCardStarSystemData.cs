using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for card star/stat system data.
    /// C++ Reader: crygame.dll+sub_1021D840 (UnkTlv0245)
    /// C++ Printer: crygame.dll+sub_1021DD80
    /// </summary>
    public class TlvCardStarSystemData : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((NewStatIds?.Length ?? 0) > MaxNewStat) throw new InvalidDataException($"[TlvCardStarSystemData] NewStatIds exceeds {MaxNewStat}.");
            if ((NewStatVals?.Length ?? 0) > MaxNewStat) throw new InvalidDataException($"[TlvCardStarSystemData] NewStatVals exceeds {MaxNewStat}.");

            WriteTlvInt32(buffer, 2, OpenFlag);
            WriteTlvInt32(buffer, 3, ResetTime);
            WriteTlvInt32(buffer, 4, RecordCardLevelUpTimes);
            WriteTlvInt32(buffer, 5, StarScore);
            WriteTlvSubStructure(buffer, 6, LevelInfo);
            WriteTlvSubStructure(buffer, 7, CardInfo);
            WriteTlvSubStructure(buffer, 8, StatInfo);
            WriteTlvSubStructure(buffer, 9, TrackCards);
            WriteTlvInt32(buffer, 10, WeeklyRefreshTime);
            WriteTlvInt32(buffer, 11, NewStatCount);
            WriteTlvInt16Arr(buffer, 12, NewStatIds);
            WriteTlvInt32Arr(buffer, 13, NewStatVals);
        }
    }
}
