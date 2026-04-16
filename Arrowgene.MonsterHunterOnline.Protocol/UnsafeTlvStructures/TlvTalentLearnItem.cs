using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_101EE0C0
    /// C++ Reader: crygame.dll+sub_XXXXX
    /// C++ Printer: crygame.dll+sub_101EE510
    /// </summary>
    public class TlvTalentLearnItem : Structure, ITlvStructure
    {
        public int Id { get; set; }
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