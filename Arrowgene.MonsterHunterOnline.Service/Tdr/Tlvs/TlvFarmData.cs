using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for farm system data.
    /// C++ Reader: crygame.dll+sub_10205140 (UnkTlv0217)
    /// C++ Printer: crygame.dll+sub_10205AD0
    /// </summary>
    public class TlvFarmData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: FarmID = reader.ReadInt(); break;
                    case 3: OwnerUID = reader.ReadInt(); break;
                    case 4: OwnerDBID = reader.ReadLong(); break;
                    case 5: Remark = reader.ReadInt(); break;
                    case 6: Credit = reader.ReadInt(); break;
                    case 7: Hits = reader.ReadInt(); break;
                    case 8: CurrentHits = reader.ReadInt(); break;
                    case 9: MaxGatherCount = reader.ReadInt(); break;
                    case 10: AutoGatherPetID = reader.ReadInt(); break;
                    case 11: AutoGatherBCPType = reader.ReadInt(); break;
                    case 12: { int len = reader.ReadInt(); if (len > 0 && len <= MaxSACPOpen) SACPOpen = reader.ReadBytes(len); } break;
                    case 13: { int len = reader.ReadInt(); if (len > 0 && len <= MaxSOFOpen) SOFOpen = reader.ReadBytes(len); } break;
                    case 14: SBCPData = ReadTlvList<TlvGatherInfo>(reader); break;
                    case 15: SPFData = ReadTlvList<TlvLevelValue>(reader); break;
                    case 16: SPlowLandData = ReadTlvList<TlvSeedSlot>(reader); break;
                    case 17: LastFarmRefreshTime = reader.ReadInt(); break;
                    case 18: FarmCanBeGatheredCount = reader.ReadInt(); break;
                    case 19: FriendGatherBonus = reader.ReadByte(); break;
                    case 20: SafeDataFlag = reader.ReadShort(); break;
                    case 21: FacilityUseFlag = reader.ReadInt(); break;
                    case 22: FarmOpenFlag = reader.ReadByte(); break;
                    case 23: reader.ReadShort(); break;
                    case 24: PetAvatarInfo = ReadTlvList<TlvPetAvatarData>(reader); break;
                    case 25: reader.ReadShort(); break;
                    case 26: EquipShowInfo = ReadTlvList<TlvEquipData>(reader); break;
                    case 27: Gender = reader.ReadByte(); break;
                    case 28: SkipCutScene = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((SACPOpen?.Length ?? 0) > MaxSACPOpen) throw new InvalidDataException($"[TlvFarmData] SACPOpen exceeds {MaxSACPOpen}.");
            if ((SOFOpen?.Length ?? 0) > MaxSOFOpen) throw new InvalidDataException($"[TlvFarmData] SOFOpen exceeds {MaxSOFOpen}.");

            WriteTlvInt(writer, 2, FarmID);
            WriteTlvInt(writer, 3, OwnerUID);
            WriteTlvLong(writer, 4, OwnerDBID);
            WriteTlvInt(writer, 5, Remark);
            WriteTlvInt(writer, 6, Credit);
            WriteTlvInt(writer, 7, Hits);
            WriteTlvInt(writer, 8, CurrentHits);
            WriteTlvInt(writer, 9, MaxGatherCount);
            WriteTlvInt(writer, 10, AutoGatherPetID);
            WriteTlvInt(writer, 11, AutoGatherBCPType);
            WriteTlvByteArray(writer, 12, SACPOpen);
            WriteTlvByteArray(writer, 13, SOFOpen);
            WriteTlvList(writer, 14, SBCPData);
            WriteTlvList(writer, 15, SPFData);
            WriteTlvList(writer, 16, SPlowLandData);
            WriteTlvInt(writer, 17, LastFarmRefreshTime);
            WriteTlvInt(writer, 18, FarmCanBeGatheredCount);
            WriteTlvByte(writer, 19, FriendGatherBonus);
            WriteTlvShort(writer, 20, SafeDataFlag);
            WriteTlvInt(writer, 21, FacilityUseFlag);
            WriteTlvByte(writer, 22, FarmOpenFlag);
            WriteTlvShort(writer, 23, PetAvatarCount);
            WriteTlvList(writer, 24, PetAvatarInfo);
            WriteTlvShort(writer, 25, EquipShowCount);
            WriteTlvList(writer, 26, EquipShowInfo);
            WriteTlvByte(writer, 27, Gender);
            WriteTlvByte(writer, 28, SkipCutScene);
        }
    }
}
