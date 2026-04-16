using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for prize state with refresh time.
    /// C++ Reader: crygame.dll+sub_10172510 (UnkTlv0124)
    /// C++ Printer: crygame.dll+sub_10172AE0
    /// </summary>
    public class TlvPrizeState : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxPrizes = 20;

        /// <summary>
        /// Refresh time.
        /// Field ID: 2
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Count (derived from arrays).
        /// Field ID: 4
        /// </summary>
        public int Count => PrizeId?.Length ?? 0;

        /// <summary>
        /// Prize IDs (int array).
        /// Field ID: 5
        /// </summary>
        public int[] PrizeId { get; set; }

        /// <summary>
        /// State bytes.
        /// Field ID: 6
        /// </summary>
        public byte[] State { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((PrizeId?.Length ?? 0) > MaxPrizes)
                throw new InvalidDataException($"[TlvPrizeState] PrizeId exceeds the maximum of {MaxPrizes} elements.");
            if ((State?.Length ?? 0) > MaxPrizes)
                throw new InvalidDataException($"[TlvPrizeState] State exceeds the maximum of {MaxPrizes} bytes.");

            WriteTlvInt32(buffer, 2, (int)RefreshTime);
            WriteTlvInt32(buffer, 4, Count);
            WriteTlvInt32Arr(buffer, 5, PrizeId);
            WriteTlvByteArr(buffer, 6, State);
        }
    }
}
