using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// health sync
    /// </summary>
    public class HealthSyncNtf : Structure, ICsStructure
    {
        public HealthSyncNtf()
        {
            NetID = 0;
            Health = 0.0f;
            HealthRecover = 0.0f;
        }

        /// <summary>
        /// logic entity id
        /// </summary>
        public int NetID;

        /// <summary>
        /// health
        /// </summary>
        public float Health;

        /// <summary>
        /// health recover
        /// </summary>
        public float HealthRecover;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, NetID);
            WriteFloat(buffer, Health);
            WriteFloat(buffer, HealthRecover);
        }

        public void ReadCs(IBuffer buffer)
        {
            NetID = ReadInt32(buffer);
            Health = ReadFloat(buffer);
            HealthRecover = ReadFloat(buffer);
        }

    }
}
