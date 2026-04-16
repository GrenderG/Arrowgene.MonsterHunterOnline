using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvMailHeader.
    /// C++ Reader: crygame.dll+sub_10235470 (UnkTlv0278)
    /// </summary>
    public class TlvFriendInsts : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from FriendInsts).
        /// Field ID: 1
        /// </summary>
        public int Count => FriendInsts?.Count ?? 0;

        /// <summary>
        /// List of TlvMailHeader.
        /// Field ID: 2
        /// </summary>
        public List<TlvMailHeader> FriendInsts { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, FriendInsts.Count, FriendInsts);
        }
    }
}
