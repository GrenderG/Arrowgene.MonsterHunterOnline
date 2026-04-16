using System;

namespace Arrowgene.MonsterHunterOnline.Protocol.Constant;

[Flags]
public enum SystemUnlockExtFlags : uint
{
    None = 0,
    EnhancedBreakthroughTransfer = 1 << 0,
    WeaponTeachingHandleMode = 1 << 1,
    Hunting = 1 << 2,
    AttributeWhetstone = 1 << 3,
    BreakthroughQualificationCertification = 1 << 4,
    EquipmentRefining = 1 << 5,
    RandomMatch = 1 << 6,
    Casting = 1 << 7,
    Unbind = 1 << 8,
    GuardStoneSmelting = 1 << 9,
    LegendarySkillOrb = 1 << 10,
}

public static class SystemUnlockExtFlagsExtensions
{

    /// <summary>
    /// Returns `ExtFlags` (bits 33 - 64)
    /// </summary>
    public static SystemUnlockExtFlags ToExtFlags(this SystemUnlockFlags flags)
    {
        return (SystemUnlockExtFlags)(flags.ToUInt64() >> 32);
    }

}