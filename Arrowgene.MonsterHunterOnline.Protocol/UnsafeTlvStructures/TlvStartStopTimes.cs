using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvIdStartStopTime.
    /// C++ Reader: crygame.dll+sub_102472A0 (UnkTlv0293)
    /// </summary>
    public class TlvStartStopTimes : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Times).
        /// Field ID: 1
        /// </summary>
        public int Count => Times?.Count ?? 0;

        /// <summary>
        /// List of TlvIdStartStopTime.
        /// Field ID: 2
        /// </summary>
        public List<TlvIdStartStopTime> Times { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Times.Count, Times);
        }
    }
}
