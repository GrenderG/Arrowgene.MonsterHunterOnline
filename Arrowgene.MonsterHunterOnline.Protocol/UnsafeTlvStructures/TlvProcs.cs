using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvTypeProcData.
    /// C++ Reader: crygame.dll+sub_10153410 (UnkTlv0082)
    /// </summary>
    public class TlvProcs : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Procs).
        /// Field ID: 1
        /// </summary>
        public int Count => Procs?.Count ?? 0;

        /// <summary>
        /// List of TlvTypeProcData.
        /// Field ID: 2
        /// </summary>
        public List<TlvTypeProcData> Procs { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Procs.Count, Procs);
        }
    }
}
