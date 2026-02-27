using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Structures
{
    /// <summary>
    /// 战斗伤害消息
    /// </summary>
    public class BattleDMG : Structure, ICsStructure
    {
        public BattleDMG()
        {
            shooterId = 0;
            targetId = 0;
            weaponId = 0;
            projectileId = 0;
            material = 0;
            type = 0;
            bulletType = 0;
            damageMin = 0.0f;
            pierce = 0.0f;
            partId = 0;
            pos = new CSVec3();
            lpos = new CSVec3();
            dir = new CSVec3();
            normal = new CSVec3();
            lnorm = new CSVec3();
            attackDir = new CSVec3();
            tScarDir = new CSVec3();
            tPos = new CSVec3();
            tUp = new CSVec3();
            tNormal = new CSVec3();
            localnormangle = new CSVec3();
            shakeStrength = 0.0f;
            shakeDurationTime = 0.0f;
            shakeStillTime = 0.0f;
            projectileClassId = 0;
            weaponClassId = 0;
            remote = 0;
            damageLevel = 0;
            attackType = 0;
            hitType = 0;
            defenseResult = 0;
            HitIndex = 0;
            shooterSrvId = 0;
            targetSrvId = 0;
            weaponSrvId = 0;
            projectileSrvId = 0;
            hashWeaponClass = 0;
            hashFireMode = 0;
            hashAttacker = 0;
            hashMeleeParams = 0;
            hashCurEvent = 0;
            skillResID = 0;
            skillSeq = 0;
            curStamina = 0.0f;
        }

        /// <summary>
        /// Entity ID of the shooter
        /// </summary>
        public uint shooterId;

        /// <summary>
        /// EntityId of the target which got shot
        /// </summary>
        public uint targetId;

        /// <summary>
        /// EntityId of the weapon
        /// </summary>
        public uint weaponId;

        /// <summary>
        /// 0 if hit was not caused by a projectile
        /// </summary>
        public uint projectileId;

        /// <summary>
        /// material id of the surface which got hit
        /// </summary>
        public int material;

        /// <summary>
        /// type id of the hit, see IGameRules::GetHitTypeId for more information
        /// </summary>
        public int type;

        /// <summary>
        /// type of bullet, if hit was of type bullet
        /// </summary>
        public int bulletType;

        public float damageMin;

        /// <summary>
        /// bullet pierceability
        /// </summary>
        public float pierce;

        public int partId;

        /// <summary>
        /// position of the hit
        /// </summary>
        public CSVec3 pos;

        /// <summary>
        /// local position of the hit
        /// </summary>
        public CSVec3 lpos;

        public CSVec3 dir;

        public CSVec3 normal;

        public CSVec3 lnorm;

        /// <summary>
        /// attack direction, namely the moving direction of weapon slash.
        /// </summary>
        public CSVec3 attackDir;

        public CSVec3 tScarDir;

        public CSVec3 tPos;

        public CSVec3 tUp;

        public CSVec3 tNormal;

        public CSVec3 localnormangle;

        public float shakeStrength;

        public float shakeDurationTime;

        public float shakeStillTime;

        public ushort projectileClassId;

        public ushort weaponClassId;

        public int remote;

        public int damageLevel;

        /// <summary>
        /// 0-normal, 1-trap, 2-flashRocket, 3-sleepCutter
        /// </summary>
        public uint attackType;

        public uint hitType;

        /// <summary>
        /// Defense result: 0 No defense 1-5 defense level
        /// </summary>
        public int defenseResult;

        /// <summary>
        /// 攻击附加伤害索引,提供给客户端索引伤害数值的位置
        /// </summary>
        public int HitIndex;

        public int shooterSrvId;

        public int targetSrvId;

        public int weaponSrvId;

        public int projectileSrvId;

        /// <summary>
        /// 武器类别
        /// </summary>
        public uint hashWeaponClass;

        /// <summary>
        /// Firemode
        /// </summary>
        public uint hashFireMode;

        /// <summary>
        /// 攻击数据
        /// </summary>
        public uint hashAttacker;

        /// <summary>
        /// 包围盒数据
        /// </summary>
        public uint hashMeleeParams;

        /// <summary>
        /// 当前攻击事件
        /// </summary>
        public uint hashCurEvent;

        /// <summary>
        /// 技能id
        /// </summary>
        public int skillResID;

        /// <summary>
        /// 技能序列号
        /// </summary>
        public long skillSeq;

        /// <summary>
        /// 玩家防御时耐力
        /// </summary>
        public float curStamina;

        public void WriteCs(IBuffer buffer)
        {
            WriteUInt32(buffer, shooterId);
            WriteUInt32(buffer, targetId);
            WriteUInt32(buffer, weaponId);
            WriteUInt32(buffer, projectileId);
            WriteInt32(buffer, material);
            WriteInt32(buffer, type);
            WriteInt32(buffer, bulletType);
            WriteFloat(buffer, damageMin);
            WriteFloat(buffer, pierce);
            WriteInt32(buffer, partId);
            pos.WriteCs(buffer);
            lpos.WriteCs(buffer);
            dir.WriteCs(buffer);
            normal.WriteCs(buffer);
            lnorm.WriteCs(buffer);
            attackDir.WriteCs(buffer);
            tScarDir.WriteCs(buffer);
            tPos.WriteCs(buffer);
            tUp.WriteCs(buffer);
            tNormal.WriteCs(buffer);
            localnormangle.WriteCs(buffer);
            WriteFloat(buffer, shakeStrength);
            WriteFloat(buffer, shakeDurationTime);
            WriteFloat(buffer, shakeStillTime);
            WriteUInt16(buffer, projectileClassId);
            WriteUInt16(buffer, weaponClassId);
            WriteInt32(buffer, remote);
            WriteInt32(buffer, damageLevel);
            WriteUInt32(buffer, attackType);
            WriteUInt32(buffer, hitType);
            WriteInt32(buffer, defenseResult);
            WriteInt32(buffer, HitIndex);
            WriteInt32(buffer, shooterSrvId);
            WriteInt32(buffer, targetSrvId);
            WriteInt32(buffer, weaponSrvId);
            WriteInt32(buffer, projectileSrvId);
            WriteUInt32(buffer, hashWeaponClass);
            WriteUInt32(buffer, hashFireMode);
            WriteUInt32(buffer, hashAttacker);
            WriteUInt32(buffer, hashMeleeParams);
            WriteUInt32(buffer, hashCurEvent);
            WriteInt32(buffer, skillResID);
            WriteInt64(buffer, skillSeq);
            WriteFloat(buffer, curStamina);
        }

        public void ReadCs(IBuffer buffer)
        {
            shooterId = ReadUInt32(buffer);
            targetId = ReadUInt32(buffer);
            weaponId = ReadUInt32(buffer);
            projectileId = ReadUInt32(buffer);
            material = ReadInt32(buffer);
            type = ReadInt32(buffer);
            bulletType = ReadInt32(buffer);
            damageMin = ReadFloat(buffer);
            pierce = ReadFloat(buffer);
            partId = ReadInt32(buffer);
            pos.ReadCs(buffer);
            lpos.ReadCs(buffer);
            dir.ReadCs(buffer);
            normal.ReadCs(buffer);
            lnorm.ReadCs(buffer);
            attackDir.ReadCs(buffer);
            tScarDir.ReadCs(buffer);
            tPos.ReadCs(buffer);
            tUp.ReadCs(buffer);
            tNormal.ReadCs(buffer);
            localnormangle.ReadCs(buffer);
            shakeStrength = ReadFloat(buffer);
            shakeDurationTime = ReadFloat(buffer);
            shakeStillTime = ReadFloat(buffer);
            projectileClassId = ReadUInt16(buffer);
            weaponClassId = ReadUInt16(buffer);
            remote = ReadInt32(buffer);
            damageLevel = ReadInt32(buffer);
            attackType = ReadUInt32(buffer);
            hitType = ReadUInt32(buffer);
            defenseResult = ReadInt32(buffer);
            HitIndex = ReadInt32(buffer);
            shooterSrvId = ReadInt32(buffer);
            targetSrvId = ReadInt32(buffer);
            weaponSrvId = ReadInt32(buffer);
            projectileSrvId = ReadInt32(buffer);
            hashWeaponClass = ReadUInt32(buffer);
            hashFireMode = ReadUInt32(buffer);
            hashAttacker = ReadUInt32(buffer);
            hashMeleeParams = ReadUInt32(buffer);
            hashCurEvent = ReadUInt32(buffer);
            skillResID = ReadInt32(buffer);
            skillSeq = ReadInt64(buffer);
            curStamina = ReadFloat(buffer);
        }

    }
}
