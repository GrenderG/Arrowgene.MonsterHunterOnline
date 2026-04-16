using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for pet battle/farm data entry.
    /// C++ Reader: crygame.dll+sub_101A54E0 (UnkTlv0185)
    /// C++ Printer: crygame.dll+sub_101A5DD0
    /// </summary>
    public class TlvPetBattleData : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((Train?.Count ?? 0) > MaxTrain)
                throw new InvalidDataException($"[TlvPetBattleData] Train exceeds {MaxTrain}.");
            if ((RngAttrs?.Count ?? 0) > MaxRngAttrs)
                throw new InvalidDataException($"[TlvPetBattleData] RngAttrs exceeds {MaxRngAttrs}.");

            WriteTlvByte(buffer, 2, Idx);
            WriteTlvInt32(buffer, 3, UId);
            WriteTlvInt32(buffer, 4, Id);
            WriteTlvString(buffer, 5, Name);
            WriteTlvString(buffer, 6, Desc);
            WriteTlvByte(buffer, 7, Sex);
            WriteTlvByte(buffer, 8, State);
            WriteTlvInt32(buffer, 9, Level);
            WriteTlvInt32(buffer, 10, Exp);
            WriteTlvInt16(buffer, 11, Loyal);
            WriteTlvInt16(buffer, 12, Potential);
            WriteTlvInt16(buffer, 14, Spirit);
            WriteTlvInt16(buffer, 15, Vigour);
            WriteTlvInt32(buffer, 16, SupportSkill);
            WriteTlvInt32(buffer, 17, GiftSkill);
            WriteTlvByte(buffer, 18, Rename);
            WriteTlvInt16(buffer, 21, GroupNum);
            WriteTlvSubStructureList(buffer, 22, Train.Count, Train);
            WriteTlvInt16(buffer, 23, PotentialNum);
            WriteTlvSubStructureList(buffer, 24, RngAttrs.Count, RngAttrs);
            WriteTlvSubStructure(buffer, 28, TalkStyle);
            WriteTlvSubStructure(buffer, 29, EquipSkills);
            WriteTlvByte(buffer, 30, Sp);
            WriteTlvSubStructure(buffer, 31, WaitSkills);
            WriteTlvInt32(buffer, 32, GrowHigherCat);
            WriteTlvSubStructure(buffer, 33, PotentialValue);
            WriteTlvSubStructure(buffer, 34, GroupExp);
        }
    }
}
