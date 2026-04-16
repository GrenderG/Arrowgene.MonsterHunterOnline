using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for three int arguments (variant B).
    /// C++ Reader: crygame.dll+sub_1017C260 (UnkTlv0138)
    /// C++ Printer: crygame.dll+sub_1017C5D0
    /// </summary>
    public class TlvThreeArgsB : Structure, ITlvStructure
    {
        /// <summary>
        /// Argument 1.
        /// Field ID: 1
        /// </summary>
        public int Arg1 { get; set; }

        /// <summary>
        /// Argument 2.
        /// Field ID: 2
        /// </summary>
        public int Arg2 { get; set; }

        /// <summary>
        /// Argument 3.
        /// Field ID: 3
        /// </summary>
        public int Arg3 { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Arg1);
            WriteTlvInt32(buffer, 2, Arg2);
            WriteTlvInt32(buffer, 3, Arg3);
        }
    }
}
