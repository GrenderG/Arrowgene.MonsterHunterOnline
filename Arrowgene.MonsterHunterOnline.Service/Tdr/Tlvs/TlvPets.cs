using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvPetInfo.
    /// C++ Reader: crygame.dll+sub_101A03C0 (UnkTlv0178)
    /// </summary>
    public class TlvPets : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from Pets).
        /// Field ID: 1
        /// </summary>
        public int Count => Pets?.Count ?? 0;

        /// <summary>
        /// List of TlvPetInfo.
        /// Field ID: 2
        /// </summary>
        public List<TlvPetInfo> Pets { get; set; }

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
                    case 2: Pets = ReadTlvList<TlvPetInfo>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Pets);
        }
    }
}
