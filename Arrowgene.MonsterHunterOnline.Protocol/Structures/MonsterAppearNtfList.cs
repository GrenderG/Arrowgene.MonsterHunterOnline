using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// monster appear notify list
    /// </summary>
    public class MonsterAppearNtfList : Structure, ICsStructure
    {
        public MonsterAppearNtfList()
        {
            Appear = new List<MonsterAppearNtf>();
        }

        public List<MonsterAppearNtf> Appear { get; }

        public  void WriteCs(IBuffer buffer)
        {
            WriteList(buffer, Appear, CsProtoConstant.CS_MAX_APPEAR_NTF_NUM, WriteInt32, WriteCsStructure);
        }

        public void ReadCs(IBuffer buffer)
        {
            ReadList(buffer, Appear, CsProtoConstant.CS_MAX_APPEAR_NTF_NUM, ReadInt32, ReadCsStructure<MonsterAppearNtf>);
        }
    }
}