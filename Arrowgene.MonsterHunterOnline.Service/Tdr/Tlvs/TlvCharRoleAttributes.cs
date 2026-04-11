using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for character role attributes (save data).
    /// C++ Reader: crygame.dll+sub_101B6660 (UnkTlv0192)
    /// C++ Printer: crygame.dll+sub_101B9A90
    /// </summary>
    public class TlvCharRoleAttributes : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Field ID: 1</summary>
        public long RoleGID { get; set; }

        /// <summary>Field ID: 2</summary>
        public int OCharLevel { get; set; }

        /// <summary>Field ID: 6</summary>
        public int OCharExp { get; set; }

        /// <summary>Field ID: 7</summary>
        public int OStarLevel { get; set; }

        /// <summary>Field ID: 16</summary>
        public int OCharHP { get; set; }

        /// <summary>Field ID: 17</summary>
        public int OCharMaxHP { get; set; }

        /// <summary>Field ID: 26</summary>
        public int OCharStr { get; set; }

        /// <summary>Field ID: 27</summary>
        public int OCharBst { get; set; }

        /// <summary>Field ID: 28</summary>
        public int OCharLck { get; set; }

        /// <summary>Field ID: 29</summary>
        public int OCharVgr { get; set; }

        /// <summary>Field ID: 32</summary>
        public int OCharDefence { get; set; }

        /// <summary>Field ID: 76</summary>
        public int OCharMoney { get; set; }

        /// <summary>Field ID: 77</summary>
        public int OCharBoundMoney { get; set; }

        /// <summary>Field ID: 78</summary>
        public int OCharCredit { get; set; }

        /// <summary>Field ID: 79</summary>
        public int OCharBoundCredit { get; set; }

        /// <summary>Field ID: 86</summary>
        public int OCharFatigue { get; set; }

        /// <summary>Field ID: 87</summary>
        public int OCharMaxFatigue { get; set; }

        /// <summary>Field ID: 90</summary>
        public int OClaymoreExp { get; set; }

        /// <summary>Field ID: 91</summary>
        public int OHammerExp { get; set; }

        /// <summary>Field ID: 92</summary>
        public int OKatanaExp { get; set; }

        /// <summary>Field ID: 93</summary>
        public int ODuelSwordExp { get; set; }

        /// <summary>Field ID: 94</summary>
        public int OSwordExp { get; set; }

        /// <summary>Field ID: 95</summary>
        public int OSpearExp { get; set; }

        /// <summary>Field ID: 96</summary>
        public int OGunExp { get; set; }

        /// <summary>Field ID: 97</summary>
        public int OBowExp { get; set; }

        /// <summary>Field ID: 98</summary>
        public int OCrossbowExp { get; set; }

        /// <summary>Field ID: 99</summary>
        public int OFluteExp { get; set; }

        /// <summary>Field ID: 108</summary>
        public int OMaleFace { get; set; }

        /// <summary>Field ID: 109</summary>
        public int OMaleHair { get; set; }

        /// <summary>Field ID: 153</summary>
        public int OEquipFoundDay { get; set; }

        /// <summary>Field ID: 173</summary>
        public int OUnderClothes { get; set; }

        /// <summary>Field ID: 174</summary>
        public int ONewbie { get; set; }

        /// <summary>Field ID: 180</summary>
        public int OCharContribution { get; set; }

        /// <summary>Field ID: 200</summary>
        public int OCharRemainsExp { get; set; }

        /// <summary>Field ID: 201</summary>
        public short OFarmOpenFlag { get; set; }

        /// <summary>Field ID: 202</summary>
        public int OFarmExp { get; set; }

        /// <summary>Field ID: 203</summary>
        public int OFarmEvaluation { get; set; }

        /// <summary>Field ID: 204</summary>
        public int OLastResetTime { get; set; }

        /// <summary>Field ID: 205</summary>
        public int OSkinColor { get; set; }

        /// <summary>Field ID: 206</summary>
        public int OHairColor { get; set; }

        /// <summary>Field ID: 207</summary>
        public int OInnerColor { get; set; }

        /// <summary>Field ID: 208</summary>
        public int OFaceTattooIndex { get; set; }

        /// <summary>Field ID: 209</summary>
        public int OEyeBall { get; set; }

        /// <summary>Field ID: 212</summary>
        public int OFarmFriendGatherCount { get; set; }

        /// <summary>Field ID: 220</summary>
        public int OFaceTattooColor { get; set; }

        /// <summary>Field ID: 221</summary>
        public int OEyeColor { get; set; }

        /// <summary>Field ID: 227</summary>
        public short OHideFashion { get; set; }

        /// <summary>Field ID: 228</summary>
        public short OHideSuite { get; set; }

        /// <summary>Field ID: 229</summary>
        public short OHideHelm { get; set; }

        /// <summary>Field ID: 230</summary>
        public int OCharCatCredit { get; set; }

        /// <summary>Field ID: 231</summary>
        public int OCharReviveCredit { get; set; }

        /// <summary>Field ID: 241</summary>
        public int OSystemUnlockData { get; set; }

        /// <summary>Field ID: 243</summary>
        public int OExtDailyExp { get; set; }

        /// <summary>Field ID: 252</summary>
        public short OFacialInfo1 { get; set; }

        /// <summary>Field ID: 253</summary>
        public short OFacialInfo2 { get; set; }

        /// <summary>Field ID: 254</summary>
        public short OFacialInfo3 { get; set; }

        /// <summary>Field ID: 255</summary>
        public short OFacialInfo4 { get; set; }

        /// <summary>Field ID: 256</summary>
        public short OFacialInfo5 { get; set; }

        /// <summary>Field ID: 257</summary>
        public short OFacialInfo6 { get; set; }

        /// <summary>Field ID: 258</summary>
        public short OFacialInfo7 { get; set; }

        /// <summary>Field ID: 259</summary>
        public short OFacialInfo8 { get; set; }

        /// <summary>Field ID: 260</summary>
        public short OFacialInfo9 { get; set; }

        /// <summary>Field ID: 261</summary>
        public short OFacialInfo10 { get; set; }

        /// <summary>Field ID: 262</summary>
        public short OFacialInfo11 { get; set; }

        /// <summary>Field ID: 263</summary>
        public short OFacialInfo12 { get; set; }

        /// <summary>Field ID: 264</summary>
        public short OFacialInfo13 { get; set; }

        /// <summary>Field ID: 265</summary>
        public short OFacialInfo14 { get; set; }

        /// <summary>Field ID: 266</summary>
        public short OFacialInfo15 { get; set; }

        /// <summary>Field ID: 267</summary>
        public short OFacialInfo16 { get; set; }

        /// <summary>Field ID: 268</summary>
        public short OFacialInfo17 { get; set; }

        /// <summary>Field ID: 269</summary>
        public short OFacialInfo18 { get; set; }

        /// <summary>Field ID: 270</summary>
        public short OFacialInfo19 { get; set; }

        /// <summary>Field ID: 271</summary>
        public short OFacialInfo20 { get; set; }

        /// <summary>Field ID: 272</summary>
        public short OFacialInfo21 { get; set; }

        /// <summary>Field ID: 273</summary>
        public short OFacialInfo22 { get; set; }

        /// <summary>Field ID: 274</summary>
        public short OFacialInfo23 { get; set; }

        /// <summary>Field ID: 275</summary>
        public short OFacialInfo24 { get; set; }

        /// <summary>Field ID: 276</summary>
        public short OFacialInfo25 { get; set; }

        /// <summary>Field ID: 284</summary>
        public short OVIP { get; set; }

        /// <summary>Field ID: 287</summary>
        public int OEntrustMoney1 { get; set; }

        /// <summary>Field ID: 288</summary>
        public int OEntrustMoney2 { get; set; }

        /// <summary>Field ID: 289</summary>
        public int OCharmFoundTimes { get; set; }

        /// <summary>Field ID: 290</summary>
        public int OWeapSysUnlockFlag { get; set; }

        /// <summary>Field ID: 291</summary>
        public int OCharRemainsDoubleExp { get; set; }

        /// <summary>Field ID: 292</summary>
        public int OExtDailyDoubleExp { get; set; }

        /// <summary>Field ID: 293</summary>
        public int OVIPLevel { get; set; }

        /// <summary>Field ID: 294</summary>
        public int OVIPExp { get; set; }

        /// <summary>Field ID: 295</summary>
        public int OVIPBaseEndTime { get; set; }

        /// <summary>Field ID: 296</summary>
        public int OVIPGrowthEndTime { get; set; }

        /// <summary>Field ID: 297</summary>
        public int OVIPProfitEndTime { get; set; }

        /// <summary>Field ID: 298</summary>
        public int OBanChatEndTime { get; set; }

        /// <summary>Field ID: 299</summary>
        public short OVIPVASFrozen { get; set; }

        /// <summary>Field ID: 300</summary>
        public short OVIPBaseCanUse { get; set; }

        /// <summary>Field ID: 301</summary>
        public short OVIPGrowthCanUse { get; set; }

        /// <summary>Field ID: 302</summary>
        public short OVIPProfitCanUse { get; set; }

        /// <summary>Field ID: 303</summary>
        public int OSystemUnlockExtData1 { get; set; }

        /// <summary>Field ID: 305</summary>
        public short OIsVIPBaseExpireNtf { get; set; }

        /// <summary>Field ID: 306</summary>
        public int OVIPBaseExpireLastNtfTime { get; set; }

        /// <summary>Field ID: 312</summary>
        public int OVIPVASFrozenTime { get; set; }

        /// <summary>Field ID: 313</summary>
        public int OClanScore { get; set; }

        /// <summary>Field ID: 314</summary>
        public int OClanScoreMax { get; set; }

        /// <summary>Field ID: 315</summary>
        public int OClanMoney { get; set; }

        /// <summary>Field ID: 322</summary>
        public int OCharHRLevel { get; set; }

        /// <summary>Field ID: 323</summary>
        public int OCharHRPoint { get; set; }

        /// <summary>Field ID: 324</summary>
        public int OClanMoneyPVP { get; set; }

        /// <summary>Field ID: 326</summary>
        public int OClanLeaveTime { get; set; }

        /// <summary>Field ID: 329</summary>
        public short OFacialInfo26 { get; set; }

        /// <summary>Field ID: 330</summary>
        public short OFacialInfo27 { get; set; }

        /// <summary>Field ID: 331</summary>
        public short OFacialInfo28 { get; set; }

        /// <summary>Field ID: 332</summary>
        public short OFacialInfo29 { get; set; }

        /// <summary>Field ID: 333</summary>
        public short OFacialInfo30 { get; set; }

        /// <summary>Field ID: 334</summary>
        public short OFacialInfo31 { get; set; }

        /// <summary>Field ID: 335</summary>
        public short OFacialInfo32 { get; set; }

        /// <summary>Field ID: 336</summary>
        public short OFacialInfo33 { get; set; }

        /// <summary>Field ID: 337</summary>
        public short OFacialInfo34 { get; set; }

        /// <summary>Field ID: 338</summary>
        public short OFacialInfo35 { get; set; }

        /// <summary>Field ID: 339</summary>
        public short OFacialInfo36 { get; set; }

        /// <summary>Field ID: 340</summary>
        public short OFacialInfo37 { get; set; }

        /// <summary>Field ID: 341</summary>
        public short OFacialInfo38 { get; set; }

        /// <summary>Field ID: 342</summary>
        public short OFacialInfo39 { get; set; }

        /// <summary>Field ID: 343</summary>
        public short OFacialInfo40 { get; set; }

        /// <summary>Field ID: 344</summary>
        public short OFacialInfo41 { get; set; }

        /// <summary>Field ID: 345</summary>
        public short OFacialInfo42 { get; set; }

        /// <summary>Field ID: 346</summary>
        public short OFacialInfo43 { get; set; }

        /// <summary>Field ID: 347</summary>
        public short OFacialInfo44 { get; set; }

        /// <summary>Field ID: 348</summary>
        public short OFacialInfo45 { get; set; }

        /// <summary>Field ID: 349</summary>
        public short OFacialInfo46 { get; set; }

        /// <summary>Field ID: 350</summary>
        public short OFacialInfo47 { get; set; }

        /// <summary>Field ID: 364</summary>
        public int OPersonalELO { get; set; }

        /// <summary>Field ID: 365</summary>
        public int OPVPMoney { get; set; }

        /// <summary>Field ID: 366</summary>
        public int OCharCatMoney { get; set; }

        /// <summary>Field ID: 367</summary>
        public int OCharCatMoneyWeek { get; set; }

        /// <summary>Field ID: 368</summary>
        public int OCharCatMoneyWeekMax { get; set; }

        /// <summary>Field ID: 369</summary>
        public int OHuntSoul { get; set; }

        /// <summary>Field ID: 370</summary>
        public int OWildHuntCamp { get; set; }

        /// <summary>Field ID: 371</summary>
        public int OWildHuntPhase { get; set; }

        /// <summary>Field ID: 372</summary>
        public long OWildHuntGuild { get; set; }

        /// <summary>Field ID: 373</summary>
        public int OTotalCredit { get; set; }

        /// <summary>Field ID: 374</summary>
        public int OBattleCount { get; set; }

        /// <summary>Field ID: 375</summary>
        public int OFirstLoginTime { get; set; }

        /// <summary>Field ID: 376</summary>
        public int OLastHuntSoul { get; set; }

        /// <summary>Field ID: 377</summary>
        public int OTotalHRPoint { get; set; }

        /// <summary>Field ID: 378</summary>
        public int OLikeHunterOfficer { get; set; }

        /// <summary>Field ID: 379</summary>
        public int OPetSkillMoral { get; set; }

        /// <summary>Field ID: 380</summary>
        public int OPetSkillUpgradeMoral { get; set; }

        /// <summary>Field ID: 381</summary>
        public int OHuntingCreditExchange { get; set; }

        /// <summary>Field ID: 383</summary>
        public int OLevelShowType { get; set; }

        /// <summary>Field ID: 385</summary>
        public int OMonolopyRoundCount { get; set; }

        /// <summary>Field ID: 386</summary>
        public int OMonolopyActivity { get; set; }

        /// <summary>Field ID: 387</summary>
        public int OMonolopyCurGrid { get; set; }

        /// <summary>Field ID: 389</summary>
        public int OShouHunPoint { get; set; }

        /// <summary>Field ID: 390</summary>
        public int OLieHunPoint { get; set; }

        /// <summary>Field ID: 391</summary>
        public int OMVPPoint { get; set; }

        /// <summary>Field ID: 392</summary>
        public int OXHunterScore { get; set; }

        /// <summary>Field ID: 400</summary>
        public int OMonolopyAccStep { get; set; }

        /// <summary>Field ID: 401</summary>
        public int OXHunterHighScore { get; set; }

        /// <summary>Field ID: 402</summary>
        public int OSoulStoneLevel { get; set; }

        /// <summary>Field ID: 403</summary>
        public int OWeeklyRefreshTime { get; set; }

        /// <summary>Field ID: 404</summary>
        public int OWeekMoneyGain { get; set; }

        /// <summary>Field ID: 405</summary>
        public int OSoulStoneAtkLevel { get; set; }

        /// <summary>Field ID: 406</summary>
        public int OXHunterActivity { get; set; }

        /// <summary>Field ID: 407</summary>
        public short OIsNewbie { get; set; }

        /// <summary>Field ID: 408</summary>
        public int OLevelHuntSoul { get; set; }

        /// <summary>Field ID: 409</summary>
        public int OTaskHuntSoul { get; set; }

        /// <summary>Field ID: 410</summary>
        public int OCampHuntSoul { get; set; }

        /// <summary>Field ID: 412</summary>
        public int OHideWeaponBreakEffect { get; set; }

        /// <summary>Field ID: 413</summary>
        public int OIllustratePoint { get; set; }

        /// <summary>Field ID: 414</summary>
        public int OGuildBanChatEndTime { get; set; }


        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: RoleGID = reader.ReadLong(); break;
                    case 2: OCharLevel = reader.ReadInt(); break;
                    case 6: OCharExp = reader.ReadInt(); break;
                    case 7: OStarLevel = reader.ReadInt(); break;
                    case 16: OCharHP = reader.ReadInt(); break;
                    case 17: OCharMaxHP = reader.ReadInt(); break;
                    case 26: OCharStr = reader.ReadInt(); break;
                    case 27: OCharBst = reader.ReadInt(); break;
                    case 28: OCharLck = reader.ReadInt(); break;
                    case 29: OCharVgr = reader.ReadInt(); break;
                    case 32: OCharDefence = reader.ReadInt(); break;
                    case 76: OCharMoney = reader.ReadInt(); break;
                    case 77: OCharBoundMoney = reader.ReadInt(); break;
                    case 78: OCharCredit = reader.ReadInt(); break;
                    case 79: OCharBoundCredit = reader.ReadInt(); break;
                    case 86: OCharFatigue = reader.ReadInt(); break;
                    case 87: OCharMaxFatigue = reader.ReadInt(); break;
                    case 90: OClaymoreExp = reader.ReadInt(); break;
                    case 91: OHammerExp = reader.ReadInt(); break;
                    case 92: OKatanaExp = reader.ReadInt(); break;
                    case 93: ODuelSwordExp = reader.ReadInt(); break;
                    case 94: OSwordExp = reader.ReadInt(); break;
                    case 95: OSpearExp = reader.ReadInt(); break;
                    case 96: OGunExp = reader.ReadInt(); break;
                    case 97: OBowExp = reader.ReadInt(); break;
                    case 98: OCrossbowExp = reader.ReadInt(); break;
                    case 99: OFluteExp = reader.ReadInt(); break;
                    case 108: OMaleFace = reader.ReadInt(); break;
                    case 109: OMaleHair = reader.ReadInt(); break;
                    case 153: OEquipFoundDay = reader.ReadInt(); break;
                    case 173: OUnderClothes = reader.ReadInt(); break;
                    case 174: ONewbie = reader.ReadInt(); break;
                    case 180: OCharContribution = reader.ReadInt(); break;
                    case 200: OCharRemainsExp = reader.ReadInt(); break;
                    case 201: OFarmOpenFlag = reader.ReadShort(); break;
                    case 202: OFarmExp = reader.ReadInt(); break;
                    case 203: OFarmEvaluation = reader.ReadInt(); break;
                    case 204: OLastResetTime = reader.ReadInt(); break;
                    case 205: OSkinColor = reader.ReadInt(); break;
                    case 206: OHairColor = reader.ReadInt(); break;
                    case 207: OInnerColor = reader.ReadInt(); break;
                    case 208: OFaceTattooIndex = reader.ReadInt(); break;
                    case 209: OEyeBall = reader.ReadInt(); break;
                    case 212: OFarmFriendGatherCount = reader.ReadInt(); break;
                    case 220: OFaceTattooColor = reader.ReadInt(); break;
                    case 221: OEyeColor = reader.ReadInt(); break;
                    case 227: OHideFashion = reader.ReadShort(); break;
                    case 228: OHideSuite = reader.ReadShort(); break;
                    case 229: OHideHelm = reader.ReadShort(); break;
                    case 230: OCharCatCredit = reader.ReadInt(); break;
                    case 231: OCharReviveCredit = reader.ReadInt(); break;
                    case 241: OSystemUnlockData = reader.ReadInt(); break;
                    case 243: OExtDailyExp = reader.ReadInt(); break;
                    case 252: OFacialInfo1 = reader.ReadShort(); break;
                    case 253: OFacialInfo2 = reader.ReadShort(); break;
                    case 254: OFacialInfo3 = reader.ReadShort(); break;
                    case 255: OFacialInfo4 = reader.ReadShort(); break;
                    case 256: OFacialInfo5 = reader.ReadShort(); break;
                    case 257: OFacialInfo6 = reader.ReadShort(); break;
                    case 258: OFacialInfo7 = reader.ReadShort(); break;
                    case 259: OFacialInfo8 = reader.ReadShort(); break;
                    case 260: OFacialInfo9 = reader.ReadShort(); break;
                    case 261: OFacialInfo10 = reader.ReadShort(); break;
                    case 262: OFacialInfo11 = reader.ReadShort(); break;
                    case 263: OFacialInfo12 = reader.ReadShort(); break;
                    case 264: OFacialInfo13 = reader.ReadShort(); break;
                    case 265: OFacialInfo14 = reader.ReadShort(); break;
                    case 266: OFacialInfo15 = reader.ReadShort(); break;
                    case 267: OFacialInfo16 = reader.ReadShort(); break;
                    case 268: OFacialInfo17 = reader.ReadShort(); break;
                    case 269: OFacialInfo18 = reader.ReadShort(); break;
                    case 270: OFacialInfo19 = reader.ReadShort(); break;
                    case 271: OFacialInfo20 = reader.ReadShort(); break;
                    case 272: OFacialInfo21 = reader.ReadShort(); break;
                    case 273: OFacialInfo22 = reader.ReadShort(); break;
                    case 274: OFacialInfo23 = reader.ReadShort(); break;
                    case 275: OFacialInfo24 = reader.ReadShort(); break;
                    case 276: OFacialInfo25 = reader.ReadShort(); break;
                    case 284: OVIP = reader.ReadShort(); break;
                    case 287: OEntrustMoney1 = reader.ReadInt(); break;
                    case 288: OEntrustMoney2 = reader.ReadInt(); break;
                    case 289: OCharmFoundTimes = reader.ReadInt(); break;
                    case 290: OWeapSysUnlockFlag = reader.ReadInt(); break;
                    case 291: OCharRemainsDoubleExp = reader.ReadInt(); break;
                    case 292: OExtDailyDoubleExp = reader.ReadInt(); break;
                    case 293: OVIPLevel = reader.ReadInt(); break;
                    case 294: OVIPExp = reader.ReadInt(); break;
                    case 295: OVIPBaseEndTime = reader.ReadInt(); break;
                    case 296: OVIPGrowthEndTime = reader.ReadInt(); break;
                    case 297: OVIPProfitEndTime = reader.ReadInt(); break;
                    case 298: OBanChatEndTime = reader.ReadInt(); break;
                    case 299: OVIPVASFrozen = reader.ReadShort(); break;
                    case 300: OVIPBaseCanUse = reader.ReadShort(); break;
                    case 301: OVIPGrowthCanUse = reader.ReadShort(); break;
                    case 302: OVIPProfitCanUse = reader.ReadShort(); break;
                    case 303: OSystemUnlockExtData1 = reader.ReadInt(); break;
                    case 305: OIsVIPBaseExpireNtf = reader.ReadShort(); break;
                    case 306: OVIPBaseExpireLastNtfTime = reader.ReadInt(); break;
                    case 312: OVIPVASFrozenTime = reader.ReadInt(); break;
                    case 313: OClanScore = reader.ReadInt(); break;
                    case 314: OClanScoreMax = reader.ReadInt(); break;
                    case 315: OClanMoney = reader.ReadInt(); break;
                    case 322: OCharHRLevel = reader.ReadInt(); break;
                    case 323: OCharHRPoint = reader.ReadInt(); break;
                    case 324: OClanMoneyPVP = reader.ReadInt(); break;
                    case 326: OClanLeaveTime = reader.ReadInt(); break;
                    case 329: OFacialInfo26 = reader.ReadShort(); break;
                    case 330: OFacialInfo27 = reader.ReadShort(); break;
                    case 331: OFacialInfo28 = reader.ReadShort(); break;
                    case 332: OFacialInfo29 = reader.ReadShort(); break;
                    case 333: OFacialInfo30 = reader.ReadShort(); break;
                    case 334: OFacialInfo31 = reader.ReadShort(); break;
                    case 335: OFacialInfo32 = reader.ReadShort(); break;
                    case 336: OFacialInfo33 = reader.ReadShort(); break;
                    case 337: OFacialInfo34 = reader.ReadShort(); break;
                    case 338: OFacialInfo35 = reader.ReadShort(); break;
                    case 339: OFacialInfo36 = reader.ReadShort(); break;
                    case 340: OFacialInfo37 = reader.ReadShort(); break;
                    case 341: OFacialInfo38 = reader.ReadShort(); break;
                    case 342: OFacialInfo39 = reader.ReadShort(); break;
                    case 343: OFacialInfo40 = reader.ReadShort(); break;
                    case 344: OFacialInfo41 = reader.ReadShort(); break;
                    case 345: OFacialInfo42 = reader.ReadShort(); break;
                    case 346: OFacialInfo43 = reader.ReadShort(); break;
                    case 347: OFacialInfo44 = reader.ReadShort(); break;
                    case 348: OFacialInfo45 = reader.ReadShort(); break;
                    case 349: OFacialInfo46 = reader.ReadShort(); break;
                    case 350: OFacialInfo47 = reader.ReadShort(); break;
                    case 364: OPersonalELO = reader.ReadInt(); break;
                    case 365: OPVPMoney = reader.ReadInt(); break;
                    case 366: OCharCatMoney = reader.ReadInt(); break;
                    case 367: OCharCatMoneyWeek = reader.ReadInt(); break;
                    case 368: OCharCatMoneyWeekMax = reader.ReadInt(); break;
                    case 369: OHuntSoul = reader.ReadInt(); break;
                    case 370: OWildHuntCamp = reader.ReadInt(); break;
                    case 371: OWildHuntPhase = reader.ReadInt(); break;
                    case 372: OWildHuntGuild = reader.ReadLong(); break;
                    case 373: OTotalCredit = reader.ReadInt(); break;
                    case 374: OBattleCount = reader.ReadInt(); break;
                    case 375: OFirstLoginTime = reader.ReadInt(); break;
                    case 376: OLastHuntSoul = reader.ReadInt(); break;
                    case 377: OTotalHRPoint = reader.ReadInt(); break;
                    case 378: OLikeHunterOfficer = reader.ReadInt(); break;
                    case 379: OPetSkillMoral = reader.ReadInt(); break;
                    case 380: OPetSkillUpgradeMoral = reader.ReadInt(); break;
                    case 381: OHuntingCreditExchange = reader.ReadInt(); break;
                    case 383: OLevelShowType = reader.ReadInt(); break;
                    case 385: OMonolopyRoundCount = reader.ReadInt(); break;
                    case 386: OMonolopyActivity = reader.ReadInt(); break;
                    case 387: OMonolopyCurGrid = reader.ReadInt(); break;
                    case 389: OShouHunPoint = reader.ReadInt(); break;
                    case 390: OLieHunPoint = reader.ReadInt(); break;
                    case 391: OMVPPoint = reader.ReadInt(); break;
                    case 392: OXHunterScore = reader.ReadInt(); break;
                    case 400: OMonolopyAccStep = reader.ReadInt(); break;
                    case 401: OXHunterHighScore = reader.ReadInt(); break;
                    case 402: OSoulStoneLevel = reader.ReadInt(); break;
                    case 403: OWeeklyRefreshTime = reader.ReadInt(); break;
                    case 404: OWeekMoneyGain = reader.ReadInt(); break;
                    case 405: OSoulStoneAtkLevel = reader.ReadInt(); break;
                    case 406: OXHunterActivity = reader.ReadInt(); break;
                    case 407: OIsNewbie = reader.ReadShort(); break;
                    case 408: OLevelHuntSoul = reader.ReadInt(); break;
                    case 409: OTaskHuntSoul = reader.ReadInt(); break;
                    case 410: OCampHuntSoul = reader.ReadInt(); break;
                    case 412: OHideWeaponBreakEffect = reader.ReadInt(); break;
                    case 413: OIllustratePoint = reader.ReadInt(); break;
                    case 414: OGuildBanChatEndTime = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvLong(writer, 1, RoleGID);
            WriteTlvInt(writer, 2, OCharLevel);
            WriteTlvInt(writer, 6, OCharExp);
            WriteTlvInt(writer, 7, OStarLevel);
            WriteTlvInt(writer, 16, OCharHP);
            WriteTlvInt(writer, 17, OCharMaxHP);
            WriteTlvInt(writer, 26, OCharStr);
            WriteTlvInt(writer, 27, OCharBst);
            WriteTlvInt(writer, 28, OCharLck);
            WriteTlvInt(writer, 29, OCharVgr);
            WriteTlvInt(writer, 32, OCharDefence);
            WriteTlvInt(writer, 76, OCharMoney);
            WriteTlvInt(writer, 77, OCharBoundMoney);
            WriteTlvInt(writer, 78, OCharCredit);
            WriteTlvInt(writer, 79, OCharBoundCredit);
            WriteTlvInt(writer, 86, OCharFatigue);
            WriteTlvInt(writer, 87, OCharMaxFatigue);
            WriteTlvInt(writer, 90, OClaymoreExp);
            WriteTlvInt(writer, 91, OHammerExp);
            WriteTlvInt(writer, 92, OKatanaExp);
            WriteTlvInt(writer, 93, ODuelSwordExp);
            WriteTlvInt(writer, 94, OSwordExp);
            WriteTlvInt(writer, 95, OSpearExp);
            WriteTlvInt(writer, 96, OGunExp);
            WriteTlvInt(writer, 97, OBowExp);
            WriteTlvInt(writer, 98, OCrossbowExp);
            WriteTlvInt(writer, 99, OFluteExp);
            WriteTlvInt(writer, 108, OMaleFace);
            WriteTlvInt(writer, 109, OMaleHair);
            WriteTlvInt(writer, 153, OEquipFoundDay);
            WriteTlvInt(writer, 173, OUnderClothes);
            WriteTlvInt(writer, 174, ONewbie);
            WriteTlvInt(writer, 180, OCharContribution);
            WriteTlvInt(writer, 200, OCharRemainsExp);
            WriteTlvShort(writer, 201, OFarmOpenFlag);
            WriteTlvInt(writer, 202, OFarmExp);
            WriteTlvInt(writer, 203, OFarmEvaluation);
            WriteTlvInt(writer, 204, OLastResetTime);
            WriteTlvInt(writer, 205, OSkinColor);
            WriteTlvInt(writer, 206, OHairColor);
            WriteTlvInt(writer, 207, OInnerColor);
            WriteTlvInt(writer, 208, OFaceTattooIndex);
            WriteTlvInt(writer, 209, OEyeBall);
            WriteTlvInt(writer, 212, OFarmFriendGatherCount);
            WriteTlvInt(writer, 220, OFaceTattooColor);
            WriteTlvInt(writer, 221, OEyeColor);
            WriteTlvShort(writer, 227, OHideFashion);
            WriteTlvShort(writer, 228, OHideSuite);
            WriteTlvShort(writer, 229, OHideHelm);
            WriteTlvInt(writer, 230, OCharCatCredit);
            WriteTlvInt(writer, 231, OCharReviveCredit);
            WriteTlvInt(writer, 241, OSystemUnlockData);
            WriteTlvInt(writer, 243, OExtDailyExp);
            WriteTlvShort(writer, 252, OFacialInfo1);
            WriteTlvShort(writer, 253, OFacialInfo2);
            WriteTlvShort(writer, 254, OFacialInfo3);
            WriteTlvShort(writer, 255, OFacialInfo4);
            WriteTlvShort(writer, 256, OFacialInfo5);
            WriteTlvShort(writer, 257, OFacialInfo6);
            WriteTlvShort(writer, 258, OFacialInfo7);
            WriteTlvShort(writer, 259, OFacialInfo8);
            WriteTlvShort(writer, 260, OFacialInfo9);
            WriteTlvShort(writer, 261, OFacialInfo10);
            WriteTlvShort(writer, 262, OFacialInfo11);
            WriteTlvShort(writer, 263, OFacialInfo12);
            WriteTlvShort(writer, 264, OFacialInfo13);
            WriteTlvShort(writer, 265, OFacialInfo14);
            WriteTlvShort(writer, 266, OFacialInfo15);
            WriteTlvShort(writer, 267, OFacialInfo16);
            WriteTlvShort(writer, 268, OFacialInfo17);
            WriteTlvShort(writer, 269, OFacialInfo18);
            WriteTlvShort(writer, 270, OFacialInfo19);
            WriteTlvShort(writer, 271, OFacialInfo20);
            WriteTlvShort(writer, 272, OFacialInfo21);
            WriteTlvShort(writer, 273, OFacialInfo22);
            WriteTlvShort(writer, 274, OFacialInfo23);
            WriteTlvShort(writer, 275, OFacialInfo24);
            WriteTlvShort(writer, 276, OFacialInfo25);
            WriteTlvShort(writer, 284, OVIP);
            WriteTlvInt(writer, 287, OEntrustMoney1);
            WriteTlvInt(writer, 288, OEntrustMoney2);
            WriteTlvInt(writer, 289, OCharmFoundTimes);
            WriteTlvInt(writer, 290, OWeapSysUnlockFlag);
            WriteTlvInt(writer, 291, OCharRemainsDoubleExp);
            WriteTlvInt(writer, 292, OExtDailyDoubleExp);
            WriteTlvInt(writer, 293, OVIPLevel);
            WriteTlvInt(writer, 294, OVIPExp);
            WriteTlvInt(writer, 295, OVIPBaseEndTime);
            WriteTlvInt(writer, 296, OVIPGrowthEndTime);
            WriteTlvInt(writer, 297, OVIPProfitEndTime);
            WriteTlvInt(writer, 298, OBanChatEndTime);
            WriteTlvShort(writer, 299, OVIPVASFrozen);
            WriteTlvShort(writer, 300, OVIPBaseCanUse);
            WriteTlvShort(writer, 301, OVIPGrowthCanUse);
            WriteTlvShort(writer, 302, OVIPProfitCanUse);
            WriteTlvInt(writer, 303, OSystemUnlockExtData1);
            WriteTlvShort(writer, 305, OIsVIPBaseExpireNtf);
            WriteTlvInt(writer, 306, OVIPBaseExpireLastNtfTime);
            WriteTlvInt(writer, 312, OVIPVASFrozenTime);
            WriteTlvInt(writer, 313, OClanScore);
            WriteTlvInt(writer, 314, OClanScoreMax);
            WriteTlvInt(writer, 315, OClanMoney);
            WriteTlvInt(writer, 322, OCharHRLevel);
            WriteTlvInt(writer, 323, OCharHRPoint);
            WriteTlvInt(writer, 324, OClanMoneyPVP);
            WriteTlvInt(writer, 326, OClanLeaveTime);
            WriteTlvShort(writer, 329, OFacialInfo26);
            WriteTlvShort(writer, 330, OFacialInfo27);
            WriteTlvShort(writer, 331, OFacialInfo28);
            WriteTlvShort(writer, 332, OFacialInfo29);
            WriteTlvShort(writer, 333, OFacialInfo30);
            WriteTlvShort(writer, 334, OFacialInfo31);
            WriteTlvShort(writer, 335, OFacialInfo32);
            WriteTlvShort(writer, 336, OFacialInfo33);
            WriteTlvShort(writer, 337, OFacialInfo34);
            WriteTlvShort(writer, 338, OFacialInfo35);
            WriteTlvShort(writer, 339, OFacialInfo36);
            WriteTlvShort(writer, 340, OFacialInfo37);
            WriteTlvShort(writer, 341, OFacialInfo38);
            WriteTlvShort(writer, 342, OFacialInfo39);
            WriteTlvShort(writer, 343, OFacialInfo40);
            WriteTlvShort(writer, 344, OFacialInfo41);
            WriteTlvShort(writer, 345, OFacialInfo42);
            WriteTlvShort(writer, 346, OFacialInfo43);
            WriteTlvShort(writer, 347, OFacialInfo44);
            WriteTlvShort(writer, 348, OFacialInfo45);
            WriteTlvShort(writer, 349, OFacialInfo46);
            WriteTlvShort(writer, 350, OFacialInfo47);
            WriteTlvInt(writer, 364, OPersonalELO);
            WriteTlvInt(writer, 365, OPVPMoney);
            WriteTlvInt(writer, 366, OCharCatMoney);
            WriteTlvInt(writer, 367, OCharCatMoneyWeek);
            WriteTlvInt(writer, 368, OCharCatMoneyWeekMax);
            WriteTlvInt(writer, 369, OHuntSoul);
            WriteTlvInt(writer, 370, OWildHuntCamp);
            WriteTlvInt(writer, 371, OWildHuntPhase);
            WriteTlvLong(writer, 372, OWildHuntGuild);
            WriteTlvInt(writer, 373, OTotalCredit);
            WriteTlvInt(writer, 374, OBattleCount);
            WriteTlvInt(writer, 375, OFirstLoginTime);
            WriteTlvInt(writer, 376, OLastHuntSoul);
            WriteTlvInt(writer, 377, OTotalHRPoint);
            WriteTlvInt(writer, 378, OLikeHunterOfficer);
            WriteTlvInt(writer, 379, OPetSkillMoral);
            WriteTlvInt(writer, 380, OPetSkillUpgradeMoral);
            WriteTlvInt(writer, 381, OHuntingCreditExchange);
            WriteTlvInt(writer, 383, OLevelShowType);
            WriteTlvInt(writer, 385, OMonolopyRoundCount);
            WriteTlvInt(writer, 386, OMonolopyActivity);
            WriteTlvInt(writer, 387, OMonolopyCurGrid);
            WriteTlvInt(writer, 389, OShouHunPoint);
            WriteTlvInt(writer, 390, OLieHunPoint);
            WriteTlvInt(writer, 391, OMVPPoint);
            WriteTlvInt(writer, 392, OXHunterScore);
            WriteTlvInt(writer, 400, OMonolopyAccStep);
            WriteTlvInt(writer, 401, OXHunterHighScore);
            WriteTlvInt(writer, 402, OSoulStoneLevel);
            WriteTlvInt(writer, 403, OWeeklyRefreshTime);
            WriteTlvInt(writer, 404, OWeekMoneyGain);
            WriteTlvInt(writer, 405, OSoulStoneAtkLevel);
            WriteTlvInt(writer, 406, OXHunterActivity);
            WriteTlvShort(writer, 407, OIsNewbie);
            WriteTlvInt(writer, 408, OLevelHuntSoul);
            WriteTlvInt(writer, 409, OTaskHuntSoul);
            WriteTlvInt(writer, 410, OCampHuntSoul);
            WriteTlvInt(writer, 412, OHideWeaponBreakEffect);
            WriteTlvInt(writer, 413, OIllustratePoint);
            WriteTlvInt(writer, 414, OGuildBanChatEndTime);
        }
    }
}
