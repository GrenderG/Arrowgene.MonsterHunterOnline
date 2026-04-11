using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for refresh + lib + task short array.
    /// C++ Reader: crygame.dll+sub_10227320 (UnkTlv0259)
    /// C++ Printer: crygame.dll+sub_10227850
    /// </summary>
    public class TlvRefreshLibTasks : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxTasks = 256;

        /// <summary>
        /// Refresh time.
        /// Field ID: 1
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Library ID.
        /// Field ID: 2
        /// </summary>
        public int Lib { get; set; }

        /// <summary>
        /// Task count (derived from Tasks array).
        /// Field ID: 3
        /// </summary>
        public short TaskCount => (short)(Tasks?.Length ?? 0);

        /// <summary>
        /// Task IDs (short array).
        /// Field ID: 4
        /// </summary>
        public short[] Tasks { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: RefreshTime = (uint)reader.ReadInt(); break;
                    case 2: Lib = reader.ReadInt(); break;
                    case 3: reader.ReadShort(); break;
                    case 4: Tasks = ReadTlvShortArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Tasks?.Length ?? 0) > MaxTasks)
                throw new InvalidDataException($"[TlvRefreshLibTasks] Tasks exceeds the maximum of {MaxTasks} elements.");

            WriteTlvInt(writer, 1, (int)RefreshTime);
            WriteTlvInt(writer, 2, Lib);
            WriteTlvShort(writer, 3, TaskCount);
            WriteTlvShortArray(writer, 4, Tasks);
        }
    }
}
