using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for sculpture current entry (score + dbid + name + uin + time).
    /// C++ Reader: crygame.dll+sub_1013A390 (UnkTlv0057 internal)
    /// </summary>
    public class TlvSculptureCurrentEntry : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Score);
            WriteTlvInt64(buffer, 2, Dbid);
            WriteTlvString(buffer, 3, Name);
            WriteTlvInt64(buffer, 4, Uin);
            WriteTlvInt32(buffer, 5, Time);
        }
    }
}
