using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for guide book data.
    /// C++ Reader: crygame.dll+sub_1018F680 (UnkTlv0163)
    /// C++ Printer: crygame.dll+sub_1018FC30
    /// </summary>
    public class TlvGuideBookData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxChapters = 8;

        /// <summary>
        /// Guide book chapter count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int GuideBookChapterCount => GuideBookChapterInfos?.Count ?? 0;

        /// <summary>
        /// Guide book chapter entries.
        /// Field ID: 2
        /// </summary>
        public List<TlvChapterProgress> GuideBookChapterInfos { get; set; }

        /// <summary>
        /// Whether first auto-open guide book.
        /// Field ID: 3
        /// </summary>
        public byte IsFirstAutoOpenGuideBook { get; set; }

        /// <summary>
        /// Weapon ID.
        /// Field ID: 4
        /// </summary>
        public byte WeaponId { get; set; }

        /// <summary>
        /// Guide action infos.
        /// Field ID: 5
        /// </summary>
        public TlvFinishActionData GuideActionInfos { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((GuideBookChapterInfos?.Count ?? 0) > MaxChapters)
                throw new InvalidDataException($"[TlvGuideBookData] GuideBookChapterInfos exceeds the maximum of {MaxChapters} elements.");

            WriteTlvInt32(buffer, 1, GuideBookChapterCount);
            WriteTlvSubStructureList(buffer, 2, GuideBookChapterInfos.Count, GuideBookChapterInfos);
            WriteTlvByte(buffer, 3, IsFirstAutoOpenGuideBook);
            WriteTlvByte(buffer, 4, WeaponId);
            WriteTlvSubStructure(buffer, 5, GuideActionInfos);
        }
    }
}
