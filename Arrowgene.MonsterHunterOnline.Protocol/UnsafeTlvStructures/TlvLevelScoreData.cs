using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for level score data.
    /// C++ Reader: crygame.dll+sub_1014C6F0 (UnkTlv0077)
    /// C++ Printer: crygame.dll+sub_1014D300
    /// </summary>
    public class TlvLevelScoreData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxLevels = 5000;

        /// <summary>
        /// Level count (derived from arrays).
        /// Field ID: 1
        /// </summary>
        public short LevelCnt => (short)(LevelID?.Length ?? 0);

        /// <summary>
        /// Level IDs (int array).
        /// Field ID: 2
        /// </summary>
        public int[] LevelID { get; set; }

        /// <summary>
        /// Best scores (short array).
        /// Field ID: 3
        /// </summary>
        public short[] TheBestScore { get; set; }

        /// <summary>
        /// States (byte array).
        /// Field ID: 4
        /// </summary>
        public byte[] State { get; set; }

        /// <summary>
        /// History final ranks (byte array).
        /// Field ID: 5
        /// </summary>
        public byte[] HistoryFinalRank { get; set; }

        /// <summary>
        /// Gain reward flags (byte array).
        /// Field ID: 6
        /// </summary>
        public byte[] GainRewardFlag { get; set; }

        /// <summary>
        /// Last times (int array).
        /// Field ID: 7
        /// </summary>
        public int[] LastTm { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((LevelID?.Length ?? 0) > MaxLevels)
                throw new InvalidDataException($"[TlvLevelScoreData] LevelID exceeds the maximum of {MaxLevels} elements.");

            WriteTlvInt16(buffer, 1, LevelCnt);
            WriteTlvInt32Arr(buffer, 2, LevelID);
            WriteTlvInt16Arr(buffer, 3, TheBestScore);
            WriteTlvByteArr(buffer, 4, State);
            WriteTlvByteArr(buffer, 5, HistoryFinalRank);
            WriteTlvByteArr(buffer, 6, GainRewardFlag);
            WriteTlvInt32Arr(buffer, 7, LastTm);
        }
    }
}
