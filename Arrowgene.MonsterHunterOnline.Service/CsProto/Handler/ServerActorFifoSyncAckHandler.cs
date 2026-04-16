using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class ServerActorFifoSyncAckHandler : CsProtoStructureHandler<ServerSyncInfoAck>
{
    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_SERVER_ACTOR_FIFO_SYNC_ACK;


    public override void Handle(Client client, ServerSyncInfoAck req)
    {
    }
}