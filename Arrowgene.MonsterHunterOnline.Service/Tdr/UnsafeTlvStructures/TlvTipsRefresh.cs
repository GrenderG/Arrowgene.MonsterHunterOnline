using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for tips check with refresh time.
    /// C++ Reader: crygame.dll+sub_1016A190 (UnkTlv0110)
    /// C++ Printer: crygame.dll+sub_1016A460
    /// </summary>
    public class TlvTipsRefresh : Structure, ITlvStructure
    {
        /// <summary>
        /// Tips check flag.
        /// Field ID: 1
        /// </summary>
        public byte TipsCheck { get; set; }

        /// <summary>
        /// Refresh time.
        /// Field ID: 2
        /// </summary>
        public uint RefreshTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, TipsCheck);
            WriteTlvInt32(buffer, 2, (int)RefreshTime);
        }
    }
}
