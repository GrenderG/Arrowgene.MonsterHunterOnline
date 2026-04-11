using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for complete data with short array and byte array.
    /// C++ Reader: crygame.dll+sub_102204F0 (UnkTlv0249)
    /// C++ Printer: crygame.dll+sub_10220CF0
    /// </summary>
    public class TlvCompleteTaskCount : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // completeCount, derived from arrays
                    case 2: Task = ReadTlvShortArray(reader); break;
                    case 3:
                        int byteLen = reader.ReadInt();
                        if (byteLen > 0 && byteLen <= MaxElements)
                            Count = reader.ReadBytes(byteLen);
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Task?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvCompleteTaskCount] Task exceeds the maximum of {MaxElements} elements.");
            if ((Count?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvCompleteTaskCount] Count exceeds the maximum of {MaxElements} bytes.");

            WriteTlvInt(writer, 1, CompleteCount);
            WriteTlvShortArray(writer, 2, Task);
            WriteTlvByteArray(writer, 3, Count, Count?.Length ?? 0);
        }
    }
}
