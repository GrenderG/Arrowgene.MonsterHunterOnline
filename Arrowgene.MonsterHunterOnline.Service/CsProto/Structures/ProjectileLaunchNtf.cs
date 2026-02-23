using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System.Collections.Generic;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Structures
{
    /// <summary>
    /// 飞行道具发射
    /// </summary>
    public class ProjectileLaunchNtf : Structure, ICsStructure
    {
        public ProjectileLaunchNtf()
        {
            SyncTime = 0;
            NetID = 0;
            LauncherID = 0;
            VehicleID = 0;
            TypeID = 0;
            pos = new CSVec3();
            dir = new CSVec3();
            additiveVel = new CSVec3();
            skillId = 0;
            itemId = 0;
            delay = 0.0f;
            speedScale = 0.0f;
            damageScale = 0.0f;
            overrideTrail = 0;
            acc = new CSVec3();
            vel = new CSVec3();
            radius = 0.0f;
            gravityChangeTime = 0.0f;
            additiveGravity = 0.0f;
            launchType = 0;
            additiveAccXYZMode = 0;
            additiveAccXYZ = new List<CSVec3>();
            additiveAccTime = new List<float>();
        }

        /// <summary>
        /// 同步时间
        /// </summary>
        public long SyncTime;

        /// <summary>
        /// 飞行道具id
        /// </summary>
        public int NetID;

        /// <summary>
        /// 释放飞行道具者id
        /// </summary>
        public int LauncherID;

        /// <summary>
        /// 用于发射的载具id
        /// </summary>
        public uint VehicleID;

        /// <summary>
        /// 飞行道具类型id
        /// </summary>
        public int TypeID;

        /// <summary>
        /// pose init
        /// </summary>
        public CSVec3 pos;

        /// <summary>
        /// direction init
        /// </summary>
        public CSVec3 dir;

        /// <summary>
        /// 外界附加速度
        /// </summary>
        public CSVec3 additiveVel;

        /// <summary>
        /// 发射技能索引
        /// </summary>
        public int skillId;

        /// <summary>
        /// 生成时使用的道具Id（如果有）
        /// </summary>
        public int itemId;

        /// <summary>
        /// the time between spawn and launch
        /// </summary>
        public float delay;

        /// <summary>
        /// speed scale on static data speed
        /// </summary>
        public float speedScale;

        /// <summary>
        /// damage scale to calculate damage
        /// </summary>
        public float damageScale;

        /// <summary>
        /// bool变量，是否用以下参数覆盖excel中对应参数
        /// </summary>
        public int overrideTrail;

        /// <summary>
        /// world加速度（未附加z轴旋转）
        /// </summary>
        public CSVec3 acc;

        /// <summary>
        /// 初速度
        /// </summary>
        public CSVec3 vel;

        /// <summary>
        /// 判定半径大小
        /// </summary>
        public float radius;

        /// <summary>
        /// 重力从该时刻后改变（仅弓用）
        /// </summary>
        public float gravityChangeTime;

        /// <summary>
        /// 重力改变附加值
        /// </summary>
        public float additiveGravity;

        /// <summary>
        /// 发射方式（用于验证和同步，详见ProjLaunchParams.h）
        /// </summary>
        public int launchType;

        /// <summary>
        /// bool变量，是否用XYZ轴的额外加速度
        /// </summary>
        public int additiveAccXYZMode;

        public List<CSVec3> additiveAccXYZ;

        public List<float> additiveAccTime;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt64(buffer, SyncTime);
            WriteInt32(buffer, NetID);
            WriteInt32(buffer, LauncherID);
            WriteUInt32(buffer, VehicleID);
            WriteInt32(buffer, TypeID);
            pos.WriteCs(buffer);
            dir.WriteCs(buffer);
            additiveVel.WriteCs(buffer);
            WriteInt32(buffer, skillId);
            WriteInt32(buffer, itemId);
            WriteFloat(buffer, delay);
            WriteFloat(buffer, speedScale);
            WriteFloat(buffer, damageScale);
            WriteInt32(buffer, overrideTrail);
            acc.WriteCs(buffer);
            vel.WriteCs(buffer);
            WriteFloat(buffer, radius);
            WriteFloat(buffer, gravityChangeTime);
            WriteFloat(buffer, additiveGravity);
            WriteInt32(buffer, launchType);
            WriteInt32(buffer, additiveAccXYZMode);
            int additiveAccXYZCount = (int)additiveAccXYZ.Count;
            WriteInt32(buffer, additiveAccXYZCount);
            for (int i = 0; i < additiveAccXYZCount; i++)
            {
                additiveAccXYZ[i].WriteCs(buffer);
            }
            int additiveAccTimeCount = (int)additiveAccTime.Count;
            buffer.WriteInt32(additiveAccTimeCount);
            for (int i = 0; i < additiveAccTimeCount; i++)
            {
                buffer.WriteFloat(additiveAccTime[i]);
            }
        }

        public void ReadCs(IBuffer buffer)
        {
            SyncTime = ReadInt64(buffer);
            NetID = ReadInt32(buffer);
            LauncherID = ReadInt32(buffer);
            VehicleID = ReadUInt32(buffer);
            TypeID = ReadInt32(buffer);
            pos.ReadCs(buffer);
            dir.ReadCs(buffer);
            additiveVel.ReadCs(buffer);
            skillId = ReadInt32(buffer);
            itemId = ReadInt32(buffer);
            delay = ReadFloat(buffer);
            speedScale = ReadFloat(buffer);
            damageScale = ReadFloat(buffer);
            overrideTrail = ReadInt32(buffer);
            acc.ReadCs(buffer);
            vel.ReadCs(buffer);
            radius = ReadFloat(buffer);
            gravityChangeTime = ReadFloat(buffer);
            additiveGravity = ReadFloat(buffer);
            launchType = ReadInt32(buffer);
            additiveAccXYZMode = ReadInt32(buffer);
            additiveAccXYZ.Clear();
            int additiveAccXYZCount = ReadInt32(buffer);
            for (int i = 0; i < additiveAccXYZCount; i++)
            {
                CSVec3 additiveAccXYZEntry = new CSVec3();
                additiveAccXYZEntry.ReadCs(buffer);
                additiveAccXYZ.Add(additiveAccXYZEntry);
            }
            additiveAccTime.Clear();
            int additiveAccTimeCount = ReadInt32(buffer);
            for (int i = 0; i < additiveAccTimeCount; i++)
            {
                float additiveAccTimeEntry = ReadFloat(buffer);
                additiveAccTime.Add(additiveAccTimeEntry);
            }
        }
    }
}
