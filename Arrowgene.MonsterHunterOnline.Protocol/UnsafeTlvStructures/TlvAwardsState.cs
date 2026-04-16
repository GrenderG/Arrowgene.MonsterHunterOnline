using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for awards state with refresh time + byte/int arrays.
    /// C++ Reader: crygame.dll+sub_101777A0 (UnkTlv0131)
    /// C++ Printer: crygame.dll+sub_10177E90
    /// </summary>
    public class TlvAwardsState : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxAwards = 5;

        /// <summary>
        /// Refresh time.
        /// Field ID: 1
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Count (derived from arrays).
        /// Field ID: 2
        /// </summary>
        public int Count => AwardsType?.Length ?? 0;

        /// <summary>
        /// Award type bytes.
        /// Field ID: 3
        /// </summary>
        public byte[] AwardsType { get; set; }

        /// <summary>
        /// Award state bytes.
        /// Field ID: 4
        /// </summary>
        public byte[] AwardsState { get; set; }

        /// <summary>
        /// Award ID ints.
        /// Field ID: 5
        /// </summary>
        public int[] AwardsId { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((AwardsType?.Length ?? 0) > MaxAwards)
                throw new InvalidDataException($"[TlvAwardsState] AwardsType exceeds the maximum of {MaxAwards} elements.");
            if ((AwardsState?.Length ?? 0) > MaxAwards)
                throw new InvalidDataException($"[TlvAwardsState] AwardsState exceeds the maximum of {MaxAwards} elements.");
            if ((AwardsId?.Length ?? 0) > MaxAwards)
                throw new InvalidDataException($"[TlvAwardsState] AwardsId exceeds the maximum of {MaxAwards} elements.");

            WriteTlvInt32(buffer, 1, (int)RefreshTime);
            WriteTlvInt32(buffer, 2, Count);
            WriteTlvByteArr(buffer, 3, AwardsType);
            WriteTlvByteArr(buffer, 4, AwardsState);
            WriteTlvInt32Arr(buffer, 5, AwardsId);
        }
    }
}
