using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for commerce boat with contribution data.
    /// C++ Reader: crygame.dll+sub_1011FB00 (UnkTlv0023)
    /// C++ Printer: crygame.dll+sub_101200E0
    /// </summary>
    public class TlvCommerceBoatContribution : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Commerce boat info. Field ID: 1</summary>
        public TlvCommerceBoat CommerceBoatInfo { get; set; } = new();

        /// <summary>Contribute resource point. Field ID: 2</summary>
        public int ContributeResPoint { get; set; }

        /// <summary>Challenge times. Field ID: 3</summary>
        public byte ChallengeTimes { get; set; }

        /// <summary>Refresh timestamp. Field ID: 4</summary>
        public int RefreshTimestamp { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: CommerceBoatInfo = ReadTlvObject<TlvCommerceBoat>(reader); break;
                    case 2: ContributeResPoint = reader.ReadInt(); break;
                    case 3: ChallengeTimes = reader.ReadByte(); break;
                    case 4: RefreshTimestamp = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvObject(writer, 1, CommerceBoatInfo);
            WriteTlvInt(writer, 2, ContributeResPoint);
            WriteTlvByte(writer, 3, ChallengeTimes);
            WriteTlvInt(writer, 4, RefreshTimestamp);
        }
    }
}
