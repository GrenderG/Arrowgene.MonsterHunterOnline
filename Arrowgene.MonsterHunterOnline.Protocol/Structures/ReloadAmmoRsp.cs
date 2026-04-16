using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Collections.Generic;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// 装载弹药响应
    /// </summary>
    public class ReloadAmmoRsp : Structure, ICsStructure
    {
        public ReloadAmmoRsp()
        {
            NetID = 0;
            Ammos = new List<AmmoInfo>();
        }

        /// <summary>
        /// 装载者id
        /// </summary>
        public int NetID;

        public List<AmmoInfo> Ammos;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, NetID);
            int ammosCount = (int)Ammos.Count;
            WriteInt32(buffer, ammosCount);
            for (int i = 0; i < ammosCount; i++)
            {
                Ammos[i].WriteCs(buffer);
            }
        }

        public void ReadCs(IBuffer buffer)
        {
            NetID = ReadInt32(buffer);
            Ammos.Clear();
            int ammosCount = ReadInt32(buffer);
            for (int i = 0; i < ammosCount; i++)
            {
                AmmoInfo AmmosEntry = new AmmoInfo();
                AmmosEntry.ReadCs(buffer);
                Ammos.Add(AmmosEntry);
            }
        }
    }
}
