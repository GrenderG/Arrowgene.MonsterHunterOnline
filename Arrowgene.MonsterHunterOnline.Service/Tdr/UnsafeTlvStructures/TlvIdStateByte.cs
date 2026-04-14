using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for ID with state byte (int-size ID).
    /// C++ Reader: crygame.dll+sub_1022D080 (UnkTlv0268)
    /// C++ Printer: crygame.dll+sub_1022D390
    /// </summary>
    public class TlvIdStateByte : Structure, ITlvStructure
    {
        /// <summary>
        /// ID.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// State.
        /// Field ID: 2
        /// </summary>
        public byte State { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvByte(buffer, 2, State);
        }
    }
}
