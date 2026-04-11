using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for dual int arrays (20 max each).
    /// C++ Reader: crygame.dll+sub_1017F930 (UnkTlv0143)
    /// C++ Printer: crygame.dll+sub_1017FE40
    /// </summary>
    public class TlvWeaponRecord : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxElements = 20;

        /// <summary>
        /// Weapon record data.
        /// Field ID: 1
        /// </summary>
        public int[] WeaponRecord { get; set; }

        /// <summary>
        /// Weapon record time data.
        /// Field ID: 2
        /// </summary>
        public int[] WeaponRecordTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: WeaponRecord = ReadTlvIntArray(reader); break;
                    case 2: WeaponRecordTime = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((WeaponRecord?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvWeaponRecord] WeaponRecord exceeds the maximum of {MaxElements} elements.");
            if ((WeaponRecordTime?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvWeaponRecord] WeaponRecordTime exceeds the maximum of {MaxElements} elements.");

            WriteTlvIntArray(writer, 1, WeaponRecord);
            WriteTlvIntArray(writer, 2, WeaponRecordTime);
        }
    }
}
