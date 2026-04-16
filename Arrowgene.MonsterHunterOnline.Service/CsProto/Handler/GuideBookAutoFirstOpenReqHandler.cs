using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Old.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class GuideBookAutoFirstOpenReqHandler : CsProtoStructureHandler<CSGuideBookAutoFirstOpenReq>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(GuideBookAutoFirstOpenReqHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.C2S_CMD_GUIDE_BOOK_AUTO_FIRST_OPEN_REQ;

    public override void Handle(Client client, CSGuideBookAutoFirstOpenReq req)
    {
        CsCsProtoStructurePacket<SCGuideBookAutoFirstOpenRsp> rsp = CsProtoResponse.SCGuideBookAutoFirstOpenRsp;

        rsp.Structure.ErrCode = 0;

        client.SendCsProtoStructurePacket(rsp);
    }
}