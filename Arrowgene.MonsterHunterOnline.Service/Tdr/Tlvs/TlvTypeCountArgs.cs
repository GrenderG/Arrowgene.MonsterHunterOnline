using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for type + count + three int arrays.
    /// C++ Reader: crygame.dll+sub_10179DA0 (UnkTlv0135)
    /// C++ Printer: crygame.dll+sub_1017A3D0
    /// </summary>
    public class TlvTypeCountArgs : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxArgs = 5;

        /// <summary>
        /// Type byte.
        /// Field ID: 1
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Count (derived from arrays).
        /// Field ID: 2
        /// </summary>
        public int Count => Arg1?.Length ?? 0;

        /// <summary>
        /// Arg1 int array.
        /// Field ID: 4
        /// </summary>
        public int[] Arg1 { get; set; }

        /// <summary>
        /// Arg2 int array.
        /// Field ID: 5
        /// </summary>
        public int[] Arg2 { get; set; }

        /// <summary>
        /// Arg3 int array.
        /// Field ID: 6
        /// </summary>
        public int[] Arg3 { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Type = reader.ReadByte(); break;
                    case 2: reader.ReadInt(); break; // count, derived
                    case 4: Arg1 = ReadTlvIntArray(reader); break;
                    case 5: Arg2 = ReadTlvIntArray(reader); break;
                    case 6: Arg3 = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Arg1?.Length ?? 0) > MaxArgs)
                throw new InvalidDataException($"[TlvTypeCountArgs] Arg1 exceeds the maximum of {MaxArgs} elements.");
            if ((Arg2?.Length ?? 0) > MaxArgs)
                throw new InvalidDataException($"[TlvTypeCountArgs] Arg2 exceeds the maximum of {MaxArgs} elements.");
            if ((Arg3?.Length ?? 0) > MaxArgs)
                throw new InvalidDataException($"[TlvTypeCountArgs] Arg3 exceeds the maximum of {MaxArgs} elements.");

            WriteTlvByte(writer, 1, Type);
            WriteTlvInt(writer, 2, Count);
            WriteTlvIntArray(writer, 4, Arg1, Count);
            WriteTlvIntArray(writer, 5, Arg2, Count);
            WriteTlvIntArray(writer, 6, Arg3, Count);
        }
    }
}
