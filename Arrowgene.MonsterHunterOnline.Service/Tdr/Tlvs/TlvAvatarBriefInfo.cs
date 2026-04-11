using Rathalos.Core.Utils.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for player avatar brief info (sex, dbid, name, equips, attrs).
    /// C++ Reader: crygame.dll+sub_1013CE90 (UnkTlv0056)
    /// C++ Printer: crygame.dll+sub_1013D300
    /// </summary>
    public class TlvAvatarBriefInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Field ID: 1</summary>
        public int Sex { get; set; }

        /// <summary>Field ID: 2</summary>
        public long Dbid { get; set; }

        /// <summary>Field ID: 3</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Field ID: 4</summary>
        public TlvEquips Equips { get; set; } = new();

        /// <summary>Field ID: 5</summary>
        public TlvAttrData Attrs { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Sex = reader.ReadInt(); break;
                    case 2: Dbid = reader.ReadLong(); break;
                    case 3: { int len = reader.ReadInt(); Name = len > 0 ? Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 4: Equips = ReadTlvObject<TlvEquips>(reader); break;
                    case 5: Attrs = ReadTlvObject<TlvAttrData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Sex);
            WriteTlvLong(writer, 2, Dbid);
            WriteTlvString(writer, 3, Name);
            WriteTlvObject(writer, 4, Equips);
            WriteTlvObject(writer, 5, Attrs);
        }
    }
}
