using Arrowgene.Buffers;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// 付费重新捏脸响应
    /// </summary>
    public class ModifyFaceRsp : Structure, ICsStructure
    {
        private static readonly ILogger Logger = LogProvider.Logger(typeof(ModifyFaceRsp));

        public ModifyFaceRsp()
        {
            Result = 0;
        }

        /// <summary>
        /// 结果
        /// </summary>
        public int Result { get; set; }

        public  void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, Result);
        }

        public void ReadCs(IBuffer buffer)
        {
            Result = ReadInt32(buffer);
        }
    }
}