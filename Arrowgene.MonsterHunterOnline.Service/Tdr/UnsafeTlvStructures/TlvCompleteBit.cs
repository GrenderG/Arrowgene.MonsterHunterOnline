using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for complete bits.
    /// C++ Reader: crygame.dll+sub_10192CF0 (UnkTlv0158)
    /// C++ Printer: crygame.dll+sub_10192F70
    /// </summary>
    public class TlvCompleteBit : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((CompleteBit?.Length ?? 0) > MaxBitDataLength)
                throw new InvalidDataException($"[TlvCompleteBit] CompleteBit exceeds maximum length of {MaxBitDataLength} bytes.");

            WriteTlvInt32(buffer, 1, CompleteBitCount);
            WriteTlvByteArr(buffer, 2, CompleteBit);
        }
    }
}
