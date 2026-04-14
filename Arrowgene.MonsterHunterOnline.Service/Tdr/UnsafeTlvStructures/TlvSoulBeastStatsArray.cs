using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for soul beast stats arrays (7 elements each).
    /// C++ Reader: crygame.dll+sub_10207850 (UnkTlv0220)
    /// C++ Printer: crygame.dll+sub_10208030
    /// </summary>
    public class TlvSoulBeastStatsArray : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int ExactSize = 7;

        /// <summary>
        /// Character levels (int array, exactly 7).
        /// Field ID: 2
        /// </summary>
        public int[] CharLevel { get; set; } = new int[ExactSize];

        /// <summary>
        /// Character experience (int array, exactly 7).
        /// Field ID: 4
        /// </summary>
        public int[] CharExp { get; set; } = new int[ExactSize];

        /// <summary>
        /// Character gluttony (int array, exactly 7).
        /// Field ID: 5
        /// </summary>
        public int[] CharGlut { get; set; } = new int[ExactSize];

        /// <summary>
        /// Evolve stage (int array, exactly 7).
        /// Field ID: 6
        /// </summary>
        public int[] EvolveStage { get; set; } = new int[ExactSize];

        /// <summary>
        /// Image (int array, exactly 7).
        /// Field ID: 7
        /// </summary>
        public int[] Image { get; set; } = new int[ExactSize];

        /// <summary>
        /// Follow flag (int array, exactly 7).
        /// Field ID: 8
        /// </summary>
        public int[] Follow { get; set; } = new int[ExactSize];

        /// <summary>
        /// Feed time (int array, exactly 7).
        /// Field ID: 9
        /// </summary>
        public int[] FeedTime { get; set; } = new int[ExactSize];

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32Arr(buffer, 2, CharLevel);
            WriteTlvInt32Arr(buffer, 4, CharExp);
            WriteTlvInt32Arr(buffer, 5, CharGlut);
            WriteTlvInt32Arr(buffer, 6, EvolveStage);
            WriteTlvInt32Arr(buffer, 7, Image);
            WriteTlvInt32Arr(buffer, 8, Follow);
            WriteTlvInt32Arr(buffer, 9, FeedTime);
        }
    }
}
