using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for pet avatar data.
    /// C++ Reader: crygame.dll+sub_10200640 (UnkTlv0213)
    /// C++ Printer: crygame.dll+sub_10200DF0
    /// </summary>
    public class TlvPetAvatarData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int AvatarInfoSize = 6;

        /// <summary>
        /// Pet ID.
        /// Field ID: 1
        /// </summary>
        public int PetID { get; set; }

        /// <summary>
        /// Skin ID.
        /// Field ID: 2
        /// </summary>
        public int SkinID { get; set; }

        /// <summary>
        /// Avatar info (int array, exactly 6).
        /// Field ID: 3
        /// </summary>
        public int[] AvatarInfo { get; set; } = new int[AvatarInfoSize];

        /// <summary>
        /// Sex.
        /// Field ID: 4
        /// </summary>
        public byte Sex { get; set; }

        /// <summary>
        /// Slot.
        /// Field ID: 5
        /// </summary>
        public int Slot { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, PetID);
            WriteTlvInt32(buffer, 2, SkinID);
            WriteTlvInt32Arr(buffer, 3, AvatarInfo);
            WriteTlvByte(buffer, 4, Sex);
            WriteTlvInt32(buffer, 5, Slot);
        }
    }
}
