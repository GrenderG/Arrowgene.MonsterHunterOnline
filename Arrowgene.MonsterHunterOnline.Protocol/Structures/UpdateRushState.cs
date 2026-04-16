using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{

    public class UpdateRushState : Structure, ICsStructure
    {
        public UpdateRushState()
        {
            Rush = 0;
            Type = 0;
        }

        public int Rush { get; set; }

        public int Type { get; set; }


        public  void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, Rush);
            WriteInt32(buffer, Type);
           
        }

        public void ReadCs(IBuffer buffer)
        {
            Rush = ReadInt32(buffer);
            Type = ReadInt32(buffer);
        }
    }
}