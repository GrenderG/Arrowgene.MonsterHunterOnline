using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for sculpture data (id, round, best, histories, currents, avatar).
    /// C++ Reader: crygame.dll+sub_1013D700 (UnkTlv0057)
    /// C++ Printer: crygame.dll+sub_1013DF60
    /// </summary>
    public class TlvSculptureData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Field ID: 1</summary>
        public int Id { get; set; }

        /// <summary>Field ID: 2</summary>
        public int Round { get; set; }

        /// <summary>Best score entry. Field ID: 3</summary>
        public TlvSculptureScoreEntry Best { get; set; } = new();

        /// <summary>History list. Field ID: 4</summary>
        public TlvSculptureHistoryList Histories { get; set; } = new();

        /// <summary>Current list. Field ID: 5</summary>
        public TlvSculptureCurrentList Currents { get; set; } = new();

        /// <summary>Avatar brief info. Field ID: 6</summary>
        public TlvAvatarBriefInfo Avatar { get; set; } = new();

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
                    case 2: Round = reader.ReadInt(); break;
                    case 3: Best = ReadTlvObject<TlvSculptureScoreEntry>(reader); break;
                    case 4: Histories = ReadTlvObject<TlvSculptureHistoryList>(reader); break;
                    case 5: Currents = ReadTlvObject<TlvSculptureCurrentList>(reader); break;
                    case 6: Avatar = ReadTlvObject<TlvAvatarBriefInfo>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Id);
            WriteTlvInt(writer, 2, Round);
            WriteTlvObject(writer, 3, Best);
            WriteTlvObject(writer, 4, Histories);
            WriteTlvObject(writer, 5, Currents);
            WriteTlvObject(writer, 6, Avatar);
        }
    }
}
