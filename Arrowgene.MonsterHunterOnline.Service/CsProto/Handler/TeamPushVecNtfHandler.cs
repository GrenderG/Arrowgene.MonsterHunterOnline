using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class TeamPushVecNtfHandler : CsProtoStructureHandler<TeamPushVecNtf>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(TeamPushVecNtfHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_TEAM_PUSH_VEC_NTF;


    public override void Handle(Client client, TeamPushVecNtf req)
    {
        Logger.Info(client, $"{req.Vec3.x} {req.Vec3.y} {req.Vec3.z}");
    }
}