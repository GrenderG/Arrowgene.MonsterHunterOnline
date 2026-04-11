using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for skill group with skill IDs and active flags.
    /// C++ Reader: crygame.dll+sub_101890F0 (UnkTlv0154)
    /// C++ Printer: crygame.dll+sub_10189670
    /// </summary>
    public class TlvSkillGroupData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxSkills = 30;

        /// <summary>
        /// Skill group ID.
        /// Field ID: 1
        /// </summary>
        public int SkillGroup { get; set; }

        /// <summary>
        /// Left edit count.
        /// Field ID: 2
        /// </summary>
        public int LeftEditCnt { get; set; }

        /// <summary>
        /// Skill count (byte).
        /// Field ID: 3
        /// </summary>
        public byte SkillCnt => (byte)(SkillId?.Length ?? 0);

        /// <summary>
        /// Skill IDs (int array).
        /// Field ID: 4
        /// </summary>
        public int[] SkillId { get; set; }

        /// <summary>
        /// Active flag bytes.
        /// Field ID: 5
        /// </summary>
        public byte[] ActFlag { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: SkillGroup = reader.ReadInt(); break;
                    case 2: LeftEditCnt = reader.ReadInt(); break;
                    case 3: reader.ReadByte(); break; // skillCnt, derived
                    case 4: SkillId = ReadTlvIntArray(reader); break;
                    case 5:
                        int len5 = reader.ReadInt();
                        if (len5 > 0 && len5 <= MaxSkills)
                            ActFlag = reader.ReadBytes(len5);
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((SkillId?.Length ?? 0) > MaxSkills)
                throw new InvalidDataException($"[TlvSkillGroupData] SkillId exceeds the maximum of {MaxSkills} elements.");
            if ((ActFlag?.Length ?? 0) > MaxSkills)
                throw new InvalidDataException($"[TlvSkillGroupData] ActFlag exceeds the maximum of {MaxSkills} elements.");

            WriteTlvInt(writer, 1, SkillGroup);
            WriteTlvInt(writer, 2, LeftEditCnt);
            WriteTlvByte(writer, 3, SkillCnt);
            WriteTlvIntArray(writer, 4, SkillId, SkillCnt);
            WriteTlvByteArray(writer, 5, ActFlag, SkillCnt);
        }
    }
}
