using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for ID with level byte.
    /// C++ Reader: crygame.dll+sub_101C0B10 (UnkTlv0199)
    /// C++ Printer: crygame.dll+sub_101C0BF0
    /// </summary>
    public class TlvIdLevel : Structure, ITlvStructure
    {
        /// <summary>
        /// ID value.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Level value.
        /// Field ID: 2
        /// </summary>
        public byte Level { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvByte(buffer, 2, Level);
        }
    }
}
