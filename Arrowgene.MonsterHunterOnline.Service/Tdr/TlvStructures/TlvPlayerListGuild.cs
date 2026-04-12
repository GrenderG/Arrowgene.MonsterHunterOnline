using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for player list with guild info.
    /// C++ Reader: crygame.dll+sub_1011A600 (UnkTlv0016)
    /// C++ Printer: crygame.dll+sub_1011ABA0
    /// </summary>
    public class TlvPlayerListGuild : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxPlayers = 256;

        /// <summary>
        /// Player count (derived from PlayerIds array).
        /// Field ID: 1
        /// </summary>
        public int PlayerCount => PlayerIds?.Length ?? 0;

        /// <summary>
        /// Player IDs (long array).
        /// Field ID: 2
        /// </summary>
        public long[] PlayerIds { get; set; }

        /// <summary>
        /// Own guild ID.
        /// Field ID: 3
        /// </summary>
        public ulong OwnGuildId { get; set; }

        /// <summary>
        /// Minimum time.
        /// Field ID: 4
        /// </summary>
        public uint MinTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((PlayerIds?.Length ?? 0) > MaxPlayers)
                throw new InvalidDataException($"[TlvPlayerListGuild] PlayerIds exceeds the maximum of {MaxPlayers} elements.");

            WriteTlvInt32(buffer, 1, PlayerCount);
            WriteTlvInt64Arr(buffer, 2, PlayerIds);
            WriteTlvInt64(buffer, 3, (long)OwnGuildId);
            WriteTlvInt32(buffer, 4, (int)MinTime);
        }
    }
}
