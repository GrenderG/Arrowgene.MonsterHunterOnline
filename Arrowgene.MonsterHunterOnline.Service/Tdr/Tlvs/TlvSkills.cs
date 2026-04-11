using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvSkillLevel.
    /// C++ Reader: crygame.dll+sub_10128180 (UnkTlv0033)
    /// </summary>
    public class TlvSkills : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from Skills).
        /// Field ID: 1
        /// </summary>
        public int Count => Skills?.Count ?? 0;

        /// <summary>
        /// List of TlvSkillLevel.
        /// Field ID: 2
        /// </summary>
        public List<TlvSkillLevel> Skills { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break;
                    case 2: Skills = ReadTlvList<TlvSkillLevel>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Skills);
        }
    }
}
