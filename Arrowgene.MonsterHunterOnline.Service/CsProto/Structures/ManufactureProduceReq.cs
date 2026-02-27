using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Structures
{
    /// <summary>
    /// 根据配方进行生产的请求
    /// </summary>
    public class ManufactureProduceReq : Structure, ICsStructure
    {
        public ManufactureProduceReq()
        {
            ManufactureId = 0;
            ItemPlaceMent = 0;
            BindFlag = 0;
        }

        /// <summary>
        /// 配方ID
        /// </summary>
        public int ManufactureId;

        /// <summary>
        /// 物品生产放置位置1表示包包，2表示仓库
        /// </summary>
        public int ItemPlaceMent;

        /// <summary>
        /// 是否绑定
        /// </summary>
        public int BindFlag;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, ManufactureId);
            WriteInt32(buffer, ItemPlaceMent);
            WriteInt32(buffer, BindFlag);
        }

        public void ReadCs(IBuffer buffer)
        {
            ManufactureId = ReadInt32(buffer);
            ItemPlaceMent = ReadInt32(buffer);
            BindFlag = ReadInt32(buffer);
        }

    }
}
