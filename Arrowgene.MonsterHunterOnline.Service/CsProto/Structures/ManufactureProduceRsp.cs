using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Structures
{
    /// <summary>
    /// 根据配方进行生产的返回
    /// </summary>
    public class ManufactureProduceRsp : Structure, ICsStructure
    {
        public ManufactureProduceRsp()
        {
            Ret = 0;
            ItemID = 0;
            BindFlag = 0;
        }

        /// <summary>
        /// 返回值，0为成功
        /// </summary>
        public int Ret;

        /// <summary>
        /// 物品ID
        /// </summary>
        public int ItemID;

        /// <summary>
        /// 是否绑定
        /// </summary>
        public int BindFlag;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, Ret);
            WriteInt32(buffer, ItemID);
            WriteInt32(buffer, BindFlag);
        }

        public void ReadCs(IBuffer buffer)
        {
            Ret = ReadInt32(buffer);
            ItemID = ReadInt32(buffer);
            BindFlag = ReadInt32(buffer);
        }
    }
}
