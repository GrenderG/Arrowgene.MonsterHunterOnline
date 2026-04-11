using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for character public view attributes.
    /// C++ Reader: crygame.dll+sub_101E6B40 (UnkTlv0195)
    /// C++ Printer: crygame.dll+sub_101EA070
    /// </summary>
    public class TlvCharPublicAttributes : TlvStructure
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
                    case 8: OStarCollection = reader.ReadInt(); break;
                    case 9: OStarQuest = reader.ReadInt(); break;
                    case 10: OStarCombat = reader.ReadInt(); break;
                    case 11: OStarPet = reader.ReadInt(); break;
                    case 12: OStarProduct = reader.ReadInt(); break;
                    case 13: OStarGuild = reader.ReadInt(); break;
                    case 14: OStarTame = reader.ReadInt(); break;
                    case 15: OStarPvP = reader.ReadInt(); break;
                    case 16: OCharHP = reader.ReadInt(); break;
                    case 17: OCharMaxHP = reader.ReadInt(); break;
                    case 20: ODeath = reader.ReadShort(); break;
                    case 22: OCharMaxSta = reader.ReadInt(); break;
                    case 26: OCharStr = reader.ReadInt(); break;
                    case 27: OCharBst = reader.ReadInt(); break;
                    case 28: OCharLck = reader.ReadInt(); break;
                    case 29: OCharVgr = reader.ReadInt(); break;
                    case 30: OCharMelee = reader.ReadInt(); break;
                    case 31: OCharRange = reader.ReadInt(); break;
                    case 32: OCharDefence = reader.ReadInt(); break;
                    case 33: OCritLevel = reader.ReadInt(); break;
                    case 34: OCritDmg = reader.ReadInt(); break;
                    case 35: OAntiCritDmg = reader.ReadInt(); break;
                    case 39: OWaterAttack = reader.ReadInt(); break;
                    case 40: OFireAttack = reader.ReadInt(); break;
                    case 41: OLightningAttack = reader.ReadInt(); break;
                    case 42: ODragonAttack = reader.ReadInt(); break;
                    case 43: OIceAttack = reader.ReadInt(); break;
                    case 44: ONonAttack = reader.ReadInt(); break;
                    case 45: OPoisonAttack = reader.ReadInt(); break;
                    case 46: OSleepyAttack = reader.ReadInt(); break;
                    case 47: OParalysisAttack = reader.ReadInt(); break;
                    case 48: OWaterRes = reader.ReadInt(); break;
                    case 49: OFireRes = reader.ReadInt(); break;
                    case 50: OLightningRes = reader.ReadInt(); break;
                    case 51: ODragonRes = reader.ReadInt(); break;
                    case 52: OIceRes = reader.ReadInt(); break;
                    case 68: OParaThrsh = reader.ReadInt(); break;
                    case 88: OCharEnergy = reader.ReadInt(); break;
                    case 89: OCharMaxEnergy = reader.ReadInt(); break;
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
                    case 106: OCharGuild = reader.ReadInt(); break;
                    case 107: OCharWarteam = reader.ReadInt(); break;
                    case 108: OMaleFace = reader.ReadInt(); break;
                    case 109: OMaleHair = reader.ReadInt(); break;
                    case 134: OPVEDef = reader.ReadInt(); break;
                    case 135: OPVEDefAngle = reader.ReadInt(); break;
                    case 136: OPVPDef = reader.ReadInt(); break;
                    case 137: OPVPDefAngle = reader.ReadInt(); break;
                    case 173: OUnderClothes = reader.ReadInt(); break;
                    case 174: ONewbie = reader.ReadInt(); break;
                    case 175: OStateFlag = reader.ReadInt(); break;
                    case 177: OPetCarryNum = reader.ReadInt(); break;
                    case 178: OPetHomeNum = reader.ReadInt(); break;
                    case 179: OPetOwendNumMax = reader.ReadInt(); break;
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
                    case 210: OAdditionalHate = reader.ReadInt(); break;
                    case 211: OPlayerAtk = reader.ReadInt(); break;
                    case 212: OFarmFriendGatherCount = reader.ReadInt(); break;
                    case 214: OPlayerCrit = reader.ReadInt(); break;
                    case 220: OFaceTattooColor = reader.ReadInt(); break;
                    case 221: OEyeColor = reader.ReadInt(); break;
                    case 222: OAttrAtkFlag = reader.ReadInt(); break;
                    case 223: OCombatNPCID = reader.ReadInt(); break;
                    case 224: OBattleState = reader.ReadInt(); break;
                    case 226: OHammerModeTime = reader.ReadInt(); break;
                    case 227: OHideFashion = reader.ReadShort(); break;
                    case 228: OHideSuite = reader.ReadShort(); break;
                    case 229: OHideHelm = reader.ReadShort(); break;
                    case 232: OJinLiValue = reader.ReadInt(); break;
                    case 233: OJinLiStep1MaxValue = reader.ReadInt(); break;
                    case 234: OJinLiStep2MaxValue = reader.ReadInt(); break;
                    case 235: OJinLiStep1ReduceValue = reader.ReadInt(); break;
                    case 236: OJinLiStep2ReduceValue = reader.ReadInt(); break;
                    case 237: OEquipTitleID = reader.ReadInt(); break;
                    case 238: OTitleExp = reader.ReadInt(); break;
                    case 239: OTitleLevel = reader.ReadInt(); break;
                    case 240: OEquipTitleBuff = reader.ReadInt(); break;
                    case 241: OSystemUnlockData = reader.ReadInt(); break;
                    case 242: OGuildContribution = reader.ReadInt(); break;
                    case 243: OExtDailyExp = reader.ReadInt(); break;
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
                    case 406: ODynamiteAttack = reader.ReadInt(); break;
                    case 407: ODynamiteThrsh = reader.ReadInt(); break;
                    case 408: OGuildBanChatEndTime = reader.ReadInt(); break;
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
            WriteTlvInt(writer, 8, OStarCollection);
            WriteTlvInt(writer, 9, OStarQuest);
            WriteTlvInt(writer, 10, OStarCombat);
            WriteTlvInt(writer, 11, OStarPet);
            WriteTlvInt(writer, 12, OStarProduct);
            WriteTlvInt(writer, 13, OStarGuild);
            WriteTlvInt(writer, 14, OStarTame);
            WriteTlvInt(writer, 15, OStarPvP);
            WriteTlvInt(writer, 16, OCharHP);
            WriteTlvInt(writer, 17, OCharMaxHP);
            WriteTlvShort(writer, 20, ODeath);
            WriteTlvInt(writer, 22, OCharMaxSta);
            WriteTlvInt(writer, 26, OCharStr);
            WriteTlvInt(writer, 27, OCharBst);
            WriteTlvInt(writer, 28, OCharLck);
            WriteTlvInt(writer, 29, OCharVgr);
            WriteTlvInt(writer, 30, OCharMelee);
            WriteTlvInt(writer, 31, OCharRange);
            WriteTlvInt(writer, 32, OCharDefence);
            WriteTlvInt(writer, 33, OCritLevel);
            WriteTlvInt(writer, 34, OCritDmg);
            WriteTlvInt(writer, 35, OAntiCritDmg);
            WriteTlvInt(writer, 39, OWaterAttack);
            WriteTlvInt(writer, 40, OFireAttack);
            WriteTlvInt(writer, 41, OLightningAttack);
            WriteTlvInt(writer, 42, ODragonAttack);
            WriteTlvInt(writer, 43, OIceAttack);
            WriteTlvInt(writer, 44, ONonAttack);
            WriteTlvInt(writer, 45, OPoisonAttack);
            WriteTlvInt(writer, 46, OSleepyAttack);
            WriteTlvInt(writer, 47, OParalysisAttack);
            WriteTlvInt(writer, 48, OWaterRes);
            WriteTlvInt(writer, 49, OFireRes);
            WriteTlvInt(writer, 50, OLightningRes);
            WriteTlvInt(writer, 51, ODragonRes);
            WriteTlvInt(writer, 52, OIceRes);
            WriteTlvInt(writer, 68, OParaThrsh);
            WriteTlvInt(writer, 88, OCharEnergy);
            WriteTlvInt(writer, 89, OCharMaxEnergy);
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
            WriteTlvInt(writer, 106, OCharGuild);
            WriteTlvInt(writer, 107, OCharWarteam);
            WriteTlvInt(writer, 108, OMaleFace);
            WriteTlvInt(writer, 109, OMaleHair);
            WriteTlvInt(writer, 134, OPVEDef);
            WriteTlvInt(writer, 135, OPVEDefAngle);
            WriteTlvInt(writer, 136, OPVPDef);
            WriteTlvInt(writer, 137, OPVPDefAngle);
            WriteTlvInt(writer, 173, OUnderClothes);
            WriteTlvInt(writer, 174, ONewbie);
            WriteTlvInt(writer, 175, OStateFlag);
            WriteTlvInt(writer, 177, OPetCarryNum);
            WriteTlvInt(writer, 178, OPetHomeNum);
            WriteTlvInt(writer, 179, OPetOwendNumMax);
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
            WriteTlvInt(writer, 210, OAdditionalHate);
            WriteTlvInt(writer, 211, OPlayerAtk);
            WriteTlvInt(writer, 212, OFarmFriendGatherCount);
            WriteTlvInt(writer, 214, OPlayerCrit);
            WriteTlvInt(writer, 220, OFaceTattooColor);
            WriteTlvInt(writer, 221, OEyeColor);
            WriteTlvInt(writer, 222, OAttrAtkFlag);
            WriteTlvInt(writer, 223, OCombatNPCID);
            WriteTlvInt(writer, 224, OBattleState);
            WriteTlvInt(writer, 226, OHammerModeTime);
            WriteTlvShort(writer, 227, OHideFashion);
            WriteTlvShort(writer, 228, OHideSuite);
            WriteTlvShort(writer, 229, OHideHelm);
            WriteTlvInt(writer, 232, OJinLiValue);
            WriteTlvInt(writer, 233, OJinLiStep1MaxValue);
            WriteTlvInt(writer, 234, OJinLiStep2MaxValue);
            WriteTlvInt(writer, 235, OJinLiStep1ReduceValue);
            WriteTlvInt(writer, 236, OJinLiStep2ReduceValue);
            WriteTlvInt(writer, 237, OEquipTitleID);
            WriteTlvInt(writer, 238, OTitleExp);
            WriteTlvInt(writer, 239, OTitleLevel);
            WriteTlvInt(writer, 240, OEquipTitleBuff);
            WriteTlvInt(writer, 241, OSystemUnlockData);
            WriteTlvInt(writer, 242, OGuildContribution);
            WriteTlvInt(writer, 243, OExtDailyExp);
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
            WriteTlvInt(writer, 406, ODynamiteAttack);
            WriteTlvInt(writer, 407, ODynamiteThrsh);
            WriteTlvInt(writer, 408, OGuildBanChatEndTime);
        }
    }
}
