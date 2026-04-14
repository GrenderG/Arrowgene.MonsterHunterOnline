using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for step state tracking.
    /// C++ Reader: crygame.dll+sub_10166910 (UnkTlv0105)
    /// C++ Printer: crygame.dll+sub_10166AB0
    /// </summary>
    public class TlvStepState : Structure, ITlvStructure
    {
        /// <summary>
        /// Step identifier.
        /// Field ID: 1
        /// </summary>
        public byte StepId { get; set; }

        /// <summary>
        /// Step state.
        /// Field ID: 2
        /// </summary>
        public byte StepState { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, StepId);
            WriteTlvByte(buffer, 2, StepState);
        }
    }
}
