using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for group sale with refresh time.
    /// C++ Reader: crygame.dll+sub_10210AE0 (UnkTlv0227)
    /// C++ Printer: crygame.dll+sub_10210DC0
    /// </summary>
    public class TlvGroupSaleRefresh : Structure, ITlvStructure
    {
        /// <summary>
        /// Group.
        /// Field ID: 1
        /// </summary>
        public short Group { get; set; }

        /// <summary>
        /// Sold count.
        /// Field ID: 2
        /// </summary>
        public int SaledCount { get; set; }

        /// <summary>
        /// Refresh time.
        /// Field ID: 3
        /// </summary>
        public uint RefreshTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt16(buffer, 1, Group);
            WriteTlvInt32(buffer, 2, SaledCount);
            WriteTlvInt32(buffer, 3, (int)RefreshTime);
        }
    }
}
