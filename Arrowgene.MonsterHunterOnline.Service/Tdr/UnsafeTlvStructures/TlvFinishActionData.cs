using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for finish action bits and action step infos.
    /// C++ Reader: crygame.dll+sub_1018DE40 (UnkTlv0161)
    /// C++ Printer: crygame.dll+sub_1018E890
    /// </summary>
    public class TlvFinishActionData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxBitTags = 160;
        public const int MaxStepInfos = 8;

        /// <summary>
        /// Finish action bit tag count (derived from array).
        /// Field ID: 1
        /// </summary>
        public int FinishActionBitTagCount => FinishActionBitTag?.Length ?? 0;

        /// <summary>
        /// Finish action bit tags (byte array).
        /// Field ID: 2
        /// </summary>
        public byte[] FinishActionBitTag { get; set; }

        /// <summary>
        /// Action step info count (derived from list).
        /// Field ID: 3
        /// </summary>
        public int ActionStepInfoCount => ActionStepInfos?.Count ?? 0;

        /// <summary>
        /// Action step infos.
        /// Field ID: 4
        /// </summary>
        public List<TlvActionSteps> ActionStepInfos { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((FinishActionBitTag?.Length ?? 0) > MaxBitTags)
                throw new InvalidDataException($"[TlvFinishActionData] FinishActionBitTag exceeds the maximum of {MaxBitTags} elements.");
            if ((ActionStepInfos?.Count ?? 0) > MaxStepInfos)
                throw new InvalidDataException($"[TlvFinishActionData] ActionStepInfos exceeds the maximum of {MaxStepInfos} elements.");

            WriteTlvInt32(buffer, 1, FinishActionBitTagCount);
            WriteTlvByteArr(buffer, 2, FinishActionBitTag);
            WriteTlvInt32(buffer, 3, ActionStepInfoCount);
            WriteTlvSubStructureList(buffer, 4, ActionStepInfos.Count, ActionStepInfos);
        }
    }
}
