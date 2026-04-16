using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for guild team information.
    /// C++ Reader: crygame.dll+sub_1012C300 (UnkTlv0039)
    /// C++ Printer: crygame.dll+sub_1012C700
    /// </summary>
    public class TlvGuildTeam : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxTeamNameLength = 40;   // 0x28
        public const int MaxMemberCount = 4;

        /// <summary>
        /// Guild ID.
        /// Field ID: 1
        /// </summary>
        public ulong GuildId { get; set; }

        /// <summary>
        /// Team name.
        /// Field ID: 2
        /// </summary>
        public string TeamName { get; set; } = string.Empty;

        /// <summary>
        /// Match ID.
        /// Field ID: 3
        /// </summary>
        public int MatchId { get; set; }

        /// <summary>
        /// Sign up ID.
        /// Field ID: 4
        /// </summary>
        public uint SignUpId { get; set; }

        /// <summary>
        /// Sign up time.
        /// Field ID: 5
        /// </summary>
        public int SignUpTm { get; set; }

        /// <summary>
        /// Best score achieved.
        /// Field ID: 6
        /// </summary>
        public int BestScore { get; set; }

        /// <summary>
        /// Best score time.
        /// Field ID: 7
        /// </summary>
        public int BestScoreTm { get; set; }

        /// <summary>
        /// Members list.
        /// Field ID: 9
        /// </summary>
        public List<TlvDbIdInfo> Members { get; set; } = new List<TlvDbIdInfo>();

        /// <summary>
        /// Accept round flag.
        /// Field ID: 10
        /// </summary>
        public byte AcceptRound { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECKS ---
            if (!string.IsNullOrEmpty(TeamName) && Encoding.UTF8.GetByteCount(TeamName) >= MaxTeamNameLength)
                throw new InvalidDataException($"[TlvGuildTeam] TeamName exceeds or equals the maximum of {MaxTeamNameLength} bytes.");
// TODO boundary:             if (Members.Count > MaxMemberCount)
// TODO boundary:                 throw new InvalidDataException($"[TlvGuildTeam] Members count ({Members.Count}) exceeds maximum of {MaxMemberCount}.");

            // --- SERIALIZATION ---
            WriteTlvUInt64(buffer, 1, GuildId);
            WriteTlvString(buffer, 2, TeamName);
            WriteTlvInt32(buffer, 3, MatchId);
            WriteTlvInt32(buffer, 4, (int)SignUpId);
            WriteTlvInt32(buffer, 5, SignUpTm);
            WriteTlvInt32(buffer, 6, BestScore);
            WriteTlvInt32(buffer, 7, BestScoreTm);
            WriteTlvInt32(buffer, 8, Members.Count);
            WriteTlvSubStructureList(buffer, 9, Members.Count, Members);
            WriteTlvByte(buffer, 10, AcceptRound);
        }
    }
}
