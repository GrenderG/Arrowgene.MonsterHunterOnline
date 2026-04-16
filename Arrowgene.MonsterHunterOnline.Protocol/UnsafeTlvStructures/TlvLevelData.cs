using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for level data with byte and int arrays.
    /// C++ Reader: crygame.dll+sub_10146FC0 (UnkTlv0070)
    /// C++ Printer: crygame.dll+sub_101474D0
    /// </summary>
    public class TlvLevelData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxDataCount = 10;

        /// <summary>
        /// Level ID.
        /// Field ID: 1
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// Data count (derived from arrays).
        /// Field ID: 2
        /// </summary>
        public byte DataCnt => (byte)(DataType?.Length ?? 0);

        /// <summary>
        /// Data type bytes.
        /// Field ID: 3
        /// </summary>
        public byte[] DataType { get; set; }

        /// <summary>
        /// Data values (int array).
        /// Field ID: 4
        /// </summary>
        public int[] DataVal { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((DataType?.Length ?? 0) > MaxDataCount)
                throw new InvalidDataException($"[TlvLevelData] DataType exceeds the maximum of {MaxDataCount} elements.");
            if ((DataVal?.Length ?? 0) > MaxDataCount)
                throw new InvalidDataException($"[TlvLevelData] DataVal exceeds the maximum of {MaxDataCount} elements.");

            WriteTlvInt32(buffer, 1, LevelId);
            WriteTlvByte(buffer, 2, DataCnt);
            WriteTlvByteArr(buffer, 3, DataType);
            WriteTlvInt32Arr(buffer, 4, DataVal);
        }
    }
}
