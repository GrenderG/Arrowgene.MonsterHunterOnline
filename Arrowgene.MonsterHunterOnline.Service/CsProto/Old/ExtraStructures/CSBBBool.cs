using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Old.ExtraStructures
{
    public class CSBBBool : CSBBVariable
    {
        public CSBBBool(bool value)
        {
            this.value = value;
        }

        public CS_BBVALUE_TYPE Type => CS_BBVALUE_TYPE.CS_BBVALUE_TYPE_BOOL;

        public bool value;

        public void WriteCs(IBuffer buffer)
        {
            buffer.WriteBool(value);
        }

        public void ReadCs(IBuffer buffer)
        {
            value = buffer.ReadBool();
        }
    }
}
