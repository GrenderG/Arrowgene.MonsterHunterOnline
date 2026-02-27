using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class SkillEffectSyncHandler : CsProtoStructureHandler<SkillEffectInfo>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(SkillEffectSyncHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_SKILL_EFFECT_SYNC;

    public override void Handle(Client client, SkillEffectInfo req)
    {
        CsCsProtoStructurePacket<SkillEffectInfo> skillEffectInfo = CsProtoResponse.SkillEffectInfo;

        skillEffectInfo.Structure.EntityId = req.EntityId;
        skillEffectInfo.Structure.SkillID = req.SkillID;
        skillEffectInfo.Structure.SkillLevel = req.SkillLevel;
        skillEffectInfo.Structure.Type = req.Type;
        skillEffectInfo.Structure.EventName = req.EventName;

        client.SendCsProtoStructurePacket(skillEffectInfo);

        Logger.Debug($"EntityId:{req.EntityId} SkillID:{req.SkillID} SkillLevel:{req.SkillLevel} Type:{req.Type} EventName:{req.EventName}");
    }
}
