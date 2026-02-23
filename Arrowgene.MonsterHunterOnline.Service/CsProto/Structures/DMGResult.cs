using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Constant;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Structures
{
    /// <summary>
    /// 服务器发送到客户端的伤害结果返回
    /// </summary>
    public class DMGResult : Structure, ICsStructure
    {
        public DMGResult()
        {
            DamageResult = 0.0f;
            WaterDamageResult = 0.0f;
            FireDamageResult = 0.0f;
            ElectricDamageResult = 0.0f;
            DragonDamageResult = 0.0f;
            IceDamageResult = 0.0f;
            NonDamageResult = 0.0f;
            PIYOResut = 0;
            StaminaResult = 0;
            DamageMode = 0;
            DefenceLevel = 0;
            InjuryLevel = 0;
            TanDao = 0;
            Attack_levelP = 0;
            HitIndex = 0;
            AttackLogicEntityId = 0;
            HitLogicEntityId = 0;
            DefenceLogicEntityId = 0;
            DamageGener = 0.0f;
            WaterDamageGener = 0.0f;
            FireDamageGener = 0.0f;
            ElectricDamageGener = 0.0f;
            DragonDamageGener = 0.0f;
            IceDamageGener = 0.0f;
            PartId = 0;
            SkillResID = 0;
            ItemType = 0;
            AttackDataID = 0;
            Dir = new CSVec3();
            Pos = new CSVec3();
            Normal = new CSVec3();
            StateBuffID = new int[CsProtoConstant.CS_STATE_BUFF_COUNT];
        }

        /// <summary>
        /// 物理伤害
        /// </summary>
        public float DamageResult;

        /// <summary>
        /// 水属性伤害
        /// </summary>
        public float WaterDamageResult;

        /// <summary>
        /// 火属性伤害
        /// </summary>
        public float FireDamageResult;

        /// <summary>
        /// 雷属性伤害
        /// </summary>
        public float ElectricDamageResult;

        /// <summary>
        /// 龙属性伤害
        /// </summary>
        public float DragonDamageResult;

        /// <summary>
        /// 冰属性伤害
        /// </summary>
        public float IceDamageResult;

        /// <summary>
        /// 无属性伤害
        /// </summary>
        public float NonDamageResult;

        /// <summary>
        /// 晕眩值
        /// </summary>
        public int PIYOResut;

        /// <summary>
        /// 灭气
        /// </summary>
        public int StaminaResult;

        /// <summary>
        /// 负会心 普通 会心
        /// </summary>
        public int DamageMode;

        /// <summary>
        /// 防御等级
        /// </summary>
        public int DefenceLevel;

        /// <summary>
        /// 伤害等级
        /// </summary>
        public int InjuryLevel;

        /// <summary>
        /// 弹刀
        /// </summary>
        public short TanDao;

        /// <summary>
        /// 对人攻击霸体值
        /// </summary>
        public int Attack_levelP;

        /// <summary>
        /// 伤害表现索引
        /// </summary>
        public int HitIndex;

        /// <summary>
        /// 攻击者ID
        /// </summary>
        public int AttackLogicEntityId;

        /// <summary>
        /// 命中者ID
        /// </summary>
        public int HitLogicEntityId;

        /// <summary>
        /// 防御者ID
        /// </summary>
        public int DefenceLogicEntityId;

        /// <summary>
        /// 物理伤害肉质
        /// </summary>
        public float DamageGener;

        /// <summary>
        /// 水属性伤害肉质
        /// </summary>
        public float WaterDamageGener;

        /// <summary>
        /// 火属性伤害肉质
        /// </summary>
        public float FireDamageGener;

        /// <summary>
        /// 雷属性伤害肉质
        /// </summary>
        public float ElectricDamageGener;

        /// <summary>
        /// 龙属性伤害肉质
        /// </summary>
        public float DragonDamageGener;

        /// <summary>
        /// 冰属性伤害肉质
        /// </summary>
        public float IceDamageGener;

        /// <summary>
        /// 命中部位ID
        /// </summary>
        public int PartId;

        /// <summary>
        /// 技能唯一ID
        /// </summary>
        public int SkillResID;

        /// <summary>
        /// 物品
        /// </summary>
        public int ItemType;

        /// <summary>
        /// DamageInfoID
        /// </summary>
        public int AttackDataID;

        /// <summary>
        /// 命中方向
        /// </summary>
        public CSVec3 Dir;

        /// <summary>
        /// 相对命中点
        /// </summary>
        public CSVec3 Pos;

        /// <summary>
        /// normal
        /// </summary>
        public CSVec3 Normal;

        /// <summary>
        /// 异常状态Buff列表
        /// </summary>
        public int[] StateBuffID;

        public void WriteCs(IBuffer buffer)
        {
            WriteFloat(buffer, DamageResult);
            WriteFloat(buffer, WaterDamageResult);
            WriteFloat(buffer, FireDamageResult);
            WriteFloat(buffer, ElectricDamageResult);
            WriteFloat(buffer, DragonDamageResult);
            WriteFloat(buffer, IceDamageResult);
            WriteFloat(buffer, NonDamageResult);
            WriteInt32(buffer, PIYOResut);
            WriteInt32(buffer, StaminaResult);
            WriteInt32(buffer, DamageMode);
            WriteInt32(buffer, DefenceLevel);
            WriteInt32(buffer, InjuryLevel);
            WriteInt16(buffer, TanDao);
            WriteInt32(buffer, Attack_levelP);
            WriteInt32(buffer, HitIndex);
            WriteInt32(buffer, AttackLogicEntityId);
            WriteInt32(buffer, HitLogicEntityId);
            WriteInt32(buffer, DefenceLogicEntityId);
            WriteFloat(buffer, DamageGener);
            WriteFloat(buffer, WaterDamageGener);
            WriteFloat(buffer, FireDamageGener);
            WriteFloat(buffer, ElectricDamageGener);
            WriteFloat(buffer, DragonDamageGener);
            WriteFloat(buffer, IceDamageGener);
            WriteInt32(buffer, PartId);
            WriteInt32(buffer, SkillResID);
            WriteInt32(buffer, ItemType);
            WriteInt32(buffer, AttackDataID);
            Dir.WriteCs(buffer);
            Pos.WriteCs(buffer);
            Normal.WriteCs(buffer);
            for (int i = 0; i < CsProtoConstant.CS_STATE_BUFF_COUNT; i++)
            {
                WriteInt32(buffer, StateBuffID[i]);
            }
        }

        public void ReadCs(IBuffer buffer)
        {
            DamageResult = ReadFloat(buffer);
            WaterDamageResult = ReadFloat(buffer);
            FireDamageResult = ReadFloat(buffer);
            ElectricDamageResult = ReadFloat(buffer);
            DragonDamageResult = ReadFloat(buffer);
            IceDamageResult = ReadFloat(buffer);
            NonDamageResult = ReadFloat(buffer);
            PIYOResut = ReadInt32(buffer);
            StaminaResult = ReadInt32(buffer);
            DamageMode = ReadInt32(buffer);
            DefenceLevel = ReadInt32(buffer);
            InjuryLevel = ReadInt32(buffer);
            TanDao = ReadInt16(buffer);
            Attack_levelP = ReadInt32(buffer);
            HitIndex = ReadInt32(buffer);
            AttackLogicEntityId = ReadInt32(buffer);
            HitLogicEntityId = ReadInt32(buffer);
            DefenceLogicEntityId = ReadInt32(buffer);
            DamageGener = ReadFloat(buffer);
            WaterDamageGener = ReadFloat(buffer);
            FireDamageGener = ReadFloat(buffer);
            ElectricDamageGener = ReadFloat(buffer);
            DragonDamageGener = ReadFloat(buffer);
            IceDamageGener = ReadFloat(buffer);
            PartId = ReadInt32(buffer);
            SkillResID = ReadInt32(buffer);
            ItemType = ReadInt32(buffer);
            AttackDataID = ReadInt32(buffer);
            Dir.ReadCs(buffer);
            Pos.ReadCs(buffer);
            Normal.ReadCs(buffer);
            for (int i = 0; i < CsProtoConstant.CS_STATE_BUFF_COUNT; i++)
            {
                StateBuffID[i] = ReadInt32(buffer);
            }
        }
    }
}
