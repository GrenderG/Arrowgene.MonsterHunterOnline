using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for operation with arguments.
    /// C++ Reader: crygame.dll+sub_10138110 (UnkTlv0048)
    /// C++ Printer: crygame.dll+sub_101382A0
    /// </summary>
    public class TlvOperationArgs : Structure, ITlvStructure
    {
        /// <summary>
        /// Operation type.
        /// Field ID: 1
        /// </summary>
        public int Oper { get; set; }

        /// <summary>
        /// Argument 1.
        /// Field ID: 2
        /// </summary>
        public int Arg1 { get; set; }

        /// <summary>
        /// Argument 2.
        /// Field ID: 3
        /// </summary>
        public int Arg2 { get; set; }

        /// <summary>
        /// Argument 5 (64-bit).
        /// Field ID: 6
        /// </summary>
        public ulong Arg5 { get; set; }

        /// <summary>
        /// Argument 6 (64-bit).
        /// Field ID: 7
        /// </summary>
        public ulong Arg6 { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Oper);
            WriteTlvInt32(buffer, 2, Arg1);
            WriteTlvInt32(buffer, 3, Arg2);
            WriteTlvInt64(buffer, 6, (long)Arg5);
            WriteTlvInt64(buffer, 7, (long)Arg6);
        }
    }
}
