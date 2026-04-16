using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for task ID with state and argument.
    /// C++ Reader: crygame.dll+sub_1021F370 (UnkTlv0247)
    /// C++ Printer: crygame.dll+sub_1021F7C0
    /// </summary>
    public class TlvTaskIdStateArg : Structure, ITlvStructure
    {
        /// <summary>
        /// Task identifier.
        /// Field ID: 1
        /// </summary>
        public int Task { get; set; }

        /// <summary>
        /// Task sub-ID.
        /// Field ID: 2
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// State.
        /// Field ID: 3
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Argument 1.
        /// Field ID: 4
        /// </summary>
        public int Arg1 { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Task);
            WriteTlvInt32(buffer, 2, Id);
            WriteTlvInt32(buffer, 3, State);
            WriteTlvInt32(buffer, 4, Arg1);
        }
    }
}
