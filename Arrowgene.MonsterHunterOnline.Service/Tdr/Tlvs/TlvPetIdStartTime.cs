using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for dual int arrays (4 max each).
    /// C++ Reader: crygame.dll+sub_10201480 (UnkTlv0214)
    /// C++ Printer: crygame.dll+sub_10201930
    /// </summary>
    public class TlvPetIdStartTime : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxElements = 4;

        /// <summary>
        /// Pet IDs.
        /// Field ID: 1
        /// </summary>
        public int[] PetId { get; set; }

        /// <summary>
        /// Start times.
        /// Field ID: 2
        /// </summary>
        public int[] StartTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: PetId = ReadTlvIntArray(reader); break;
                    case 2: StartTime = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((PetId?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvPetIdStartTime] PetId exceeds the maximum of {MaxElements} elements.");
            if ((StartTime?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvPetIdStartTime] StartTime exceeds the maximum of {MaxElements} elements.");

            WriteTlvIntArray(writer, 1, PetId);
            WriteTlvIntArray(writer, 2, StartTime);
        }
    }
}
