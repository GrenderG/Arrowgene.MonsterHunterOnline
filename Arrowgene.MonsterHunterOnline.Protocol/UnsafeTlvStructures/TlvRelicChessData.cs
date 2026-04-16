using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for relic/chess activity data.
    /// C++ Reader: crygame.dll+sub_10230B50 (UnkTlv0273)
    /// C++ Printer: crygame.dll+sub_10231270
    /// </summary>
    public class TlvRelicChessData : Structure, ITlvStructure
    {
        /// <summary>Field ID: 1</summary>
        public int Id { get; set; }

        /// <summary>Field ID: 2</summary>
        public int RelicPoint { get; set; }

        /// <summary>Relic prize data. Field ID: 3</summary>
        public TlvIdStateByte RelicPrize { get; set; } = new();

        /// <summary>Chess data. Field ID: 4</summary>
        public TlvDragonBoxLotteryChess Chess { get; set; } = new();

        /// <summary>Field ID: 5</summary>
        public int Activity { get; set; }

        /// <summary>Field ID: 6</summary>
        public int RefreshTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvInt32(buffer, 2, RelicPoint);
            WriteTlvSubStructure(buffer, 3, RelicPrize);
            WriteTlvSubStructure(buffer, 4, Chess);
            WriteTlvInt32(buffer, 5, Activity);
            WriteTlvInt32(buffer, 6, RefreshTime);
        }
    }
}
