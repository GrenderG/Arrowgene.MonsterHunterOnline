using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// 切换弹药响应
    /// </summary>
    public class ChangeAmmoRsp : Structure, ICsStructure
    {
        public ChangeAmmoRsp()
        {
            NetID = 0;
            TypeID = 0;
        }

        /// <summary>
        /// 装载者id
        /// </summary>
        public int NetID;

        /// <summary>
        /// 弹药类型id
        /// </summary>
        public int TypeID;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, NetID);
            WriteInt32(buffer, TypeID);
        }

        public void ReadCs(IBuffer buffer)
        {
            NetID = ReadInt32(buffer);
            TypeID = ReadInt32(buffer);
        }
    }
}
