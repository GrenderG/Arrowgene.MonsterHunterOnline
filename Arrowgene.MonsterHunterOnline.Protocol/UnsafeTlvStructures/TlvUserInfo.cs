using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (User / Character Identity Reference).
    /// C++ Reader: crygame.dll+sub_10118360
    /// C++ Printer: crygame.dll+sub_10118580
    /// </summary>
    public class TlvUserInfo : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxNameLength = 32; // < 0x20

        public string Name { get; set; } = string.Empty;
        public long DbId { get; set; } // Database ID
        public int RtId { get; set; }  // Runtime ID
        public long Uin { get; set; }  // User Identification Number

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECKS ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvUserInfo] Name exceeds or equals the strict maximum of {MaxNameLength} bytes.");

            // --- SERIALIZATION ---
            WriteTlvString(buffer, 1, Name);
            WriteTlvInt64(buffer, 2, DbId);
            WriteTlvInt32(buffer, 3, RtId);
            WriteTlvInt64(buffer, 4, Uin);
        }
    }
}