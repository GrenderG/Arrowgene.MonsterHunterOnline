using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvIdNewFlag.
    /// C++ Reader: crygame.dll+sub_101A2EA0 (UnkTlv0183)
    /// </summary>
    public class TlvNewFlagSkills : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from Skill).
        /// Field ID: 1
        /// </summary>
        public int Count => Skill?.Count ?? 0;

        /// <summary>
        /// List of TlvIdNewFlag.
        /// Field ID: 2
        /// </summary>
        public List<TlvIdNewFlag> Skill { get; set; }

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
                    case 2: Skill = ReadTlvList<TlvIdNewFlag>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Skill);
        }
    }
}
