using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Old.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class CsCmdPlayerExtNotifyHandler : ICsProtoHandler
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(CsCmdPlayerExtNotifyHandler));


    public CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_PLAYER_EXT_NOTIFY;


    public void Handle(Client client, CsProtoPacket packet)
    {
        
    }
}