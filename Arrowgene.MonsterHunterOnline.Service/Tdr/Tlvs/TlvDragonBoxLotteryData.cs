using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for dragon box lottery data.
    /// C++ Reader: crygame.dll+sub_1022F560 (UnkTlv0272)
    /// C++ Printer: crygame.dll+sub_102304E0
    /// </summary>
    public class TlvDragonBoxLotteryData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
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

        /// <summary>Dragon box shop items. Field ID: 13</summary>
        public TlvDragonBoxShopItems DragonBoxShopItems { get; set; } = new();

        /// <summary>Field ID: 14</summary>
        public int FreshNumCnt { get; set; }

        /// <summary>Field ID: 15</summary>
        public int FetchState { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: HitCount = reader.ReadByte(); break;
                    case 2: Pieces = ReadTlvObject<TlvPieces>(reader); break;
                    case 3: UltimatePrize = ReadTlvObject<TlvIdStateByte>(reader); break;
                    case 4: PiecePrizes = ReadTlvObject<TlvPiecePrizes>(reader); break;
                    case 5: BlackFaceCount = reader.ReadInt(); break;
                    case 6: IFHasSSR = reader.ReadInt(); break;
                    case 7: DragonShopID = reader.ReadInt(); break;
                    case 8: DragonShopEndTime = reader.ReadInt(); break;
                    case 9: reader.ReadInt(); break;
                    case 10: FreshNumBit = ReadTlvIntArray(reader); break;
                    case 11: reader.ReadInt(); break;
                    case 12: FreshNumTen = ReadTlvIntArray(reader); break;
                    case 13: DragonBoxShopItems = ReadTlvObject<TlvDragonBoxShopItems>(reader); break;
                    case 14: FreshNumCnt = reader.ReadInt(); break;
                    case 15: FetchState = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((FreshNumBit?.Length ?? 0) > MaxFreshNum) throw new InvalidDataException($"[TlvDragonBoxLotteryData] FreshNumBit exceeds {MaxFreshNum}.");
            if ((FreshNumTen?.Length ?? 0) > MaxFreshNum) throw new InvalidDataException($"[TlvDragonBoxLotteryData] FreshNumTen exceeds {MaxFreshNum}.");

            WriteTlvByte(writer, 1, HitCount);
            WriteTlvObject(writer, 2, Pieces);
            WriteTlvObject(writer, 3, UltimatePrize);
            WriteTlvObject(writer, 4, PiecePrizes);
            WriteTlvInt(writer, 5, BlackFaceCount);
            WriteTlvInt(writer, 6, IFHasSSR);
            WriteTlvInt(writer, 7, DragonShopID);
            WriteTlvInt(writer, 8, DragonShopEndTime);
            WriteTlvInt(writer, 9, FreshNumBitCount);
            WriteTlvIntArray(writer, 10, FreshNumBit, FreshNumBitCount);
            WriteTlvInt(writer, 11, FreshNumTenCount);
            WriteTlvIntArray(writer, 12, FreshNumTen, FreshNumTenCount);
            WriteTlvObject(writer, 13, DragonBoxShopItems);
            WriteTlvInt(writer, 14, FreshNumCnt);
            WriteTlvInt(writer, 15, FetchState);
        }
    }
}
