using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for finish action bits and action step infos.
    /// C++ Reader: crygame.dll+sub_1018DE40 (UnkTlv0161)
    /// C++ Printer: crygame.dll+sub_1018E890
    /// </summary>
    public class TlvFinishActionData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // finishActionBitTagCount, derived
                    case 2:
                        int len2 = reader.ReadInt();
                        if (len2 > 0 && len2 <= MaxBitTags)
                            FinishActionBitTag = reader.ReadBytes(len2);
                        break;
                    case 3: reader.ReadInt(); break; // actionStepInfoCount, derived
                    case 4: ActionStepInfos = ReadTlvList<TlvActionSteps>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((FinishActionBitTag?.Length ?? 0) > MaxBitTags)
                throw new InvalidDataException($"[TlvFinishActionData] FinishActionBitTag exceeds the maximum of {MaxBitTags} elements.");
            if ((ActionStepInfos?.Count ?? 0) > MaxStepInfos)
                throw new InvalidDataException($"[TlvFinishActionData] ActionStepInfos exceeds the maximum of {MaxStepInfos} elements.");

            WriteTlvInt(writer, 1, FinishActionBitTagCount);
            WriteTlvByteArray(writer, 2, FinishActionBitTag, FinishActionBitTagCount);
            WriteTlvInt(writer, 3, ActionStepInfoCount);
            WriteTlvList(writer, 4, ActionStepInfos);
        }
    }
}
