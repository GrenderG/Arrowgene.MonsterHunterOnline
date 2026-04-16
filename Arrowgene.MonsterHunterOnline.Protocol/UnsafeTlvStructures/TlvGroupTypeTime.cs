using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for group type with time.
    /// C++ Reader: crygame.dll+sub_1016E820 (UnkTlv0117)
    /// C++ Printer: crygame.dll+sub_1016E980
    /// </summary>
    public class TlvGroupTypeTime : Structure, ITlvStructure
    {
        /// <summary>
        /// Group ID.
        /// Field ID: 1
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Type value.
        /// Field ID: 2
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Time value.
        /// Field ID: 3
        /// </summary>
        public uint Time { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, GroupId);
            WriteTlvByte(buffer, 2, Type);
            WriteTlvInt32(buffer, 3, (int)Time);
        }
    }
}
