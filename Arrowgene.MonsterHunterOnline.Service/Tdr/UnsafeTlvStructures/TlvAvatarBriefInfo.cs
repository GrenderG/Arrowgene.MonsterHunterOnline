using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for player avatar brief info (sex, dbid, name, equips, attrs).
    /// C++ Reader: crygame.dll+sub_1013CE90 (UnkTlv0056)
    /// C++ Printer: crygame.dll+sub_1013D300
    /// </summary>
    public class TlvAvatarBriefInfo : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Sex);
            WriteTlvInt64(buffer, 2, Dbid);
            WriteTlvString(buffer, 3, Name);
            WriteTlvSubStructure(buffer, 4, Equips);
            WriteTlvSubStructure(buffer, 5, Attrs);
        }
    }
}
