using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for database ID with name.
    /// C++ Reader: crygame.dll+sub_1012B510 (UnkTlv0038)
    /// C++ Printer: crygame.dll+sub_1012B6F0
    /// </summary>
    public class TlvDbIdInfo : Structure, ITlvStructure
    {
        /// <summary>
        /// Maximum name length.
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// Database ID.
        /// Field ID: 1
        /// </summary>
        public ulong DbId { get; set; }

        /// <summary>
        /// QQ value.
        /// Field ID: 2
        /// </summary>
        public uint QQ { get; set; }

        /// <summary>
        /// Name string.
        /// Field ID: 3
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
                throw new InvalidDataException($"[TlvDbIdInfo] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

            // --- SERIALIZATION ---
            WriteTlvUInt64(buffer, 1, DbId);
            WriteTlvInt32(buffer, 2, (int)QQ);
            WriteTlvString(buffer, 3, Name);
        }
    }
}
