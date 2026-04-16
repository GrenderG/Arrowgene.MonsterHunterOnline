using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for cat cuisine data.
    /// C++ Reader: crygame.dll+sub_1017DF80 (UnkTlv0141)
    /// C++ Printer: crygame.dll+sub_1017E490
    /// </summary>
    public class TlvCatCuisineData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxOpenSlots = 7;

        /// <summary>
        /// Cat time.
        /// Field ID: 1
        /// </summary>
        public long CatTime { get; set; }

        /// <summary>
        /// Open slots data (byte array).
        /// Field ID: 2
        /// </summary>
        public byte[] OpenData { get; set; }

        /// <summary>
        /// Open flag.
        /// Field ID: 3
        /// </summary>
        public byte IsOpen { get; set; }

        /// <summary>
        /// Open time.
        /// Field ID: 4
        /// </summary>
        public long OpenTime { get; set; }

        /// <summary>
        /// Tools flag.
        /// Field ID: 5
        /// </summary>
        public byte Tools { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((OpenData?.Length ?? 0) > MaxOpenSlots)
                throw new InvalidDataException($"[TlvCatCuisineData] OpenData exceeds the maximum of {MaxOpenSlots} elements.");

            WriteTlvInt64(buffer, 1, CatTime);
            WriteTlvByteArr(buffer, 2, OpenData);
            WriteTlvByte(buffer, 3, IsOpen);
            WriteTlvInt64(buffer, 4, OpenTime);
            WriteTlvByte(buffer, 5, Tools);
        }
    }
}
