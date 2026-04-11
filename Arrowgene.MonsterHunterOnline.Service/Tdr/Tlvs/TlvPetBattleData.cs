using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for pet battle/farm data entry.
    /// C++ Reader: crygame.dll+sub_101A54E0 (UnkTlv0185)
    /// C++ Printer: crygame.dll+sub_101A5DD0
    /// </summary>
    public class TlvPetBattleData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        public const int MaxTrain = 70;
        public const int MaxRngAttrs = 10;

        /// <summary>Field ID: 2</summary>
        public byte Idx { get; set; }

        /// <summary>Field ID: 3</summary>
        public int UId { get; set; }

        /// <summary>Field ID: 4</summary>
        public int Id { get; set; }

        /// <summary>Field ID: 5</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Field ID: 6</summary>
        public string Desc { get; set; } = string.Empty;

        /// <summary>Field ID: 7</summary>
        public byte Sex { get; set; }

        /// <summary>Field ID: 8</summary>
        public byte State { get; set; }

        /// <summary>Field ID: 9</summary>
        public int Level { get; set; }

        /// <summary>Field ID: 10</summary>
        public int Exp { get; set; }

        /// <summary>Field ID: 11</summary>
        public short Loyal { get; set; }

        /// <summary>Field ID: 12</summary>
        public short Potential { get; set; }

        /// <summary>Field ID: 14</summary>
        public short Spirit { get; set; }

        /// <summary>Field ID: 15</summary>
        public short Vigour { get; set; }

        /// <summary>Field ID: 16</summary>
        public int SupportSkill { get; set; }

        /// <summary>Field ID: 17</summary>
        public int GiftSkill { get; set; }

        /// <summary>Field ID: 18</summary>
        public byte Rename { get; set; }

        /// <summary>Group num (derived). Field ID: 21</summary>
        public short GroupNum => (short)(Train?.Count ?? 0);

        /// <summary>Train entries. Field ID: 22</summary>
        public List<TlvIdExp> Train { get; set; }

        /// <summary>Rng attrs count (derived). Field ID: 23</summary>
        public short PotentialNum => (short)(RngAttrs?.Count ?? 0);

        /// <summary>Rng attribute entries. Field ID: 24</summary>
        public List<TlvIdValuePair> RngAttrs { get; set; }

        /// <summary>Talk style data. Field ID: 28</summary>
        public TlvTrainTimeSlot TalkStyle { get; set; } = new();

        /// <summary>Equip skills data. Field ID: 29</summary>
        public TlvPetInfo EquipSkills { get; set; } = new();

        /// <summary>Field ID: 30</summary>
        public byte Sp { get; set; }

        /// <summary>Wait skills data. Field ID: 31</summary>
        public TlvSkillSlotData WaitSkills { get; set; } = new();

        /// <summary>Field ID: 32</summary>
        public int GrowHigherCat { get; set; }

        /// <summary>Potential value data. Field ID: 33</summary>
        public TlvNewFlagSkills PotentialValue { get; set; } = new();

        /// <summary>Group exp data. Field ID: 34</summary>
        public TlvDailyHighScore GroupExp { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: Idx = reader.ReadByte(); break;
                    case 3: UId = reader.ReadInt(); break;
                    case 4: Id = reader.ReadInt(); break;
                    case 5: { int len = reader.ReadInt(); Name = len > 0 ? Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 6: { int len = reader.ReadInt(); Desc = len > 0 ? Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 7: Sex = reader.ReadByte(); break;
                    case 8: State = reader.ReadByte(); break;
                    case 9: Level = reader.ReadInt(); break;
                    case 10: Exp = reader.ReadInt(); break;
                    case 11: Loyal = reader.ReadShort(); break;
                    case 12: Potential = reader.ReadShort(); break;
                    case 14: Spirit = reader.ReadShort(); break;
                    case 15: Vigour = reader.ReadShort(); break;
                    case 16: SupportSkill = reader.ReadInt(); break;
                    case 17: GiftSkill = reader.ReadInt(); break;
                    case 18: Rename = reader.ReadByte(); break;
                    case 21: reader.ReadShort(); break;
                    case 22: Train = ReadTlvList<TlvIdExp>(reader); break;
                    case 23: reader.ReadShort(); break;
                    case 24: RngAttrs = ReadTlvList<TlvIdValuePair>(reader); break;
                    case 28: TalkStyle = ReadTlvObject<TlvTrainTimeSlot>(reader); break;
                    case 29: EquipSkills = ReadTlvObject<TlvPetInfo>(reader); break;
                    case 30: Sp = reader.ReadByte(); break;
                    case 31: WaitSkills = ReadTlvObject<TlvSkillSlotData>(reader); break;
                    case 32: GrowHigherCat = reader.ReadInt(); break;
                    case 33: PotentialValue = ReadTlvObject<TlvNewFlagSkills>(reader); break;
                    case 34: GroupExp = ReadTlvObject<TlvDailyHighScore>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((Train?.Count ?? 0) > MaxTrain)
                throw new InvalidDataException($"[TlvPetBattleData] Train exceeds {MaxTrain}.");
            if ((RngAttrs?.Count ?? 0) > MaxRngAttrs)
                throw new InvalidDataException($"[TlvPetBattleData] RngAttrs exceeds {MaxRngAttrs}.");

            WriteTlvByte(writer, 2, Idx);
            WriteTlvInt(writer, 3, UId);
            WriteTlvInt(writer, 4, Id);
            WriteTlvString(writer, 5, Name);
            WriteTlvString(writer, 6, Desc);
            WriteTlvByte(writer, 7, Sex);
            WriteTlvByte(writer, 8, State);
            WriteTlvInt(writer, 9, Level);
            WriteTlvInt(writer, 10, Exp);
            WriteTlvShort(writer, 11, Loyal);
            WriteTlvShort(writer, 12, Potential);
            WriteTlvShort(writer, 14, Spirit);
            WriteTlvShort(writer, 15, Vigour);
            WriteTlvInt(writer, 16, SupportSkill);
            WriteTlvInt(writer, 17, GiftSkill);
            WriteTlvByte(writer, 18, Rename);
            WriteTlvShort(writer, 21, GroupNum);
            WriteTlvList(writer, 22, Train);
            WriteTlvShort(writer, 23, PotentialNum);
            WriteTlvList(writer, 24, RngAttrs);
            WriteTlvObject(writer, 28, TalkStyle);
            WriteTlvObject(writer, 29, EquipSkills);
            WriteTlvByte(writer, 30, Sp);
            WriteTlvObject(writer, 31, WaitSkills);
            WriteTlvInt(writer, 32, GrowHigherCat);
            WriteTlvObject(writer, 33, PotentialValue);
            WriteTlvObject(writer, 34, GroupExp);
        }
    }
}
