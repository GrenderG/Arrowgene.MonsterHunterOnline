using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for ID and slot information.
    /// C++ Reader: crygame.dll+sub_101A0D80 (UnkTlv0179)
    /// C++ Printer: crygame.dll+sub_101A0E40
    /// </summary>
    public class TlvIdSlot : Structure, ITlvStructure
    {
        /// <summary>
        /// ID value.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Slot value.
        /// Field ID: 2
        /// </summary>
        public int Slot { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvInt32(buffer, 2, Slot);
        }
    }
}
