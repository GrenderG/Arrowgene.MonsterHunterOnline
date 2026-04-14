using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for star level data with stats.
    /// C++ Reader: crygame.dll+sub_10215960 (inner of UnkTlv0171)
    /// C++ Printer: crygame.dll+sub_10215C50
    /// </summary>
    public class TlvStarLevelData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxStars = 20;
        public const int MaxStats = 10;

        /// <summary>
        /// Star count (derived from array).
        /// Field ID: 1
        /// </summary>
        public byte StarNum => (byte)(StarList?.Length ?? 0);

        /// <summary>
        /// Star list (byte array).
        /// Field ID: 2
        /// </summary>
        public byte[] StarList { get; set; }

        /// <summary>
        /// Stat count (derived from list).
        /// Field ID: 3
        /// </summary>
        public byte StatNum => (byte)(StatList?.Count ?? 0);

        /// <summary>
        /// Stat list entries.
        /// Field ID: 4
        /// </summary>
        public List<TlvStatTypeValue> StatList { get; set; }

        /// <summary>
        /// Star points.
        /// Field ID: 5
        /// </summary>
        public int StarPoints { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((StarList?.Length ?? 0) > MaxStars)
                throw new InvalidDataException($"[TlvStarLevelData] StarList exceeds the maximum of {MaxStars} elements.");
            if ((StatList?.Count ?? 0) > MaxStats)
                throw new InvalidDataException($"[TlvStarLevelData] StatList exceeds the maximum of {MaxStats} elements.");

            WriteTlvByte(buffer, 1, StarNum);
            WriteTlvByteArr(buffer, 2, StarList);
            WriteTlvByte(buffer, 3, StatNum);
            WriteTlvSubStructureList(buffer, 4, StatList.Count, StatList);
            WriteTlvInt32(buffer, 5, StarPoints);
        }
    }
}
