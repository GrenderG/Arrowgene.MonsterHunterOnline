using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for level/hub/entrust system data.
    /// C++ Reader: crygame.dll+sub_1014FF80 (UnkTlv0079)
    /// C++ Printer: crygame.dll+sub_10150C90
    /// </summary>
    public class TlvLevelHubSystemData : Structure, ITlvStructure
    {
        public const int MaxLevelStat = 70;
        public const int MaxLevelWarning = 8192;
        public const int MaxHubPages = 64;
        public const int MaxUnlockLevels = 5000;

        /// <summary>Field ID: 2</summary>
        public int EntrustMoneyLastTm { get; set; }

        /// <summary>Field ID: 19</summary>
        public int ThousandLayerDataCount { get; set; }

        /// <summary>Level stat count (derived). Field ID: 20 (count part)</summary>
        public int LevelStatLen => LevelStatDataPack?.Count ?? 0;

        /// <summary>Level stat data entries. Field ID: 20</summary>
        public List<TlvLevelTimeLayer> LevelStatDataPack { get; set; }

        /// <summary>Field ID: 21</summary>
        public int EntrustAllStat { get; set; }

        /// <summary>Level warning byte data count (derived). Field ID: 22 (count part)</summary>
        public int LevelWarningByteCount => LevelWarningBytes?.Length ?? 0;

        /// <summary>Level warning byte data. Field ID: 22</summary>
        public byte[] LevelWarningBytes { get; set; }

        /// <summary>Entrust all stat data. Field ID: 23</summary>
        public TlvEntrustGroupStatList EntrustAllStatData { get; set; } = new();

        /// <summary>Level warning data. Field ID: 24</summary>
        public TlvLevelWarningRefresh LevelWarningData { get; set; } = new();

        /// <summary>Monster size history info. Field ID: 25</summary>
        public TlvMonsterSizeData MonsterSizeHistoryInfo { get; set; } = new();

        /// <summary>Unlock hub page count. Field ID: 26</summary>
        public short UnlockHubPageCount { get; set; }

        /// <summary>Unlock hub page data. Field ID: 27</summary>
        public List<TlvHubPage> UnlockHubPageData { get; set; }

        /// <summary>Level unlock count (derived). Field ID: 28</summary>
        public int LevelUnLockCount => UnLockLevelData?.Length ?? 0;

        /// <summary>Unlock level data. Field ID: 29</summary>
        public int[] UnLockLevelData { get; set; }

        /// <summary>Level integrate data. Field ID: 30</summary>
        public TlvLevelScoreData LevelIntegrateData { get; set; } = new();

        /// <summary>Hub reward count (derived). Field ID: 31</summary>
        public byte HubRewardCount => (byte)(HubRewardData?.Count ?? 0);

        /// <summary>Hub reward data. Field ID: 32</summary>
        public List<TlvHubIdPageIndex> HubRewardData { get; set; }

        /// <summary>Unlock level group count (derived). Field ID: 33</summary>
        public byte UnlockLevelGroupCount => (byte)(UnlockLevelGroupData?.Length ?? 0);

        /// <summary>Unlock level group data. Field ID: 34</summary>
        public int[] UnlockLevelGroupData { get; set; }

        /// <summary>Hub page star unlock count (derived). Field ID: 35</summary>
        public byte HubPageStarUnLockCount => (byte)(HubPageStarUnLockData?.Count ?? 0);

        /// <summary>Hub page star unlock data. Field ID: 36</summary>
        public List<TlvHubPageStar> HubPageStarUnLockData { get; set; }

        /// <summary>Super hunter data. Field ID: 37</summary>
        public TlvChallengeRewardStats SuperHunterData { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((LevelStatDataPack?.Count ?? 0) > MaxLevelStat) throw new InvalidDataException($"[TlvLevelHubSystemData] LevelStatDataPack exceeds {MaxLevelStat}.");
            if ((LevelWarningBytes?.Length ?? 0) > MaxLevelWarning) throw new InvalidDataException($"[TlvLevelHubSystemData] LevelWarningBytes exceeds {MaxLevelWarning}.");
            if ((UnlockHubPageData?.Count ?? 0) > MaxHubPages) throw new InvalidDataException($"[TlvLevelHubSystemData] UnlockHubPageData exceeds {MaxHubPages}.");
            if ((UnLockLevelData?.Length ?? 0) > MaxUnlockLevels) throw new InvalidDataException($"[TlvLevelHubSystemData] UnLockLevelData exceeds {MaxUnlockLevels}.");

            WriteTlvInt32(buffer, 2, EntrustMoneyLastTm);
            WriteTlvInt32(buffer, 19, ThousandLayerDataCount);
            WriteTlvInt32(buffer, 20, LevelStatLen);
            WriteTlvSubStructureList(buffer, 20, LevelStatDataPack.Count, LevelStatDataPack);
            WriteTlvInt32(buffer, 21, EntrustAllStat);
            WriteTlvInt32(buffer, 22, LevelWarningByteCount);
            WriteTlvByteArr(buffer, 22, LevelWarningBytes);
            WriteTlvSubStructure(buffer, 23, EntrustAllStatData);
            WriteTlvSubStructure(buffer, 24, LevelWarningData);
            WriteTlvSubStructure(buffer, 25, MonsterSizeHistoryInfo);
            WriteTlvInt16(buffer, 26, UnlockHubPageCount);
            WriteTlvSubStructureList(buffer, 27, UnlockHubPageData.Count, UnlockHubPageData);
            WriteTlvInt32(buffer, 28, LevelUnLockCount);
            WriteTlvInt32Arr(buffer, 29, UnLockLevelData);
            WriteTlvSubStructure(buffer, 30, LevelIntegrateData);
            WriteTlvByte(buffer, 31, HubRewardCount);
            WriteTlvSubStructureList(buffer, 32, HubRewardData.Count, HubRewardData);
            WriteTlvByte(buffer, 33, UnlockLevelGroupCount);
            WriteTlvInt32Arr(buffer, 34, UnlockLevelGroupData);
            WriteTlvByte(buffer, 35, HubPageStarUnLockCount);
            WriteTlvSubStructureList(buffer, 36, HubPageStarUnLockData.Count, HubPageStarUnLockData);
            WriteTlvSubStructure(buffer, 37, SuperHunterData);
        }
    }
}
