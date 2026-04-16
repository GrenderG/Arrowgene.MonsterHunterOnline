using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for item ID.
    /// C++ Reader: crygame.dll+sub_10163AE0 (UnkTlv0100)
    /// C++ Printer: crygame.dll+sub_10163CD0
    /// </summary>
    public class TlvItemId : Structure, ITlvStructure
    {
        /// <summary>
        /// Item ID.
        /// Field ID: 2
        /// </summary>
        public uint ItemId { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 2, (int)ItemId);
        }
    }
}
