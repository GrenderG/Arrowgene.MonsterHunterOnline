using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for entrust group stat list.
    /// C++ Reader: crygame.dll+sub_1014A1C0 (UnkTlv0073)
    /// C++ Printer: crygame.dll+sub_10149FE0
    /// </summary>
    public class TlvEntrustGroupStatList : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxGroups = 15;

        /// <summary>
        /// Group stat count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int Count => EntrustGroupStatInfo?.Count ?? 0;

        /// <summary>
        /// Group stat entries.
        /// Field ID: 2
        /// </summary>
        public List<TlvGroupEntrustStatData> EntrustGroupStatInfo { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((EntrustGroupStatInfo?.Count ?? 0) > MaxGroups)
                throw new InvalidDataException($"[TlvEntrustGroupStatList] EntrustGroupStatInfo exceeds the maximum of {MaxGroups} elements.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, EntrustGroupStatInfo.Count, EntrustGroupStatInfo);
        }
    }
}
