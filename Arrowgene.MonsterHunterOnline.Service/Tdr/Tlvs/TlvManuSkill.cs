using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (Manufacturing Skills).
    /// C++ Writer: crygame.dll+sub_101F1260
    /// </summary>
    public class TlvManuSkill : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Version = reader.ReadVarInt(); break;
                    case 2: reader.ReadVarShort(); break; // Discard ManuSkillCount
                    case 3: ManuSkills = ReadTlvList<TlvSkillItem>(reader); break;
                    case 4: reader.ReadVarShort(); break; // Discard IngredientCount
                    case 5: Ingredients = ReadTlvList<TlvIngredientItem>(reader); break;
                    case 6: SkillWeapons = ReadTlvList<TlvSkillWeaponItem>(reader); break;
                    case 7: reader.ReadVarInt(); break; // Discard FormulaBitCount
                    case 8: FormulaBits = reader.ReadBytes(reader.ReadInt()); break;
                    case 9: reader.ReadVarShort(); break; // Discard ExpressionCount
                    case 10: Expressions = ReadTlvList<TlvExpressionItem>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if (SkillWeapons.Count != ExactSkillWeapons) throw new InvalidDataException($"[TlvManuSkill] SkillWeapons count must be exactly {ExactSkillWeapons}.");
            if (ManuSkills.Count > MaxManuSkills) throw new InvalidDataException($"[TlvManuSkill] ManuSkills exceeds max.");
            if (Ingredients.Count > MaxIngredients) throw new InvalidDataException($"[TlvManuSkill] Ingredients exceeds max.");
            if (FormulaBits.Length > MaxFormulaBits) throw new InvalidDataException($"[TlvManuSkill] FormulaBits exceeds max.");
            if (Expressions.Count > MaxExpressions) throw new InvalidDataException($"[TlvManuSkill] Expressions exceeds max.");

            WriteTlvInt(writer, 1, Version);
            WriteTlvShort(writer, 2, (short)ManuSkills.Count);
            WriteTlvList(writer, 3, ManuSkills);
            WriteTlvShort(writer, 4, (short)Ingredients.Count);
            WriteTlvList(writer, 5, Ingredients);
            WriteTlvList(writer, 6, SkillWeapons);
            WriteTlvInt(writer, 7, FormulaBits.Length);
            WriteTlvByteArray(writer, 8, FormulaBits);
            WriteTlvShort(writer, 9, (short)Expressions.Count);
            WriteTlvList(writer, 10, Expressions);
        }
    }
}