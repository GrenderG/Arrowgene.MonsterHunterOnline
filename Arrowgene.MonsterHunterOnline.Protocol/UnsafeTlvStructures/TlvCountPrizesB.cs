using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for prize count + int array (3 max).
    /// C++ Reader: crygame.dll+sub_1012ACA0 (UnkTlv0037)
    /// C++ Printer: crygame.dll+sub_1012B0C0
    /// </summary>
    public class TlvCountPrizesB : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxElements = 3;

        /// <summary>
        /// Prize count (derived from Data array).
        /// Field ID: 1
        /// </summary>
        public int PrizeCount => Data?.Length ?? 0;

        /// <summary>
        /// Prize data array.
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
                throw new InvalidDataException($"[TlvCountPrizesB] Data exceeds the maximum of {MaxElements} elements.");

            WriteTlvInt32(buffer, 1, PrizeCount);
            WriteTlvInt32Arr(buffer, 2, Data);
        }
    }
}
