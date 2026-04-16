using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for stat index pair (short fields).
    /// C++ Reader: crygame.dll+sub_1021A880 (UnkTlv0241)
    /// C++ Printer: crygame.dll+sub_1021AB50
    /// </summary>
    public class TlvStatIdxPair : Structure, ITlvStructure
    {
        /// <summary>
        /// Stat index.
        /// Field ID: 1
        /// </summary>
        public short StatIdx { get; set; }

        /// <summary>
        /// Stat value.
        /// Field ID: 2
        /// </summary>
        public short StatValue { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt16(buffer, 1, StatIdx);
            WriteTlvInt16(buffer, 2, StatValue);
        }
    }
}
