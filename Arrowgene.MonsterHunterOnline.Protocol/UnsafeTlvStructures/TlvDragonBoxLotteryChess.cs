using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for dragon box lottery data (chess variant, no shop/fetch fields).
    /// C++ Reader: crygame.dll+sub_1022FF30 (UnkTlv0170 internal / used by UnkTlv0273)
    /// </summary>
    public class TlvDragonBoxLotteryChess : Structure, ITlvStructure
    {
        public const int MaxFreshNum = 10;

        /// <summary>Field ID: 1</summary>
        public byte HitCount { get; set; }

        /// <summary>Pieces data. Field ID: 2</summary>
        public TlvPieces Pieces { get; set; } = new();

        /// <summary>Ultimate prize data. Field ID: 3</summary>
        public TlvIdStateByte UltimatePrize { get; set; } = new();

        /// <summary>Piece prizes data. Field ID: 4</summary>
        public TlvPiecePrizes PiecePrizes { get; set; } = new();

        /// <summary>Field ID: 5</summary>
        public int BlackFaceCount { get; set; }

        /// <summary>Field ID: 6</summary>
        public int IFHasSSR { get; set; }

        /// <summary>Field ID: 7</summary>
        public int DragonShopID { get; set; }

        /// <summary>Field ID: 8</summary>
        public int DragonShopEndTime { get; set; }

        /// <summary>Fresh num bit count (derived). Field ID: 9</summary>
        public int FreshNumBitCount => FreshNumBit?.Length ?? 0;

        /// <summary>Fresh num bits. Field ID: 10</summary>
        public int[] FreshNumBit { get; set; }

        /// <summary>Fresh num ten count (derived). Field ID: 11</summary>
        public int FreshNumTenCount => FreshNumTen?.Length ?? 0;

        /// <summary>Fresh num tens. Field ID: 12</summary>
        public int[] FreshNumTen { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((FreshNumBit?.Length ?? 0) > MaxFreshNum) throw new InvalidDataException($"[TlvDragonBoxLotteryChess] FreshNumBit exceeds {MaxFreshNum}.");
            if ((FreshNumTen?.Length ?? 0) > MaxFreshNum) throw new InvalidDataException($"[TlvDragonBoxLotteryChess] FreshNumTen exceeds {MaxFreshNum}.");

            WriteTlvByte(buffer, 1, HitCount);
            WriteTlvSubStructure(buffer, 2, Pieces);
            WriteTlvSubStructure(buffer, 3, UltimatePrize);
            WriteTlvSubStructure(buffer, 4, PiecePrizes);
            WriteTlvInt32(buffer, 5, BlackFaceCount);
            WriteTlvInt32(buffer, 6, IFHasSSR);
            WriteTlvInt32(buffer, 7, DragonShopID);
            WriteTlvInt32(buffer, 8, DragonShopEndTime);
            WriteTlvInt32(buffer, 9, FreshNumBitCount);
            WriteTlvInt32Arr(buffer, 10, FreshNumBit);
            WriteTlvInt32(buffer, 11, FreshNumTenCount);
            WriteTlvInt32Arr(buffer, 12, FreshNumTen);
        }
    }
}
