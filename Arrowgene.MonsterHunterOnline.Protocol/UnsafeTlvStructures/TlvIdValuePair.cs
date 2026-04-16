using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for ID with value pair.
    /// C++ Reader: crygame.dll+sub_1019E830 (UnkTlv0175)
    /// C++ Printer: crygame.dll+sub_1019EAC0
    /// </summary>
    public class TlvIdValuePair : Structure, ITlvStructure
    {
        /// <summary>
        /// ID.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Value.
        /// Field ID: 2
        /// </summary>
        public int Value { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvInt32(buffer, 2, Value);
        }
    }
}
