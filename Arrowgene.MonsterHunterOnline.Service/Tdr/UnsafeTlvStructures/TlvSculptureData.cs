using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for sculpture data (id, round, best, histories, currents, avatar).
    /// C++ Reader: crygame.dll+sub_1013D700 (UnkTlv0057)
    /// C++ Printer: crygame.dll+sub_1013DF60
    /// </summary>
    public class TlvSculptureData : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvInt32(buffer, 2, Round);
            WriteTlvSubStructure(buffer, 3, Best);
            WriteTlvSubStructure(buffer, 4, Histories);
            WriteTlvSubStructure(buffer, 5, Currents);
            WriteTlvSubStructure(buffer, 6, Avatar);
        }
    }
}
