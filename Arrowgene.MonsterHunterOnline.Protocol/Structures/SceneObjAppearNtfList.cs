using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// scene obj appear notify list
    /// </summary>
    public class SceneObjAppearNtfList : Structure, ICsStructure
    {
        public SceneObjAppearNtfList()
        {
            Appear = new List<SceneObjAppearNtf>();
        }

        public List<SceneObjAppearNtf> Appear { get; }

        public  void WriteCs(IBuffer buffer)
        {
            WriteList(buffer, Appear, CsProtoConstant.CS_MAX_APPEAR_NTF_NUM, WriteInt32, WriteCsStructure);
        }

        public void ReadCs(IBuffer buffer)
        {
            ReadList(buffer, Appear, CsProtoConstant.CS_MAX_APPEAR_NTF_NUM, ReadInt32, ReadCsStructure<SceneObjAppearNtf>);
        }
    }
}