using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for type state with ID.
    /// C++ Reader: crygame.dll+sub_10177010 (UnkTlv0130)
    /// C++ Printer: crygame.dll+sub_10177100
    /// </summary>
    public class TlvTypeStateId : Structure, ITlvStructure
    {
        /// <summary>
        /// Type value.
        /// Field ID: 1
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// State value.
        /// Field ID: 2
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// ID value.
        /// Field ID: 3
        /// </summary>
        public int Id { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, Type);
            WriteTlvByte(buffer, 2, State);
            WriteTlvInt32(buffer, 3, Id);
        }
    }
}
