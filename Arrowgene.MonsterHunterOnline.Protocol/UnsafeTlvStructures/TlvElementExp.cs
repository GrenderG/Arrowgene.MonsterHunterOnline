using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for element experience.
    /// C++ Reader: crygame.dll+sub_101856F0 (UnkTlv0151)
    /// C++ Printer: crygame.dll+sub_10185920
    /// </summary>
    public class TlvElementExp : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxSlots = 8;

        /// <summary>
        /// Water experience.
        /// Field ID: 1
        /// </summary>
        public int WaterExp { get; set; }

        /// <summary>
        /// Fire experience.
        /// Field ID: 2
        /// </summary>
        public int FireExp { get; set; }

        /// <summary>
        /// Thunder experience.
        /// Field ID: 3
        /// </summary>
        public int ThunderExp { get; set; }

        /// <summary>
        /// Dragon experience.
        /// Field ID: 4
        /// </summary>
        public int DragonExp { get; set; }

        /// <summary>
        /// Ice experience.
        /// Field ID: 5
        /// </summary>
        public int IceExp { get; set; }

        /// <summary>
        /// Duration value.
        /// Field ID: 6
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Slots (element slot list, max 8).
        /// Field ID: 7
        /// </summary>
        public List<TlvElementSlot> Slots { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, WaterExp);
            WriteTlvInt32(buffer, 2, FireExp);
            WriteTlvInt32(buffer, 3, ThunderExp);
            WriteTlvInt32(buffer, 4, DragonExp);
            WriteTlvInt32(buffer, 5, IceExp);
            WriteTlvInt32(buffer, 6, Duration);
            WriteTlvSubStructureList(buffer, 7, Slots.Count, Slots);
        }
    }
}
