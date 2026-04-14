using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for alarm / online time refresh data.
    /// C++ Reader: crygame.dll+sub_10157BB0 (UnkTlv0089)
    /// C++ Printer: crygame.dll+sub_10157F00
    /// </summary>
    public class TlvAlarmTimeData : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((SelfDefs?.Count ?? 0) > MaxSelfDefs)
                throw new InvalidDataException($"[TlvAlarmTimeData] SelfDefs exceeds the maximum of {MaxSelfDefs} elements.");

            WriteTlvSubStructure(buffer, 1, Daily);
            WriteTlvSubStructure(buffer, 2, Weekly);
            WriteTlvSubStructure(buffer, 3, Monthly);
            WriteTlvByte(buffer, 4, Count);
            WriteTlvSubStructureList(buffer, 5, SelfDefs.Count, SelfDefs);
        }
    }
}
