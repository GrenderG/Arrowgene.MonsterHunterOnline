using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// 进入战斗副本验证响应
    /// </summary>
    public class InstanceVerifyRsp : Structure, ICsStructure
    {
        public InstanceVerifyRsp()
        {
            ErrNo = 0;
        }

        /// <summary>
        /// 响应码, 0为成功
        /// </summary>
        public int ErrNo { get; set; }

        public void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, ErrNo);
        }

        public void ReadCs(IBuffer buffer)
        {
            ErrNo = ReadInt32(buffer);
        }
    }
}