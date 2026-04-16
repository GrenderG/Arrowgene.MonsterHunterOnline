using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for depot rights list.
    /// C++ Reader: crygame.dll+sub_1011E040 (inner of UnkTlv0021)
    /// C++ Printer: crygame.dll+sub_1011E280
    /// </summary>
    public class TlvDepotRightsList : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxDepots = 8;

        /// <summary>
        /// Depot count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int Count => Depots?.Count ?? 0;

        /// <summary>
        /// Depot rights entries.
        /// Field ID: 2
        /// </summary>
        public List<TlvDepotRights> Depots { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Depots?.Count ?? 0) > MaxDepots)
                throw new InvalidDataException($"[TlvDepotRightsList] Depots exceeds the maximum of {MaxDepots} elements.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Depots.Count, Depots);
        }
    }
}
