using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for subject count with short array and refresh time.
    /// C++ Reader: crygame.dll+sub_1015CE80 (UnkTlv0096)
    /// C++ Printer: crygame.dll+sub_1015D380
    /// </summary>
    public class TlvSubjectRefresh : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxSubjects = 32;

        /// <summary>
        /// Subject count (derived from Subjects array).
        /// Field ID: 1
        /// </summary>
        public int SubjectCnt => Subject?.Length ?? 0;

        /// <summary>
        /// Subject IDs (short array).
        /// Field ID: 2
        /// </summary>
        public short[] Subject { get; set; }

        /// <summary>
        /// Refresh time.
        /// Field ID: 3
        /// </summary>
        public uint RefreshTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Subject?.Length ?? 0) > MaxSubjects)
                throw new InvalidDataException($"[TlvSubjectRefresh] Subject exceeds the maximum of {MaxSubjects} elements.");

            WriteTlvInt32(buffer, 1, SubjectCnt);
            WriteTlvInt16Arr(buffer, 2, Subject);
            WriteTlvInt32(buffer, 3, (int)RefreshTime);
        }
    }
}
