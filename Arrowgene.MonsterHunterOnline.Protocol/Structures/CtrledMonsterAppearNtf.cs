using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// monster appear notify
    /// </summary>
    public class CtrledMonsterAppearNtf : Structure, ICsStructure
    {
        public CtrledMonsterAppearNtf()
        {
            BaseInfo = new MonsterAppearNtf();
            OwnerId = 0;
            Type = 0;
            Duration = 0.0f;
        }

        /// <summary>
        /// common monster appare info
        /// </summary>
        public MonsterAppearNtf BaseInfo;

        /// <summary>
        /// Owner logic entity id
        /// </summary>
        public int OwnerId;

        /// <summary>
        /// Control type, 0 - MvM, 1 - P+M
        /// </summary>
        public int Type;

        /// <summary>
        /// Pet existance time
        /// </summary>
        public float Duration;

        public void WriteCs(IBuffer buffer)
        {
            BaseInfo.WriteCs(buffer);
            WriteInt32(buffer, OwnerId);
            WriteInt32(buffer, Type);
            WriteFloat(buffer, Duration);
        }

        public void ReadCs(IBuffer buffer)
        {
            BaseInfo.ReadCs(buffer);
            OwnerId = ReadInt32(buffer);
            Type = ReadInt32(buffer);
            Duration = ReadFloat(buffer);
        }
    }
}
