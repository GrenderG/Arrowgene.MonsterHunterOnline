using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for complete bits.
    /// C++ Reader: crygame.dll+sub_10192CF0 (UnkTlv0158)
    /// C++ Printer: crygame.dll+sub_10192F70
    /// </summary>
    public class TlvCompleteBit : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxBitDataLength = 128;

        /// <summary>
        /// Complete bit count (derived from BitData).
        /// Field ID: 1
        /// </summary>
        public int CompleteBitCount => CompleteBit?.Length ?? 0;

        /// <summary>
        /// Complete bit data bytes.
        /// Field ID: 2
        /// </summary>
        public byte[] CompleteBit { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // completeBitCount, derived from array
                    case 2:
                        int byteLen = reader.ReadInt();
                        if (byteLen > 0 && byteLen <= MaxBitDataLength)
                            CompleteBit = reader.ReadBytes(byteLen);
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((CompleteBit?.Length ?? 0) > MaxBitDataLength)
                throw new InvalidDataException($"[TlvCompleteBit] CompleteBit exceeds maximum length of {MaxBitDataLength} bytes.");

            WriteTlvInt(writer, 1, CompleteBitCount);
            WriteTlvByteArray(writer, 2, CompleteBit, CompleteBitCount);
        }
    }
}
