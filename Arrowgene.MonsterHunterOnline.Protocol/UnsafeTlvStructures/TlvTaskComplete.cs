using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_10220230
    /// C++ Reader: crygame.dll+sub_102207E0
    /// C++ Printer: crygame.dll+sub_10220AD0
    /// </summary>
    public class TlvTaskComplete : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary from C++ (v8 > 2048) ---
        public const int MaxTasks = 2048;

        public int CompleteCount { get; set; }

        // Tag 0x25: Array of VarShorts (Task IDs)
        public short[] Tasks { get; set; } = new short[0];

        // Tag 0x35: Array of Bytes (Completion Counts per Task)
        public byte[] Counts { get; set; } = new byte[0];

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECKS ---
// TODO boundary:             if (CompleteCount > MaxTasks)
// TODO boundary:                 throw new InvalidDataException($"[TlvTaskComplete] CompleteCount ({CompleteCount}) exceeds maximum of {MaxTasks}.");
// TODO boundary:             if (Tasks.Length > MaxTasks)
// TODO boundary:                 throw new InvalidDataException($"[TlvTaskComplete] Tasks array length ({Tasks.Length}) exceeds maximum of {MaxTasks}.");
// TODO boundary:             if (Counts.Length > MaxTasks)
// TODO boundary:                 throw new InvalidDataException($"[TlvTaskComplete] Counts array length ({Counts.Length}) exceeds maximum of {MaxTasks}.");

            // --- SERIALIZATION ---
            WriteTlvVarInt32(buffer, 1, CompleteCount);

            // Use the new protected helpers, passing in CompleteCount!
            WriteTlvVarInt16Arr(buffer, 2, Tasks);
            WriteTlvByteArr(buffer, 3, Counts);
        }
    }
}