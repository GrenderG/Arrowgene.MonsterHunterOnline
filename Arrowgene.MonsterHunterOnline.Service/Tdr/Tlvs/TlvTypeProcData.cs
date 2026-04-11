using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for type with process data.
    /// C++ Reader: crygame.dll+sub_101514C0 (UnkTlv0081)
    /// C++ Printer: crygame.dll+sub_10152C90
    /// </summary>
    public class TlvTypeProcData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxProcDataLength = 256;

        /// <summary>
        /// Type.
        /// Field ID: 1
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Process data length (derived from ProcData).
        /// Field ID: 2
        /// </summary>
        public int ProcLen => ProcData?.Length ?? 0;

        /// <summary>
        /// Process data bytes.
        /// Field ID: 3
        /// </summary>
        public byte[] ProcData { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Type = reader.ReadInt(); break;
                    case 2: reader.ReadInt(); break; // procLen, derived from array
                    case 3:
                        int byteLen = reader.ReadInt();
                        if (byteLen > 0 && byteLen <= MaxProcDataLength)
                            ProcData = reader.ReadBytes(byteLen);
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((ProcData?.Length ?? 0) > MaxProcDataLength)
                throw new InvalidDataException($"[TlvTypeProcData] ProcData exceeds the maximum length of {MaxProcDataLength} bytes.");

            WriteTlvInt(writer, 1, Type);
            WriteTlvInt(writer, 2, ProcLen);
            WriteTlvByteArray(writer, 3, ProcData, ProcLen);
        }
    }
}
