using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class BattleActorIdleMoveHandler : CsProtoStructureHandler<ActorIdleMove>
{
    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_BATTLE_ACTOR_IDLEMOVE;


    public override void Handle(Client client, ActorIdleMove req)
    {
        CsCsProtoStructurePacket<ActorIdleMoveNtf> actorIdleMoveNtf = CsProtoResponse.ActorIdleMoveNtf;
        actorIdleMoveNtf.Structure.NetObjId = client.Character.Id;
        actorIdleMoveNtf.Structure.ActorIdleMove = req;
        //client.SendCsProtoStructurePacket(actorIdleMoveNtf);
    }
}