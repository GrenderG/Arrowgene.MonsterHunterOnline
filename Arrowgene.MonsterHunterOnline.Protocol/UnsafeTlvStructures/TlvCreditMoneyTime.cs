using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for credit, money and time.
    /// C++ Reader: crygame.dll+sub_1023B050 (UnkTlv0284)
    /// C++ Printer: crygame.dll+sub_1023B2A0
    /// </summary>
    public class TlvCreditMoneyTime : Structure, ITlvStructure
    {
        /// <summary>
        /// Total credit.
        /// Field ID: 1
        /// </summary>
        public uint TotalCredit { get; set; }

        /// <summary>
        /// Total money.
        /// Field ID: 2
        /// </summary>
        public uint TotalMoney { get; set; }

        /// <summary>
        /// Last time.
        /// Field ID: 3
        /// </summary>
        public uint LastTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)TotalCredit);
            WriteTlvInt32(buffer, 2, (int)TotalMoney);
            WriteTlvInt32(buffer, 3, (int)LastTime);
        }
    }
}
