using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for points, context info and prize IDs.
    /// C++ Reader: crygame.dll+sub_10175EB0 (UnkTlv0129)
    /// C++ Printer: crygame.dll+sub_101767E0
    /// </summary>
    public class TlvPointsCtxPrizes : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxCtx = 200;
        public const int MaxPrizes = 100;

        /// <summary>
        /// Points.
        /// Field ID: 1
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Context count (derived from CtxInfo).
        /// Field ID: 6
        /// </summary>
        public int CtxCount => CtxInfo?.Length ?? 0;

        /// <summary>
        /// Context info (int array).
        /// Field ID: 7
        /// </summary>
        public int[] CtxInfo { get; set; }

        /// <summary>
        /// Prizes count (derived from PrizesID).
        /// Field ID: 8
        /// </summary>
        public int PrizesCount => PrizesID?.Length ?? 0;

        /// <summary>
        /// Prize IDs (int array).
        /// Field ID: 9
        /// </summary>
        public int[] PrizesID { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((CtxInfo?.Length ?? 0) > MaxCtx)
                throw new InvalidDataException($"[TlvPointsCtxPrizes] CtxInfo exceeds the maximum of {MaxCtx} elements.");
            if ((PrizesID?.Length ?? 0) > MaxPrizes)
                throw new InvalidDataException($"[TlvPointsCtxPrizes] PrizesID exceeds the maximum of {MaxPrizes} elements.");

            WriteTlvInt32(buffer, 1, Points);
            WriteTlvInt32(buffer, 6, CtxCount);
            WriteTlvInt32Arr(buffer, 7, CtxInfo);
            WriteTlvInt32(buffer, 8, PrizesCount);
            WriteTlvInt32Arr(buffer, 9, PrizesID);
        }
    }
}
