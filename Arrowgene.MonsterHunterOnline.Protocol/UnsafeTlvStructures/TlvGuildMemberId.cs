using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for guild member identity (name + dbid + rtid + uin).
    /// C++ Reader: crygame.dll+sub_10118360 (UnkTlv0024 internal)
    /// C++ Printer: crygame.dll+sub_10118580
    /// </summary>
    public class TlvGuildMemberId : Structure, ITlvStructure
    {
        /// <summary>Field ID: 1</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Field ID: 2</summary>
        public long Dbid { get; set; }

        /// <summary>Field ID: 3</summary>
        public int Rtid { get; set; }

        /// <summary>Field ID: 4</summary>
        public long Uin { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvString(buffer, 1, Name);
            WriteTlvInt64(buffer, 2, Dbid);
            WriteTlvInt32(buffer, 3, Rtid);
            WriteTlvInt64(buffer, 4, Uin);
        }
    }
}
