using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_1010D720
    /// C++ Printer: crygame.dll+sub_1010DA70
    /// </summary>
    public class TlvAttributeItem : Structure, ITlvStructure
    {
        public byte AttrId { get; set; }
        public int AttrValue { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, AttrId);
            WriteTlvInt32(buffer, 2, AttrValue);
        }
    }
}