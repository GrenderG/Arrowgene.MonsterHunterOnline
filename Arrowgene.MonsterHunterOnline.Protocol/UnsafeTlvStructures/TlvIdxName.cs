using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for index with name.
    /// C++ Reader: crygame.dll+sub_10128D30 (UnkTlv0034)
    /// C++ Printer: crygame.dll+sub_10128E40
    /// </summary>
    public class TlvIdxName : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 8;

        /// <summary>
        /// Index value.
        /// Field ID: 1
        /// </summary>
        public byte Idx { get; set; }

        /// <summary>
        /// Name string.
        /// Field ID: 2
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvIdxName] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvByte(buffer, 1, Idx);
            WriteTlvString(buffer, 2, Name);
        }
    }
}
