using Rathalos.Core.Utils.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for sculpture score entry (score + hisCount + name).
    /// C++ Reader: crygame.dll+sub_101390C0 (UnkTlv0057 internal)
    /// </summary>
    public class TlvSculptureScoreEntry : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Field ID: 1</summary>
        public int Score { get; set; }

        /// <summary>Field ID: 2</summary>
        public int HisCount { get; set; }

        /// <summary>Field ID: 3</summary>
        public string Name { get; set; } = string.Empty;

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Score = reader.ReadInt(); break;
                    case 2: HisCount = reader.ReadInt(); break;
                    case 3: { int len = reader.ReadInt(); Name = len > 0 ? Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Score);
            WriteTlvInt(writer, 2, HisCount);
            WriteTlvString(writer, 3, Name);
        }
    }
}
