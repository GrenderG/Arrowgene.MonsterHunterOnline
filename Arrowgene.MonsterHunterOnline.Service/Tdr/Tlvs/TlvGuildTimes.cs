using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for various time trackers.
    /// C++ Reader: crygame.dll+sub_1011C900 (UnkTlv0018)
    /// C++ Printer: crygame.dll+sub_1011C990
    /// </summary>
    public class TlvGuildTimes : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: WageTime = (uint)reader.ReadInt(); break;
                    case 2: LogTime = (uint)reader.ReadInt(); break;
                    case 3: DepotFetchCountTime = (uint)reader.ReadInt(); break;
                    case 4: RandCommodityTime = (uint)reader.ReadInt(); break;
                    case 6: Daily3Time = (uint)reader.ReadInt(); break;
                    case 7: Week3Time = (uint)reader.ReadInt(); break;
                    case 8: EliteGuildCount = reader.ReadByte(); break;
                    case 9: EliteGuilds = ReadTlvLongArray(reader); break;
                    case 10: CommerceCount = reader.ReadInt(); break;
                    case 11: CommerceInfo = ReadTlvList<TlvCommerceInfo>(reader); break;
                    case 12: DragonBoatCount = reader.ReadByte(); break;
                    case 13: DragonBoatInfo = ReadTlvObject<TlvCommerceBoat>(reader); break;
                    case 14: reader.ReadInt(); break; // guildWarHistoryCount - derived
                    case 15: GuildWarHistory = ReadTlvList<TlvGuildWarHistory>(reader); break;
                    case 16: GuildWarDailyRefreshTimestamp = reader.ReadInt(); break;
                    case 17: GuildWarWeeklyRefreshTimestamp = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if (EliteGuilds.Length > MaxEliteGuilds)
                throw new InvalidDataException($"[TlvGuildTimes] EliteGuilds array length ({EliteGuilds.Length}) exceeds maximum of {MaxEliteGuilds}.");

            WriteTlvInt(writer, 1, (int)WageTime);
            WriteTlvInt(writer, 2, (int)LogTime);
            WriteTlvInt(writer, 3, (int)DepotFetchCountTime);
            WriteTlvInt(writer, 4, (int)RandCommodityTime);
            WriteTlvInt(writer, 6, (int)Daily3Time);
            WriteTlvInt(writer, 7, (int)Week3Time);
            WriteTlvByte(writer, 8, EliteGuildCount);
            WriteTlvLongArray(writer, 9, EliteGuilds);
            WriteTlvInt(writer, 10, CommerceCount);
            WriteTlvList(writer, 11, CommerceInfo);
            WriteTlvByte(writer, 12, DragonBoatCount);
            WriteTlvObject(writer, 13, DragonBoatInfo);
            WriteTlvInt(writer, 14, GuildWarHistoryCount);
            WriteTlvList(writer, 15, GuildWarHistory);
            WriteTlvInt(writer, 16, GuildWarDailyRefreshTimestamp);
            WriteTlvInt(writer, 17, GuildWarWeeklyRefreshTimestamp);
        }
    }
}
