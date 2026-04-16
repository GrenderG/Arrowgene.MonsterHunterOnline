using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// 战斗伤害消息
    /// </summary>
    public class BattlePVPDMG : Structure, ICsStructure
    {
        public BattlePVPDMG()
        {
            SyncTime = 0;
            Sequence = 0;
            HitInfo = new BattleDMG();
            DamageResult = new DMGResult();
        }

        /// <summary>
        /// 同步时间
        /// </summary>
        public long SyncTime;

        /// <summary>
        /// 序列
        /// </summary>
        public long Sequence;

        /// <summary>
        /// 打击信息
        /// </summary>
        public BattleDMG HitInfo;

        /// <summary>
        /// 打击结果
        /// </summary>
        public DMGResult DamageResult;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt64(buffer, SyncTime);
            WriteInt64(buffer, Sequence);
            HitInfo.WriteCs(buffer);
            DamageResult.WriteCs(buffer);
        }

        public void ReadCs(IBuffer buffer)
        {
            SyncTime = ReadInt64(buffer);
            Sequence = ReadInt64(buffer);
            HitInfo.ReadCs(buffer);
            DamageResult.ReadCs(buffer);
        }

    }
}
