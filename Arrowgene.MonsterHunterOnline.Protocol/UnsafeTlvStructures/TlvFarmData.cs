using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for farm system data.
    /// C++ Reader: crygame.dll+sub_10205140 (UnkTlv0217)
    /// C++ Printer: crygame.dll+sub_10205AD0
    /// </summary>
    public class TlvFarmData : Structure, ITlvStructure
    {
        public const int MaxSACPOpen = 4;
        public const int MaxSOFOpen = 2;
        public const int MaxBCPData = 6;
        public const int MaxPFData = 6;
        public const int MaxPlowLand = 3;
        public const int MaxPetAvatar = 30;
        public const int MaxEquipShow = 12;

        /// <summary>Field ID: 2</summary>
        public int FarmID { get; set; }
        /// <summary>Field ID: 3</summary>
        public int OwnerUID { get; set; }
        /// <summary>Field ID: 4</summary>
        public long OwnerDBID { get; set; }
        /// <summary>Field ID: 5</summary>
        public int Remark { get; set; }
        /// <summary>Field ID: 6</summary>
        public int Credit { get; set; }
        /// <summary>Field ID: 7</summary>
        public int Hits { get; set; }
        /// <summary>Field ID: 8</summary>
        public int CurrentHits { get; set; }
        /// <summary>Field ID: 9</summary>
        public int MaxGatherCount { get; set; }
        /// <summary>Field ID: 10</summary>
        public int AutoGatherPetID { get; set; }
        /// <summary>Field ID: 11</summary>
        public int AutoGatherBCPType { get; set; }

        /// <summary>sACPOpen inline bytes (max 4). Field ID: 12</summary>
        public byte[] SACPOpen { get; set; }

        /// <summary>sOFOpen inline bytes (max 2). Field ID: 13</summary>
        public byte[] SOFOpen { get; set; }

        /// <summary>sBCPData entries. Field ID: 14</summary>
        public List<TlvGatherInfo> SBCPData { get; set; }

        /// <summary>sPFData entries. Field ID: 15</summary>
        public List<TlvLevelValue> SPFData { get; set; }

        /// <summary>sPlowLandData entries. Field ID: 16</summary>
        public List<TlvSeedSlot> SPlowLandData { get; set; }

        /// <summary>Field ID: 17</summary>
        public int LastFarmRefreshTime { get; set; }
        /// <summary>Field ID: 18</summary>
        public int FarmCanBeGatheredCount { get; set; }

        /// <summary>FriendGatherBonus (single byte inline). Field ID: 19</summary>
        public byte FriendGatherBonus { get; set; }

        /// <summary>Field ID: 20</summary>
        public short SafeDataFlag { get; set; }
        /// <summary>Field ID: 21</summary>
        public int FacilityUseFlag { get; set; }
        /// <summary>Field ID: 22</summary>
        public byte FarmOpenFlag { get; set; }

        /// <summary>Pet avatar count (derived). Field ID: 23</summary>
        public short PetAvatarCount => (short)(PetAvatarInfo?.Count ?? 0);

        /// <summary>Pet avatar info entries. Field ID: 24</summary>
        public List<TlvPetAvatarData> PetAvatarInfo { get; set; }

        /// <summary>Equip show count (derived). Field ID: 25</summary>
        public short EquipShowCount => (short)(EquipShowInfo?.Count ?? 0);

        /// <summary>Equip show info entries. Field ID: 26</summary>
        public List<TlvEquipData> EquipShowInfo { get; set; }

        /// <summary>Field ID: 27</summary>
        public byte Gender { get; set; }
        /// <summary>Field ID: 28</summary>
        public byte SkipCutScene { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((SACPOpen?.Length ?? 0) > MaxSACPOpen) throw new InvalidDataException($"[TlvFarmData] SACPOpen exceeds {MaxSACPOpen}.");
            if ((SOFOpen?.Length ?? 0) > MaxSOFOpen) throw new InvalidDataException($"[TlvFarmData] SOFOpen exceeds {MaxSOFOpen}.");

            WriteTlvInt32(buffer, 2, FarmID);
            WriteTlvInt32(buffer, 3, OwnerUID);
            WriteTlvInt64(buffer, 4, OwnerDBID);
            WriteTlvInt32(buffer, 5, Remark);
            WriteTlvInt32(buffer, 6, Credit);
            WriteTlvInt32(buffer, 7, Hits);
            WriteTlvInt32(buffer, 8, CurrentHits);
            WriteTlvInt32(buffer, 9, MaxGatherCount);
            WriteTlvInt32(buffer, 10, AutoGatherPetID);
            WriteTlvInt32(buffer, 11, AutoGatherBCPType);
            WriteTlvByteArr(buffer, 12, SACPOpen);
            WriteTlvByteArr(buffer, 13, SOFOpen);
            WriteTlvSubStructureList(buffer, 14, SBCPData.Count, SBCPData);
            WriteTlvSubStructureList(buffer, 15, SPFData.Count, SPFData);
            WriteTlvSubStructureList(buffer, 16, SPlowLandData.Count, SPlowLandData);
            WriteTlvInt32(buffer, 17, LastFarmRefreshTime);
            WriteTlvInt32(buffer, 18, FarmCanBeGatheredCount);
            WriteTlvByte(buffer, 19, FriendGatherBonus);
            WriteTlvInt16(buffer, 20, SafeDataFlag);
            WriteTlvInt32(buffer, 21, FacilityUseFlag);
            WriteTlvByte(buffer, 22, FarmOpenFlag);
            WriteTlvInt16(buffer, 23, PetAvatarCount);
            WriteTlvSubStructureList(buffer, 24, PetAvatarInfo.Count, PetAvatarInfo);
            WriteTlvInt16(buffer, 25, EquipShowCount);
            WriteTlvSubStructureList(buffer, 26, EquipShowInfo.Count, EquipShowInfo);
            WriteTlvByte(buffer, 27, Gender);
            WriteTlvByte(buffer, 28, SkipCutScene);
        }
    }
}
