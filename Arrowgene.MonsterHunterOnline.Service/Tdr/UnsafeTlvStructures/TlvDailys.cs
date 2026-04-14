using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvRefreshLibTasks.
    /// C++ Reader: crygame.dll+sub_10228260 (UnkTlv0260)
    /// </summary>
    public class TlvDailys : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Dailys).
        /// Field ID: 1
        /// </summary>
        public int Count => Dailys?.Count ?? 0;

        /// <summary>
        /// List of TlvRefreshLibTasks.
        /// Field ID: 2
        /// </summary>
        public List<TlvRefreshLibTasks> Dailys { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Dailys.Count, Dailys);
        }
    }
}
