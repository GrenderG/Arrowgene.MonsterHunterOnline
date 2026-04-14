using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for rate and credit/money history.
    /// C++ Reader: crygame.dll+sub_1023B630 (UnkTlv0285)
    /// C++ Printer: crygame.dll+sub_1023BCC0
    /// </summary>
    public class TlvRateHistory : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxHistory = 10;

        /// <summary>
        /// Rate.
        /// Field ID: 1
        /// </summary>
        public short Rate { get; set; }

        /// <summary>
        /// History count (derived from History).
        /// Field ID: 2
        /// </summary>
        public short HistoryCount => (short)(History?.Count ?? 0);

        /// <summary>
        /// History entries.
        /// Field ID: 3
        /// </summary>
        public List<TlvCreditMoneyTime> History { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((History?.Count ?? 0) > MaxHistory)
                throw new InvalidDataException($"[TlvRateHistory] History exceeds the maximum of {MaxHistory} elements.");

            WriteTlvInt16(buffer, 1, Rate);
            WriteTlvInt16(buffer, 2, HistoryCount);
            WriteTlvSubStructureList(buffer, 3, History.Count, History);
        }
    }
}
