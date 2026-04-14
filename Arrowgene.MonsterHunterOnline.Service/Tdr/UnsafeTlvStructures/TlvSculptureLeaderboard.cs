using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvLeaderboardEntry.
    /// C++ Reader: crygame.dll+sub_1013AAE0 (UnkTlv0052)
    /// </summary>
    public class TlvSculptureLeaderboard : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Sculptures).
        /// Field ID: 1
        /// </summary>
        public int Count => Sculptures?.Count ?? 0;

        /// <summary>
        /// List of TlvLeaderboardEntry.
        /// Field ID: 2
        /// </summary>
        public List<TlvLeaderboardEntry> Sculptures { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Sculptures.Count, Sculptures);
        }
    }
}
