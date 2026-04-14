using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for leaderboard entry with score.
    /// C++ Reader: crygame.dll+sub_1013A390 (UnkTlv0051)
    /// C++ Printer: crygame.dll+sub_1013A5D0
    /// </summary>
    public class TlvLeaderboardEntry : Structure, ITlvStructure
    {
        /// <summary>
        /// Maximum name length.
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// Player score.
        /// Field ID: 1
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Database ID.
        /// Field ID: 2
        /// </summary>
        public ulong DbId { get; set; }

        /// <summary>
        /// Player name.
        /// Field ID: 3
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// User identification number.
        /// Field ID: 4
        /// </summary>
        public ulong Uin { get; set; }

        /// <summary>
        /// Time value.
        /// Field ID: 5
        /// </summary>
        public int Time { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvLeaderboardEntry] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

            // --- SERIALIZATION ---
            WriteTlvInt32(buffer, 1, Score);
            WriteTlvUInt64(buffer, 2, DbId);
            WriteTlvString(buffer, 3, Name);
            WriteTlvUInt64(buffer, 4, Uin);
            WriteTlvInt32(buffer, 5, Time);
        }
    }
}
