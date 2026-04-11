using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for action point data.
    /// C++ Reader: crygame.dll+sub_101660B0 (UnkTlv0104)
    /// C++ Printer: crygame.dll+sub_10166570
    /// </summary>
    public class TlvActionPointData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxResetTimes = 2;

        /// <summary>
        /// Action point values (int array, max 2).
        /// Field ID: 2
        /// </summary>
        public int[] ActionPoint { get; set; }

        /// <summary>
        /// Next reset time.
        /// Field ID: 3
        /// </summary>
        public int NextResetTime { get; set; }

        /// <summary>
        /// Action point flags.
        /// Field ID: 4
        /// </summary>
        public uint ActionPointFlags { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: ActionPoint = ReadTlvIntArray(reader); break;
                    case 3: NextResetTime = reader.ReadInt(); break;
                    case 4: ActionPointFlags = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((ActionPoint?.Length ?? 0) > MaxResetTimes)
                throw new InvalidDataException($"[TlvActionPointData] ActionPoint exceeds the maximum of {MaxResetTimes} elements.");

            WriteTlvIntArray(writer, 2, ActionPoint);
            WriteTlvInt(writer, 3, NextResetTime);
            WriteTlvInt(writer, 4, (int)ActionPointFlags);
        }
    }
}
