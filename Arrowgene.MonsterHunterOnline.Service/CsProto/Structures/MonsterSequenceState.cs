using Arrowgene.Buffers;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Structures
{
    /// <summary>
    /// 怪物技能同步消息
    /// </summary>
    public class MonsterSequenceState : Structure, ICsStructure
    {
        public MonsterSequenceState()
        {
            MonsterID = 0;
            AnimSeqName = "";
            CurTime = 0.0f;
            Location = new CSVec3();
            Rotation = new CSQuat();
        }

        /// <summary>
        /// 怪物ID
        /// </summary>
        public uint MonsterID;

        /// <summary>
        /// 技能name
        /// </summary>
        public string AnimSeqName;

        /// <summary>
        /// 技能时间
        /// </summary>
        public float CurTime;

        /// <summary>
        /// 世界坐标系下的位置
        /// </summary>
        public CSVec3 Location;

        /// <summary>
        /// 朝向
        /// </summary>
        public CSQuat Rotation;

        public void WriteCs(IBuffer buffer)
        {
            WriteUInt32(buffer, MonsterID);
            WriteString(buffer, AnimSeqName);
            WriteFloat(buffer, CurTime);
            Location.WriteCs(buffer);
            Rotation.WriteCs(buffer);
        }

        public void ReadCs(IBuffer buffer)
        {
            MonsterID = ReadUInt32(buffer);
            AnimSeqName = ReadString(buffer);
            CurTime = ReadFloat(buffer);
            Location.ReadCs(buffer);
            Rotation.ReadCs(buffer);
        }
    }
}
