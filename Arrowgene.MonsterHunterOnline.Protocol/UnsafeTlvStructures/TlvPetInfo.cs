using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for pet info.
    /// C++ Reader: crygame.dll+sub_1019F9B0 (UnkTlv0177)
    /// C++ Printer: crygame.dll+sub_1019FE20
    /// </summary>
    public class TlvPetInfo : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;

        /// <summary>
        /// Pet ID.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Quality.
        /// Field ID: 2
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// Character type.
        /// Field ID: 3
        /// </summary>
        public int Character { get; set; }

        /// <summary>
        /// Attack target.
        /// Field ID: 4
        /// </summary>
        public int AtkTarget { get; set; }

        /// <summary>
        /// Attack mode.
        /// Field ID: 5
        /// </summary>
        public int AtkMode { get; set; }

        /// <summary>
        /// Pet name.
        /// Field ID: 6
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Skin ID.
        /// Field ID: 7
        /// </summary>
        public int Skin { get; set; }

        /// <summary>
        /// Support skill.
        /// Field ID: 8
        /// </summary>
        public int SupportSkill { get; set; }

        /// <summary>
        /// Random type.
        /// Field ID: 9
        /// </summary>
        public int RandType { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvPetInfo] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvInt32(buffer, 1, Id);
            WriteTlvInt32(buffer, 2, Quality);
            WriteTlvInt32(buffer, 3, Character);
            WriteTlvInt32(buffer, 4, AtkTarget);
            WriteTlvInt32(buffer, 5, AtkMode);
            WriteTlvString(buffer, 6, Name);
            WriteTlvInt32(buffer, 7, Skin);
            WriteTlvInt32(buffer, 8, SupportSkill);
            WriteTlvInt32(buffer, 9, RandType);
        }
    }
}
