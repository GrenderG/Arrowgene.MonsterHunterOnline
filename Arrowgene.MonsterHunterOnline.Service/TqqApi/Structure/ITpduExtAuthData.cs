using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Service.TqqApi.Structure;

public interface CSICsTpduExtAuthData : ICsStructure
{
    public uint Uin { get; set; }
}