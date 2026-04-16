using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class PlayerAmmoChangeReqHandler : CsProtoStructureHandler<PlayerAmmoChangeReq>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(PlayerAmmoChangeReqHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_PLAYER_AMMO_CHANGE_REQ;

    public override void Handle(Client client, PlayerAmmoChangeReq req)
    {
        CsCsProtoStructurePacket<PlayerAmmoChangeRsp> rsp = CsProtoResponse.PlayerAmmoChangeRsp;
        //CsCsProtoStructurePacket<CSPlayerAmmoChangeReq> reqq = CsProtoResponse.CSPlayerAmmoChangeReq;

        //TODO: get ammo using
        //req.NextAmmoID
        //req.SubAmmoID

        rsp.Structure.Reserve = 100;

        //reqq.Structure.NextAmmoID = req.NextAmmoID;
        //reqq.Structure.SubAmmoID = req.SubAmmoID;

        //CsCsProtoStructurePacket<CSChangeAmmoRsp> rsp2 = CsProtoResponse.CSChangeAmmoRsp;
        //rsp2.Structure.NetID = (int)client.Character.Id;
        //rsp2.Structure.TypeID = req.NextAmmoID;
        //client.SendCsProtoStructurePacket(rsp2);

        client.SendCsProtoStructurePacket(rsp);
        //client.SendCsProtoStructurePacket(reqq);
    }
}