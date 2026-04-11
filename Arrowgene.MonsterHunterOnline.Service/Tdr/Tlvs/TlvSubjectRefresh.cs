using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for subject count with short array and refresh time.
    /// C++ Reader: crygame.dll+sub_1015CE80 (UnkTlv0096)
    /// C++ Printer: crygame.dll+sub_1015D380
    /// </summary>
    public class TlvSubjectRefresh : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxSubjects = 32;

        /// <summary>
        /// Subject count (derived from Subjects array).
        /// Field ID: 1
        /// </summary>
        public int SubjectCnt => Subject?.Length ?? 0;

        /// <summary>
        /// Subject IDs (short array).
        /// Field ID: 2
        /// </summary>
        public short[] Subject { get; set; }

        /// <summary>
        /// Refresh time.
        /// Field ID: 3
        /// </summary>
        public uint RefreshTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // subjectCnt, derived from array
                    case 2: Subject = ReadTlvShortArray(reader); break;
                    case 3: RefreshTime = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Subject?.Length ?? 0) > MaxSubjects)
                throw new InvalidDataException($"[TlvSubjectRefresh] Subject exceeds the maximum of {MaxSubjects} elements.");

            WriteTlvInt(writer, 1, SubjectCnt);
            WriteTlvShortArray(writer, 2, Subject);
            WriteTlvInt(writer, 3, (int)RefreshTime);
        }
    }
}
