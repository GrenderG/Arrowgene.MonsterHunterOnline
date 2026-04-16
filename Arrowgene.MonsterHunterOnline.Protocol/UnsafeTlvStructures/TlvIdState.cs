using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for ID with state byte.
    /// C++ Reader: crygame.dll+sub_10124930 (UnkTlv0028)
    /// C++ Printer: crygame.dll+sub_10124A80
    /// </summary>
    public class TlvIdState : Structure, ITlvStructure
    {
        /// <summary>
        /// ID value (short).
        /// Field ID: 1
        /// </summary>
        public short Id { get; set; }

        /// <summary>
        /// State byte value.
        /// Field ID: 2
        /// </summary>
        public byte State { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt16(buffer, 1, Id);
            WriteTlvByte(buffer, 2, State);
        }
    }
}
