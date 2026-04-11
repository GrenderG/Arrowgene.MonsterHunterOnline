using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for relic/chess activity data.
    /// C++ Reader: crygame.dll+sub_10230B50 (UnkTlv0273)
    /// C++ Printer: crygame.dll+sub_10231270
    /// </summary>
    public class TlvRelicChessData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = reader.ReadInt(); break;
                    case 2: RelicPoint = reader.ReadInt(); break;
                    case 3: RelicPrize = ReadTlvObject<TlvIdStateByte>(reader); break;
                    case 4: Chess = ReadTlvObject<TlvDragonBoxLotteryChess>(reader); break;
                    case 5: Activity = reader.ReadInt(); break;
                    case 6: RefreshTime = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Id);
            WriteTlvInt(writer, 2, RelicPoint);
            WriteTlvObject(writer, 3, RelicPrize);
            WriteTlvObject(writer, 4, Chess);
            WriteTlvInt(writer, 5, Activity);
            WriteTlvInt(writer, 6, RefreshTime);
        }
    }
}
