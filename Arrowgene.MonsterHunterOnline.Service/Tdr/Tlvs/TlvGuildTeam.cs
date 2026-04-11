using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guild team information.
    /// C++ Reader: crygame.dll+sub_1012C300 (UnkTlv0039)
    /// C++ Printer: crygame.dll+sub_1012C700
    /// </summary>
    public class TlvGuildTeam : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: GuildId = (ulong)reader.ReadLong(); break;
                    case 2:
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxTeamNameLength)
                        {
                            byte[] nameBytes = reader.ReadBytes(nameLen);
                            TeamName = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
                        }
                        break;
                    case 3: MatchId = reader.ReadInt(); break;
                    case 4: SignUpId = (uint)reader.ReadInt(); break;
                    case 5: SignUpTm = reader.ReadInt(); break;
                    case 6: BestScore = reader.ReadInt(); break;
                    case 7: BestScoreTm = reader.ReadInt(); break;
                    case 8: reader.ReadInt(); break; // Discard count field
                    case 9: Members = ReadTlvList<TlvDbIdInfo>(reader); break;
                    case 10: AcceptRound = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECKS ---
            if (!string.IsNullOrEmpty(TeamName) && Encoding.UTF8.GetByteCount(TeamName) >= MaxTeamNameLength)
                throw new InvalidDataException($"[TlvGuildTeam] TeamName exceeds or equals the maximum of {MaxTeamNameLength} bytes.");
            if (Members.Count > MaxMemberCount)
                throw new InvalidDataException($"[TlvGuildTeam] Members count ({Members.Count}) exceeds maximum of {MaxMemberCount}.");

            // --- SERIALIZATION ---
            WriteTlvULong(writer, 1, GuildId);
            WriteTlvString(writer, 2, TeamName);
            WriteTlvInt(writer, 3, MatchId);
            WriteTlvInt(writer, 4, (int)SignUpId);
            WriteTlvInt(writer, 5, SignUpTm);
            WriteTlvInt(writer, 6, BestScore);
            WriteTlvInt(writer, 7, BestScoreTm);
            WriteTlvInt(writer, 8, Members.Count);
            WriteTlvList(writer, 9, Members);
            WriteTlvByte(writer, 10, AcceptRound);
        }
    }
}
