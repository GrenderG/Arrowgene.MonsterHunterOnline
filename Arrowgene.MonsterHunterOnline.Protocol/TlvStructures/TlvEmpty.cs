using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.TlvStructures;

public class TlvEmpty : Structure, ITlvStructure
{
    public TlvEmpty()
    {
    }

    public void WriteTlv(IBuffer buffer)
    {
    }

    public void ReadTlv(IBuffer buffer)
    {
    }
}