using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvValHit.
    /// C++ Reader: crygame.dll+sub_1022C7C0 (UnkTlv0267)
    /// </summary>
    public class TlvPieces : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from Pieces).
        /// Field ID: 1
        /// </summary>
        public int Count => Pieces?.Count ?? 0;

        /// <summary>
        /// List of TlvValHit.
        /// Field ID: 2
        /// </summary>
        public List<TlvValHit> Pieces { get; set; }

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
                    case 2: Pieces = ReadTlvList<TlvValHit>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Pieces);
        }
    }
}
