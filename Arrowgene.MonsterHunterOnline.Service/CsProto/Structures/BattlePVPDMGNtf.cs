using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Structures
{
    /// <summary>
    /// 战斗伤害消息
    /// </summary>
    public class BattlePVPDMGNtf : Structure, ICsStructure
    {
        public BattlePVPDMGNtf()
        {
            DamageResult = new DMGResult();
        }

        /// <summary>
        /// 打击结果
        /// </summary>
        public DMGResult DamageResult;

        public void WriteCs(IBuffer buffer)
        {
            DamageResult.WriteCs(buffer);
        }

        public void ReadCs(IBuffer buffer)
        {
            DamageResult.ReadCs(buffer);
        }
    }
}
