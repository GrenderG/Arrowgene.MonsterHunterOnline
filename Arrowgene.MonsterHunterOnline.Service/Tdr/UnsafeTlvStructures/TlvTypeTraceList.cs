using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvTypeTrace.
    /// C++ Reader: crygame.dll+sub_1017D230 (UnkTlv0140)
    /// C++ Printer: crygame.dll+sub_1017DA00
    /// </summary>
    public class TlvTypeTraceList : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxTraces = 50;

        /// <summary>
        /// Count (derived from Data).
        /// Field ID: 1
        /// </summary>
        public int Count => Data?.Count ?? 0;

        /// <summary>
        /// Trace sets.
        /// Field ID: 2
        /// </summary>
        public List<TlvTypeTrace> Data { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Data?.Count ?? 0) > MaxTraces)
                throw new InvalidDataException($"[TlvTypeTraceList] Data exceeds the maximum of {MaxTraces} elements.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Data.Count, Data);
        }
    }
}
