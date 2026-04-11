using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for a list of typed variant values.
    /// C++ Reader: crygame.dll+sub_101AEBA0 (UnkTlv0189)
    /// C++ Printer: crygame.dll+sub_101AF840
    /// </summary>
    public class TlvTypedVariantList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxValues = 7;

        /// <summary>
        /// Value list.
        /// Field ID: 1
        /// </summary>
        public List<TlvTypedVariant> Value { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Value = ReadTlvList<TlvTypedVariant>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvList(writer, 1, Value);
        }
    }
}
