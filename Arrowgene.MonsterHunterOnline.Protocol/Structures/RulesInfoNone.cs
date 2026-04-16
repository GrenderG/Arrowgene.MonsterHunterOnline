using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;
using Arrowgene.MonsterHunterOnline.Protocol.Old.Structures;
using Arrowgene.MonsterHunterOnline.Protocol.Constant;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures;

public class RulesInfoNone : Structure, ICsStructure, CSRulesInfo
{
    public void WriteCs(IBuffer buffer)
    {
    }

    public void ReadCs(IBuffer buffer)
    {
    }

    public CS_BATTLE_RULES_TYPE RulesInfoType => CS_BATTLE_RULES_TYPE.BATTLE_RULES_DEFAULT_INFO;
}