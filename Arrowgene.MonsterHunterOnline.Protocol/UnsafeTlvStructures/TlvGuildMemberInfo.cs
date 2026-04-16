using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for guild member info.
    /// C++ Reader: crygame.dll+sub_10123180 (UnkTlv0026)
    /// C++ Printer: crygame.dll+sub_10123940
    /// </summary>
    public class TlvGuildMemberInfo : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxHunterStarLen = 128;
        public const int MaxNoteLen = 256;

        /// <summary>
        /// Member ID.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Role info.
        /// Field ID: 2
        /// </summary>
        public TlvUserInfo Role { get; set; } = new();

        /// <summary>
        /// Level.
        /// Field ID: 3
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Hunter star string.
        /// Field ID: 4
        /// </summary>
        public string HunterStar { get; set; } = string.Empty;

        /// <summary>
        /// Note string.
        /// Field ID: 5
        /// </summary>
        public string Note { get; set; } = string.Empty;

        /// <summary>
        /// Time.
        /// Field ID: 6
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// HR level.
        /// Field ID: 7
        /// </summary>
        public int HRLevel { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvSubStructure(buffer, 2, Role);
            WriteTlvInt32(buffer, 3, Level);
            WriteTlvString(buffer, 4, HunterStar);
            WriteTlvString(buffer, 5, Note);
            WriteTlvInt32(buffer, 6, Time);
            WriteTlvInt32(buffer, 7, HRLevel);
        }
    }
}
