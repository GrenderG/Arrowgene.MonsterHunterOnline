using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for prize ID with state.
    /// C++ Reader: crygame.dll+sub_10172170 (UnkTlv0123)
    /// C++ Printer: crygame.dll+sub_10172250
    /// </summary>
    public class TlvPrizeIdState : Structure, ITlvStructure
    {
        /// <summary>
        /// Prize ID.
        /// Field ID: 1
        /// </summary>
        public int PrizeId { get; set; }

        /// <summary>
        /// State value.
        /// Field ID: 2
        /// </summary>
        public byte State { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, PrizeId);
            WriteTlvByte(buffer, 2, State);
        }
    }
}
