using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures;

public class EnterLevelNtf : Structure, ICsStructure
{
    public EnterLevelNtf()
    {
        Reserver = 0;
    }

    public int Reserver { get; set; }

    public  void WriteCs(IBuffer buffer)
    {
        WriteInt32(buffer, Reserver);
    }

    public void ReadCs(IBuffer buffer)
    {
        Reserver = ReadInt32(buffer);
    }
}