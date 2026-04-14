using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvPhaseCounter.
    /// C++ Reader: crygame.dll+sub_10153F00 (UnkTlv0084)
    /// </summary>
    public class TlvCounterData : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from CounterData).
        /// Field ID: 1
        /// </summary>
        public int Count => CounterData?.Count ?? 0;

        /// <summary>
        /// List of TlvPhaseCounter.
        /// Field ID: 2
        /// </summary>
        public List<TlvPhaseCounter> CounterData { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, CounterData.Count, CounterData);
        }
    }
}
