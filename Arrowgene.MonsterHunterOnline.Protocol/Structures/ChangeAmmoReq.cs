using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// 切换弹药请求
    /// </summary>
    public class ChangeAmmoReq : Structure, ICsStructure
    {
        public ChangeAmmoReq()
        {
            TypeID = 0;
        }

        /// <summary>
        /// 弹药类型id
        /// </summary>
        public int TypeID;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, TypeID);
        }

        public void ReadCs(IBuffer buffer)
        {
            TypeID = ReadInt32(buffer);
        }
    }
}
