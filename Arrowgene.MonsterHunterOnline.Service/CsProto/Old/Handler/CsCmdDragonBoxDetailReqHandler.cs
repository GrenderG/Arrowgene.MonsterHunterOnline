using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Old.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class CsCmdDragonBoxDetailReqHandler : ICsProtoHandler
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(CsCmdDragonBoxDetailReqHandler));


    public CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_DRAGONBOX_DETAIL_REQ;


    public void Handle(Client client, CsProtoPacket packet)
    {
        client.SendCsPacket(NewCsPacket.DragonBoxDetailRsp(new CSDragonBoxDetailRsp()));
    }
}