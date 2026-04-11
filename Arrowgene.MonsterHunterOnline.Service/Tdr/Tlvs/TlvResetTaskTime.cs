using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for reset data with short array and byte array.
    /// C++ Reader: crygame.dll+sub_10221760 (UnkTlv0251)
    /// C++ Printer: crygame.dll+sub_10221EF0
    /// </summary>
    public class TlvResetTaskTime : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 3: reader.ReadInt(); break; // resetCount, derived from arrays
                    case 4: Task = ReadTlvShortArray(reader); break;
                    case 5:
                        int byteLen = reader.ReadInt();
                        if (byteLen > 0 && byteLen <= MaxElements)
                            Time = reader.ReadBytes(byteLen);
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Task?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvResetTaskTime] Task exceeds the maximum of {MaxElements} elements.");
            if ((Time?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvResetTaskTime] Time exceeds the maximum of {MaxElements} bytes.");

            WriteTlvInt(writer, 3, ResetCount);
            WriteTlvShortArray(writer, 4, Task);
            WriteTlvByteArray(writer, 5, Time, Time?.Length ?? 0);
        }
    }
}
