using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// 同步玩家换子弹行为
    /// </summary>
    public class PlayerAmmoChangeReq : Structure, ICsStructure
    {
        public PlayerAmmoChangeReq()
        {
            NextAmmoID = 0;
            SubAmmoID = 0;
        }

        /// <summary>
        /// 激活子弹的道具ID
        /// </summary>
        public int NextAmmoID;

        /// <summary>
        /// 副子弹的道具ID
        /// </summary>
        public int SubAmmoID;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, NextAmmoID);
            WriteInt32(buffer, SubAmmoID);
        }

        public void ReadCs(IBuffer buffer)
        {
            NextAmmoID = ReadInt32(buffer);
            SubAmmoID = ReadInt32(buffer);
        }
    }
}
