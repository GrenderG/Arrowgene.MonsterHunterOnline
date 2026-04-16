using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for count + int array (30 max).
    /// C++ Reader: crygame.dll+sub_1012A460 (UnkTlv0036)
    /// C++ Printer: crygame.dll+sub_1012A8E0
    /// </summary>
    public class TlvCountCtxsB : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxElements = 30;

        /// <summary>
        /// Element count (derived from Data array).
        /// Field ID: 1
        /// </summary>
        public int Count => Data?.Length ?? 0;

        /// <summary>
        /// Integer data array.
        /// Field ID: 2
        /// </summary>
        public int[] Data { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Data?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvCountCtxsB] Data exceeds the maximum of {MaxElements} elements.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvInt32Arr(buffer, 2, Data);
        }
    }
}
