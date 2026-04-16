using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures;

public class DeleteRoleReq : Structure, ICsStructure
{
    public DeleteRoleReq()
    {
        RoleIndex = 0;
    }

    public int RoleIndex { get; set; }

    public  void WriteCs(IBuffer buffer)
    {
        WriteInt32(buffer, RoleIndex);
    }

    public void ReadCs(IBuffer buffer)
    {
        RoleIndex = ReadInt32(buffer);
    }
}