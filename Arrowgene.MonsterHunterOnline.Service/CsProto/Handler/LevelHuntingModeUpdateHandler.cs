using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class LevelHuntingModeUpdateHandler : CsProtoStructureHandler<InstanceHuntingModeReq>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(LevelHuntingModeUpdateHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_LEVEL_HUNTINGMODE_UPDATE;

    public LevelHuntingModeUpdateHandler()
    {
    }

    public override void Handle(Client client, InstanceHuntingModeReq req)
    {
     
    }
}