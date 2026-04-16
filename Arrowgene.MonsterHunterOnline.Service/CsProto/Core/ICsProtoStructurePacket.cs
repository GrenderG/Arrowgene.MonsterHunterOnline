using Arrowgene.MonsterHunterOnline.Protocol;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Core
{
    public interface CSICsCsProtoStructurePacket : ICsStructure
    {
        public CS_CMD_ID Cmd { get; }
    }
}