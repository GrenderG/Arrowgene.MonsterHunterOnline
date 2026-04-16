using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class ChangeAmmoReqHandler : CsProtoStructureHandler<ChangeAmmoReq>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(ChangeAmmoReqHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_CHANGE_AMMO_REQ;

    public override void Handle(Client client, ChangeAmmoReq req)
    {
        //CsCsProtoStructurePacket<ChangeAmmoReq> reqq = CsProtoResponse.ChangeAmmoReq;
        CsCsProtoStructurePacket<ChangeAmmoRsp> rsp = CsProtoResponse.ChangeAmmoRsp;

        rsp.Structure.NetID = (int)client.Character.Id;
        rsp.Structure.TypeID = req.TypeID;

        //reqq.Structure.TypeID = req.TypeID;


        //client.SendCsProtoStructurePacket(reqq);
        client.SendCsProtoStructurePacket(rsp);
    }
}