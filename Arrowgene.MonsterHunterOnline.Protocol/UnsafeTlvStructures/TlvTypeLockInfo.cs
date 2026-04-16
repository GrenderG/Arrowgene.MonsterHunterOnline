using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for type lock information.
    /// C++ Reader: crygame.dll+sub_101A1A60 (UnkTlv0181)
    /// C++ Printer: crygame.dll+sub_101A1B20
    /// </summary>
    public class TlvTypeLockInfo : Structure, ITlvStructure
    {
        /// <summary>
        /// Type identifier.
        /// Field ID: 1
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Lock information.
        /// Field ID: 2
        /// </summary>
        public int LockInfo { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Type);
            WriteTlvInt32(buffer, 2, LockInfo);
        }
    }
}
