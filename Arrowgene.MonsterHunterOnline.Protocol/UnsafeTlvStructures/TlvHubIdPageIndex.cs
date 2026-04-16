using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for hub ID with page index.
    /// C++ Reader: crygame.dll+sub_10143CB0 (UnkTlv0065)
    /// C++ Printer: crygame.dll+sub_10143D90
    /// </summary>
    public class TlvHubIdPageIndex : Structure, ITlvStructure
    {
        /// <summary>
        /// Hub ID.
        /// Field ID: 1
        /// </summary>
        public byte HubId { get; set; }

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
            WriteTlvByte(buffer, 1, HubId);
            WriteTlvInt32(buffer, 2, PageIndex);
        }
    }
}
