using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for task state with arguments.
    /// C++ Reader: crygame.dll+sub_10125280 (UnkTlv0029)
    /// C++ Printer: crygame.dll+sub_101253C0
    /// </summary>
    public class TlvTaskState : Structure, ITlvStructure
    {
        /// <summary>
        /// Task identifier (short).
        /// Field ID: 1
        /// </summary>
        public short Task { get; set; }

        /// <summary>
        /// ID value.
        /// Field ID: 2
        /// </summary>
        public byte Id { get; set; }

        /// <summary>
        /// State value.
        /// Field ID: 3
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// Argument 1.
        /// Field ID: 4
        /// </summary>
        public byte Arg1 { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt16(buffer, 1, Task);
            WriteTlvByte(buffer, 2, Id);
            WriteTlvByte(buffer, 3, State);
            WriteTlvByte(buffer, 4, Arg1);
        }
    }
}
