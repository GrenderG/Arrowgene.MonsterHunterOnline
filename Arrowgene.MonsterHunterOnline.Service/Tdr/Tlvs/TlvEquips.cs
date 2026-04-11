using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvItemType.
    /// C++ Reader: crygame.dll+sub_1013B9E0 (UnkTlv0054)
    /// </summary>
    public class TlvEquips : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from Equips).
        /// Field ID: 1
        /// </summary>
        public int Count => Equips?.Count ?? 0;

        /// <summary>
        /// List of TlvItemType.
        /// Field ID: 2
        /// </summary>
        public List<TlvItemType> Equips { get; set; }

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
                    case 2: Equips = ReadTlvList<TlvItemType>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Equips);
        }
    }
}
