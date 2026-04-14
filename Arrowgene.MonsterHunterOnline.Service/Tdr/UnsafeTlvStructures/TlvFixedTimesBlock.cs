using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for fixed times with block arguments.
    /// C++ Reader: crygame.dll+sub_10163300 (UnkTlv0098)
    /// C++ Printer: crygame.dll+sub_10163560
    /// </summary>
    public class TlvFixedTimesBlock : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxCompleteBits = 5;

        /// <summary>
        /// Fixed times value.
        /// Field ID: 1
        /// </summary>
        public int FixedTimes { get; set; }

        /// <summary>
        /// Block argument 1.
        /// Field ID: 2
        /// </summary>
        public int BlockArg1 { get; set; }

        /// <summary>
        /// Block argument 2.
        /// Field ID: 3
        /// </summary>
        public int BlockArg2 { get; set; }

        /// <summary>
        /// Block argument 3.
        /// Field ID: 4
        /// </summary>
        public int BlockArg3 { get; set; }

        /// <summary>
        /// Block argument 4.
        /// Field ID: 5
        /// </summary>
        public int BlockArg4 { get; set; }

        /// <summary>
        /// Complete bit count (derived from array).
        /// Field ID: 6
        /// </summary>
        public int CompleteBitCount => CompleteBit?.Length ?? 0;

        /// <summary>
        /// Complete bit array (max 5).
        /// Field ID: 7
        /// </summary>
        public byte[] CompleteBit { get; set; }

        /// <summary>
        /// Level ID.
        /// Field ID: 8
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// Level result.
        /// Field ID: 9
        /// </summary>
        public int LevelResult { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, FixedTimes);
            WriteTlvInt32(buffer, 2, BlockArg1);
            WriteTlvInt32(buffer, 3, BlockArg2);
            WriteTlvInt32(buffer, 4, BlockArg3);
            WriteTlvInt32(buffer, 5, BlockArg4);
            WriteTlvInt32(buffer, 6, CompleteBitCount);
            WriteTlvByteArr(buffer, 7, CompleteBit);
            WriteTlvInt32(buffer, 8, LevelId);
            WriteTlvInt32(buffer, 9, LevelResult);
        }
    }
}
