using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for single int array (20 max).
    /// C++ Reader: crygame.dll+sub_10180360 (UnkTlv0144)
    /// C++ Printer: crygame.dll+sub_101805F0
    /// </summary>
    public class TlvWeaponStyleData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxElements = 20;

        /// <summary>
        /// Weapon style data.
        /// Field ID: 1
        /// </summary>
        public int[] WeaponStyleData { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: WeaponStyleData = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((WeaponStyleData?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvWeaponStyleData] WeaponStyleData exceeds the maximum of {MaxElements} elements.");

            WriteTlvIntArray(writer, 1, WeaponStyleData);
        }
    }
}
