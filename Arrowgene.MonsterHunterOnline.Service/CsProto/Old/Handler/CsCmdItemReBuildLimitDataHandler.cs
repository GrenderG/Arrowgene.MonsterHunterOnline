using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Old.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class CsCmdItemReBuildLimitDataHandler : ICsProtoHandler
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(CsCmdItemReBuildLimitDataHandler));

    public CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_ITEMREBUILD_LIMITDATA_REQ;

    public void Handle(Client client, CsProtoPacket packet)
    {
        CSItemRebuildLimitInfo rsp = new CSItemRebuildLimitInfo();
        client.SendCsPacket(NewCsPacket.ItemRebuildLimitDataNtf(rsp));
    }
}