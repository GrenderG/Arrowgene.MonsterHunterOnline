using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for activity data list.
    /// C++ Reader: crygame.dll+sub_10156350 (UnkTlv0087)
    /// C++ Printer: crygame.dll+sub_10156960
    /// </summary>
    public class TlvActivityDataList : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxActivities = 56;

        /// <summary>
        /// Activity count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int Count => Data?.Count ?? 0;

        /// <summary>
        /// Activity data entries.
        /// Field ID: 2
        /// </summary>
        public List<TlvActivityData> Data { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Data?.Count ?? 0) > MaxActivities)
                throw new InvalidDataException($"[TlvActivityDataList] Data exceeds the maximum of {MaxActivities} elements.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Data.Count, Data);
        }
    }
}
