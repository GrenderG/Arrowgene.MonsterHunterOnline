using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for daily high score tracking.
    /// C++ Reader: crygame.dll+sub_101A3830 (UnkTlv0184)
    /// C++ Printer: crygame.dll+sub_101A38F0
    /// </summary>
    public class TlvDailyHighScore : Structure, ITlvStructure
    {
        /// <summary>
        /// Date day value.
        /// Field ID: 1
        /// </summary>
        public int DateDay { get; set; }

        /// <summary>
        /// Current higher score.
        /// Field ID: 2
        /// </summary>
        public int CurHigher { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, DateDay);
            WriteTlvInt32(buffer, 2, CurHigher);
        }
    }
}
