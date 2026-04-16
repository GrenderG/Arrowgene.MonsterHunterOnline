using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.TlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_101EEE50
    /// C++ Reader: crygame.dll+sub_XXXXX
    /// C++ Printer: crygame.dll+sub_101EF2F0
    /// </summary>
    public class TlvExpressionItem : Structure, ITlvStructure
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public int Point { get; set; }
        public int CollectLevel { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvInt32(buffer, 2, Level);
            WriteTlvInt32(buffer, 3, Point);
            WriteTlvInt32(buffer, 4, CollectLevel);
        }
    }
}