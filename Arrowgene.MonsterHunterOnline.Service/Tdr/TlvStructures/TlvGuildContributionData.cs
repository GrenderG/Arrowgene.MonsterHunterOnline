using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for guild contribution data.
    /// C++ Reader: crygame.dll+sub_1016FB80 (UnkTlv0120)
    /// C++ Printer: crygame.dll+sub_10170270
    /// </summary>
    public class TlvGuildContributionData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxGuilds = 10;

        /// <summary>Field ID: 1</summary>
        public long Guild { get; set; }

        /// <summary>Field ID: 2</summary>
        public int Contribution { get; set; }

        /// <summary>Field ID: 3</summary>
        public long ContributionAcc { get; set; }

        /// <summary>Field ID: 4</summary>
        public long ContributionWeekAcc { get; set; }

        /// <summary>Field ID: 5</summary>
        public int LastTime { get; set; }

        /// <summary>Field ID: 6</summary>
        public int RefreshTimestamp { get; set; }

        /// <summary>Field ID: 7</summary>
        public int StartBoatTimes { get; set; }

        /// <summary>Field ID: 8</summary>
        public byte BuyStartBoatTimes { get; set; }

        /// <summary>Guild count (derived). Field ID: 9</summary>
        public int GuildCount => GuildwarGrabPlayerId?.Count ?? 0;

        /// <summary>Guild war grab player data. Field ID: 10</summary>
        public List<TlvGuildTimestamp> GuildwarGrabPlayerId { get; set; }

        /// <summary>Field ID: 11</summary>
        public long GuildwarGrabPlayerTimeStamp { get; set; }

        /// <summary>Field ID: 12</summary>
        public int OtherGuildNews { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((GuildwarGrabPlayerId?.Count ?? 0) > MaxGuilds)
                throw new InvalidDataException($"[TlvGuildContributionData] GuildwarGrabPlayerId exceeds {MaxGuilds}.");

            WriteTlvInt64(buffer, 1, Guild);
            WriteTlvInt32(buffer, 2, Contribution);
            WriteTlvInt64(buffer, 3, ContributionAcc);
            WriteTlvInt64(buffer, 4, ContributionWeekAcc);
            WriteTlvInt32(buffer, 5, LastTime);
            WriteTlvInt32(buffer, 6, RefreshTimestamp);
            WriteTlvInt32(buffer, 7, StartBoatTimes);
            WriteTlvByte(buffer, 8, BuyStartBoatTimes);
            WriteTlvInt32(buffer, 9, GuildCount);
            WriteTlvSubStructureList(buffer, 10, GuildwarGrabPlayerId.Count, GuildwarGrabPlayerId);
            WriteTlvInt64(buffer, 11, GuildwarGrabPlayerTimeStamp);
            WriteTlvInt32(buffer, 12, OtherGuildNews);
        }
    }
}
