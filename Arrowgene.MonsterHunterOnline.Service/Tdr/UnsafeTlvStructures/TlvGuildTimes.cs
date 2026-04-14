using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for various time trackers.
    /// C++ Reader: crygame.dll+sub_1011C900 (UnkTlv0018)
    /// C++ Printer: crygame.dll+sub_1011C990
    /// </summary>
    public class TlvGuildTimes : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxEliteGuilds = 200; // 0xC8
        public const int MaxGuildWarHistory = 7;

        /// <summary>
        /// Wage time.
        /// Field ID: 1
        /// </summary>
        public uint WageTime { get; set; }

        /// <summary>
        /// Log time.
        /// Field ID: 2
        /// </summary>
        public uint LogTime { get; set; }

        /// <summary>
        /// Depot fetch count time.
        /// Field ID: 3
        /// </summary>
        public uint DepotFetchCountTime { get; set; }

        /// <summary>
        /// Random commodity time.
        /// Field ID: 4
        /// </summary>
        public uint RandCommodityTime { get; set; }

        /// <summary>
        /// Daily 3 time.
        /// Field ID: 6
        /// </summary>
        public uint Daily3Time { get; set; }

        /// <summary>
        /// Week 3 time.
        /// Field ID: 7
        /// </summary>
        public uint Week3Time { get; set; }

        /// <summary>
        /// Elite guild count.
        /// Field ID: 8
        /// </summary>
        public byte EliteGuildCount { get; set; }

        /// <summary>
        /// Elite guilds array (guild IDs).
        /// Field ID: 9
        /// </summary>
        public long[] EliteGuilds { get; set; }

        /// <summary>
        /// Commerce count.
        /// Field ID: 10
        /// </summary>
        public int CommerceCount { get; set; }

        /// <summary>
        /// Commerce info list.
        /// Field ID: 11
        /// </summary>
        public List<TlvCommerceInfo> CommerceInfo { get; set; }

        /// <summary>
        /// Dragon boat count.
        /// Field ID: 12
        /// </summary>
        public byte DragonBoatCount { get; set; }

        /// <summary>
        /// Dragon boat info.
        /// Field ID: 13
        /// </summary>
        public TlvCommerceBoat DragonBoatInfo { get; set; } = new();

        /// <summary>
        /// Guild war history count (derived from list).
        /// Field ID: 14
        /// </summary>
        public int GuildWarHistoryCount => GuildWarHistory?.Count ?? 0;

        /// <summary>
        /// Guild war history list (max 7).
        /// Field ID: 15
        /// </summary>
        public List<TlvGuildWarHistory> GuildWarHistory { get; set; }

        /// <summary>
        /// Guild war daily refresh timestamp.
        /// Field ID: 16
        /// </summary>
        public int GuildWarDailyRefreshTimestamp { get; set; }

        /// <summary>
        /// Guild war weekly refresh timestamp.
        /// Field ID: 17
        /// </summary>
        public int GuildWarWeeklyRefreshTimestamp { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
// TODO boundary:             if (EliteGuilds.Length > MaxEliteGuilds)
// TODO boundary:                 throw new InvalidDataException($"[TlvGuildTimes] EliteGuilds array length ({EliteGuilds.Length}) exceeds maximum of {MaxEliteGuilds}.");

            WriteTlvInt32(buffer, 1, (int)WageTime);
            WriteTlvInt32(buffer, 2, (int)LogTime);
            WriteTlvInt32(buffer, 3, (int)DepotFetchCountTime);
            WriteTlvInt32(buffer, 4, (int)RandCommodityTime);
            WriteTlvInt32(buffer, 6, (int)Daily3Time);
            WriteTlvInt32(buffer, 7, (int)Week3Time);
            WriteTlvByte(buffer, 8, EliteGuildCount);
            WriteTlvInt64Arr(buffer, 9, EliteGuilds);
            WriteTlvInt32(buffer, 10, CommerceCount);
            WriteTlvSubStructureList(buffer, 11, CommerceInfo.Count, CommerceInfo);
            WriteTlvByte(buffer, 12, DragonBoatCount);
            WriteTlvSubStructure(buffer, 13, DragonBoatInfo);
            WriteTlvInt32(buffer, 14, GuildWarHistoryCount);
            WriteTlvSubStructureList(buffer, 15, GuildWarHistory.Count, GuildWarHistory);
            WriteTlvInt32(buffer, 16, GuildWarDailyRefreshTimestamp);
            WriteTlvInt32(buffer, 17, GuildWarWeeklyRefreshTimestamp);
        }
    }
}
