using Arrowgene.Buffers;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// 同步玩家换子弹行为回包
    /// </summary>
    public class PlayerAmmoChangeRsp : Structure, ICsStructure
    {
        public PlayerAmmoChangeRsp()
        {
            Reserve = 0;
        }

        public int Reserve;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, Reserve);
        }

        public void ReadCs(IBuffer buffer)
        {
            Reserve = ReadInt32(buffer);
        }
    }
}
