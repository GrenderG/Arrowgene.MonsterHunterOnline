using Rathalos.Core.Utils.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for sculpture current entry (score + dbid + name + uin + time).
    /// C++ Reader: crygame.dll+sub_1013A390 (UnkTlv0057 internal)
    /// </summary>
    public class TlvSculptureCurrentEntry : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Field ID: 1</summary>
        public int Score { get; set; }

        /// <summary>Field ID: 2</summary>
        public long Dbid { get; set; }

        /// <summary>Field ID: 3</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Field ID: 4</summary>
        public long Uin { get; set; }

        /// <summary>Field ID: 5</summary>
        public int Time { get; set; }

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
                    case 2: Dbid = reader.ReadLong(); break;
                    case 3: { int len = reader.ReadInt(); Name = len > 0 ? Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 4: Uin = reader.ReadLong(); break;
                    case 5: Time = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Score);
            WriteTlvLong(writer, 2, Dbid);
            WriteTlvString(writer, 3, Name);
            WriteTlvLong(writer, 4, Uin);
            WriteTlvInt(writer, 5, Time);
        }
    }
}
