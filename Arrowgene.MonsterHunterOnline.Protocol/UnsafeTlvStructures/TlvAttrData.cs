using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for attribute block.
    /// C++ Reader: crygame.dll+sub_1013C920 (UnkTlv0055)
    /// C++ Printer: crygame.dll+sub_1013CB70
    /// </summary>
    public class TlvAttrData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxAttrDataLength = 1286;

        /// <summary>
        /// Attribute count (derived from AttrData).
        /// Field ID: 1
        /// </summary>
        public int AttrSize => Attrs?.Length ?? 0;

        /// <summary>
        /// Attribute data bytes.
        /// Field ID: 2
        /// </summary>
        public byte[] Attrs { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Attrs?.Length ?? 0) > MaxAttrDataLength)
                throw new InvalidDataException($"[TlvAttrData] Attrs exceeds the maximum of {MaxAttrDataLength} elements.");

            WriteTlvInt32(buffer, 1, AttrSize);
            WriteTlvByteArr(buffer, 2, Attrs);
        }
    }
}