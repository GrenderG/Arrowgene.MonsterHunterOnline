using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for activity variables.
    /// C++ Reader: crygame.dll+sub_10154F30 (UnkTlv0085)
    /// C++ Printer: crygame.dll+sub_10155200
    /// </summary>
    public class TlvVarData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxVarCount = 16;

        /// <summary>
        /// Variable count (derived from Vars array length).
        /// Field ID: 1
        /// </summary>
        public byte VarNum => (byte)(VarData?.Length ?? 0);

        /// <summary>
        /// Variable values (int array).
        /// Field ID: 2
        /// </summary>
        public int[] VarData { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadByte(); break; // varNum, derived from array
                    case 2: VarData = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((VarData?.Length ?? 0) > MaxVarCount)
                throw new InvalidDataException($"[TlvVarData] VarData exceeds the maximum of {MaxVarCount} elements.");

            WriteTlvByte(writer, 1, VarNum);
            WriteTlvIntArray(writer, 2, VarData, VarNum);
        }
    }
}
