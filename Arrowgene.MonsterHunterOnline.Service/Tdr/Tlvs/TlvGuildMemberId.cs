using Rathalos.Core.Utils.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guild member identity (name + dbid + rtid + uin).
    /// C++ Reader: crygame.dll+sub_10118360 (UnkTlv0024 internal)
    /// C++ Printer: crygame.dll+sub_10118580
    /// </summary>
    public class TlvGuildMemberId : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Field ID: 1</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Field ID: 2</summary>
        public long Dbid { get; set; }

        /// <summary>Field ID: 3</summary>
        public int Rtid { get; set; }

        /// <summary>Field ID: 4</summary>
        public long Uin { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: { int len = reader.ReadInt(); Name = len > 0 ? Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 2: Dbid = reader.ReadLong(); break;
                    case 3: Rtid = reader.ReadInt(); break;
                    case 4: Uin = reader.ReadLong(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvString(writer, 1, Name);
            WriteTlvLong(writer, 2, Dbid);
            WriteTlvInt(writer, 3, Rtid);
            WriteTlvLong(writer, 4, Uin);
        }
    }
}
