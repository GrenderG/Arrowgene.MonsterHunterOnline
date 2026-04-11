using Rathalos.Core.Utils.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for friend brief role info.
    /// C++ Reader: crygame.dll+sub_1024B590 (UnkTlv0275)
    /// C++ Printer: crygame.dll+sub_1024BBE0
    /// </summary>
    public class TlvFriendBriefInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Field ID: 1</summary>
        public long DBID { get; set; }

        /// <summary>Field ID: 2</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Field ID: 3</summary>
        public int NetID { get; set; }

        /// <summary>Field ID: 4</summary>
        public int Level { get; set; }

        /// <summary>Field ID: 5</summary>
        public byte IsOnline { get; set; }

        /// <summary>Field ID: 6</summary>
        public int LevelID { get; set; }

        /// <summary>Field ID: 7</summary>
        public int LineId { get; set; }

        /// <summary>Field ID: 8</summary>
        public string Mood { get; set; } = string.Empty;

        /// <summary>Field ID: 9</summary>
        public string GuildName { get; set; } = string.Empty;

        /// <summary>Field ID: 10</summary>
        public int FarmPoint { get; set; }

        /// <summary>Field ID: 11</summary>
        public int FarmCanBeGatheredCount { get; set; }

        /// <summary>Field ID: 12</summary>
        public int TeamId { get; set; }

        /// <summary>Field ID: 13</summary>
        public int TeamPwdFlag { get; set; }

        /// <summary>Field ID: 14</summary>
        public string Star { get; set; } = string.Empty;

        /// <summary>Field ID: 15</summary>
        public string Clan { get; set; } = string.Empty;

        /// <summary>Field ID: 16</summary>
        public int HRLevel { get; set; }

        /// <summary>Field ID: 17</summary>
        public int AddTime { get; set; }

        /// <summary>Field ID: 18</summary>
        public int SvrId { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: DBID = reader.ReadLong(); break;
                    case 2: { int len = reader.ReadInt(); Name = len > 0 ? Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 3: NetID = reader.ReadInt(); break;
                    case 4: Level = reader.ReadInt(); break;
                    case 5: IsOnline = reader.ReadByte(); break;
                    case 6: LevelID = reader.ReadInt(); break;
                    case 7: LineId = reader.ReadInt(); break;
                    case 8: { int len = reader.ReadInt(); Mood = len > 0 ? Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 9: { int len = reader.ReadInt(); GuildName = len > 0 ? Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 10: FarmPoint = reader.ReadInt(); break;
                    case 11: FarmCanBeGatheredCount = reader.ReadInt(); break;
                    case 12: TeamId = reader.ReadInt(); break;
                    case 13: TeamPwdFlag = reader.ReadInt(); break;
                    case 14: { int len = reader.ReadInt(); Star = len > 0 ? Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 15: { int len = reader.ReadInt(); Clan = len > 0 ? Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 16: HRLevel = reader.ReadInt(); break;
                    case 17: AddTime = reader.ReadInt(); break;
                    case 18: SvrId = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvLong(writer, 1, DBID);
            WriteTlvString(writer, 2, Name);
            WriteTlvInt(writer, 3, NetID);
            WriteTlvInt(writer, 4, Level);
            WriteTlvByte(writer, 5, IsOnline);
            WriteTlvInt(writer, 6, LevelID);
            WriteTlvInt(writer, 7, LineId);
            WriteTlvString(writer, 8, Mood);
            WriteTlvString(writer, 9, GuildName);
            WriteTlvInt(writer, 10, FarmPoint);
            WriteTlvInt(writer, 11, FarmCanBeGatheredCount);
            WriteTlvInt(writer, 12, TeamId);
            WriteTlvInt(writer, 13, TeamPwdFlag);
            WriteTlvString(writer, 14, Star);
            WriteTlvString(writer, 15, Clan);
            WriteTlvInt(writer, 16, HRLevel);
            WriteTlvInt(writer, 17, AddTime);
            WriteTlvInt(writer, 18, SvrId);
        }
    }
}
