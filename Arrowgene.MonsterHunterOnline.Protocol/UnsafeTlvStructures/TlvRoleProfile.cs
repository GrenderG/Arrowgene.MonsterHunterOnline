using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (Character / Role Profile Summary).
    /// C++ Reader: crygame.dll+sub_10119210
    /// C++ Printer: crygame.dll+sub_101195F0
    /// </summary>
    public class TlvRoleProfile : Structure, ITlvStructure
    {
        // --- Hardcoded String Boundaries ---
        public const int MaxHunterStarLen = 128; // < 0x80
        public const int MaxNameLen = 32;        // < 0x20
        public const int MaxNoteLen = 256;       // < 0x100

        public TlvUserInfo Role { get; set; } = new TlvUserInfo();
        public int Level { get; set; }
        public string HunterStar { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Icon { get; set; }
        public string Note { get; set; } = string.Empty;
        public int Gold { get; set; }
        public int BindGold { get; set; }
        public int HRLevel { get; set; } // Hunter Rank

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECKS ---
            if (!string.IsNullOrEmpty(HunterStar) && Encoding.UTF8.GetByteCount(HunterStar) >= MaxHunterStarLen)
                throw new InvalidDataException($"[TlvRoleProfile] HunterStar exceeds maximum of {MaxHunterStarLen} bytes.");
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLen)
                throw new InvalidDataException($"[TlvRoleProfile] Name exceeds maximum of {MaxNameLen} bytes.");
            if (!string.IsNullOrEmpty(Note) && Encoding.UTF8.GetByteCount(Note) >= MaxNoteLen)
                throw new InvalidDataException($"[TlvRoleProfile] Note exceeds maximum of {MaxNoteLen} bytes.");

            // --- SERIALIZATION ---

            if (Role != null)
            {
                WriteTlvSubStructure(buffer, 1, Role);
            }

            WriteTlvInt32(buffer, 2, Level);
            WriteTlvString(buffer, 3, HunterStar);
            WriteTlvString(buffer, 4, Name);
            WriteTlvInt32(buffer, 5, Icon);
            WriteTlvString(buffer, 6, Note);
            WriteTlvInt32(buffer, 7, Gold);
            WriteTlvInt32(buffer, 8, BindGold);
            WriteTlvInt32(buffer, 9, HRLevel);
        }
    }
}