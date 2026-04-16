using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for star and branch data.
    /// C++ Reader: crygame.dll+sub_102175D0 (UnkTlv0236)
    /// C++ Printer: crygame.dll+sub_10217B40
    /// </summary>
    public class TlvStarBranchData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxBranches = 10;
        public const int MaxStars = 20;

        /// <summary>
        /// Star count (derived from list).
        /// Field ID: 1
        /// </summary>
        public byte StarNum => (byte)(StarList?.Count ?? 0);

        /// <summary>
        /// Branch count (derived from list).
        /// Field ID: 3
        /// </summary>
        public byte BranchNum => (byte)(BranchList?.Count ?? 0);

        /// <summary>
        /// Branch list.
        /// Field ID: 4
        /// </summary>
        public List<TlvBranchStatsB> BranchList { get; set; }

        /// <summary>
        /// Star list.
        /// Field ID: 5
        /// </summary>
        public List<TlvQualityFinishTimeVar> StarList { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((BranchList?.Count ?? 0) > MaxBranches)
                throw new InvalidDataException($"[TlvStarBranchData] BranchList exceeds the maximum of {MaxBranches} elements.");
            if ((StarList?.Count ?? 0) > MaxStars)
                throw new InvalidDataException($"[TlvStarBranchData] StarList exceeds the maximum of {MaxStars} elements.");

            WriteTlvByte(buffer, 1, StarNum);
            WriteTlvByte(buffer, 3, BranchNum);
            WriteTlvSubStructureList(buffer, 4, BranchList.Count, BranchList);
            WriteTlvSubStructureList(buffer, 5, StarList.Count, StarList);
        }
    }
}
