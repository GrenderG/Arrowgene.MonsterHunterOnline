using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for attribute block.
    /// C++ Reader: crygame.dll+sub_1013C920 (UnkTlv0055)
    /// C++ Printer: crygame.dll+sub_1013CB70
    /// </summary>
    public class TlvAttrData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxAttrDataLength = 1286;

        /// <summary>
        /// Attribute count (derived from AttrData).
        /// Field ID: 1
        /// </summary>
        public int AttrSize => Attrs?.Length ?? 0;

        /// <summary>
        /// Attribute data bytes.
        /// Field ID: 2
        /// </summary>
        public byte[] Attrs { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // attrSize, derived from array
                    case 2:
                        int byteLen = reader.ReadInt();
                        if (byteLen > 0 && byteLen <= MaxAttrDataLength)
                            Attrs = reader.ReadBytes(byteLen);
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Attrs?.Length ?? 0) > MaxAttrDataLength)
                throw new InvalidDataException($"[TlvAttrData] Attrs exceeds the maximum of {MaxAttrDataLength} elements.");

            WriteTlvInt(writer, 1, AttrSize);
            WriteTlvByteArray(writer, 2, Attrs, AttrSize);
        }
    }
}