using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for complete data with short array and byte array.
    /// C++ Reader: crygame.dll+sub_102204F0 (UnkTlv0249)
    /// C++ Printer: crygame.dll+sub_10220CF0
    /// </summary>
    public class TlvCompleteTaskCount : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxElements = 2048;

        /// <summary>
        /// Complete count (derived from arrays).
        /// Field ID: 1
        /// </summary>
        public int CompleteCount => Task?.Length ?? 0;

        /// <summary>
        /// Task IDs (short array).
        /// Field ID: 2
        /// </summary>
        public short[] Task { get; set; }

        /// <summary>
        /// Count bytes.
        /// Field ID: 3
        /// </summary>
        public byte[] Count { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Task?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvCompleteTaskCount] Task exceeds the maximum of {MaxElements} elements.");
            if ((Count?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvCompleteTaskCount] Count exceeds the maximum of {MaxElements} bytes.");

            WriteTlvInt32(buffer, 1, CompleteCount);
            WriteTlvInt16Arr(buffer, 2, Task);
            WriteTlvByteArr(buffer, 3, Count);
        }
    }
}
