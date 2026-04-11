using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for commerce boat information.
    /// C++ Reader: crygame.dll+sub_1011B450 (UnkTlv0017)
    /// C++ Printer: crygame.dll+sub_1011B670
    /// </summary>
    public class TlvCommerceBoat : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Commerce boat identifier.
        /// Field ID: 1
        /// </summary>
        public uint CommerceBoatId { get; set; }

        /// <summary>
        /// Commerce boat start time.
        /// Field ID: 2
        /// </summary>
        public int CommerceBoatStartTime { get; set; }

        /// <summary>
        /// Commerce boat status.
        /// Field ID: 3
        /// </summary>
        public int CommerceBoatStatus { get; set; }

        /// <summary>
        /// Level identifier.
        /// Field ID: 4
        /// </summary>
        public int LevelId { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: CommerceBoatId = (uint)reader.ReadInt(); break;
                    case 2: CommerceBoatStartTime = reader.ReadInt(); break;
                    case 3: CommerceBoatStatus = reader.ReadInt(); break;
                    case 4: LevelId = reader.ReadInt(); break;
                    case 5: SkipTlvField(reader, wireType); break; // grabInfo - nested structure
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, (int)CommerceBoatId);
            WriteTlvInt(writer, 2, CommerceBoatStartTime);
            WriteTlvInt(writer, 3, CommerceBoatStatus);
            WriteTlvInt(writer, 4, LevelId);
        }
    }
}
