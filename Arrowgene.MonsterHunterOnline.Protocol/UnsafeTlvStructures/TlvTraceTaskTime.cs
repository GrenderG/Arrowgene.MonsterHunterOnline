using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for trace data with short array and byte array.
    /// C++ Reader: crygame.dll+sub_10222970 (UnkTlv0253)
    /// C++ Printer: crygame.dll+sub_10223100
    /// </summary>
    public class TlvTraceTaskTime : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxElements = 16;

        /// <summary>
        /// Trace count (derived from arrays).
        /// Field ID: 3
        /// </summary>
        public int TraceCount => Task?.Length ?? 0;

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
                throw new InvalidDataException($"[TlvTraceTaskTime] Task exceeds the maximum of {MaxElements} elements.");
            if ((Time?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvTraceTaskTime] Time exceeds the maximum of {MaxElements} bytes.");

            WriteTlvInt32(buffer, 3, TraceCount);
            WriteTlvInt16Arr(buffer, 4, Task);
            WriteTlvByteArr(buffer, 5, Time);
        }
    }
}
