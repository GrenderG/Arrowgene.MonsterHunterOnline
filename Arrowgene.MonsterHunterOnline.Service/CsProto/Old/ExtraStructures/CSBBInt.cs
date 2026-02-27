using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Old.ExtraStructures
{
    public class CSBBInt : CSBBVariable
    {
        public CSBBInt()
        {
            value = 0;
        }

        public CS_BBVALUE_TYPE Type => CS_BBVALUE_TYPE.CS_BBVALUE_TYPE_INT;

        public int value;

        public void WriteCs(IBuffer buffer)
        {
            buffer.WriteInt32(value, Endianness.Big);
        }

        public void ReadCs(IBuffer buffer)
        {
            value = buffer.ReadInt32(Endianness.Big);
        }
    }
}
