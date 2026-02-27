using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Structures
{

    /// <summary>
    /// 技能效果同步信息
    /// </summary>
    public class SkillEffectInfo : Structure, ICsStructure
    {
        public SkillEffectInfo()
        {
            EntityId = 0;
            SkillID = 0;
            SkillLevel = 0;
            Type = 0;
            EventName = "";
        }

        /// <summary>
        /// Entity ID
        /// </summary>
        public uint EntityId;

        /// <summary>
        /// 技能ID
        /// </summary>
        public int SkillID;

        /// <summary>
        /// 技能等级
        /// </summary>
        public int SkillLevel;

        /// <summary>
        /// 技能效果类型
        /// </summary>
        public int Type;

        /// <summary>
        /// 技能效果名
        /// </summary>
        public string EventName;

        public void WriteCs(IBuffer buffer)
        {
            WriteUInt32(buffer, EntityId);
            WriteInt32(buffer, SkillID);
            WriteInt32(buffer, SkillLevel);
            WriteInt32(buffer, Type);
            WriteString(buffer, EventName);
        }

        public void ReadCs(IBuffer buffer)
        {
            EntityId = ReadUInt32(buffer);
            SkillID = ReadInt32(buffer);
            SkillLevel = ReadInt32(buffer);
            Type = ReadInt32(buffer);
            EventName = ReadString(buffer);
        }
    }
}
