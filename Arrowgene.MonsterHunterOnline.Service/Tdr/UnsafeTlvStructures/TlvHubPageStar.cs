using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for hub page with star level.
    /// C++ Reader: crygame.dll+sub_101445D0 (UnkTlv0066)
    /// C++ Printer: crygame.dll+sub_101446E0
    /// </summary>
    public class TlvHubPageStar : Structure, ITlvStructure
    {
        /// <summary>
        /// Hub identifier.
        /// Field ID: 1
        /// </summary>
        public byte HubId { get; set; }

        /// <summary>
        /// Page index.
        /// Field ID: 2
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Star level.
        /// Field ID: 3
        /// </summary>
        public int StarLv { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, HubId);
            WriteTlvInt32(buffer, 2, PageIndex);
            WriteTlvInt32(buffer, 3, StarLv);
        }
    }
}
