using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for sculpture score entry (score + hisCount + name).
    /// C++ Reader: crygame.dll+sub_101390C0 (UnkTlv0057 internal)
    /// </summary>
    public class TlvSculptureScoreEntry : Structure, ITlvStructure
    {
        /// <summary>Field ID: 1</summary>
        public int Score { get; set; }

        /// <summary>Field ID: 2</summary>
        public int HisCount { get; set; }

        /// <summary>Field ID: 3</summary>
        public string Name { get; set; } = string.Empty;

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Score);
            WriteTlvInt32(buffer, 2, HisCount);
            WriteTlvString(buffer, 3, Name);
        }
    }
}
