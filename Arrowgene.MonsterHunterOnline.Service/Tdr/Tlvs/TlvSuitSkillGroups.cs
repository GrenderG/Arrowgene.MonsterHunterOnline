using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvSkillGroupData.
    /// C++ Reader: crygame.dll+sub_10189400 (UnkTlv0155)
    /// </summary>
    public class TlvSuitSkillGroups : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from SuitSkillGroupData).
        /// Field ID: 1
        /// </summary>
        public int Count => SuitSkillGroupData?.Count ?? 0;

        /// <summary>
        /// List of TlvSkillGroupData.
        /// Field ID: 2
        /// </summary>
        public List<TlvSkillGroupData> SuitSkillGroupData { get; set; }

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
                    case 2: SuitSkillGroupData = ReadTlvList<TlvSkillGroupData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, SuitSkillGroupData);
        }
    }
}
