using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for complete bits and illustrate card info.
    /// C++ Reader: crygame.dll+sub_1018D110 (UnkTlv0159)
    /// C++ Printer: crygame.dll+sub_1018D540
    /// </summary>
    public class TlvCompleteBitCards : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxCompleteBits = 320;
        public const int MaxCards = 1024;

        /// <summary>
        /// Complete bit count (derived from array).
        /// Field ID: 1
        /// </summary>
        public int CompleteBitCount => CompleteBit?.Length ?? 0;

        /// <summary>
        /// Complete bit data (byte array).
        /// Field ID: 2
        /// </summary>
        public byte[] CompleteBit { get; set; }

        /// <summary>
        /// Illustrate card count (derived from array).
        /// Field ID: 3
        /// </summary>
        public int IllustrateCardCount => IllustrateCardInfo?.Length ?? 0;

        /// <summary>
        /// Illustrate card info (int array).
        /// Field ID: 4
        /// </summary>
        public int[] IllustrateCardInfo { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((CompleteBit?.Length ?? 0) > MaxCompleteBits)
                throw new InvalidDataException($"[TlvCompleteBitCards] CompleteBit exceeds the maximum of {MaxCompleteBits} elements.");
            if ((IllustrateCardInfo?.Length ?? 0) > MaxCards)
                throw new InvalidDataException($"[TlvCompleteBitCards] IllustrateCardInfo exceeds the maximum of {MaxCards} elements.");

            WriteTlvInt32(buffer, 1, CompleteBitCount);
            WriteTlvByteArr(buffer, 2, CompleteBit);
            WriteTlvInt32(buffer, 3, IllustrateCardCount);
            WriteTlvInt32Arr(buffer, 4, IllustrateCardInfo);
        }
    }
}
