using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for credit.
    /// C++ Reader: crygame.dll+sub_1016E000 (UnkTlv0116)
    /// C++ Printer: crygame.dll+sub_1016E1F0
    /// </summary>
    public class TlvCredit : Structure, ITlvStructure
    {
        /// <summary>
        /// Credit value.
        /// Field ID: 1
        /// </summary>
        public int Credit { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Credit);
        }
    }
}
