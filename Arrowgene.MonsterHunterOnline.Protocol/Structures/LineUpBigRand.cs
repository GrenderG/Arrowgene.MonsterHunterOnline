using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// 大随机匹配请求
    /// </summary>
    public class LineUpBigRand : Structure, ICsStructure
    {
        public LineUpBigRand()
        {
            Type = 0;
        }

        public int Type { get; set; }

        public  void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, Type);
        }

        public void ReadCs(IBuffer buffer)
        {
            Type = ReadInt32(buffer);
        }
    }
}