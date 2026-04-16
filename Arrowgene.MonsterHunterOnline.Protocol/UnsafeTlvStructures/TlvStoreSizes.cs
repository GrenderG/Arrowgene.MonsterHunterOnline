using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for storage sizes.
    /// C++ Reader: crygame.dll+sub_10164390 (UnkTlv0101)
    /// C++ Printer: crygame.dll+sub_101644B0
    /// </summary>
    public class TlvStoreSizes : Structure, ITlvStructure
    {
        /// <summary>
        /// Store size.
        /// Field ID: 2
        /// </summary>
        public short StoreSize { get; set; }

        /// <summary>
        /// Normal size.
        /// Field ID: 3
        /// </summary>
        public short NormalSize { get; set; }

        /// <summary>
        /// Material store size.
        /// Field ID: 4
        /// </summary>
        public short MaterialStoreSize { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt16(buffer, 2, StoreSize);
            WriteTlvInt16(buffer, 3, NormalSize);
            WriteTlvInt16(buffer, 4, MaterialStoreSize);
        }
    }
}
