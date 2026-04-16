using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvItemType.
    /// C++ Reader: crygame.dll+sub_1013B9E0 (UnkTlv0054)
    /// </summary>
    public class TlvEquips : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Equips).
        /// Field ID: 1
        /// </summary>
        public int Count => Equips?.Count ?? 0;

        /// <summary>
        /// List of TlvItemType.
        /// Field ID: 2
        /// </summary>
        public List<TlvItemType> Equips { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Equips.Count, Equips);
        }
    }
}
