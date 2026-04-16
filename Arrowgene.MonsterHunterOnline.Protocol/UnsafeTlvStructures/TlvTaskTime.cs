using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for task with time.
    /// C++ Reader: crygame.dll+sub_10220BC0 (UnkTlv0250)
    /// C++ Printer: crygame.dll+sub_10220CA0
    /// </summary>
    public class TlvTaskTime : Structure, ITlvStructure
    {
        /// <summary>
        /// Task identifier (short).
        /// Field ID: 1
        /// </summary>
        public short Task { get; set; }

        /// <summary>
        /// Time value.
        /// Field ID: 2
        /// </summary>
        public uint Time { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt16(buffer, 1, Task);
            WriteTlvInt32(buffer, 2, (int)Time);
        }
    }
}
