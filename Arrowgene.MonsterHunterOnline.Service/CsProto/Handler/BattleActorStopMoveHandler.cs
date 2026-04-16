using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class BattleActorStopMoveHandler : CsProtoStructureHandler<ActorStopMove>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(BattleActorStopMoveHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_BATTLE_ACTOR_STOPMOVE;


    public override void Handle(Client client, ActorStopMove req)
    {
        CsCsProtoStructurePacket<ActorStopMoveNtf> actorMoveStateNtf = CsProtoResponse.ActorStopMoveNtf;
        actorMoveStateNtf.Structure.NetObjId = client.Character.Id;
        actorMoveStateNtf.Structure.ActorStopMove = req;
    }
}