using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for count + int array (200 max).
    /// C++ Reader: crygame.dll+sub_10174E00 (UnkTlv0127)
    /// C++ Printer: crygame.dll+sub_10175340
    /// </summary>
    public class TlvCountCtxs : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxElements = 200;

        /// <summary>
        /// Element count (derived from Data array).
        /// Field ID: 1
        /// </summary>
        public int Count => Ctxs?.Length ?? 0;

        /// <summary>
        /// Context values (int array).
        /// Field ID: 2
        /// </summary>
        public int[] Ctxs { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Ctxs?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvCountCtxs] Ctxs exceeds the maximum of {MaxElements} elements.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvInt32Arr(buffer, 2, Ctxs);
        }
    }
}
