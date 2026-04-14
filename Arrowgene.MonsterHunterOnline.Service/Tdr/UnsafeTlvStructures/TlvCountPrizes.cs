using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for count + int array (100 max).
    /// C++ Reader: crygame.dll+sub_101756A0 (UnkTlv0128)
    /// C++ Printer: crygame.dll+sub_10175B40
    /// </summary>
    public class TlvCountPrizes : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxElements = 100;

        /// <summary>
        /// Element count (derived from Data array).
        /// Field ID: 1
        /// </summary>
        public int Count => Prizes?.Length ?? 0;

        /// <summary>
        /// Prize values (int array).
        /// Field ID: 2
        /// </summary>
        public int[] Prizes { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Prizes?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvCountPrizes] Prizes exceeds the maximum of {MaxElements} elements.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvInt32Arr(buffer, 2, Prizes);
        }
    }
}
