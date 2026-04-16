using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    public class RemoteDataLoadReq : Structure, ICsStructure
    {
        public RemoteDataLoadReq()
        {
            RemoteDataType = 0;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public ROMTE_DATA_TYPE RemoteDataType { get; set; }

        public  void WriteCs(IBuffer buffer)
        {
            WriteUInt16(buffer, (ushort)RemoteDataType);
        }

        public void ReadCs(IBuffer buffer)
        {
            RemoteDataType = (ROMTE_DATA_TYPE)ReadUInt16(buffer);
        }
    }
}