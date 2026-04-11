using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_10220230
    /// C++ Reader: crygame.dll+sub_102207E0
    /// C++ Printer: crygame.dll+sub_10220AD0
    /// </summary>
    public class TlvTaskComplete : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary from C++ (v8 > 2048) ---
        public const int MaxTasks = 2048;

        public int CompleteCount { get; set; }

        // Tag 0x25: Array of VarShorts (Task IDs)
        public short[] Tasks { get; set; } = new short[0];

        // Tag 0x35: Array of Bytes (Completion Counts per Task)
        public byte[] Counts { get; set; } = new byte[0];

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1:
                        CompleteCount = reader.ReadVarInt();
                        break;
                    case 2:
                        Tasks = ReadTlvVarShortArray(reader);
                        break;
                    case 3:
                        // The C++ writes this as individual bytes inside a length-delimited block
                        int byteLen = reader.ReadInt();
                        Counts = reader.ReadBytes(byteLen);
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECKS ---
            if (CompleteCount > MaxTasks)
                throw new InvalidDataException($"[TlvTaskComplete] CompleteCount ({CompleteCount}) exceeds maximum of {MaxTasks}.");
            if (Tasks.Length > MaxTasks)
                throw new InvalidDataException($"[TlvTaskComplete] Tasks array length ({Tasks.Length}) exceeds maximum of {MaxTasks}.");
            if (Counts.Length > MaxTasks)
                throw new InvalidDataException($"[TlvTaskComplete] Counts array length ({Counts.Length}) exceeds maximum of {MaxTasks}.");

            // --- SERIALIZATION ---
            WriteTlvVarInt(writer, 1, CompleteCount);

            // Use the new protected helpers, passing in CompleteCount!
            WriteTlvVarShortArray(writer, 2, Tasks);
            WriteTlvByteArray(writer, 3, Counts);
        }
    }
}