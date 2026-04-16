using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvChannelNameFlags.
    /// C++ Reader: crygame.dll+sub_10167AE0 (UnkTlv0107)
    /// </summary>
    public class TlvChannelTabs : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from AstTabs).
        /// Field ID: 1
        /// </summary>
        public int Count => AstTabs?.Count ?? 0;

        /// <summary>
        /// List of TlvChannelNameFlags.
        /// Field ID: 2
        /// </summary>
        public List<TlvChannelNameFlags> AstTabs { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, AstTabs.Count, AstTabs);
        }
    }
}
