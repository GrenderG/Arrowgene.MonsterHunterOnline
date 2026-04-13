using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (Manufacturing Skills).
    /// C++ Writer: crygame.dll+sub_101F1260
    /// </summary>
    public class TlvManuSkill : Structure, ITlvStructure
    {
        public const int MaxManuSkills = 5;
        public const int MaxIngredients = 640;
        public const int MaxFormulaBits = 640;
        public const int MaxExpressions = 256;
        public const int ExactSkillWeapons = 13;

        public int Version { get; set; }

        public List<TlvSkillItem> ManuSkills { get; set; } = new List<TlvSkillItem>();
        public List<TlvIngredientItem> Ingredients { get; set; } = new List<TlvIngredientItem>();
        public List<TlvSkillWeaponItem> SkillWeapons { get; set; } = new List<TlvSkillWeaponItem>();
        public byte[] FormulaBits { get; set; } = new byte[0];
        public List<TlvExpressionItem> Expressions { get; set; } = new List<TlvExpressionItem>();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            //if (SkillWeapons.Count != ExactSkillWeapons) throw new InvalidDataException($"[TlvManuSkill] SkillWeapons count must be exactly {ExactSkillWeapons}.");
            if (ManuSkills.Count > MaxManuSkills) throw new InvalidDataException($"[TlvManuSkill] ManuSkills exceeds max.");
            if (Ingredients.Count > MaxIngredients) throw new InvalidDataException($"[TlvManuSkill] Ingredients exceeds max.");
            if (FormulaBits.Length > MaxFormulaBits) throw new InvalidDataException($"[TlvManuSkill] FormulaBits exceeds max.");
            if (Expressions.Count > MaxExpressions) throw new InvalidDataException($"[TlvManuSkill] Expressions exceeds max.");

            WriteTlvInt32(buffer, 1, Version);
            WriteTlvInt16(buffer, 2, (short)ManuSkills.Count);
            WriteTlvSubStructureList(buffer, 3, ManuSkills.Count, ManuSkills);
            WriteTlvInt16(buffer, 4, (short)Ingredients.Count);
            WriteTlvSubStructureList(buffer, 5, Ingredients.Count, Ingredients);
            WriteTlvSubStructureList(buffer, 6, SkillWeapons.Count, SkillWeapons);
            //Case 7 is ignored when magic is NoVariant
            WriteTlvInt32(buffer, 7, FormulaBits.Length * 8); // bit count, not byte count
            WriteTlvByteArr(buffer, 8, FormulaBits);
            WriteTlvInt16(buffer, 9, (short)Expressions.Count);
            WriteTlvSubStructureList(buffer, 10, Expressions.Count, Expressions);
        }
    }
}