using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for hub page reference.
    /// C++ Reader: crygame.dll+sub_101436B0 (UnkTlv0064)
    /// C++ Printer: crygame.dll+sub_10143770
    /// </summary>
    public class TlvHubPage : Structure, ITlvStructure
    {
        /// <summary>
        /// Hub identifier.
        /// Field ID: 1
        /// </summary>
        public int HubId { get; set; }

        /// <summary>
        /// Page index.
        /// Field ID: 2
        /// </summary>
        public int PageIndex { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, HubId);
            WriteTlvInt32(buffer, 2, PageIndex);
        }
    }
}
