using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;
using Arrowgene.MonsterHunterOnline.Protocol.Structures;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class HealthSyncNtfHandler : CsProtoStructureHandler<HealthSyncNtf>
{
    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_HEALTH_SYNC;

    public override void Handle(Client client, HealthSyncNtf req)
    {
        CsCsProtoStructurePacket<HealthSyncNtf> healthSync = CsProtoResponse.HealthSyncNtf;

        healthSync.Structure.Health = 1f;
        healthSync.Structure.HealthRecover = 1f;
        healthSync.Structure.NetID = (int)client.Character.Id;

        client.SendCsProtoStructurePacket(healthSync);
    }
}
