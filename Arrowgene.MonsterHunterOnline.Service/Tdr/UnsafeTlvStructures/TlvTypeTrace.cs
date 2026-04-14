using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for typed trace data with three args.
    /// C++ Reader: crygame.dll+sub_1017CB40 (UnkTlv0139)
    /// C++ Printer: crygame.dll+sub_1017D010
    /// </summary>
    public class TlvTypeTrace : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxTrace = 5;

        /// <summary>
        /// Type.
        /// Field ID: 1
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Count (derived from Trace).
        /// Field ID: 2
        /// </summary>
        public int Count => Trace?.Count ?? 0;

        /// <summary>
        /// Trace list.
        /// Field ID: 3
        /// </summary>
        public List<TlvThreeArgs> Trace { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Trace?.Count ?? 0) > MaxTrace)
                throw new InvalidDataException($"[TlvTypeTrace] Trace exceeds the maximum of {MaxTrace} elements.");

            WriteTlvByte(buffer, 1, Type);
            WriteTlvInt32(buffer, 2, Count);
            WriteTlvSubStructureList(buffer, 3, Trace.Count, Trace);
        }
    }
}
