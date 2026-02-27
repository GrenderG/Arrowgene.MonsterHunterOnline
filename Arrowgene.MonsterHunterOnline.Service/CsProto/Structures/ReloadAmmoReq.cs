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
    /// 装载弹药请求
    /// </summary>
    public class ReloadAmmoReq : Structure, ICsStructure
    {
        public ReloadAmmoReq()
        {
            TypeID = 0;
            Reserved = 0;
        }

        /// <summary>
        /// 弹药类型id
        /// </summary>
        public int TypeID;

        /// <summary>
        /// reserved
        /// </summary>
        public int Reserved;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, TypeID);
            WriteInt32(buffer, Reserved);
        }

        public void ReadCs(IBuffer buffer)
        {
            TypeID = ReadInt32(buffer);
            Reserved = ReadInt32(buffer);
        }
    }
}
