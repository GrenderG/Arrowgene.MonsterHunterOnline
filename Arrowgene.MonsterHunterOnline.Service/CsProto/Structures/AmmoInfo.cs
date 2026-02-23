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
    /// ammo info
    /// </summary>
    public class AmmoInfo : Structure, ICsStructure
    {
        public AmmoInfo()
        {
            NetID = 0;
            TypeID = 0;
        }

        /// <summary>
        /// logic entity id
        /// </summary>
        public int NetID;

        /// <summary>
        /// ammo type id
        /// </summary>
        public int TypeID;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, NetID);
            WriteInt32(buffer, TypeID);
        }

        public void ReadCs(IBuffer buffer)
        {
            NetID = ReadInt32(buffer);
            TypeID = ReadInt32(buffer);
        }

    }
}
