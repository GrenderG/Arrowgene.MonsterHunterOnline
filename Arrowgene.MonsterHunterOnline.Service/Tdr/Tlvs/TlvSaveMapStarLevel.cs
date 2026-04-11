using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for save map + star level container.
    /// C++ Reader: crygame.dll+sub_1019B5E0 (UnkTlv0171)
    /// C++ Printer: crygame.dll+sub_1019B8A0
    /// </summary>
    public class TlvSaveMapStarLevel : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Save map (level position data).
        /// Field ID: 2
        /// </summary>
        public TlvLevelPosition SaveMap { get; set; } = new();

        /// <summary>
        /// Star level data.
        /// Field ID: 3
        /// </summary>
        public TlvStarLevelData StarLevel { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: SaveMap = ReadTlvObject<TlvLevelPosition>(reader); break;
                    case 3: StarLevel = ReadTlvObject<TlvStarLevelData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvObject(writer, 2, SaveMap);
            WriteTlvObject(writer, 3, StarLevel);
        }
    }
}
