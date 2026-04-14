using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for combined stat data with int-value and short-value stat lists.
    /// C++ Reader: crygame.dll+sub_1021B540 (UnkTlv0243)
    /// C++ Printer: crygame.dll+sub_1021BF40
    /// </summary>
    public class TlvStatData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxStatInt = 200;
        public const int MaxStat = 3700;

        /// <summary>
        /// Int-value stat count (derived from list).
        /// Field ID: 1
        /// </summary>
        public short StatNumInt => (short)(StatListInt?.Count ?? 0);

        /// <summary>
        /// Int-value stat list.
        /// Field ID: 2
        /// </summary>
        public List<TlvStatIdxValue> StatListInt { get; set; }

        /// <summary>
        /// Short-value stat count (derived from list).
        /// Field ID: 3
        /// </summary>
        public short StatNum => (short)(StatList?.Count ?? 0);

        /// <summary>
        /// Short-value stat list.
        /// Field ID: 4
        /// </summary>
        public List<TlvStatIdxPair> StatList { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((StatListInt?.Count ?? 0) > MaxStatInt)
                throw new InvalidDataException($"[TlvStatData] StatListInt exceeds the maximum of {MaxStatInt} elements.");
            if ((StatList?.Count ?? 0) > MaxStat)
                throw new InvalidDataException($"[TlvStatData] StatList exceeds the maximum of {MaxStat} elements.");

            WriteTlvInt16(buffer, 1, StatNumInt);
            WriteTlvSubStructureList(buffer, 2, StatListInt.Count, StatListInt);
            WriteTlvInt16(buffer, 3, StatNum);
            WriteTlvSubStructureList(buffer, 4, StatList.Count, StatList);
        }
    }
}
