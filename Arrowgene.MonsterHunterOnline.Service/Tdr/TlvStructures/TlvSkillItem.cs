using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_101ECF70
    /// C++ Reader: crygame.dll+sub_XXXXX
    /// C++ Printer: crygame.dll+sub_101ED330
    /// </summary>
    public class TlvSkillItem : Structure, ITlvStructure
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public byte Level { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }
        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvVarInt32(buffer, 1, Id);
            WriteTlvVarInt32(buffer, 2, Value);
            WriteTlvByte(buffer, 3, Level);
        }
    }
}
