using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for action with steps.
    /// C++ Reader: crygame.dll+sub_1018DAC0 (UnkTlv0160)
    /// C++ Printer: crygame.dll+sub_1018DB80
    /// </summary>
    public class TlvActionSteps : Structure, ITlvStructure
    {
        /// <summary>
        /// Action identifier.
        /// Field ID: 1
        /// </summary>
        public int ActionId { get; set; }

        /// <summary>
        /// Number of steps.
        /// Field ID: 2
        /// </summary>
        public int Steps { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, ActionId);
            WriteTlvInt32(buffer, 2, Steps);
        }
    }
}
