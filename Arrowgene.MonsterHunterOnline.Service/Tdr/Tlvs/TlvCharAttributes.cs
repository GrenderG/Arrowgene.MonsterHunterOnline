using Arrowgene.MonsterHunterOnline.Service.CsProto.Constant;
using Arrowgene.MonsterHunterOnline.Service.System.UnlockSystem;
using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_101C01B0
    /// C++ Reader: crygame.dll+sub_101CB370
    /// C++ Printer: crygame.dll+sub_XXXXX
    /// </summary>
    public class TlvCharAttributes : TlvStructure
    {
        // The universal array boundary for character attributes
        public const int MaxArrayElements = 7;
        public override TlvMagic Magic => TlvMagic.Fixed;

        #region Properties - Integer Arrays
        public int[] CharLevel { get; set; } = new int[MaxArrayElements];
        public int[] CharMaxHP { get; set; } = new int[MaxArrayElements];
        public int[] CharReju { get; set; } = new int[MaxArrayElements];
        public int[] CharMaxReju { get; set; } = new int[MaxArrayElements];
        public int[] CharMaxSta { get; set; } = new int[MaxArrayElements];
        public int[] CharStaDdctPeriod { get; set; } = new int[MaxArrayElements];
        public int[] CharDefence { get; set; } = new int[MaxArrayElements];
        public int[] CritLevel { get; set; } = new int[MaxArrayElements];
        public int[] CritDmg { get; set; } = new int[MaxArrayElements];
        public int[] AntiCritDmg { get; set; } = new int[MaxArrayElements];
        public int[] MaxSharpness { get; set; } = new int[MaxArrayElements];
        public int[] SharpAttackMdf { get; set; } = new int[MaxArrayElements];
        public int[] WaterAttack { get; set; } = new int[MaxArrayElements];
        public int[] FireAttack { get; set; } = new int[MaxArrayElements];
        public int[] LightningAttack { get; set; } = new int[MaxArrayElements];
        public int[] DragonAttack { get; set; } = new int[MaxArrayElements];
        public int[] IceAttack { get; set; } = new int[MaxArrayElements];
        public int[] NonAttack { get; set; } = new int[MaxArrayElements];
        public int[] PoisonAttack { get; set; } = new int[MaxArrayElements];
        public int[] SleepyAttack { get; set; } = new int[MaxArrayElements];
        public int[] ParalysisAttack { get; set; } = new int[MaxArrayElements];
        public int[] WaterThrsh { get; set; } = new int[MaxArrayElements];
        public int[] FireThrsh { get; set; } = new int[MaxArrayElements];
        public int[] LightningThrsh { get; set; } = new int[MaxArrayElements];
        public int[] DragonThrsh { get; set; } = new int[MaxArrayElements];
        public int[] IceThrsh { get; set; } = new int[MaxArrayElements];
        public int[] NonThrsh { get; set; } = new int[MaxArrayElements];
        public int[] PiyoThrsh { get; set; } = new int[MaxArrayElements];
        public int[] PoisonThrsh { get; set; } = new int[MaxArrayElements];
        public int[] SleepingThrsh { get; set; } = new int[MaxArrayElements];
        public int[] ParaThrsh { get; set; } = new int[MaxArrayElements];
        public int[] PiyoDdct { get; set; } = new int[MaxArrayElements];
        public int[] CharSpeed { get; set; } = new int[MaxArrayElements];
        public int[] CharAnimSpeed { get; set; } = new int[MaxArrayElements];
        public int[] CharMaxRage { get; set; } = new int[MaxArrayElements];
        public int[] RejuDmgRatio { get; set; } = new int[MaxArrayElements];
        public int[] RageRatio { get; set; } = new int[MaxArrayElements];
        public int[] QiRenMax { get; set; } = new int[MaxArrayElements];
        public int[] QirenSpeed { get; set; } = new int[MaxArrayElements];
        public int[] LV1QiRenTime { get; set; } = new int[MaxArrayElements];
        public int[] LV2QiRenTime { get; set; } = new int[MaxArrayElements];
        public int[] LV3QiRenTime { get; set; } = new int[MaxArrayElements];
        public int[] QirenMaxTime { get; set; } = new int[MaxArrayElements];
        public int[] AmmoMax { get; set; } = new int[MaxArrayElements];
        public int[] RageSpeed { get; set; } = new int[MaxArrayElements];
        public int[] PerfectReloadAmount { get; set; } = new int[MaxArrayElements];
        public int[] AttackLevelDef { get; set; } = new int[MaxArrayElements];
        public int[] ReloadLevel { get; set; } = new int[MaxArrayElements];
        public int[] RecoilLevel { get; set; } = new int[MaxArrayElements];
        public int[] BulletSpeed { get; set; } = new int[MaxArrayElements];
        public int[] RecoilCameraShake { get; set; } = new int[MaxArrayElements];
        public int[] CharRejuPer { get; set; } = new int[MaxArrayElements];
        public int[] BowStringTenacity { get; set; } = new int[MaxArrayElements];
        public int[] MeleeBottleMax { get; set; } = new int[MaxArrayElements];
        public int[] StrikeBottleMax { get; set; } = new int[MaxArrayElements];
        public int[] PoisonBottleMax { get; set; } = new int[MaxArrayElements];
        public int[] SleepyBottleMax { get; set; } = new int[MaxArrayElements];
        public int[] ParaBottleMax { get; set; } = new int[MaxArrayElements];
        public int[] BowChargeLevelMax { get; set; } = new int[MaxArrayElements];
        public int[] RapidShootAdd { get; set; } = new int[MaxArrayElements];
        public int[] CharDefenseSuperArmorLevel { get; set; } = new int[MaxArrayElements];
        public int[] CharLanceDefenseChargeLv { get; set; } = new int[MaxArrayElements];
        public int[] LuckValule { get; set; } = new int[MaxArrayElements];
        public int[] ZhanJiValue { get; set; } = new int[MaxArrayElements];
        public int[] CharEvadeTime { get; set; } = new int[MaxArrayElements];
        public int[] PvPAttackLevelDef { get; set; } = new int[MaxArrayElements];
        public int[] DynamiteAttack { get; set; } = new int[MaxArrayElements];
        public int[] DynamiteThrsh { get; set; } = new int[MaxArrayElements];
        public int[] DynamiteBottleMax { get; set; } = new int[MaxArrayElements];
        public int[] GhostStaminaDdctSpeed { get; set; } = new int[MaxArrayElements];
        public int[] JinLiStep1MaxValue { get; set; } = new int[MaxArrayElements];
        public int[] JinLiStep2MaxValue { get; set; } = new int[MaxArrayElements];
        public int[] HammerModeTime { get; set; } = new int[MaxArrayElements];
        public int[] AttrAtkFlag { get; set; } = new int[MaxArrayElements];
        public int[] GunLanceMaxAmmoCount { get; set; } = new int[MaxArrayElements];
        #endregion

        #region Properties - Float Arrays
        public float[] CharStaRecovery { get; set; } = new float[MaxArrayElements];
        public float[] CharStaDdct { get; set; } = new float[MaxArrayElements];
        public float[] StaReduce { get; set; } = new float[MaxArrayElements];
        public float[] CurStaReduce { get; set; } = new float[MaxArrayElements];
        public float[] PerfectReloadTime { get; set; } = new float[MaxArrayElements];
        public float[] PlayerAtk { get; set; } = new float[MaxArrayElements];
        public float[] BackBossRunStaReduce { get; set; } = new float[MaxArrayElements];
        public float[] PlayerCrit { get; set; } = new float[MaxArrayElements];
        public float[] GhostMax { get; set; } = new float[MaxArrayElements];
        public float[] GhostDdctSpeed { get; set; } = new float[MaxArrayElements];
        public float[] DefenseReduceHPModifyRate { get; set; } = new float[MaxArrayElements];
        public float[] DefenseReduceStaModifyRate { get; set; } = new float[MaxArrayElements];
        public float[] GunLanceSkillZAngleSpeed { get; set; } = new float[MaxArrayElements];
        public float[] BowBlastSPMax { get; set; } = new float[MaxArrayElements];
        public float[] CurrentBowBlastSpeed { get; set; } = new float[MaxArrayElements];
        public float[] GunLanceMax { get; set; } = new float[MaxArrayElements];
        public float[] GunLanceDdctSpeed { get; set; } = new float[MaxArrayElements];
        #endregion

        #region Properties - Singular Integers
        public int CharSex { get; set; }
        public int CharExp { get; set; }
        public int StarLevel { get; set; }
        public int CharHP { get; set; }
        public int Sharpness { get; set; }
        public int WaterAccum { get; set; }
        public int FireAccum { get; set; }
        public int LightningAccum { get; set; }
        public int DragonAccum { get; set; }
        public int IceAccum { get; set; }
        public int NonAccum { get; set; }
        public int PiyoAccum { get; set; }
        public int PoisonAccum { get; set; }
        public int SleepingAccum { get; set; }
        public int ParaAccum { get; set; }
        public int CharMoney { get; set; }
        public int CharBoundMoney { get; set; }
        public int CharCredit { get; set; }
        public int CharBoundCredit { get; set; }
        public int CharFatigue { get; set; }
        public int CharMaxFatigue { get; set; }
        public int ClaymoreExp { get; set; }
        public int HammerExp { get; set; }
        public int KatanaExp { get; set; }
        public int DuelSwordExp { get; set; }
        public int SwordExp { get; set; }
        public int SpearExp { get; set; }
        public int GunExp { get; set; }
        public int BowExp { get; set; }
        public int CrossbowExp { get; set; }
        public int FluteExp { get; set; }
        public int MaleFace { get; set; }
        public int MaleHair { get; set; }
        public int CharRage { get; set; }
        public int QiRenValue { get; set; }
        public int QiRenLevel { get; set; }
        public int RejuFlag { get; set; }
        public int AmmoCount { get; set; }
        public int AmmoID { get; set; }
        public int NextAmmoID { get; set; }
        public int SubAmmoID { get; set; }
        public int TeamID { get; set; }
        public int PerfAmmoPos { get; set; }
        public int PowerAmmoPos { get; set; }
        public int WindPressureDef { get; set; }
        public int QuakeDef { get; set; }
        public int RoarDef { get; set; }
        public int PalsyDef { get; set; }
        public int SnowManDef { get; set; }
        public int TiredDef { get; set; }
        public int Region { get; set; }
        public int Adult { get; set; }
        public int AASStatus { get; set; }
        public int EquipFoundDay { get; set; }
        public int UnderClothes { get; set; }
        public int StateFlag { get; set; }
        public int PetCarryNum { get; set; }
        public int PetHomeNum { get; set; }
        public int PetOwendNumMax { get; set; }
        public int CharContribution { get; set; }
        public int MeleeBottleUsed { get; set; }
        public int StrikeBottleUsed { get; set; }
        public int PoisonBottleUsed { get; set; }
        public int SleepyBottleUsed { get; set; }
        public int ParaBottleUsed { get; set; }
        public int SecurityStatus { get; set; }
        public int BowShootCount { get; set; }
        public int BowBlastType { get; set; }
        public int CharRemainsExp { get; set; }
        public int FarmExp { get; set; }
        public int FarmEvaluation { get; set; }
        public int SkinColor { get; set; }
        public int HairColor { get; set; }
        public int InnerColor { get; set; }
        public int FaceTattooIndex { get; set; }
        public int EyeBall { get; set; }
        public int FarmFriendGatherCount { get; set; }
        public int FaceTattooColor { get; set; }
        public int EyeColor { get; set; }
        public int BattleState { get; set; }
        public int HammerMode { get; set; }
        public int CharCatCredit { get; set; }
        public int CharReviveCredit { get; set; }
        public int JinLiValue { get; set; }
        public int JinLiStep1ReduceValue { get; set; }
        public int JinLiStep2ReduceValue { get; set; }
        public int EquipTitleID { get; set; }
        public int TitleExp { get; set; }
        public int TitleLevel { get; set; }
        public int EquipTitleBuff { get; set; }
        public SystemUnlockFlags SystemUnlockData { get; set; }
        public int GuildContribution { get; set; }
        public int ExtDailyExp { get; set; }
        public int GuildId { get; set; }
        public int GhostLevel { get; set; }
        public int DistArrowUsed { get; set; }
        public int ExplodeArrowUsed { get; set; }
        public int TiringArrowUsed { get; set; }
        public int CharScore { get; set; }
        public int EntrustMoney1 { get; set; }
        public int EntrustMoney2 { get; set; }
        public int CharmFoundTimes { get; set; }
        public int WeapSysUnlockFlag { get; set; }
        public int CharRemainsDoubleExp { get; set; }
        public int ExtDailyDoubleExp { get; set; }
        public int VIPLevel { get; set; }
        public int VIPExp { get; set; }
        public int VIPBaseEndTime { get; set; }
        public int VIPGrowthEndTime { get; set; }
        public int VIPProfitEndTime { get; set; }
        public int BanChatEndTime { get; set; }
        public SystemUnlockExtFlags SystemUnlockExtData1 { get; set; }
        public int VIPBaseExpireLastNtfTime { get; set; }
        public int VIPVASFrozenTime { get; set; }
        public int ClanScore { get; set; }
        public int ClanScoreMax { get; set; }
        public int ClanMoney { get; set; }
        public int GunLanceAmmoCount { get; set; }
        public int CharHRLevel { get; set; }
        public int CharHRPoint { get; set; }
        public int ClanMoneyPVP { get; set; }
        public int ClanLeaveTime { get; set; }
        public int SanctionPunishEndTime { get; set; }
        public int MVMMonsterFirst { get; set; }
        public int MVMMonsterSecond { get; set; }
        public int MVMMonsterThird { get; set; }
        public int SanctionPunishRatio { get; set; }
        public int CheatSanctionPunishRatio { get; set; }
        public int FluteTune { get; set; }
        public int DeadTime { get; set; }
        public int PersonalELO { get; set; }
        public int PVPMoney { get; set; }
        public int CharCatMoney { get; set; }
        public int CharCatMoneyWeek { get; set; }
        public int CharCatMoneyWeekMax { get; set; }
        public int HuntSoul { get; set; }
        public int WildHuntCamp { get; set; }
        public int WildHuntPhase { get; set; }
        public int BattleCount { get; set; }
        public int FirstLoginTime { get; set; }
        public int LastHuntSoul { get; set; }
        public int TotalHRPoint { get; set; }
        public int LikeHunterOfficer { get; set; }
        public int HuntingCreditExchange { get; set; }
        public int LevelShowType { get; set; }
        public int MonolopyRoundCount { get; set; }
        public int MonolopyActivity { get; set; }
        public int MonolopyCurGrid { get; set; }
        public int ShouHunPoint { get; set; }
        public int LieHunPoint { get; set; }
        public int MVPPoint { get; set; }
        public int XHunterScore { get; set; }
        public int MonolopyAccStep { get; set; }
        public int XHunterHighScore { get; set; }
        public int SoulStoneLevel { get; set; }
        public int WeekMoneyGain { get; set; }
        public int SoulStoneAtkLevel { get; set; }
        public int XHunterActivity { get; set; }
        public int LevelHuntSoul { get; set; }
        public int TaskHuntSoul { get; set; }
        public int CampHuntSoul { get; set; }
        public int PrivateRealHuntSoul { get; set; }
        public int HideWeaponBreakEffect { get; set; }
        public int IllustratePoint { get; set; }
        public int DynamiteAccum { get; set; }
        public int DynamiteDef { get; set; }
        public int DynamiteBottleUsed { get; set; }
        public int GuildBanChatEndTime { get; set; }
        #endregion

        #region Properties - Singular Floats
        public float CharSta { get; set; }
        public float GunLanceValue { get; set; }
        public float LeftGhostValue { get; set; }
        public float RightGhostValue { get; set; }
        public float TotalCredit { get; set; }
        public float BowBlastSpeedVeryFast { get; set; }
        public float BowBlastSpeedFast { get; set; }
        public float BowBlastSpeedNormal { get; set; }
        public float BowBlastSpeedSlow { get; set; }
        public float BowBlastSP { get; set; }
        #endregion

        #region Properties - Singular booleans
        public bool HideFashion { get; set; }
        public bool HideSuite { get; set; }
        public bool HideHelm { get; set; }
        #endregion

        #region Properties - Singular Shorts
        public short Death { get; set; }
        public short TeamPasswordFlag { get; set; }
        public short VIP { get; set; }
        public short VIPVASFrozen { get; set; }
        public short VIPBaseCanUse { get; set; }
        public short VIPGrowthCanUse { get; set; }
        public short VIPProfitCanUse { get; set; }
        public short GameVIP { get; set; }
        public short QQVIP { get; set; }
        public short YearQQVIP { get; set; }
        public short SuperQQVIP { get; set; }
        public short NetbarLevel { get; set; }
        public short XYVIP { get; set; }
        public short TGPVIP { get; set; }
        public short IsNewbie { get; set; }
        public short IsVIPBaseExpireNtf { get; set; }

        public short[] FacialInfo { get; set; } = new short[47]; // Collapsed FacialInfo 1 to 47
        #endregion

        #region Properties - Singular Long
        public long WildHuntGuild { get; set; }
        #endregion

        #region Serialization Methods

        protected override void DeserializeContent(IDataReader reader)
        {
            try
            {
                while (reader.BytesAvailable > 0)
                {
                    uint tag = reader.ReadVarUInt();
                    uint fieldId = tag >> 4;

                    switch (fieldId)
                    {
                        // Integers
                        case 4: CharSex = reader.ReadInt(); break;
                        case 6: CharExp = reader.ReadInt(); break;
                        case 7: StarLevel = reader.ReadInt(); break;
                        case 16: CharHP = reader.ReadInt(); break;
                        case 36: Sharpness = reader.ReadInt(); break;
                        case 53: WaterAccum = reader.ReadInt(); break;
                        case 54: FireAccum = reader.ReadInt(); break;
                        case 55: LightningAccum = reader.ReadInt(); break;
                        case 56: DragonAccum = reader.ReadInt(); break;
                        case 57: IceAccum = reader.ReadInt(); break;
                        case 58: NonAccum = reader.ReadInt(); break;
                        case 70: PiyoAccum = reader.ReadInt(); break;
                        case 71: PoisonAccum = reader.ReadInt(); break;
                        case 72: SleepingAccum = reader.ReadInt(); break;
                        case 73: ParaAccum = reader.ReadInt(); break;
                        case 76: CharMoney = reader.ReadInt(); break;
                        case 77: CharBoundMoney = reader.ReadInt(); break;
                        case 78: CharCredit = reader.ReadInt(); break;
                        case 79: CharBoundCredit = reader.ReadInt(); break;
                        case 86: CharFatigue = reader.ReadInt(); break;
                        case 87: CharMaxFatigue = reader.ReadInt(); break;
                        case 90: ClaymoreExp = reader.ReadInt(); break;
                        case 91: HammerExp = reader.ReadInt(); break;
                        case 92: KatanaExp = reader.ReadInt(); break;
                        case 93: DuelSwordExp = reader.ReadInt(); break;
                        case 94: SwordExp = reader.ReadInt(); break;
                        case 95: SpearExp = reader.ReadInt(); break;
                        case 96: GunExp = reader.ReadInt(); break;
                        case 97: BowExp = reader.ReadInt(); break;
                        case 98: CrossbowExp = reader.ReadInt(); break;
                        case 99: FluteExp = reader.ReadInt(); break;
                        case 108: MaleFace = reader.ReadInt(); break;
                        case 109: MaleHair = reader.ReadInt(); break;
                        case 110: CharRage = reader.ReadInt(); break;
                        case 114: QiRenValue = reader.ReadInt(); break;
                        case 116: QiRenLevel = reader.ReadInt(); break;
                        case 122: RejuFlag = reader.ReadInt(); break;
                        case 124: AmmoCount = reader.ReadInt(); break;
                        case 125: AmmoID = reader.ReadInt(); break;
                        case 126: NextAmmoID = reader.ReadInt(); break;
                        case 127: SubAmmoID = reader.ReadInt(); break;
                        case 128: TeamID = reader.ReadInt(); break;
                        case 139: PerfAmmoPos = reader.ReadInt(); break;
                        case 140: PowerAmmoPos = reader.ReadInt(); break;
                        case 143: WindPressureDef = reader.ReadInt(); break;
                        case 144: QuakeDef = reader.ReadInt(); break;
                        case 145: RoarDef = reader.ReadInt(); break;
                        case 146: PalsyDef = reader.ReadInt(); break;
                        case 147: SnowManDef = reader.ReadInt(); break;
                        case 148: TiredDef = reader.ReadInt(); break;
                        case 150: Region = reader.ReadInt(); break;
                        case 151: Adult = reader.ReadInt(); break;
                        case 152: AASStatus = reader.ReadInt(); break;
                        case 153: EquipFoundDay = reader.ReadInt(); break;
                        case 173: UnderClothes = reader.ReadInt(); break;
                        case 175: StateFlag = reader.ReadInt(); break;
                        case 177: PetCarryNum = reader.ReadInt(); break;
                        case 178: PetHomeNum = reader.ReadInt(); break;
                        case 179: PetOwendNumMax = reader.ReadInt(); break;
                        case 180: CharContribution = reader.ReadInt(); break;
                        case 187: MeleeBottleUsed = reader.ReadInt(); break;
                        case 188: StrikeBottleUsed = reader.ReadInt(); break;
                        case 189: PoisonBottleUsed = reader.ReadInt(); break;
                        case 190: SleepyBottleUsed = reader.ReadInt(); break;
                        case 191: ParaBottleUsed = reader.ReadInt(); break;
                        case 195: SecurityStatus = reader.ReadInt(); break;
                        case 196: BowShootCount = reader.ReadInt(); break;
                        case 199: BowBlastType = reader.ReadInt(); break;
                        case 200: CharRemainsExp = reader.ReadInt(); break;
                        case 202: FarmExp = reader.ReadInt(); break;
                        case 203: FarmEvaluation = reader.ReadInt(); break;
                        case 205: SkinColor = reader.ReadInt(); break;
                        case 206: HairColor = reader.ReadInt(); break;
                        case 207: InnerColor = reader.ReadInt(); break;
                        case 208: FaceTattooIndex = reader.ReadInt(); break;
                        case 209: EyeBall = reader.ReadInt(); break;
                        case 212: FarmFriendGatherCount = reader.ReadInt(); break;
                        case 220: FaceTattooColor = reader.ReadInt(); break;
                        case 221: EyeColor = reader.ReadInt(); break;
                        case 224: BattleState = reader.ReadInt(); break;
                        case 225: HammerMode = reader.ReadInt(); break;
                        case 230: CharCatCredit = reader.ReadInt(); break;
                        case 231: CharReviveCredit = reader.ReadInt(); break;
                        case 232: JinLiValue = reader.ReadInt(); break;
                        case 235: JinLiStep1ReduceValue = reader.ReadInt(); break;
                        case 236: JinLiStep2ReduceValue = reader.ReadInt(); break;
                        case 237: EquipTitleID = reader.ReadInt(); break;
                        case 238: TitleExp = reader.ReadInt(); break;
                        case 239: TitleLevel = reader.ReadInt(); break;
                        case 240: EquipTitleBuff = reader.ReadInt(); break;
                        case 241: SystemUnlockData = (SystemUnlockFlags)reader.ReadInt(); break;
                        case 242: GuildContribution = reader.ReadInt(); break;
                        case 243: ExtDailyExp = reader.ReadInt(); break;
                        case 244: GuildId = reader.ReadInt(); break;
                        case 249: GhostLevel = reader.ReadInt(); break;
                        case 280: DistArrowUsed = reader.ReadInt(); break;
                        case 281: ExplodeArrowUsed = reader.ReadInt(); break;
                        case 282: TiringArrowUsed = reader.ReadInt(); break;
                        case 283: CharScore = reader.ReadInt(); break;
                        case 287: EntrustMoney1 = reader.ReadInt(); break;
                        case 288: EntrustMoney2 = reader.ReadInt(); break;
                        case 289: CharmFoundTimes = reader.ReadInt(); break;
                        case 290: WeapSysUnlockFlag = reader.ReadInt(); break;
                        case 291: CharRemainsDoubleExp = reader.ReadInt(); break;
                        case 292: ExtDailyDoubleExp = reader.ReadInt(); break;
                        case 293: VIPLevel = reader.ReadInt(); break;
                        case 294: VIPExp = reader.ReadInt(); break;
                        case 295: VIPBaseEndTime = reader.ReadInt(); break;
                        case 296: VIPGrowthEndTime = reader.ReadInt(); break;
                        case 297: VIPProfitEndTime = reader.ReadInt(); break;
                        case 298: BanChatEndTime = reader.ReadInt(); break;
                        case 303: SystemUnlockExtData1 = (SystemUnlockExtFlags)reader.ReadInt(); break;
                        case 306: VIPBaseExpireLastNtfTime = reader.ReadInt(); break;
                        case 312: VIPVASFrozenTime = reader.ReadInt(); break;
                        case 313: ClanScore = reader.ReadInt(); break;
                        case 314: ClanScoreMax = reader.ReadInt(); break;
                        case 315: ClanMoney = reader.ReadInt(); break;
                        case 319: GunLanceAmmoCount = reader.ReadInt(); break;
                        case 322: CharHRLevel = reader.ReadInt(); break;
                        case 323: CharHRPoint = reader.ReadInt(); break;
                        case 324: ClanMoneyPVP = reader.ReadInt(); break;
                        case 326: ClanLeaveTime = reader.ReadInt(); break;
                        case 351: SanctionPunishEndTime = reader.ReadInt(); break;
                        case 353: MVMMonsterFirst = reader.ReadInt(); break;
                        case 354: MVMMonsterSecond = reader.ReadInt(); break;
                        case 355: MVMMonsterThird = reader.ReadInt(); break;
                        case 357: SanctionPunishRatio = reader.ReadInt(); break;
                        case 358: CheatSanctionPunishRatio = reader.ReadInt(); break;
                        case 359: FluteTune = reader.ReadInt(); break;
                        case 363: DeadTime = reader.ReadInt(); break;
                        case 364: PersonalELO = reader.ReadInt(); break;
                        case 365: PVPMoney = reader.ReadInt(); break;
                        case 366: CharCatMoney = reader.ReadInt(); break;
                        case 367: CharCatMoneyWeek = reader.ReadInt(); break;
                        case 368: CharCatMoneyWeekMax = reader.ReadInt(); break;
                        case 369: HuntSoul = reader.ReadInt(); break;
                        case 370: WildHuntCamp = reader.ReadInt(); break;
                        case 371: WildHuntPhase = reader.ReadInt(); break;
                        case 374: BattleCount = reader.ReadInt(); break;
                        case 375: FirstLoginTime = reader.ReadInt(); break;
                        case 376: LastHuntSoul = reader.ReadInt(); break;
                        case 377: TotalHRPoint = reader.ReadInt(); break;
                        case 378: LikeHunterOfficer = reader.ReadInt(); break;
                        case 381: HuntingCreditExchange = reader.ReadInt(); break;
                        case 383: LevelShowType = reader.ReadInt(); break;
                        case 385: MonolopyRoundCount = reader.ReadInt(); break;
                        case 386: MonolopyActivity = reader.ReadInt(); break;
                        case 387: MonolopyCurGrid = reader.ReadInt(); break;
                        case 389: ShouHunPoint = reader.ReadInt(); break;
                        case 390: LieHunPoint = reader.ReadInt(); break;
                        case 391: MVPPoint = reader.ReadInt(); break;
                        case 392: XHunterScore = reader.ReadInt(); break;
                        case 400: MonolopyAccStep = reader.ReadInt(); break;
                        case 401: XHunterHighScore = reader.ReadInt(); break;
                        case 402: SoulStoneLevel = reader.ReadInt(); break;
                        case 404: WeekMoneyGain = reader.ReadInt(); break;
                        case 405: SoulStoneAtkLevel = reader.ReadInt(); break;
                        case 406: XHunterActivity = reader.ReadInt(); break;
                        case 408: LevelHuntSoul = reader.ReadInt(); break;
                        case 409: TaskHuntSoul = reader.ReadInt(); break;
                        case 410: CampHuntSoul = reader.ReadInt(); break;
                        case 411: PrivateRealHuntSoul = reader.ReadInt(); break;
                        case 412: HideWeaponBreakEffect = reader.ReadInt(); break;
                        case 413: IllustratePoint = reader.ReadInt(); break;
                        case 416: DynamiteAccum = reader.ReadInt(); break;
                        case 417: DynamiteDef = reader.ReadInt(); break;
                        case 419: DynamiteBottleUsed = reader.ReadInt(); break;
                        case 420: GuildBanChatEndTime = reader.ReadInt(); break;

                        // Floats
                        case 21: CharSta = reader.ReadFloat(); break;
                        case 246: LeftGhostValue = reader.ReadFloat(); break;
                        case 247: RightGhostValue = reader.ReadFloat(); break;
                        case 316: GunLanceValue = reader.ReadFloat(); break;
                        case 373: TotalCredit = reader.ReadFloat(); break;
                        case 393: BowBlastSpeedVeryFast = reader.ReadFloat(); break;
                        case 394: BowBlastSpeedFast = reader.ReadFloat(); break;
                        case 395: BowBlastSpeedNormal = reader.ReadFloat(); break;
                        case 396: BowBlastSpeedSlow = reader.ReadFloat(); break;
                        case 397: BowBlastSP = reader.ReadFloat(); break;

                        // Booleans
                        case 227: HideFashion = reader.ReadShort() != 0; break;
                        case 228: HideSuite = reader.ReadShort() != 0; break;
                        case 229: HideHelm = reader.ReadShort() != 0; break;

                        // Shorts
                        case 20: Death = reader.ReadShort(); break;
                        
                        case 245: TeamPasswordFlag = reader.ReadShort(); break;
                        case 284: VIP = reader.ReadShort(); break;
                        case 299: VIPVASFrozen = reader.ReadShort(); break;
                        case 300: VIPBaseCanUse = reader.ReadShort(); break;
                        case 301: VIPGrowthCanUse = reader.ReadShort(); break;
                        case 302: VIPProfitCanUse = reader.ReadShort(); break;
                        case 305: IsVIPBaseExpireNtf = reader.ReadShort(); break;
                        case 307: GameVIP = reader.ReadShort(); break;
                        case 308: QQVIP = reader.ReadShort(); break;
                        case 309: YearQQVIP = reader.ReadShort(); break;
                        case 310: SuperQQVIP = reader.ReadShort(); break;
                        case 311: NetbarLevel = reader.ReadShort(); break;
                        case 328: XYVIP = reader.ReadShort(); break;
                        case 352: TGPVIP = reader.ReadShort(); break;
                        case 407: IsNewbie = reader.ReadShort(); break;

                        // Facial Info (Mapping exact 47 short fields into an array for cleanliness)
                        // Range 1 (1 to 25): 252 (0xFC) to 276 (0x114)
                        case >= 252 and <= 276:
                            FacialInfo[fieldId - 252] = reader.ReadShort();
                            break;
                        // Range 2 (26 to 47): 329 (0x149) to 350 (0x15E)
                        case >= 329 and <= 350:
                            FacialInfo[fieldId - 329 + 25] = reader.ReadShort();
                            break;

                        // Longs
                        case 372: WildHuntGuild = reader.ReadLong(); break;

                        // Int Arrays
                        case 2: CharLevel = ReadTlvIntArray(reader); break;
                        case 17: CharMaxHP = ReadTlvIntArray(reader); break;
                        case 18: CharReju = ReadTlvIntArray(reader); break;
                        case 19: CharMaxReju = ReadTlvIntArray(reader); break;
                        case 22: CharMaxSta = ReadTlvIntArray(reader); break;
                        case 25: CharStaDdctPeriod = ReadTlvIntArray(reader); break;
                        case 32: CharDefence = ReadTlvIntArray(reader); break;
                        case 33: CritLevel = ReadTlvIntArray(reader); break;
                        case 34: CritDmg = ReadTlvIntArray(reader); break;
                        case 35: AntiCritDmg = ReadTlvIntArray(reader); break;
                        case 37: MaxSharpness = ReadTlvIntArray(reader); break;
                        case 38: SharpAttackMdf = ReadTlvIntArray(reader); break;
                        case 39: WaterAttack = ReadTlvIntArray(reader); break;
                        case 40: FireAttack = ReadTlvIntArray(reader); break;
                        case 41: LightningAttack = ReadTlvIntArray(reader); break;
                        case 42: DragonAttack = ReadTlvIntArray(reader); break;
                        case 43: IceAttack = ReadTlvIntArray(reader); break;
                        case 44: NonAttack = ReadTlvIntArray(reader); break;
                        case 45: PoisonAttack = ReadTlvIntArray(reader); break;
                        case 46: SleepyAttack = ReadTlvIntArray(reader); break;
                        case 47: ParalysisAttack = ReadTlvIntArray(reader); break;
                        case 59: WaterThrsh = ReadTlvIntArray(reader); break;
                        case 60: FireThrsh = ReadTlvIntArray(reader); break;
                        case 61: LightningThrsh = ReadTlvIntArray(reader); break;
                        case 62: DragonThrsh = ReadTlvIntArray(reader); break;
                        case 63: IceThrsh = ReadTlvIntArray(reader); break;
                        case 64: NonThrsh = ReadTlvIntArray(reader); break;
                        case 65: PiyoThrsh = ReadTlvIntArray(reader); break;
                        case 66: PoisonThrsh = ReadTlvIntArray(reader); break;
                        case 67: SleepingThrsh = ReadTlvIntArray(reader); break;
                        case 68: ParaThrsh = ReadTlvIntArray(reader); break;
                        case 69: PiyoDdct = ReadTlvIntArray(reader); break;
                        case 74: CharSpeed = ReadTlvIntArray(reader); break;
                        case 75: CharAnimSpeed = ReadTlvIntArray(reader); break;
                        case 111: CharMaxRage = ReadTlvIntArray(reader); break;
                        case 112: RejuDmgRatio = ReadTlvIntArray(reader); break;
                        case 113: RageRatio = ReadTlvIntArray(reader); break;
                        case 115: QiRenMax = ReadTlvIntArray(reader); break;
                        case 117: QirenSpeed = ReadTlvIntArray(reader); break;
                        case 118: LV1QiRenTime = ReadTlvIntArray(reader); break;
                        case 119: LV2QiRenTime = ReadTlvIntArray(reader); break;
                        case 120: LV3QiRenTime = ReadTlvIntArray(reader); break;
                        case 121: QirenMaxTime = ReadTlvIntArray(reader); break;
                        case 123: AmmoMax = ReadTlvIntArray(reader); break;
                        case 132: RageSpeed = ReadTlvIntArray(reader); break;
                        case 142: PerfectReloadAmount = ReadTlvIntArray(reader); break;
                        case 149: AttackLevelDef = ReadTlvIntArray(reader); break;
                        case 154: ReloadLevel = ReadTlvIntArray(reader); break;
                        case 155: RecoilLevel = ReadTlvIntArray(reader); break;
                        case 156: BulletSpeed = ReadTlvIntArray(reader); break;
                        case 157: RecoilCameraShake = ReadTlvIntArray(reader); break;
                        case 176: CharRejuPer = ReadTlvIntArray(reader); break;
                        case 181: BowStringTenacity = ReadTlvIntArray(reader); break;
                        case 182: MeleeBottleMax = ReadTlvIntArray(reader); break;
                        case 183: StrikeBottleMax = ReadTlvIntArray(reader); break;
                        case 184: PoisonBottleMax = ReadTlvIntArray(reader); break;
                        case 185: SleepyBottleMax = ReadTlvIntArray(reader); break;
                        case 186: ParaBottleMax = ReadTlvIntArray(reader); break;
                        case 222: AttrAtkFlag = ReadTlvIntArray(reader); break;
                        case 226: HammerModeTime = ReadTlvIntArray(reader); break;
                        case 233: JinLiStep1MaxValue = ReadTlvIntArray(reader); break;
                        case 234: JinLiStep2MaxValue = ReadTlvIntArray(reader); break;
                        case 251: GhostStaminaDdctSpeed = ReadTlvIntArray(reader); break;
                        case 304: BowChargeLevelMax = ReadTlvIntArray(reader); break;
                        case 320: GunLanceMaxAmmoCount = ReadTlvIntArray(reader); break;
                        case 321: RapidShootAdd = ReadTlvIntArray(reader); break;
                        case 325: CharDefenseSuperArmorLevel = ReadTlvIntArray(reader); break;
                        case 327: CharLanceDefenseChargeLv = ReadTlvIntArray(reader); break;
                        case 356: LuckValule = ReadTlvIntArray(reader); break;
                        case 382: PvPAttackLevelDef = ReadTlvIntArray(reader); break;
                        case 384: CharEvadeTime = ReadTlvIntArray(reader); break;
                        case 388: ZhanJiValue = ReadTlvIntArray(reader); break;
                        case 414: DynamiteAttack = ReadTlvIntArray(reader); break;
                        case 415: DynamiteThrsh = ReadTlvIntArray(reader); break;
                        case 418: DynamiteBottleMax = ReadTlvIntArray(reader); break;

                        // Float Arrays
                        case 23: CharStaRecovery = ReadTlvFloatArray(reader); break;
                        case 24: CharStaDdct = ReadTlvFloatArray(reader); break;
                        case 130: StaReduce = ReadTlvFloatArray(reader); break;
                        case 138: CurStaReduce = ReadTlvFloatArray(reader); break;
                        case 141: PerfectReloadTime = ReadTlvFloatArray(reader); break;
                        case 211: PlayerAtk = ReadTlvFloatArray(reader); break;
                        case 213: BackBossRunStaReduce = ReadTlvFloatArray(reader); break;
                        case 214: PlayerCrit = ReadTlvFloatArray(reader); break;
                        case 248: GhostMax = ReadTlvFloatArray(reader); break;
                        case 250: GhostDdctSpeed = ReadTlvFloatArray(reader); break;
                        case 317: GunLanceMax = ReadTlvFloatArray(reader); break;
                        case 318: GunLanceDdctSpeed = ReadTlvFloatArray(reader); break;
                        case 360: DefenseReduceHPModifyRate = ReadTlvFloatArray(reader); break;
                        case 361: DefenseReduceStaModifyRate = ReadTlvFloatArray(reader); break;
                        case 362: GunLanceSkillZAngleSpeed = ReadTlvFloatArray(reader); break;
                        case 398: BowBlastSPMax = ReadTlvFloatArray(reader); break;
                        case 399: CurrentBowBlastSpeed = ReadTlvFloatArray(reader); break;

                        default: SkipTlvField(reader, tag); break;
                    }
                }
            }
            catch (EndOfStreamException)
            {
                // Stream completely parsed
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteStrictIntArray(writer, 2, CharLevel, MaxArrayElements);
            WriteTlvInt(writer, 4, CharSex);
            WriteTlvInt(writer, 6, CharExp);
            WriteTlvInt(writer, 7, StarLevel);
            WriteTlvInt(writer, 16, CharHP);
            WriteStrictIntArray(writer, 17, CharMaxHP, MaxArrayElements);
            WriteStrictIntArray(writer, 18, CharReju, MaxArrayElements);
            WriteStrictIntArray(writer, 19, CharMaxReju, MaxArrayElements);
            WriteTlvShort(writer, 20, Death);
            WriteTlvFloat(writer, 21, CharSta);
            WriteStrictIntArray(writer, 22, CharMaxSta, MaxArrayElements);
            WriteStrictFloatArray(writer, 23, CharStaRecovery, MaxArrayElements);
            WriteStrictFloatArray(writer, 24, CharStaDdct, MaxArrayElements);
            WriteStrictIntArray(writer, 25, CharStaDdctPeriod, MaxArrayElements);
            WriteStrictIntArray(writer, 32, CharDefence, MaxArrayElements);
            WriteStrictIntArray(writer, 33, CritLevel, MaxArrayElements);
            WriteStrictIntArray(writer, 34, CritDmg, MaxArrayElements);
            WriteStrictIntArray(writer, 35, AntiCritDmg, MaxArrayElements);
            WriteTlvInt(writer, 36, Sharpness);
            WriteStrictIntArray(writer, 37, MaxSharpness, MaxArrayElements);
            WriteStrictIntArray(writer, 38, SharpAttackMdf, MaxArrayElements);
            WriteStrictIntArray(writer, 39, WaterAttack, MaxArrayElements);
            WriteStrictIntArray(writer, 40, FireAttack, MaxArrayElements);
            WriteStrictIntArray(writer, 41, LightningAttack, MaxArrayElements);
            WriteStrictIntArray(writer, 42, DragonAttack, MaxArrayElements);
            WriteStrictIntArray(writer, 43, IceAttack, MaxArrayElements);
            WriteStrictIntArray(writer, 44, NonAttack, MaxArrayElements);
            WriteStrictIntArray(writer, 45, PoisonAttack, MaxArrayElements);
            WriteStrictIntArray(writer, 46, SleepyAttack, MaxArrayElements);
            WriteStrictIntArray(writer, 47, ParalysisAttack, MaxArrayElements);
            WriteTlvInt(writer, 53, WaterAccum);
            WriteTlvInt(writer, 54, FireAccum);
            WriteTlvInt(writer, 55, LightningAccum);
            WriteTlvInt(writer, 56, DragonAccum);
            WriteTlvInt(writer, 57, IceAccum);
            WriteTlvInt(writer, 58, NonAccum);
            WriteStrictIntArray(writer, 59, WaterThrsh, MaxArrayElements);
            WriteStrictIntArray(writer, 60, FireThrsh, MaxArrayElements);
            WriteStrictIntArray(writer, 61, LightningThrsh, MaxArrayElements);
            WriteStrictIntArray(writer, 62, DragonThrsh, MaxArrayElements);
            WriteStrictIntArray(writer, 63, IceThrsh, MaxArrayElements);
            WriteStrictIntArray(writer, 64, NonThrsh, MaxArrayElements);
            WriteStrictIntArray(writer, 65, PiyoThrsh, MaxArrayElements);
            WriteStrictIntArray(writer, 66, PoisonThrsh, MaxArrayElements);
            WriteStrictIntArray(writer, 67, SleepingThrsh, MaxArrayElements);
            WriteStrictIntArray(writer, 68, ParaThrsh, MaxArrayElements);
            WriteStrictIntArray(writer, 69, PiyoDdct, MaxArrayElements);
            WriteTlvInt(writer, 70, PiyoAccum);
            WriteTlvInt(writer, 71, PoisonAccum);
            WriteTlvInt(writer, 72, SleepingAccum);
            WriteTlvInt(writer, 73, ParaAccum);
            WriteStrictIntArray(writer, 74, CharSpeed, MaxArrayElements);
            WriteStrictIntArray(writer, 75, CharAnimSpeed, MaxArrayElements);
            WriteTlvInt(writer, 76, CharMoney);
            WriteTlvInt(writer, 77, CharBoundMoney);
            WriteTlvInt(writer, 78, CharCredit);
            WriteTlvInt(writer, 79, CharBoundCredit);
            WriteTlvInt(writer, 86, CharFatigue);
            WriteTlvInt(writer, 87, CharMaxFatigue);
            WriteTlvInt(writer, 90, ClaymoreExp);
            WriteTlvInt(writer, 91, HammerExp);
            WriteTlvInt(writer, 92, KatanaExp);
            WriteTlvInt(writer, 93, DuelSwordExp);
            WriteTlvInt(writer, 94, SwordExp);
            WriteTlvInt(writer, 95, SpearExp);
            WriteTlvInt(writer, 96, GunExp);
            WriteTlvInt(writer, 97, BowExp);
            WriteTlvInt(writer, 98, CrossbowExp);
            WriteTlvInt(writer, 99, FluteExp);
            WriteTlvInt(writer, 108, MaleFace);
            WriteTlvInt(writer, 109, MaleHair);
            WriteTlvInt(writer, 110, CharRage);
            WriteStrictIntArray(writer, 111, CharMaxRage, MaxArrayElements);
            WriteStrictIntArray(writer, 112, RejuDmgRatio, MaxArrayElements);
            WriteStrictIntArray(writer, 113, RageRatio, MaxArrayElements);
            WriteTlvInt(writer, 114, QiRenValue);
            WriteStrictIntArray(writer, 115, QiRenMax, MaxArrayElements);
            WriteTlvInt(writer, 116, QiRenLevel);
            WriteStrictIntArray(writer, 117, QirenSpeed, MaxArrayElements);
            WriteStrictIntArray(writer, 118, LV1QiRenTime, MaxArrayElements);
            WriteStrictIntArray(writer, 119, LV2QiRenTime, MaxArrayElements);
            WriteStrictIntArray(writer, 120, LV3QiRenTime, MaxArrayElements);
            WriteStrictIntArray(writer, 121, QirenMaxTime, MaxArrayElements);
            WriteTlvInt(writer, 122, RejuFlag);
            WriteStrictIntArray(writer, 123, AmmoMax, MaxArrayElements);
            WriteTlvInt(writer, 124, AmmoCount);
            WriteTlvInt(writer, 125, AmmoID);
            WriteTlvInt(writer, 126, NextAmmoID);
            WriteTlvInt(writer, 127, SubAmmoID);
            WriteTlvInt(writer, 128, TeamID);
            WriteStrictFloatArray(writer, 130, StaReduce, MaxArrayElements);
            WriteStrictIntArray(writer, 132, RageSpeed, MaxArrayElements);
            WriteStrictFloatArray(writer, 138, CurStaReduce, MaxArrayElements);
            WriteTlvInt(writer, 139, PerfAmmoPos);
            WriteTlvInt(writer, 140, PowerAmmoPos);
            WriteStrictFloatArray(writer, 141, PerfectReloadTime, MaxArrayElements);
            WriteStrictIntArray(writer, 142, PerfectReloadAmount, MaxArrayElements);
            WriteTlvInt(writer, 143, WindPressureDef);
            WriteTlvInt(writer, 144, QuakeDef);
            WriteTlvInt(writer, 145, RoarDef);
            WriteTlvInt(writer, 146, PalsyDef);
            WriteTlvInt(writer, 147, SnowManDef);
            WriteTlvInt(writer, 148, TiredDef);
            WriteStrictIntArray(writer, 149, AttackLevelDef, MaxArrayElements);
            WriteTlvInt(writer, 150, Region);
            WriteTlvInt(writer, 151, Adult);
            WriteTlvInt(writer, 152, AASStatus);
            WriteTlvInt(writer, 153, EquipFoundDay);
            WriteStrictIntArray(writer, 154, ReloadLevel, MaxArrayElements);
            WriteStrictIntArray(writer, 155, RecoilLevel, MaxArrayElements);
            WriteStrictIntArray(writer, 156, BulletSpeed, MaxArrayElements);
            WriteStrictIntArray(writer, 157, RecoilCameraShake, MaxArrayElements);
            WriteTlvInt(writer, 173, UnderClothes);
            WriteTlvInt(writer, 175, StateFlag);
            WriteStrictIntArray(writer, 176, CharRejuPer, MaxArrayElements);
            WriteTlvInt(writer, 177, PetCarryNum);
            WriteTlvInt(writer, 178, PetHomeNum);
            WriteTlvInt(writer, 179, PetOwendNumMax);
            WriteTlvInt(writer, 180, CharContribution);
            WriteStrictIntArray(writer, 181, BowStringTenacity, MaxArrayElements);
            WriteStrictIntArray(writer, 182, MeleeBottleMax, MaxArrayElements);
            WriteStrictIntArray(writer, 183, StrikeBottleMax, MaxArrayElements);
            WriteStrictIntArray(writer, 184, PoisonBottleMax, MaxArrayElements);
            WriteStrictIntArray(writer, 185, SleepyBottleMax, MaxArrayElements);
            WriteStrictIntArray(writer, 186, ParaBottleMax, MaxArrayElements);
            WriteTlvInt(writer, 187, MeleeBottleUsed);
            WriteTlvInt(writer, 188, StrikeBottleUsed);
            WriteTlvInt(writer, 189, PoisonBottleUsed);
            WriteTlvInt(writer, 190, SleepyBottleUsed);
            WriteTlvInt(writer, 191, ParaBottleUsed);
            WriteTlvInt(writer, 195, SecurityStatus);
            WriteTlvInt(writer, 196, BowShootCount);
            WriteTlvInt(writer, 199, BowBlastType);
            WriteTlvInt(writer, 200, CharRemainsExp);
            WriteTlvInt(writer, 202, FarmExp);
            WriteTlvInt(writer, 203, FarmEvaluation);
            WriteTlvInt(writer, 205, SkinColor);
            WriteTlvInt(writer, 206, HairColor);
            WriteTlvInt(writer, 207, InnerColor);
            WriteTlvInt(writer, 208, FaceTattooIndex);
            WriteTlvInt(writer, 209, EyeBall);
            WriteStrictFloatArray(writer, 211, PlayerAtk, MaxArrayElements);
            WriteTlvInt(writer, 212, FarmFriendGatherCount);
            WriteStrictFloatArray(writer, 213, BackBossRunStaReduce, MaxArrayElements);
            WriteStrictFloatArray(writer, 214, PlayerCrit, MaxArrayElements);
            WriteTlvInt(writer, 220, FaceTattooColor);
            WriteTlvInt(writer, 221, EyeColor);
            WriteStrictIntArray(writer, 222, AttrAtkFlag, MaxArrayElements);
            WriteTlvInt(writer, 224, BattleState);
            WriteTlvInt(writer, 225, HammerMode);
            WriteStrictIntArray(writer, 226, HammerModeTime, MaxArrayElements);
            WriteTlvShort(writer, 227, (short)(HideFashion ? 1 : 0));
            WriteTlvShort(writer, 228, (short)(HideSuite ? 1 : 0));
            WriteTlvShort(writer, 229, (short)(HideHelm ? 1 : 0));
            WriteTlvInt(writer, 230, CharCatCredit);
            WriteTlvInt(writer, 231, CharReviveCredit);
            WriteTlvInt(writer, 232, JinLiValue);
            WriteStrictIntArray(writer, 233, JinLiStep1MaxValue, MaxArrayElements);
            WriteStrictIntArray(writer, 234, JinLiStep2MaxValue, MaxArrayElements);
            WriteTlvInt(writer, 235, JinLiStep1ReduceValue);
            WriteTlvInt(writer, 236, JinLiStep2ReduceValue);
            WriteTlvInt(writer, 237, EquipTitleID);
            WriteTlvInt(writer, 238, TitleExp);
            WriteTlvInt(writer, 239, TitleLevel);
            WriteTlvInt(writer, 240, EquipTitleBuff);
            WriteTlvInt(writer, 241, (int)SystemUnlockData);
            WriteTlvInt(writer, 242, GuildContribution);
            WriteTlvInt(writer, 243, ExtDailyExp);
            WriteTlvInt(writer, 244, GuildId);
            WriteTlvShort(writer, 245, TeamPasswordFlag);
            WriteTlvFloat(writer, 246, LeftGhostValue);
            WriteTlvFloat(writer, 247, RightGhostValue);
            WriteStrictFloatArray(writer, 248, GhostMax, MaxArrayElements);
            WriteTlvInt(writer, 249, GhostLevel);
            WriteStrictFloatArray(writer, 250, GhostDdctSpeed, MaxArrayElements);
            WriteStrictIntArray(writer, 251, GhostStaminaDdctSpeed, MaxArrayElements);

            for (int i = 0; i < 25; i++)
                WriteTlvShort(writer, (uint)(252 + i), FacialInfo[i]);

            WriteTlvInt(writer, 280, DistArrowUsed);
            WriteTlvInt(writer, 281, ExplodeArrowUsed);
            WriteTlvInt(writer, 282, TiringArrowUsed);
            WriteTlvInt(writer, 283, CharScore);
            WriteTlvShort(writer, 284, VIP);
            WriteTlvInt(writer, 287, EntrustMoney1);
            WriteTlvInt(writer, 288, EntrustMoney2);
            WriteTlvInt(writer, 289, CharmFoundTimes);
            WriteTlvInt(writer, 290, WeapSysUnlockFlag);
            WriteTlvInt(writer, 291, CharRemainsDoubleExp);
            WriteTlvInt(writer, 292, ExtDailyDoubleExp);
            WriteTlvInt(writer, 293, VIPLevel);
            WriteTlvInt(writer, 294, VIPExp);
            WriteTlvInt(writer, 295, VIPBaseEndTime);
            WriteTlvInt(writer, 296, VIPGrowthEndTime);
            WriteTlvInt(writer, 297, VIPProfitEndTime);
            WriteTlvInt(writer, 298, BanChatEndTime);
            WriteTlvShort(writer, 299, VIPVASFrozen);
            WriteTlvShort(writer, 300, VIPBaseCanUse);
            WriteTlvShort(writer, 301, VIPGrowthCanUse);
            WriteTlvShort(writer, 302, VIPProfitCanUse);
            WriteTlvInt(writer, 303, (int)SystemUnlockExtData1);
            WriteStrictIntArray(writer, 304, BowChargeLevelMax, MaxArrayElements);
            WriteTlvShort(writer, 305, IsVIPBaseExpireNtf);
            WriteTlvInt(writer, 306, VIPBaseExpireLastNtfTime);
            WriteTlvShort(writer, 307, GameVIP);
            WriteTlvShort(writer, 308, QQVIP);
            WriteTlvShort(writer, 309, YearQQVIP);
            WriteTlvShort(writer, 310, SuperQQVIP);
            WriteTlvShort(writer, 311, NetbarLevel);
            WriteTlvInt(writer, 312, VIPVASFrozenTime);
            WriteTlvInt(writer, 313, ClanScore);
            WriteTlvInt(writer, 314, ClanScoreMax);
            WriteTlvInt(writer, 315, ClanMoney);
            WriteTlvFloat(writer, 316, GunLanceValue);
            WriteStrictFloatArray(writer, 317, GunLanceMax, MaxArrayElements);
            WriteStrictFloatArray(writer, 318, GunLanceDdctSpeed, MaxArrayElements);
            WriteTlvInt(writer, 319, GunLanceAmmoCount);
            WriteStrictIntArray(writer, 320, GunLanceMaxAmmoCount, MaxArrayElements);
            WriteStrictIntArray(writer, 321, RapidShootAdd, MaxArrayElements);
            WriteTlvInt(writer, 322, CharHRLevel);
            WriteTlvInt(writer, 323, CharHRPoint);
            WriteTlvInt(writer, 324, ClanMoneyPVP);
            WriteStrictIntArray(writer, 325, CharDefenseSuperArmorLevel, MaxArrayElements);
            WriteTlvInt(writer, 326, ClanLeaveTime);
            WriteStrictIntArray(writer, 327, CharLanceDefenseChargeLv, MaxArrayElements);
            WriteTlvShort(writer, 328, XYVIP);

            for (int i = 0; i < 22; i++)
                WriteTlvShort(writer, (uint)(329 + i), FacialInfo[25 + i]);

            WriteTlvInt(writer, 351, SanctionPunishEndTime);
            WriteTlvShort(writer, 352, TGPVIP);
            WriteTlvInt(writer, 353, MVMMonsterFirst);
            WriteTlvInt(writer, 354, MVMMonsterSecond);
            WriteTlvInt(writer, 355, MVMMonsterThird);
            WriteStrictIntArray(writer, 356, LuckValule, MaxArrayElements);
            WriteTlvInt(writer, 357, SanctionPunishRatio);
            WriteTlvInt(writer, 358, CheatSanctionPunishRatio);
            WriteTlvInt(writer, 359, FluteTune);
            WriteStrictFloatArray(writer, 360, DefenseReduceHPModifyRate, MaxArrayElements);
            WriteStrictFloatArray(writer, 361, DefenseReduceStaModifyRate, MaxArrayElements);
            WriteStrictFloatArray(writer, 362, GunLanceSkillZAngleSpeed, MaxArrayElements);
            WriteTlvInt(writer, 363, DeadTime);
            WriteTlvInt(writer, 364, PersonalELO);
            WriteTlvInt(writer, 365, PVPMoney);
            WriteTlvInt(writer, 366, CharCatMoney);
            WriteTlvInt(writer, 367, CharCatMoneyWeek);
            WriteTlvInt(writer, 368, CharCatMoneyWeekMax);
            WriteTlvInt(writer, 369, HuntSoul);
            WriteTlvInt(writer, 370, WildHuntCamp);
            WriteTlvInt(writer, 371, WildHuntPhase);
            WriteTlvLong(writer, 372, WildHuntGuild);
            WriteTlvFloat(writer, 373, TotalCredit);
            WriteTlvInt(writer, 374, BattleCount);
            WriteTlvInt(writer, 375, FirstLoginTime);
            WriteTlvInt(writer, 376, LastHuntSoul);
            WriteTlvInt(writer, 377, TotalHRPoint);
            WriteTlvInt(writer, 378, LikeHunterOfficer);
            WriteTlvInt(writer, 381, HuntingCreditExchange);
            WriteStrictIntArray(writer, 382, PvPAttackLevelDef, MaxArrayElements);
            WriteTlvInt(writer, 383, LevelShowType);
            WriteStrictIntArray(writer, 384, CharEvadeTime, MaxArrayElements);
            WriteTlvInt(writer, 385, MonolopyRoundCount);
            WriteTlvInt(writer, 386, MonolopyActivity);
            WriteTlvInt(writer, 387, MonolopyCurGrid);
            WriteStrictIntArray(writer, 388, ZhanJiValue, MaxArrayElements);
            WriteTlvInt(writer, 389, ShouHunPoint);
            WriteTlvInt(writer, 390, LieHunPoint);
            WriteTlvInt(writer, 391, MVPPoint);
            WriteTlvInt(writer, 392, XHunterScore);
            WriteTlvFloat(writer, 393, BowBlastSpeedVeryFast);
            WriteTlvFloat(writer, 394, BowBlastSpeedFast);
            WriteTlvFloat(writer, 395, BowBlastSpeedNormal);
            WriteTlvFloat(writer, 396, BowBlastSpeedSlow);
            WriteTlvFloat(writer, 397, BowBlastSP);
            WriteStrictFloatArray(writer, 398, BowBlastSPMax, MaxArrayElements);
            WriteStrictFloatArray(writer, 399, CurrentBowBlastSpeed, MaxArrayElements);
            WriteTlvInt(writer, 400, MonolopyAccStep);
            WriteTlvInt(writer, 401, XHunterHighScore);
            WriteTlvInt(writer, 402, SoulStoneLevel);
            WriteTlvInt(writer, 404, WeekMoneyGain);
            WriteTlvInt(writer, 405, SoulStoneAtkLevel);
            WriteTlvInt(writer, 406, XHunterActivity);
            WriteTlvShort(writer, 407, IsNewbie);
            WriteTlvInt(writer, 408, LevelHuntSoul);
            WriteTlvInt(writer, 409, TaskHuntSoul);
            WriteTlvInt(writer, 410, CampHuntSoul);
            WriteTlvInt(writer, 411, PrivateRealHuntSoul);
            WriteTlvInt(writer, 412, HideWeaponBreakEffect);
            WriteTlvInt(writer, 413, IllustratePoint);
            WriteStrictIntArray(writer, 414, DynamiteAttack, MaxArrayElements);
            WriteStrictIntArray(writer, 415, DynamiteThrsh, MaxArrayElements);
            WriteTlvInt(writer, 416, DynamiteAccum);
            WriteTlvInt(writer, 417, DynamiteDef);
            WriteStrictIntArray(writer, 418, DynamiteBottleMax, MaxArrayElements);
            WriteTlvInt(writer, 419, DynamiteBottleUsed);
            WriteTlvInt(writer, 420, GuildBanChatEndTime);
        }
        #endregion


        public void SetFacialInfo(short[] facialInfo)
        {
            for (int i = 0; i < CsProtoConstant.CS_MAX_FACIALINFO_COUNT; i++)
            {
                FacialInfo[i] = facialInfo[i];
            }
        }
        public void SetCharLevel(int val)
        {
            SetProp(CharLevel, val);
        }
        public void SetCharSpeed(int val)
        {
            SetProp(CharSpeed, val);
        }

        public void SetCharMaxHP(int val)
        {
            SetProp(CharMaxHP, val);
        }

        public void SetCharMaxSta(int val)
        {
            SetProp(CharMaxSta, val);
        }

        private void SetProp(int[] prop, int val)
        {
            for (int i = 0; i < prop.Length; i++)
            {
                prop[i] = val;
            }
        }
    }
}
