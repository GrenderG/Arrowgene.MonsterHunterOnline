using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for counter data list with count.
    /// C++ Reader: crygame.dll+sub_10154870 (inner of UnkTlv0086)
    /// </summary>
    public class TlvCounterDataList : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxCounters = 64;

        /// <summary>
        /// Counter count (derived from list).
        /// Field ID: 1
        /// </summary>
        public byte CounterNum => (byte)(CounterData?.Count ?? 0);

        /// <summary>
        /// Counter data entries.
        /// Field ID: 2
        /// </summary>
        public List<TlvCounterData> CounterData { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((CounterData?.Count ?? 0) > MaxCounters)
                throw new InvalidDataException($"[TlvCounterDataList] CounterData exceeds the maximum of {MaxCounters} elements.");

            WriteTlvByte(buffer, 1, CounterNum);
            WriteTlvSubStructureList(buffer, 2, CounterData.Count, CounterData);
        }
    }
}
