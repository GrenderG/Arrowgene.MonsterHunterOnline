using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures;

public class NotifyInfo : Structure, ICsStructure
{
    public NotifyInfo()
    {
        Info = "";
    }

    public string Info { get; set; }

    public  void WriteCs(IBuffer buffer)
    {
        WriteString(buffer, Info);
    }

    public void ReadCs(IBuffer buffer)
    {
        Info = ReadString(buffer);
    }
}