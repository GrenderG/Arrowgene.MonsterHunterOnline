using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for character battle/scene attributes.
    /// C++ Reader: crygame.dll+sub_101DACC0 (UnkTlv0194)
    /// C++ Printer: crygame.dll+sub_101DE620
    /// </summary>
    public class TlvCharBattleAttributes : Structure, ITlvStructure
    {
        /// <summary>Field ID: 2</summary>
        public int OCharLevel { get; set; }

        /// <summary>Field ID: 4</summary>
        public int OCharSex { get; set; }

        /// <summary>Field ID: 6</summary>
        public int OCharExp { get; set; }

        /// <summary>Field ID: 7</summary>
        public int OStarLevel { get; set; }

        /// <summary>Field ID: 16</summary>
        public int OCharHP { get; set; }

        /// <summary>Field ID: 17</summary>
        public int OCharMaxHP { get; set; }

        /// <summary>Field ID: 19</summary>
        public int OCharMaxReju { get; set; }

        /// <summary>Field ID: 20</summary>
        public short ODeath { get; set; }

        /// <summary>Field ID: 36</summary>
        public int OSharpness { get; set; }

        /// <summary>Field ID: 75</summary>
        public int OCharAnimSpeed { get; set; }

        /// <summary>Field ID: 108</summary>
        public int OMaleFace { get; set; }

        /// <summary>Field ID: 109</summary>
        public int OMaleHair { get; set; }

        /// <summary>Field ID: 116</summary>
        public int OQiRenLevel { get; set; }

        /// <summary>Field ID: 122</summary>
        public int ORejuFlag { get; set; }

        /// <summary>Field ID: 128</summary>
        public int OTeamID { get; set; }

        /// <summary>Field ID: 136</summary>
        public int OPVPDef { get; set; }

        /// <summary>Field ID: 137</summary>
        public int OPVPDefAngle { get; set; }

        /// <summary>Field ID: 150</summary>
        public int ORegion { get; set; }

        /// <summary>Field ID: 173</summary>
        public int OUnderClothes { get; set; }

        /// <summary>Field ID: 176</summary>
        public int OCharRejuPer { get; set; }

        /// <summary>Field ID: 200</summary>
        public int OCharRemainsExp { get; set; }

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

        /// <summary>Field ID: 220</summary>
        public int OFaceTattooColor { get; set; }

        /// <summary>Field ID: 221</summary>
        public int OEyeColor { get; set; }

        /// <summary>Field ID: 223</summary>
        public int OCombatNPCID { get; set; }

        /// <summary>Field ID: 227</summary>
        public short OHideFashion { get; set; }

        /// <summary>Field ID: 228</summary>
        public short OHideSuite { get; set; }

        /// <summary>Field ID: 229</summary>
        public short OHideHelm { get; set; }

        /// <summary>Field ID: 232</summary>
        public int OJinLiValue { get; set; }

        /// <summary>Field ID: 237</summary>
        public int OEquipTitleID { get; set; }

        /// <summary>Field ID: 239</summary>
        public int OTitleLevel { get; set; }

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

        /// <summary>Field ID: 363</summary>
        public int ODeadTime { get; set; }

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
            WriteTlvInt32(buffer, 16, OCharHP);
            WriteTlvInt32(buffer, 17, OCharMaxHP);
            WriteTlvInt32(buffer, 19, OCharMaxReju);
            WriteTlvInt16(buffer, 20, ODeath);
            WriteTlvInt32(buffer, 36, OSharpness);
            WriteTlvInt32(buffer, 75, OCharAnimSpeed);
            WriteTlvInt32(buffer, 108, OMaleFace);
            WriteTlvInt32(buffer, 109, OMaleHair);
            WriteTlvInt32(buffer, 116, OQiRenLevel);
            WriteTlvInt32(buffer, 122, ORejuFlag);
            WriteTlvInt32(buffer, 128, OTeamID);
            WriteTlvInt32(buffer, 136, OPVPDef);
            WriteTlvInt32(buffer, 137, OPVPDefAngle);
            WriteTlvInt32(buffer, 150, ORegion);
            WriteTlvInt32(buffer, 173, OUnderClothes);
            WriteTlvInt32(buffer, 176, OCharRejuPer);
            WriteTlvInt32(buffer, 200, OCharRemainsExp);
            WriteTlvInt32(buffer, 204, OLastResetTime);
            WriteTlvInt32(buffer, 205, OSkinColor);
            WriteTlvInt32(buffer, 206, OHairColor);
            WriteTlvInt32(buffer, 207, OInnerColor);
            WriteTlvInt32(buffer, 208, OFaceTattooIndex);
            WriteTlvInt32(buffer, 209, OEyeBall);
            WriteTlvInt32(buffer, 220, OFaceTattooColor);
            WriteTlvInt32(buffer, 221, OEyeColor);
            WriteTlvInt32(buffer, 223, OCombatNPCID);
            WriteTlvInt16(buffer, 227, OHideFashion);
            WriteTlvInt16(buffer, 228, OHideSuite);
            WriteTlvInt16(buffer, 229, OHideHelm);
            WriteTlvInt32(buffer, 232, OJinLiValue);
            WriteTlvInt32(buffer, 237, OEquipTitleID);
            WriteTlvInt32(buffer, 239, OTitleLevel);
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
            WriteTlvInt32(buffer, 363, ODeadTime);
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
        }
    }
}
