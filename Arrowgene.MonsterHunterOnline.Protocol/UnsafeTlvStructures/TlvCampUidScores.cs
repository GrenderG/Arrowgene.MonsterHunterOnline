using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for camp with long array + int array.
    /// C++ Reader: crygame.dll+sub_102496C0 (UnkTlv0295)
    /// C++ Printer: crygame.dll+sub_10249E50
    /// </summary>
    public class TlvCampUidScores : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxArrayElements = 8000;

        /// <summary>
        /// Count (derived from arrays).
        /// Field ID: 1
        /// </summary>
        public int Count => Uids?.Length ?? 0;

        /// <summary>
        /// Camp.
        /// Field ID: 2
        /// </summary>
        public int Camp { get; set; }

        /// <summary>
        /// UIDs (long array).
        /// Field ID: 3
        /// </summary>
        public long[] Uids { get; set; }

        /// <summary>
        /// Scores (int array).
        /// Field ID: 4
        /// </summary>
        public int[] Scores { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Uids?.Length ?? 0) > MaxArrayElements)
                throw new InvalidDataException($"[TlvCampUidScores] Uids exceeds the maximum of {MaxArrayElements} elements.");
            if ((Scores?.Length ?? 0) > MaxArrayElements)
                throw new InvalidDataException($"[TlvCampUidScores] Scores exceeds the maximum of {MaxArrayElements} elements.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvInt32(buffer, 2, Camp);
            WriteTlvInt64Arr(buffer, 3, Uids);
            WriteTlvInt32Arr(buffer, 4, Scores);
        }
    }
}
