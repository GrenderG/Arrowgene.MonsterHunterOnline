using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guild contribution data.
    /// C++ Reader: crygame.dll+sub_1016FB80 (UnkTlv0120)
    /// C++ Printer: crygame.dll+sub_10170270
    /// </summary>
    public class TlvGuildContributionData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Guild = reader.ReadLong(); break;
                    case 2: Contribution = reader.ReadInt(); break;
                    case 3: ContributionAcc = reader.ReadLong(); break;
                    case 4: ContributionWeekAcc = reader.ReadLong(); break;
                    case 5: LastTime = reader.ReadInt(); break;
                    case 6: RefreshTimestamp = reader.ReadInt(); break;
                    case 7: StartBoatTimes = reader.ReadInt(); break;
                    case 8: BuyStartBoatTimes = reader.ReadByte(); break;
                    case 9: reader.ReadInt(); break;
                    case 10: GuildwarGrabPlayerId = ReadTlvList<TlvGuildTimestamp>(reader); break;
                    case 11: GuildwarGrabPlayerTimeStamp = reader.ReadLong(); break;
                    case 12: OtherGuildNews = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((GuildwarGrabPlayerId?.Count ?? 0) > MaxGuilds)
                throw new InvalidDataException($"[TlvGuildContributionData] GuildwarGrabPlayerId exceeds {MaxGuilds}.");

            WriteTlvLong(writer, 1, Guild);
            WriteTlvInt(writer, 2, Contribution);
            WriteTlvLong(writer, 3, ContributionAcc);
            WriteTlvLong(writer, 4, ContributionWeekAcc);
            WriteTlvInt(writer, 5, LastTime);
            WriteTlvInt(writer, 6, RefreshTimestamp);
            WriteTlvInt(writer, 7, StartBoatTimes);
            WriteTlvByte(writer, 8, BuyStartBoatTimes);
            WriteTlvInt(writer, 9, GuildCount);
            WriteTlvList(writer, 10, GuildwarGrabPlayerId);
            WriteTlvLong(writer, 11, GuildwarGrabPlayerTimeStamp);
            WriteTlvInt(writer, 12, OtherGuildNews);
        }
    }
}
