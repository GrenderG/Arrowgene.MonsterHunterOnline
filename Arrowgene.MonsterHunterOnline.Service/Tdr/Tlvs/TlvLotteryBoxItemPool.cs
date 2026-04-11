using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for lottery box item pool data.
    /// C++ Reader: crygame.dll+sub_10191850 (UnkTlv0167)
    /// C++ Printer: crygame.dll+sub_10192230
    /// </summary>
    public class TlvLotteryBoxItemPool : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxItems = 8;

        /// <summary>
        /// Re-search count.
        /// Field ID: 1
        /// </summary>
        public int ReSearchCount { get; set; }

        /// <summary>
        /// Refresh count.
        /// Field ID: 2
        /// </summary>
        public int RefreshCount { get; set; }

        /// <summary>
        /// VIP refresh count.
        /// Field ID: 3
        /// </summary>
        public int VipRefreshCount { get; set; }

        /// <summary>
        /// Item pool list.
        /// Field ID: 4
        /// </summary>
        public List<TlvPositionItemQuality> ItemPoolList { get; set; }

        /// <summary>
        /// Box ID.
        /// Field ID: 5
        /// </summary>
        public int BoxId { get; set; }

        /// <summary>
        /// Last clock refresh time.
        /// Field ID: 6
        /// </summary>
        public int LastClockRefreshTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: ReSearchCount = reader.ReadInt(); break;
                    case 2: RefreshCount = reader.ReadInt(); break;
                    case 3: VipRefreshCount = reader.ReadInt(); break;
                    case 4: ItemPoolList = ReadTlvList<TlvPositionItemQuality>(reader); break;
                    case 5: BoxId = reader.ReadInt(); break;
                    case 6: LastClockRefreshTime = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((ItemPoolList?.Count ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvLotteryBoxItemPool] ItemPoolList exceeds the maximum of {MaxItems} elements.");

            WriteTlvInt(writer, 1, ReSearchCount);
            WriteTlvInt(writer, 2, RefreshCount);
            WriteTlvInt(writer, 3, VipRefreshCount);
            WriteTlvList(writer, 4, ItemPoolList);
            WriteTlvInt(writer, 5, BoxId);
            WriteTlvInt(writer, 6, LastClockRefreshTime);
        }
    }
}
