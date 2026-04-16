using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for reset data with short array and byte array.
    /// C++ Reader: crygame.dll+sub_10221760 (UnkTlv0251)
    /// C++ Printer: crygame.dll+sub_10221EF0
    /// </summary>
    public class TlvResetTaskTime : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxElements = 64;

        /// <summary>
        /// Reset count (derived from arrays).
        /// Field ID: 3
        /// </summary>
        public int ResetCount => Task?.Length ?? 0;

        /// <summary>
        /// Task IDs (short array).
        /// Field ID: 4
        /// </summary>
        public short[] Task { get; set; }

        /// <summary>
        /// Time bytes.
        /// Field ID: 5
        /// </summary>
        public byte[] Time { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Task?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvResetTaskTime] Task exceeds the maximum of {MaxElements} elements.");
            if ((Time?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvResetTaskTime] Time exceeds the maximum of {MaxElements} bytes.");

            WriteTlvInt32(buffer, 3, ResetCount);
            WriteTlvInt16Arr(buffer, 4, Task);
            WriteTlvByteArr(buffer, 5, Time);
        }
    }
}
