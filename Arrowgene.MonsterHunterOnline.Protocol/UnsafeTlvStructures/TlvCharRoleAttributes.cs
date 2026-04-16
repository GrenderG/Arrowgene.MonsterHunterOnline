using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for character role attributes (save data).
    /// C++ Reader: crygame.dll+sub_101B6660 (UnkTlv0192)
    /// C++ Printer: crygame.dll+sub_101B9A90
    /// </summary>
    public class TlvCharRoleAttributes : Structure, ITlvStructure
    {
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


        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt64(buffer, 1, RoleGID);
            WriteTlvInt32(buffer, 2, OCharLevel);
            WriteTlvInt32(buffer, 6, OCharExp);
            WriteTlvInt32(buffer, 7, OStarLevel);
            WriteTlvInt32(buffer, 16, OCharHP);
            WriteTlvInt32(buffer, 17, OCharMaxHP);
            WriteTlvInt32(buffer, 26, OCharStr);
            WriteTlvInt32(buffer, 27, OCharBst);
            WriteTlvInt32(buffer, 28, OCharLck);
            WriteTlvInt32(buffer, 29, OCharVgr);
            WriteTlvInt32(buffer, 32, OCharDefence);
            WriteTlvInt32(buffer, 76, OCharMoney);
            WriteTlvInt32(buffer, 77, OCharBoundMoney);
            WriteTlvInt32(buffer, 78, OCharCredit);
            WriteTlvInt32(buffer, 79, OCharBoundCredit);
            WriteTlvInt32(buffer, 86, OCharFatigue);
            WriteTlvInt32(buffer, 87, OCharMaxFatigue);
            WriteTlvInt32(buffer, 90, OClaymoreExp);
            WriteTlvInt32(buffer, 91, OHammerExp);
            WriteTlvInt32(buffer, 92, OKatanaExp);
            WriteTlvInt32(buffer, 93, ODuelSwordExp);
            WriteTlvInt32(buffer, 94, OSwordExp);
            WriteTlvInt32(buffer, 95, OSpearExp);
            WriteTlvInt32(buffer, 96, OGunExp);
            WriteTlvInt32(buffer, 97, OBowExp);
            WriteTlvInt32(buffer, 98, OCrossbowExp);
            WriteTlvInt32(buffer, 99, OFluteExp);
            WriteTlvInt32(buffer, 108, OMaleFace);
            WriteTlvInt32(buffer, 109, OMaleHair);
            WriteTlvInt32(buffer, 153, OEquipFoundDay);
            WriteTlvInt32(buffer, 173, OUnderClothes);
            WriteTlvInt32(buffer, 174, ONewbie);
            WriteTlvInt32(buffer, 180, OCharContribution);
            WriteTlvInt32(buffer, 200, OCharRemainsExp);
            WriteTlvInt16(buffer, 201, OFarmOpenFlag);
            WriteTlvInt32(buffer, 202, OFarmExp);
            WriteTlvInt32(buffer, 203, OFarmEvaluation);
            WriteTlvInt32(buffer, 204, OLastResetTime);
            WriteTlvInt32(buffer, 205, OSkinColor);
            WriteTlvInt32(buffer, 206, OHairColor);
            WriteTlvInt32(buffer, 207, OInnerColor);
            WriteTlvInt32(buffer, 208, OFaceTattooIndex);
            WriteTlvInt32(buffer, 209, OEyeBall);
            WriteTlvInt32(buffer, 212, OFarmFriendGatherCount);
            WriteTlvInt32(buffer, 220, OFaceTattooColor);
            WriteTlvInt32(buffer, 221, OEyeColor);
            WriteTlvInt16(buffer, 227, OHideFashion);
            WriteTlvInt16(buffer, 228, OHideSuite);
            WriteTlvInt16(buffer, 229, OHideHelm);
            WriteTlvInt32(buffer, 230, OCharCatCredit);
            WriteTlvInt32(buffer, 231, OCharReviveCredit);
            WriteTlvInt32(buffer, 241, OSystemUnlockData);
            WriteTlvInt32(buffer, 243, OExtDailyExp);
            WriteTlvInt16(buffer, 252, OFacialInfo1);
            WriteTlvInt16(buffer, 253, OFacialInfo2);
            WriteTlvInt16(buffer, 254, OFacialInfo3);
            WriteTlvInt16(buffer, 255, OFacialInfo4);
            WriteTlvInt16(buffer, 256, OFacialInfo5);
            WriteTlvInt16(buffer, 257, OFacialInfo6);
            WriteTlvInt16(buffer, 258, OFacialInfo7);
            WriteTlvInt16(buffer, 259, OFacialInfo8);
            WriteTlvInt16(buffer, 260, OFacialInfo9);
            WriteTlvInt16(buffer, 261, OFacialInfo10);
            WriteTlvInt16(buffer, 262, OFacialInfo11);
            WriteTlvInt16(buffer, 263, OFacialInfo12);
            WriteTlvInt16(buffer, 264, OFacialInfo13);
            WriteTlvInt16(buffer, 265, OFacialInfo14);
            WriteTlvInt16(buffer, 266, OFacialInfo15);
            WriteTlvInt16(buffer, 267, OFacialInfo16);
            WriteTlvInt16(buffer, 268, OFacialInfo17);
            WriteTlvInt16(buffer, 269, OFacialInfo18);
            WriteTlvInt16(buffer, 270, OFacialInfo19);
            WriteTlvInt16(buffer, 271, OFacialInfo20);
            WriteTlvInt16(buffer, 272, OFacialInfo21);
            WriteTlvInt16(buffer, 273, OFacialInfo22);
            WriteTlvInt16(buffer, 274, OFacialInfo23);
            WriteTlvInt16(buffer, 275, OFacialInfo24);
            WriteTlvInt16(buffer, 276, OFacialInfo25);
            WriteTlvInt16(buffer, 284, OVIP);
            WriteTlvInt32(buffer, 287, OEntrustMoney1);
            WriteTlvInt32(buffer, 288, OEntrustMoney2);
            WriteTlvInt32(buffer, 289, OCharmFoundTimes);
            WriteTlvInt32(buffer, 290, OWeapSysUnlockFlag);
            WriteTlvInt32(buffer, 291, OCharRemainsDoubleExp);
            WriteTlvInt32(buffer, 292, OExtDailyDoubleExp);
            WriteTlvInt32(buffer, 293, OVIPLevel);
            WriteTlvInt32(buffer, 294, OVIPExp);
            WriteTlvInt32(buffer, 295, OVIPBaseEndTime);
            WriteTlvInt32(buffer, 296, OVIPGrowthEndTime);
            WriteTlvInt32(buffer, 297, OVIPProfitEndTime);
            WriteTlvInt32(buffer, 298, OBanChatEndTime);
            WriteTlvInt16(buffer, 299, OVIPVASFrozen);
            WriteTlvInt16(buffer, 300, OVIPBaseCanUse);
            WriteTlvInt16(buffer, 301, OVIPGrowthCanUse);
            WriteTlvInt16(buffer, 302, OVIPProfitCanUse);
            WriteTlvInt32(buffer, 303, OSystemUnlockExtData1);
            WriteTlvInt16(buffer, 305, OIsVIPBaseExpireNtf);
            WriteTlvInt32(buffer, 306, OVIPBaseExpireLastNtfTime);
            WriteTlvInt32(buffer, 312, OVIPVASFrozenTime);
            WriteTlvInt32(buffer, 313, OClanScore);
            WriteTlvInt32(buffer, 314, OClanScoreMax);
            WriteTlvInt32(buffer, 315, OClanMoney);
            WriteTlvInt32(buffer, 322, OCharHRLevel);
            WriteTlvInt32(buffer, 323, OCharHRPoint);
            WriteTlvInt32(buffer, 324, OClanMoneyPVP);
            WriteTlvInt32(buffer, 326, OClanLeaveTime);
            WriteTlvInt16(buffer, 329, OFacialInfo26);
            WriteTlvInt16(buffer, 330, OFacialInfo27);
            WriteTlvInt16(buffer, 331, OFacialInfo28);
            WriteTlvInt16(buffer, 332, OFacialInfo29);
            WriteTlvInt16(buffer, 333, OFacialInfo30);
            WriteTlvInt16(buffer, 334, OFacialInfo31);
            WriteTlvInt16(buffer, 335, OFacialInfo32);
            WriteTlvInt16(buffer, 336, OFacialInfo33);
            WriteTlvInt16(buffer, 337, OFacialInfo34);
            WriteTlvInt16(buffer, 338, OFacialInfo35);
            WriteTlvInt16(buffer, 339, OFacialInfo36);
            WriteTlvInt16(buffer, 340, OFacialInfo37);
            WriteTlvInt16(buffer, 341, OFacialInfo38);
            WriteTlvInt16(buffer, 342, OFacialInfo39);
            WriteTlvInt16(buffer, 343, OFacialInfo40);
            WriteTlvInt16(buffer, 344, OFacialInfo41);
            WriteTlvInt16(buffer, 345, OFacialInfo42);
            WriteTlvInt16(buffer, 346, OFacialInfo43);
            WriteTlvInt16(buffer, 347, OFacialInfo44);
            WriteTlvInt16(buffer, 348, OFacialInfo45);
            WriteTlvInt16(buffer, 349, OFacialInfo46);
            WriteTlvInt16(buffer, 350, OFacialInfo47);
            WriteTlvInt32(buffer, 364, OPersonalELO);
            WriteTlvInt32(buffer, 365, OPVPMoney);
            WriteTlvInt32(buffer, 366, OCharCatMoney);
            WriteTlvInt32(buffer, 367, OCharCatMoneyWeek);
            WriteTlvInt32(buffer, 368, OCharCatMoneyWeekMax);
            WriteTlvInt32(buffer, 369, OHuntSoul);
            WriteTlvInt32(buffer, 370, OWildHuntCamp);
            WriteTlvInt32(buffer, 371, OWildHuntPhase);
            WriteTlvInt64(buffer, 372, OWildHuntGuild);
            WriteTlvInt32(buffer, 373, OTotalCredit);
            WriteTlvInt32(buffer, 374, OBattleCount);
            WriteTlvInt32(buffer, 375, OFirstLoginTime);
            WriteTlvInt32(buffer, 376, OLastHuntSoul);
            WriteTlvInt32(buffer, 377, OTotalHRPoint);
            WriteTlvInt32(buffer, 378, OLikeHunterOfficer);
            WriteTlvInt32(buffer, 379, OPetSkillMoral);
            WriteTlvInt32(buffer, 380, OPetSkillUpgradeMoral);
            WriteTlvInt32(buffer, 381, OHuntingCreditExchange);
            WriteTlvInt32(buffer, 383, OLevelShowType);
            WriteTlvInt32(buffer, 385, OMonolopyRoundCount);
            WriteTlvInt32(buffer, 386, OMonolopyActivity);
            WriteTlvInt32(buffer, 387, OMonolopyCurGrid);
            WriteTlvInt32(buffer, 389, OShouHunPoint);
            WriteTlvInt32(buffer, 390, OLieHunPoint);
            WriteTlvInt32(buffer, 391, OMVPPoint);
            WriteTlvInt32(buffer, 392, OXHunterScore);
            WriteTlvInt32(buffer, 400, OMonolopyAccStep);
            WriteTlvInt32(buffer, 401, OXHunterHighScore);
            WriteTlvInt32(buffer, 402, OSoulStoneLevel);
            WriteTlvInt32(buffer, 403, OWeeklyRefreshTime);
            WriteTlvInt32(buffer, 404, OWeekMoneyGain);
            WriteTlvInt32(buffer, 405, OSoulStoneAtkLevel);
            WriteTlvInt32(buffer, 406, OXHunterActivity);
            WriteTlvInt16(buffer, 407, OIsNewbie);
            WriteTlvInt32(buffer, 408, OLevelHuntSoul);
            WriteTlvInt32(buffer, 409, OTaskHuntSoul);
            WriteTlvInt32(buffer, 410, OCampHuntSoul);
            WriteTlvInt32(buffer, 412, OHideWeaponBreakEffect);
            WriteTlvInt32(buffer, 413, OIllustratePoint);
            WriteTlvInt32(buffer, 414, OGuildBanChatEndTime);
        }
    }
}
