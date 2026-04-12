using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_101EF630
    /// C++ Reader: crygame.dll+sub_XXXXX
    /// C++ Printer: crygame.dll+sub_101F0B50
    /// </summary>
    public class TlvSkillWeaponItem : Structure, ITlvStructure
    {
        public const int MaxSkillLearns = 4;
        public const int MaxTalentLearns = 16;
        public const int MaxTalentEquips = 8;
        public const int MaxRages = 5;

        public List<TlvSkillLearnIdItem> SkillLearns { get; set; } = new List<TlvSkillLearnIdItem>();
        public List<TlvTalentLearnItem> TalentLearns { get; set; } = new List<TlvTalentLearnItem>();
        public List<TlvTalentEquipItem> TalentEquips { get; set; } = new List<TlvTalentEquipItem>();

        public int RageIdx { get; set; }
        public int[] Rages { get; set; } = new int[0];
        public int[] BushidoRages { get; set; } = new int[0];

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECKS ---
// TODO boundary:             if (SkillLearns.Count > MaxSkillLearns)
// TODO boundary:                 throw new InvalidDataException($"[TlvSkillWeaponItem] SkillLearns count exceeds max of {MaxSkillLearns}.");
// TODO boundary:             if (TalentLearns.Count > MaxTalentLearns)
// TODO boundary:                 throw new InvalidDataException($"[TlvSkillWeaponItem] TalentLearns count exceeds max of {MaxTalentLearns}.");
// TODO boundary:             if (TalentEquips.Count > MaxTalentEquips)
// TODO boundary:                 throw new InvalidDataException($"[TlvSkillWeaponItem] TalentEquips count exceeds max of {MaxTalentEquips}.");
// TODO boundary:             if (Rages.Length > MaxRages)
// TODO boundary:                 throw new InvalidDataException($"[TlvSkillWeaponItem] Rages array exceeds max of {MaxRages}.");
            if (BushidoRages.Length > MaxRages) // Uses the same max count (5)
                throw new InvalidDataException($"[TlvSkillWeaponItem] BushidoRages array exceeds max of {MaxRages}.");

            WriteTlvByte(buffer, 2, (byte)SkillLearns.Count);
            WriteTlvSubStructureList(buffer, 3, SkillLearns.Count, SkillLearns);
            WriteTlvByte(buffer, 4, (byte)TalentLearns.Count);
            WriteTlvSubStructureList(buffer, 5, TalentLearns.Count, TalentLearns);
            WriteTlvByte(buffer, 6, (byte)TalentEquips.Count);
            WriteTlvSubStructureList(buffer, 7, TalentEquips.Count, TalentEquips);
            WriteTlvByte(buffer, 8, (byte)RageIdx);
            WriteTlvByte(buffer, 9, (byte)Rages.Length);
            WriteTlvVarInt32Arr(buffer, 10, Rages);
            WriteTlvByte(buffer, 11, (byte)BushidoRages.Length);
            WriteTlvVarInt32Arr(buffer, 12, BushidoRages);
        }
    }
}