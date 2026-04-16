using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class PlayerRegionJumpEndHandler : CsProtoStructureHandler<PlayerRegionJumpEnd>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(PlayerRegionJumpEnd));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_PLAYER_REGION_JUMP_END;


    public override void Handle(Client client, PlayerRegionJumpEnd req)
    {
        //This just returns the region ID sent to it during the PlayerRegionJumpReq, used to confirm arrival? But we aren't using it at least not yet
        //int regionID = req.RegionId;
        return;
    }
}