using Arrowgene.MonsterHunterOnline.Protocol;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    public interface CSICsRemoteDataInfo : ICsStructure
    {
        public ROMTE_DATA_TYPE DataType { get; }
    }
}