using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guide book data.
    /// C++ Reader: crygame.dll+sub_1018F680 (UnkTlv0163)
    /// C++ Printer: crygame.dll+sub_1018FC30
    /// </summary>
    public class TlvGuideBookData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // count, derived
                    case 2: GuideBookChapterInfos = ReadTlvList<TlvChapterProgress>(reader); break;
                    case 3: IsFirstAutoOpenGuideBook = reader.ReadByte(); break;
                    case 4: WeaponId = reader.ReadByte(); break;
                    case 5: GuideActionInfos = ReadTlvObject<TlvFinishActionData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((GuideBookChapterInfos?.Count ?? 0) > MaxChapters)
                throw new InvalidDataException($"[TlvGuideBookData] GuideBookChapterInfos exceeds the maximum of {MaxChapters} elements.");

            WriteTlvInt(writer, 1, GuideBookChapterCount);
            WriteTlvList(writer, 2, GuideBookChapterInfos);
            WriteTlvByte(writer, 3, IsFirstAutoOpenGuideBook);
            WriteTlvByte(writer, 4, WeaponId);
            WriteTlvObject(writer, 5, GuideActionInfos);
        }
    }
}
