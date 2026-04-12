using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvOperationLog.
    /// C++ Reader: crygame.dll+sub_10131790 (UnkTlv0045)
    /// </summary>
    public class TlvGuildFuncRecords : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from GuildFuncRecordInfosPkg).
        /// Field ID: 1
        /// </summary>
        public int Count => GuildFuncRecordInfosPkg?.Count ?? 0;

        /// <summary>
        /// List of TlvOperationLog.
        /// Field ID: 2
        /// </summary>
        public List<TlvOperationLog> GuildFuncRecordInfosPkg { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, GuildFuncRecordInfosPkg.Count, GuildFuncRecordInfosPkg);
        }
    }
}
