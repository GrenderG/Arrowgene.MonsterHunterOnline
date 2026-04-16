using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for character public view attributes.
    /// C++ Reader: crygame.dll+sub_101E6B40 (UnkTlv0195)
    /// C++ Printer: crygame.dll+sub_101EA070
    /// </summary>
    public class TlvCharPublicAttributes : Structure, ITlvStructure
    {
        /// <summary>Field ID: 2</summary>
        public int OCharLevel { get; set; }

        /// <summary>Field ID: 4</summary>
        public int OCharSex { get; set; }

        /// <summary>Field ID: 6</summary>
        public int OCharExp { get; set; }

        /// <summary>Field ID: 7</summary>
        public int OStarLevel { get; set; }

        /// <summary>Field ID: 8</summary>
        public int OStarCollection { get; set; }

        /// <summary>Field ID: 9</summary>
        public int OStarQuest { get; set; }

        /// <summary>Field ID: 10</summary>
        public int OStarCombat { get; set; }

        /// <summary>Field ID: 11</summary>
        public int OStarPet { get; set; }

        /// <summary>Field ID: 12</summary>
        public int OStarProduct { get; set; }

        /// <summary>Field ID: 13</summary>
        public int OStarGuild { get; set; }

        /// <summary>Field ID: 14</summary>
        public int OStarTame { get; set; }

        /// <summary>Field ID: 15</summary>
        public int OStarPvP { get; set; }

        /// <summary>Field ID: 16</summary>
        public int OCharHP { get; set; }

        /// <summary>Field ID: 17</summary>
        public int OCharMaxHP { get; set; }

        /// <summary>Field ID: 20</summary>
        public short ODeath { get; set; }

        /// <summary>Field ID: 22</summary>
        public int OCharMaxSta { get; set; }

        /// <summary>Field ID: 26</summary>
        public int OCharStr { get; set; }

        /// <summary>Field ID: 27</summary>
        public int OCharBst { get; set; }

        /// <summary>Field ID: 28</summary>
        public int OCharLck { get; set; }

        /// <summary>Field ID: 29</summary>
        public int OCharVgr { get; set; }

        /// <summary>Field ID: 30</summary>
        public int OCharMelee { get; set; }

        /// <summary>Field ID: 31</summary>
        public int OCharRange { get; set; }

        /// <summary>Field ID: 32</summary>
        public int OCharDefence { get; set; }

        /// <summary>Field ID: 33</summary>
        public int OCritLevel { get; set; }

        /// <summary>Field ID: 34</summary>
        public int OCritDmg { get; set; }

        /// <summary>Field ID: 35</summary>
        public int OAntiCritDmg { get; set; }

        /// <summary>Field ID: 39</summary>
        public int OWaterAttack { get; set; }

        /// <summary>Field ID: 40</summary>
        public int OFireAttack { get; set; }

        /// <summary>Field ID: 41</summary>
        public int OLightningAttack { get; set; }

        /// <summary>Field ID: 42</summary>
        public int ODragonAttack { get; set; }

        /// <summary>Field ID: 43</summary>
        public int OIceAttack { get; set; }

        /// <summary>Field ID: 44</summary>
        public int ONonAttack { get; set; }

        /// <summary>Field ID: 45</summary>
        public int OPoisonAttack { get; set; }

        /// <summary>Field ID: 46</summary>
        public int OSleepyAttack { get; set; }

        /// <summary>Field ID: 47</summary>
        public int OParalysisAttack { get; set; }

        /// <summary>Field ID: 48</summary>
        public int OWaterRes { get; set; }

        /// <summary>Field ID: 49</summary>
        public int OFireRes { get; set; }

        /// <summary>Field ID: 50</summary>
        public int OLightningRes { get; set; }

        /// <summary>Field ID: 51</summary>
        public int ODragonRes { get; set; }

        /// <summary>Field ID: 52</summary>
        public int OIceRes { get; set; }

        /// <summary>Field ID: 68</summary>
        public int OParaThrsh { get; set; }

        /// <summary>Field ID: 88</summary>
        public int OCharEnergy { get; set; }

        /// <summary>Field ID: 89</summary>
        public int OCharMaxEnergy { get; set; }

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

        /// <summary>Field ID: 106</summary>
        public int OCharGuild { get; set; }

        /// <summary>Field ID: 107</summary>
        public int OCharWarteam { get; set; }

        /// <summary>Field ID: 108</summary>
        public int OMaleFace { get; set; }

        /// <summary>Field ID: 109</summary>
        public int OMaleHair { get; set; }

        /// <summary>Field ID: 134</summary>
        public int OPVEDef { get; set; }

        /// <summary>Field ID: 135</summary>
        public int OPVEDefAngle { get; set; }

        /// <summary>Field ID: 136</summary>
        public int OPVPDef { get; set; }

        /// <summary>Field ID: 137</summary>
        public int OPVPDefAngle { get; set; }

        /// <summary>Field ID: 173</summary>
        public int OUnderClothes { get; set; }

        /// <summary>Field ID: 174</summary>
        public int ONewbie { get; set; }

        /// <summary>Field ID: 175</summary>
        public int OStateFlag { get; set; }

        /// <summary>Field ID: 177</summary>
        public int OPetCarryNum { get; set; }

        /// <summary>Field ID: 178</summary>
        public int OPetHomeNum { get; set; }

        /// <summary>Field ID: 179</summary>
        public int OPetOwendNumMax { get; set; }

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

        /// <summary>Field ID: 210</summary>
        public int OAdditionalHate { get; set; }

        /// <summary>Field ID: 211</summary>
        public int OPlayerAtk { get; set; }

        /// <summary>Field ID: 212</summary>
        public int OFarmFriendGatherCount { get; set; }

        /// <summary>Field ID: 214</summary>
        public int OPlayerCrit { get; set; }

        /// <summary>Field ID: 220</summary>
        public int OFaceTattooColor { get; set; }

        /// <summary>Field ID: 221</summary>
        public int OEyeColor { get; set; }

        /// <summary>Field ID: 222</summary>
        public int OAttrAtkFlag { get; set; }

        /// <summary>Field ID: 223</summary>
        public int OCombatNPCID { get; set; }

        /// <summary>Field ID: 224</summary>
        public int OBattleState { get; set; }

        /// <summary>Field ID: 226</summary>
        public int OHammerModeTime { get; set; }

        /// <summary>Field ID: 227</summary>
        public short OHideFashion { get; set; }

        /// <summary>Field ID: 228</summary>
        public short OHideSuite { get; set; }

        /// <summary>Field ID: 229</summary>
        public short OHideHelm { get; set; }

        /// <summary>Field ID: 232</summary>
        public int OJinLiValue { get; set; }

        /// <summary>Field ID: 233</summary>
        public int OJinLiStep1MaxValue { get; set; }

        /// <summary>Field ID: 234</summary>
        public int OJinLiStep2MaxValue { get; set; }

        /// <summary>Field ID: 235</summary>
        public int OJinLiStep1ReduceValue { get; set; }

        /// <summary>Field ID: 236</summary>
        public int OJinLiStep2ReduceValue { get; set; }

        /// <summary>Field ID: 237</summary>
        public int OEquipTitleID { get; set; }

        /// <summary>Field ID: 238</summary>
        public int OTitleExp { get; set; }

        /// <summary>Field ID: 239</summary>
        public int OTitleLevel { get; set; }

        /// <summary>Field ID: 240</summary>
        public int OEquipTitleBuff { get; set; }

        /// <summary>Field ID: 241</summary>
        public int OSystemUnlockData { get; set; }

        /// <summary>Field ID: 242</summary>
        public int OGuildContribution { get; set; }

        /// <summary>Field ID: 243</summary>
        public int OExtDailyExp { get; set; }

        /// <summary>Field ID: 244</summary>
        public int OGuildId { get; set; }

        /// <summary>Field ID: 245</summary>
        public short OTeamPasswordFlag { get; set; }

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

        /// <summary>Field ID: 293</summary>
        public int OVIPLevel { get; set; }

        /// <summary>Field ID: 294</summary>
        public int OVIPExp { get; set; }

        /// <summary>Field ID: 300</summary>
        public short OVIPBaseCanUse { get; set; }

        /// <summary>Field ID: 307</summary>
        public short OGameVIP { get; set; }

        /// <summary>Field ID: 308</summary>
        public short OQQVIP { get; set; }

        /// <summary>Field ID: 309</summary>
        public short OYearQQVIP { get; set; }

        /// <summary>Field ID: 310</summary>
        public short OSuperQQVIP { get; set; }

        /// <summary>Field ID: 311</summary>
        public short ONetbarLevel { get; set; }

        /// <summary>Field ID: 322</summary>
        public int OCharHRLevel { get; set; }

        /// <summary>Field ID: 323</summary>
        public int OCharHRPoint { get; set; }

        /// <summary>Field ID: 328</summary>
        public short OXYVIP { get; set; }

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

        /// <summary>Field ID: 352</summary>
        public short OTGPVIP { get; set; }

        /// <summary>Field ID: 359</summary>
        public int OFluteTune { get; set; }

        /// <summary>Field ID: 360</summary>
        public int ODefenseReduceHPModifyRate { get; set; }

        /// <summary>Field ID: 361</summary>
        public int ODefenseReduceStaModifyRate { get; set; }

        /// <summary>Field ID: 370</summary>
        public int OWildHuntCamp { get; set; }

        /// <summary>Field ID: 377</summary>
        public int OTotalHRPoint { get; set; }

        /// <summary>Field ID: 378</summary>
        public int OLikeHunterOfficer { get; set; }

        /// <summary>Field ID: 383</summary>
        public int OLevelShowType { get; set; }

        /// <summary>Field ID: 385</summary>
        public int OMonolopyRoundCount { get; set; }

        /// <summary>Field ID: 386</summary>
        public int OMonolopyActivity { get; set; }

        /// <summary>Field ID: 387</summary>
        public int OMonolopyCurGrid { get; set; }

        /// <summary>Field ID: 402</summary>
        public int OSoulStoneLevel { get; set; }

        /// <summary>Field ID: 403</summary>
        public int OWeeklyRefreshTime { get; set; }

        /// <summary>Field ID: 405</summary>
        public int OSoulStoneAtkLevel { get; set; }

        /// <summary>Field ID: 406</summary>
        public int ODynamiteAttack { get; set; }

        /// <summary>Field ID: 407</summary>
        public int ODynamiteThrsh { get; set; }

        /// <summary>Field ID: 408</summary>
        public int OGuildBanChatEndTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 2, OCharLevel);
            WriteTlvInt32(buffer, 4, OCharSex);
            WriteTlvInt32(buffer, 6, OCharExp);
            WriteTlvInt32(buffer, 7, OStarLevel);
            WriteTlvInt32(buffer, 8, OStarCollection);
            WriteTlvInt32(buffer, 9, OStarQuest);
            WriteTlvInt32(buffer, 10, OStarCombat);
            WriteTlvInt32(buffer, 11, OStarPet);
            WriteTlvInt32(buffer, 12, OStarProduct);
            WriteTlvInt32(buffer, 13, OStarGuild);
            WriteTlvInt32(buffer, 14, OStarTame);
            WriteTlvInt32(buffer, 15, OStarPvP);
            WriteTlvInt32(buffer, 16, OCharHP);
            WriteTlvInt32(buffer, 17, OCharMaxHP);
            WriteTlvInt16(buffer, 20, ODeath);
            WriteTlvInt32(buffer, 22, OCharMaxSta);
            WriteTlvInt32(buffer, 26, OCharStr);
            WriteTlvInt32(buffer, 27, OCharBst);
            WriteTlvInt32(buffer, 28, OCharLck);
            WriteTlvInt32(buffer, 29, OCharVgr);
            WriteTlvInt32(buffer, 30, OCharMelee);
            WriteTlvInt32(buffer, 31, OCharRange);
            WriteTlvInt32(buffer, 32, OCharDefence);
            WriteTlvInt32(buffer, 33, OCritLevel);
            WriteTlvInt32(buffer, 34, OCritDmg);
            WriteTlvInt32(buffer, 35, OAntiCritDmg);
            WriteTlvInt32(buffer, 39, OWaterAttack);
            WriteTlvInt32(buffer, 40, OFireAttack);
            WriteTlvInt32(buffer, 41, OLightningAttack);
            WriteTlvInt32(buffer, 42, ODragonAttack);
            WriteTlvInt32(buffer, 43, OIceAttack);
            WriteTlvInt32(buffer, 44, ONonAttack);
            WriteTlvInt32(buffer, 45, OPoisonAttack);
            WriteTlvInt32(buffer, 46, OSleepyAttack);
            WriteTlvInt32(buffer, 47, OParalysisAttack);
            WriteTlvInt32(buffer, 48, OWaterRes);
            WriteTlvInt32(buffer, 49, OFireRes);
            WriteTlvInt32(buffer, 50, OLightningRes);
            WriteTlvInt32(buffer, 51, ODragonRes);
            WriteTlvInt32(buffer, 52, OIceRes);
            WriteTlvInt32(buffer, 68, OParaThrsh);
            WriteTlvInt32(buffer, 88, OCharEnergy);
            WriteTlvInt32(buffer, 89, OCharMaxEnergy);
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
            WriteTlvInt32(buffer, 106, OCharGuild);
            WriteTlvInt32(buffer, 107, OCharWarteam);
            WriteTlvInt32(buffer, 108, OMaleFace);
            WriteTlvInt32(buffer, 109, OMaleHair);
            WriteTlvInt32(buffer, 134, OPVEDef);
            WriteTlvInt32(buffer, 135, OPVEDefAngle);
            WriteTlvInt32(buffer, 136, OPVPDef);
            WriteTlvInt32(buffer, 137, OPVPDefAngle);
            WriteTlvInt32(buffer, 173, OUnderClothes);
            WriteTlvInt32(buffer, 174, ONewbie);
            WriteTlvInt32(buffer, 175, OStateFlag);
            WriteTlvInt32(buffer, 177, OPetCarryNum);
            WriteTlvInt32(buffer, 178, OPetHomeNum);
            WriteTlvInt32(buffer, 179, OPetOwendNumMax);
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
            WriteTlvInt32(buffer, 210, OAdditionalHate);
            WriteTlvInt32(buffer, 211, OPlayerAtk);
            WriteTlvInt32(buffer, 212, OFarmFriendGatherCount);
            WriteTlvInt32(buffer, 214, OPlayerCrit);
            WriteTlvInt32(buffer, 220, OFaceTattooColor);
            WriteTlvInt32(buffer, 221, OEyeColor);
            WriteTlvInt32(buffer, 222, OAttrAtkFlag);
            WriteTlvInt32(buffer, 223, OCombatNPCID);
            WriteTlvInt32(buffer, 224, OBattleState);
            WriteTlvInt32(buffer, 226, OHammerModeTime);
            WriteTlvInt16(buffer, 227, OHideFashion);
            WriteTlvInt16(buffer, 228, OHideSuite);
            WriteTlvInt16(buffer, 229, OHideHelm);
            WriteTlvInt32(buffer, 232, OJinLiValue);
            WriteTlvInt32(buffer, 233, OJinLiStep1MaxValue);
            WriteTlvInt32(buffer, 234, OJinLiStep2MaxValue);
            WriteTlvInt32(buffer, 235, OJinLiStep1ReduceValue);
            WriteTlvInt32(buffer, 236, OJinLiStep2ReduceValue);
            WriteTlvInt32(buffer, 237, OEquipTitleID);
            WriteTlvInt32(buffer, 238, OTitleExp);
            WriteTlvInt32(buffer, 239, OTitleLevel);
            WriteTlvInt32(buffer, 240, OEquipTitleBuff);
            WriteTlvInt32(buffer, 241, OSystemUnlockData);
            WriteTlvInt32(buffer, 242, OGuildContribution);
            WriteTlvInt32(buffer, 243, OExtDailyExp);
            WriteTlvInt32(buffer, 244, OGuildId);
            WriteTlvInt16(buffer, 245, OTeamPasswordFlag);
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
            WriteTlvInt32(buffer, 293, OVIPLevel);
            WriteTlvInt32(buffer, 294, OVIPExp);
            WriteTlvInt16(buffer, 300, OVIPBaseCanUse);
            WriteTlvInt16(buffer, 307, OGameVIP);
            WriteTlvInt16(buffer, 308, OQQVIP);
            WriteTlvInt16(buffer, 309, OYearQQVIP);
            WriteTlvInt16(buffer, 310, OSuperQQVIP);
            WriteTlvInt16(buffer, 311, ONetbarLevel);
            WriteTlvInt32(buffer, 322, OCharHRLevel);
            WriteTlvInt32(buffer, 323, OCharHRPoint);
            WriteTlvInt16(buffer, 328, OXYVIP);
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
            WriteTlvInt16(buffer, 352, OTGPVIP);
            WriteTlvInt32(buffer, 359, OFluteTune);
            WriteTlvInt32(buffer, 360, ODefenseReduceHPModifyRate);
            WriteTlvInt32(buffer, 361, ODefenseReduceStaModifyRate);
            WriteTlvInt32(buffer, 370, OWildHuntCamp);
            WriteTlvInt32(buffer, 377, OTotalHRPoint);
            WriteTlvInt32(buffer, 378, OLikeHunterOfficer);
            WriteTlvInt32(buffer, 383, OLevelShowType);
            WriteTlvInt32(buffer, 385, OMonolopyRoundCount);
            WriteTlvInt32(buffer, 386, OMonolopyActivity);
            WriteTlvInt32(buffer, 387, OMonolopyCurGrid);
            WriteTlvInt32(buffer, 402, OSoulStoneLevel);
            WriteTlvInt32(buffer, 403, OWeeklyRefreshTime);
            WriteTlvInt32(buffer, 405, OSoulStoneAtkLevel);
            WriteTlvInt32(buffer, 406, ODynamiteAttack);
            WriteTlvInt32(buffer, 407, ODynamiteThrsh);
            WriteTlvInt32(buffer, 408, OGuildBanChatEndTime);
        }
    }
}
