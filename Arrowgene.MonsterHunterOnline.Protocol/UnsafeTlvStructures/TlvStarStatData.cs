using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for star points with stat list.
    /// C++ Reader: crygame.dll+sub_10215210 (UnkTlv0233)
    /// C++ Printer: crygame.dll+sub_10215C50
    /// </summary>
    public class TlvStarStatData : Structure, ITlvStructure
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
        /// Stat list.
        /// Field ID: 4
        /// </summary>
        public List<TlvStatTypeValue> StatList { get; set; }

        /// <summary>
        /// Star points.
        /// Field ID: 5
        /// </summary>
        public uint StarPoints { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((StarList?.Length ?? 0) > MaxStars)
                throw new InvalidDataException($"[TlvStarStatData] StarList exceeds the maximum of {MaxStars} elements.");
            if ((StatList?.Count ?? 0) > MaxStats)
                throw new InvalidDataException($"[TlvStarStatData] StatList exceeds the maximum of {MaxStats} elements.");

            WriteTlvByte(buffer, 1, StarNum);
            WriteTlvByteArr(buffer, 2, StarList);
            WriteTlvByte(buffer, 3, StatNum);
            WriteTlvSubStructureList(buffer, 4, StatList.Count, StatList);
            WriteTlvInt32(buffer, 5, (int)StarPoints);
        }
    }
}
