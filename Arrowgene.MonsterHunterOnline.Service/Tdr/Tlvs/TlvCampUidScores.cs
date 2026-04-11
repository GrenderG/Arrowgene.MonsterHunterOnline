using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for camp with long array + int array.
    /// C++ Reader: crygame.dll+sub_102496C0 (UnkTlv0295)
    /// C++ Printer: crygame.dll+sub_10249E50
    /// </summary>
    public class TlvCampUidScores : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxArrayElements = 8000;

        /// <summary>
        /// Count (derived from arrays).
        /// Field ID: 1
        /// </summary>
        public int Count => Uids?.Length ?? 0;

        /// <summary>
        /// Camp.
        /// Field ID: 2
        /// </summary>
        public int Camp { get; set; }

        /// <summary>
        /// UIDs (long array).
        /// Field ID: 3
        /// </summary>
        public long[] Uids { get; set; }

        /// <summary>
        /// Scores (int array).
        /// Field ID: 4
        /// </summary>
        public int[] Scores { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break;
                    case 2: Camp = reader.ReadInt(); break;
                    case 3: Uids = ReadTlvLongArray(reader); break;
                    case 4: Scores = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Uids?.Length ?? 0) > MaxArrayElements)
                throw new InvalidDataException($"[TlvCampUidScores] Uids exceeds the maximum of {MaxArrayElements} elements.");
            if ((Scores?.Length ?? 0) > MaxArrayElements)
                throw new InvalidDataException($"[TlvCampUidScores] Scores exceeds the maximum of {MaxArrayElements} elements.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvInt(writer, 2, Camp);
            WriteTlvLongArray(writer, 3, Uids);
            WriteTlvIntArray(writer, 4, Scores);
        }
    }
}
