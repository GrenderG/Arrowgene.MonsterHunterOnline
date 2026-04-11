using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for character battle/scene attributes.
    /// C++ Reader: crygame.dll+sub_101DACC0 (UnkTlv0194)
    /// C++ Printer: crygame.dll+sub_101DE620
    /// </summary>
    public class TlvCharBattleAttributes : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: OCharLevel = reader.ReadInt(); break;
                    case 4: OCharSex = reader.ReadInt(); break;
                    case 6: OCharExp = reader.ReadInt(); break;
                    case 7: OStarLevel = reader.ReadInt(); break;
                    case 16: OCharHP = reader.ReadInt(); break;
                    case 17: OCharMaxHP = reader.ReadInt(); break;
                    case 19: OCharMaxReju = reader.ReadInt(); break;
                    case 20: ODeath = reader.ReadShort(); break;
                    case 36: OSharpness = reader.ReadInt(); break;
                    case 75: OCharAnimSpeed = reader.ReadInt(); break;
                    case 108: OMaleFace = reader.ReadInt(); break;
                    case 109: OMaleHair = reader.ReadInt(); break;
                    case 116: OQiRenLevel = reader.ReadInt(); break;
                    case 122: ORejuFlag = reader.ReadInt(); break;
                    case 128: OTeamID = reader.ReadInt(); break;
                    case 136: OPVPDef = reader.ReadInt(); break;
                    case 137: OPVPDefAngle = reader.ReadInt(); break;
                    case 150: ORegion = reader.ReadInt(); break;
                    case 173: OUnderClothes = reader.ReadInt(); break;
                    case 176: OCharRejuPer = reader.ReadInt(); break;
                    case 200: OCharRemainsExp = reader.ReadInt(); break;
                    case 204: OLastResetTime = reader.ReadInt(); break;
                    case 205: OSkinColor = reader.ReadInt(); break;
                    case 206: OHairColor = reader.ReadInt(); break;
                    case 207: OInnerColor = reader.ReadInt(); break;
                    case 208: OFaceTattooIndex = reader.ReadInt(); break;
                    case 209: OEyeBall = reader.ReadInt(); break;
                    case 220: OFaceTattooColor = reader.ReadInt(); break;
                    case 221: OEyeColor = reader.ReadInt(); break;
                    case 223: OCombatNPCID = reader.ReadInt(); break;
                    case 227: OHideFashion = reader.ReadShort(); break;
                    case 228: OHideSuite = reader.ReadShort(); break;
                    case 229: OHideHelm = reader.ReadShort(); break;
                    case 232: OJinLiValue = reader.ReadInt(); break;
                    case 237: OEquipTitleID = reader.ReadInt(); break;
                    case 239: OTitleLevel = reader.ReadInt(); break;
                    case 244: OGuildId = reader.ReadInt(); break;
                    case 245: OTeamPasswordFlag = reader.ReadShort(); break;
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
                    case 293: OVIPLevel = reader.ReadInt(); break;
                    case 294: OVIPExp = reader.ReadInt(); break;
                    case 300: OVIPBaseCanUse = reader.ReadShort(); break;
                    case 307: OGameVIP = reader.ReadShort(); break;
                    case 308: OQQVIP = reader.ReadShort(); break;
                    case 309: OYearQQVIP = reader.ReadShort(); break;
                    case 310: OSuperQQVIP = reader.ReadShort(); break;
                    case 311: ONetbarLevel = reader.ReadShort(); break;
                    case 322: OCharHRLevel = reader.ReadInt(); break;
                    case 323: OCharHRPoint = reader.ReadInt(); break;
                    case 328: OXYVIP = reader.ReadShort(); break;
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
                    case 352: OTGPVIP = reader.ReadShort(); break;
                    case 359: OFluteTune = reader.ReadInt(); break;
                    case 360: ODefenseReduceHPModifyRate = reader.ReadInt(); break;
                    case 361: ODefenseReduceStaModifyRate = reader.ReadInt(); break;
                    case 363: ODeadTime = reader.ReadInt(); break;
                    case 370: OWildHuntCamp = reader.ReadInt(); break;
                    case 377: OTotalHRPoint = reader.ReadInt(); break;
                    case 378: OLikeHunterOfficer = reader.ReadInt(); break;
                    case 383: OLevelShowType = reader.ReadInt(); break;
                    case 385: OMonolopyRoundCount = reader.ReadInt(); break;
                    case 386: OMonolopyActivity = reader.ReadInt(); break;
                    case 387: OMonolopyCurGrid = reader.ReadInt(); break;
                    case 402: OSoulStoneLevel = reader.ReadInt(); break;
                    case 403: OWeeklyRefreshTime = reader.ReadInt(); break;
                    case 405: OSoulStoneAtkLevel = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 2, OCharLevel);
            WriteTlvInt(writer, 4, OCharSex);
            WriteTlvInt(writer, 6, OCharExp);
            WriteTlvInt(writer, 7, OStarLevel);
            WriteTlvInt(writer, 16, OCharHP);
            WriteTlvInt(writer, 17, OCharMaxHP);
            WriteTlvInt(writer, 19, OCharMaxReju);
            WriteTlvShort(writer, 20, ODeath);
            WriteTlvInt(writer, 36, OSharpness);
            WriteTlvInt(writer, 75, OCharAnimSpeed);
            WriteTlvInt(writer, 108, OMaleFace);
            WriteTlvInt(writer, 109, OMaleHair);
            WriteTlvInt(writer, 116, OQiRenLevel);
            WriteTlvInt(writer, 122, ORejuFlag);
            WriteTlvInt(writer, 128, OTeamID);
            WriteTlvInt(writer, 136, OPVPDef);
            WriteTlvInt(writer, 137, OPVPDefAngle);
            WriteTlvInt(writer, 150, ORegion);
            WriteTlvInt(writer, 173, OUnderClothes);
            WriteTlvInt(writer, 176, OCharRejuPer);
            WriteTlvInt(writer, 200, OCharRemainsExp);
            WriteTlvInt(writer, 204, OLastResetTime);
            WriteTlvInt(writer, 205, OSkinColor);
            WriteTlvInt(writer, 206, OHairColor);
            WriteTlvInt(writer, 207, OInnerColor);
            WriteTlvInt(writer, 208, OFaceTattooIndex);
            WriteTlvInt(writer, 209, OEyeBall);
            WriteTlvInt(writer, 220, OFaceTattooColor);
            WriteTlvInt(writer, 221, OEyeColor);
            WriteTlvInt(writer, 223, OCombatNPCID);
            WriteTlvShort(writer, 227, OHideFashion);
            WriteTlvShort(writer, 228, OHideSuite);
            WriteTlvShort(writer, 229, OHideHelm);
            WriteTlvInt(writer, 232, OJinLiValue);
            WriteTlvInt(writer, 237, OEquipTitleID);
            WriteTlvInt(writer, 239, OTitleLevel);
            WriteTlvInt(writer, 244, OGuildId);
            WriteTlvShort(writer, 245, OTeamPasswordFlag);
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
            WriteTlvInt(writer, 293, OVIPLevel);
            WriteTlvInt(writer, 294, OVIPExp);
            WriteTlvShort(writer, 300, OVIPBaseCanUse);
            WriteTlvShort(writer, 307, OGameVIP);
            WriteTlvShort(writer, 308, OQQVIP);
            WriteTlvShort(writer, 309, OYearQQVIP);
            WriteTlvShort(writer, 310, OSuperQQVIP);
            WriteTlvShort(writer, 311, ONetbarLevel);
            WriteTlvInt(writer, 322, OCharHRLevel);
            WriteTlvInt(writer, 323, OCharHRPoint);
            WriteTlvShort(writer, 328, OXYVIP);
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
            WriteTlvShort(writer, 352, OTGPVIP);
            WriteTlvInt(writer, 359, OFluteTune);
            WriteTlvInt(writer, 360, ODefenseReduceHPModifyRate);
            WriteTlvInt(writer, 361, ODefenseReduceStaModifyRate);
            WriteTlvInt(writer, 363, ODeadTime);
            WriteTlvInt(writer, 370, OWildHuntCamp);
            WriteTlvInt(writer, 377, OTotalHRPoint);
            WriteTlvInt(writer, 378, OLikeHunterOfficer);
            WriteTlvInt(writer, 383, OLevelShowType);
            WriteTlvInt(writer, 385, OMonolopyRoundCount);
            WriteTlvInt(writer, 386, OMonolopyActivity);
            WriteTlvInt(writer, 387, OMonolopyCurGrid);
            WriteTlvInt(writer, 402, OSoulStoneLevel);
            WriteTlvInt(writer, 403, OWeeklyRefreshTime);
            WriteTlvInt(writer, 405, OSoulStoneAtkLevel);
        }
    }
}
