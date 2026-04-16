using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for friend brief role info.
    /// C++ Reader: crygame.dll+sub_1024B590 (UnkTlv0275)
    /// C++ Printer: crygame.dll+sub_1024BBE0
    /// </summary>
    public class TlvFriendBriefInfo : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt64(buffer, 1, DBID);
            WriteTlvString(buffer, 2, Name);
            WriteTlvInt32(buffer, 3, NetID);
            WriteTlvInt32(buffer, 4, Level);
            WriteTlvByte(buffer, 5, IsOnline);
            WriteTlvInt32(buffer, 6, LevelID);
            WriteTlvInt32(buffer, 7, LineId);
            WriteTlvString(buffer, 8, Mood);
            WriteTlvString(buffer, 9, GuildName);
            WriteTlvInt32(buffer, 10, FarmPoint);
            WriteTlvInt32(buffer, 11, FarmCanBeGatheredCount);
            WriteTlvInt32(buffer, 12, TeamId);
            WriteTlvInt32(buffer, 13, TeamPwdFlag);
            WriteTlvString(buffer, 14, Star);
            WriteTlvString(buffer, 15, Clan);
            WriteTlvInt32(buffer, 16, HRLevel);
            WriteTlvInt32(buffer, 17, AddTime);
            WriteTlvInt32(buffer, 18, SvrId);
        }
    }
}
