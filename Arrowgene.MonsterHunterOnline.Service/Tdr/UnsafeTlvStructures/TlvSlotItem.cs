using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (Equipment Slot / Bead Item).
    /// C++ Writer: crygame.dll+sub_1010F880
    /// C++ Printer: crygame.dll+sub_1010FBE0
    /// </summary>
    public class TlvSlotItem : Structure, ITlvStructure
    {
        public int SlotItemId { get; set; }
        public int BeadLevel { get; set; }
        public int IsLegend { get; set; } // Often treated as a boolean (0 or 1) in memory, but written as Int32

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, SlotItemId);
            WriteTlvInt32(buffer, 2, BeadLevel);
            WriteTlvInt32(buffer, 3, IsLegend);
        }
    }
}