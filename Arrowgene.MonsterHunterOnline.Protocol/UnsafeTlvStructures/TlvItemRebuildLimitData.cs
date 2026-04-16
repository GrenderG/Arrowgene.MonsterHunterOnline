using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for item rebuild limit data.
    /// C++ Reader: crygame.dll+sub_10178850 (UnkTlv0133)
    /// C++ Printer: crygame.dll+sub_10178F00
    /// </summary>
    public class TlvItemRebuildLimitData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxLimits = 8;

        /// <summary>
        /// Item rebuild limit count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int ItemRebuildLimitCount => ItemRebuildLimitInfo?.Count ?? 0;

        /// <summary>
        /// Last item rebuild time.
        /// Field ID: 2
        /// </summary>
        public long LastItemRebuildTime { get; set; }

        /// <summary>
        /// Item rebuild limit info list.
        /// Field ID: 3
        /// </summary>
        public List<TlvLimitCount> ItemRebuildLimitInfo { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((ItemRebuildLimitInfo?.Count ?? 0) > MaxLimits)
                throw new InvalidDataException($"[TlvItemRebuildLimitData] ItemRebuildLimitInfo exceeds the maximum of {MaxLimits} elements.");

            WriteTlvInt32(buffer, 1, ItemRebuildLimitCount);
            WriteTlvInt64(buffer, 2, LastItemRebuildTime);
            WriteTlvSubStructureList(buffer, 3, ItemRebuildLimitInfo.Count, ItemRebuildLimitInfo);
        }
    }
}
