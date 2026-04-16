using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for count + int array (10 max).
    /// C++ Reader: crygame.dll+sub_1021C4D0 (UnkTlv0244)
    /// C++ Printer: crygame.dll+sub_1021C920
    /// </summary>
    public class TlvCountCards : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxElements = 10;

        /// <summary>
        /// Element count (derived from Data array).
        /// Field ID: 1
        /// </summary>
        public int Count => Cards?.Length ?? 0;

        /// <summary>
        /// Card values (int array).
        /// Field ID: 2
        /// </summary>
        public int[] Cards { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Cards?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvCountCards] Cards exceeds the maximum of {MaxElements} elements.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvInt32Arr(buffer, 2, Cards);
        }
    }
}
