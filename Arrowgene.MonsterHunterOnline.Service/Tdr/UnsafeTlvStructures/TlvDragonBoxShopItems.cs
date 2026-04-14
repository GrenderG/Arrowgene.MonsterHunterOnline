using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvIdBuyTimes.
    /// C++ Reader: crygame.dll+sub_1022E940 (UnkTlv0271)
    /// </summary>
    public class TlvDragonBoxShopItems : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from DragonBoxShopItems).
        /// Field ID: 1
        /// </summary>
        public int Count => DragonBoxShopItems?.Count ?? 0;

        /// <summary>
        /// List of TlvIdBuyTimes.
        /// Field ID: 2
        /// </summary>
        public List<TlvIdBuyTimes> DragonBoxShopItems { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, DragonBoxShopItems.Count, DragonBoxShopItems);
        }
    }
}
