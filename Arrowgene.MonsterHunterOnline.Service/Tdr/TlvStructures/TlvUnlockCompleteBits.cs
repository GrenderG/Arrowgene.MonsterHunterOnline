using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for unlock/complete bits and new finish card list.
    /// C++ Reader: crygame.dll+sub_10200A30 (UnkTlv0240)
    /// C++ Printer: crygame.dll+sub_10201520
    /// </summary>
    public class TlvUnlockCompleteBits : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxBits = 2500;
        public const int MaxNewCards = 10;

        /// <summary>
        /// Unlock bit count (derived from array).
        /// Field ID: 1
        /// </summary>
        public int UnlockBitCount => UnlockBit?.Length ?? 0;

        /// <summary>
        /// Unlock bit data (byte array).
        /// Field ID: 2
        /// </summary>
        public byte[] UnlockBit { get; set; }

        /// <summary>
        /// Complete bit count (derived from array).
        /// Field ID: 3
        /// </summary>
        public int CompleteBitCount => CompleteBit?.Length ?? 0;

        /// <summary>
        /// Complete bit data (byte array).
        /// Field ID: 4
        /// </summary>
        public byte[] CompleteBit { get; set; }

        /// <summary>
        /// New finish card count (derived from array).
        /// Field ID: 5
        /// </summary>
        public short NewFinishCardNum => (short)(NewFinishCardList?.Length ?? 0);

        /// <summary>
        /// New finish card IDs (int array).
        /// Field ID: 6
        /// </summary>
        public int[] NewFinishCardList { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((UnlockBit?.Length ?? 0) > MaxBits)
                throw new InvalidDataException($"[TlvUnlockCompleteBits] UnlockBit exceeds the maximum of {MaxBits} elements.");
            if ((CompleteBit?.Length ?? 0) > MaxBits)
                throw new InvalidDataException($"[TlvUnlockCompleteBits] CompleteBit exceeds the maximum of {MaxBits} elements.");
            if ((NewFinishCardList?.Length ?? 0) > MaxNewCards)
                throw new InvalidDataException($"[TlvUnlockCompleteBits] NewFinishCardList exceeds the maximum of {MaxNewCards} elements.");

            WriteTlvInt32(buffer, 1, UnlockBitCount);
            WriteTlvByteArr(buffer, 2, UnlockBit);
            WriteTlvInt32(buffer, 3, CompleteBitCount);
            WriteTlvByteArr(buffer, 4, CompleteBit);
            WriteTlvInt16(buffer, 5, NewFinishCardNum);
            WriteTlvInt32Arr(buffer, 6, NewFinishCardList);
        }
    }
}
