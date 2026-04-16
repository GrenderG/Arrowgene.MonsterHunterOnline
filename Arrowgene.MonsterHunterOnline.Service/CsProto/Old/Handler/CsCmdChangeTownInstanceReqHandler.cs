using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Old.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class CsCmdChangeTownInstanceReqHandler : ICsProtoHandler
{
    public CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_CHANGE_TOWN_INSTANCE_REQ;

    public void Handle(Client client, CsProtoPacket packet)
    {
        CSChangeTownInstanceReq req = new CSChangeTownInstanceReq();
        req.ReadCs(packet.NewBuffer());

      //  client.State.OnChangeTownInstance(req);
    }
}