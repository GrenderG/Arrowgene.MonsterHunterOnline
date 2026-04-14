using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for round score with name.
    /// C++ Reader: crygame.dll+sub_101390F0 (UnkTlv0049)
    /// C++ Printer: crygame.dll+sub_10139290
    /// </summary>
    public class TlvRoundScore : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;

        /// <summary>
        /// Round number.
        /// Field ID: 1
        /// </summary>
        public int Round { get; set; }

        /// <summary>
        /// Score value.
        /// Field ID: 2
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Player name.
        /// Field ID: 3
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvRoundScore] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvInt32(buffer, 1, Round);
            WriteTlvInt32(buffer, 2, Score);
            WriteTlvString(buffer, 3, Name);
        }
    }
}
