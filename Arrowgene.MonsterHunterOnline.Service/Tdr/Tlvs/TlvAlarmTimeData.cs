using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for alarm / online time refresh data.
    /// C++ Reader: crygame.dll+sub_10157BB0 (UnkTlv0089)
    /// C++ Printer: crygame.dll+sub_10157F00
    /// </summary>
    public class TlvAlarmTimeData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxSelfDefs = 10;

        /// <summary>
        /// Daily online time.
        /// Field ID: 1
        /// </summary>
        public TlvOnlineTime Daily { get; set; } = new();

        /// <summary>
        /// Weekly online time.
        /// Field ID: 2
        /// </summary>
        public TlvOnlineTime Weekly { get; set; } = new();

        /// <summary>
        /// Monthly online time.
        /// Field ID: 3
        /// </summary>
        public TlvOnlineTime Monthly { get; set; } = new();

        /// <summary>
        /// Self-defined count (derived from list).
        /// Field ID: 4
        /// </summary>
        public byte Count => (byte)(SelfDefs?.Count ?? 0);

        /// <summary>
        /// Self-defined online time entries.
        /// Field ID: 5
        /// </summary>
        public List<TlvOnlineTime> SelfDefs { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Daily = ReadTlvObject<TlvOnlineTime>(reader); break;
                    case 2: Weekly = ReadTlvObject<TlvOnlineTime>(reader); break;
                    case 3: Monthly = ReadTlvObject<TlvOnlineTime>(reader); break;
                    case 4: reader.ReadByte(); break; // count, derived
                    case 5: SelfDefs = ReadTlvList<TlvOnlineTime>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((SelfDefs?.Count ?? 0) > MaxSelfDefs)
                throw new InvalidDataException($"[TlvAlarmTimeData] SelfDefs exceeds the maximum of {MaxSelfDefs} elements.");

            WriteTlvObject(writer, 1, Daily);
            WriteTlvObject(writer, 2, Weekly);
            WriteTlvObject(writer, 3, Monthly);
            WriteTlvByte(writer, 4, Count);
            WriteTlvList(writer, 5, SelfDefs);
        }
    }
}
