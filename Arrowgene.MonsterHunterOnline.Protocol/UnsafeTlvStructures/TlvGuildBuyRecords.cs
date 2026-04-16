using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvOperationLog.
    /// C++ Reader: crygame.dll+sub_10132170 (UnkTlv0046)
    /// </summary>
    public class TlvGuildBuyRecords : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from GuildBuyRecordInfosPkg).
        /// Field ID: 1
        /// </summary>
        public int Count => GuildBuyRecordInfosPkg?.Count ?? 0;

        /// <summary>
        /// List of TlvOperationLog.
        /// Field ID: 2
        /// </summary>
        public List<TlvOperationLog> GuildBuyRecordInfosPkg { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, GuildBuyRecordInfosPkg.Count, GuildBuyRecordInfosPkg);
        }
    }
}
