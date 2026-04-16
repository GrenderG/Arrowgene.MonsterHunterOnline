using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.TlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_101ED5D0
    /// C++ Reader: crygame.dll+sub_XXXXX
    /// C++ Printer: crygame.dll+sub_101ED8D0
    /// </summary>
    public class TlvIngredientItem : Structure, ITlvStructure
    {
        public int Id { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
        }
    }
}