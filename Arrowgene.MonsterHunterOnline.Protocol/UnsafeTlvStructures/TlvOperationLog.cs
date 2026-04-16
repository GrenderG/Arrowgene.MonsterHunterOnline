using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for operation log entry.
    /// C++ Reader: crygame.dll+sub_10130FB0 (UnkTlv0044)
    /// C++ Printer: crygame.dll+sub_10131250
    /// </summary>
    public class TlvOperationLog : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxNameLength = 32;

        /// <summary>
        /// Operation type.
        /// Field ID: 1
        /// </summary>
        public int OperType { get; set; }

        /// <summary>
        /// Executor name.
        /// Field ID: 2
        /// </summary>
        public string Executor { get; set; } = string.Empty;

        /// <summary>
        /// Being executed name.
        /// Field ID: 3
        /// </summary>
        public string BeExecutored { get; set; } = string.Empty;

        /// <summary>
        /// Arguments value.
        /// Field ID: 4
        /// </summary>
        public int Args { get; set; }

        /// <summary>
        /// Time value.
        /// Field ID: 5
        /// </summary>
        public uint Time { get; set; }

        /// <summary>
        /// Arguments 2 value.
        /// Field ID: 6
        /// </summary>
        public int Args2 { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECKS ---
            if (!string.IsNullOrEmpty(Executor) && Encoding.UTF8.GetByteCount(Executor) >= MaxNameLength)
                throw new InvalidDataException($"[TlvOperationLog] Executor exceeds or equals the maximum of {MaxNameLength} bytes.");
            if (!string.IsNullOrEmpty(BeExecutored) && Encoding.UTF8.GetByteCount(BeExecutored) >= MaxNameLength)
                throw new InvalidDataException($"[TlvOperationLog] BeExecutored exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvInt32(buffer, 1, OperType);
            WriteTlvString(buffer, 2, Executor);
            WriteTlvString(buffer, 3, BeExecutored);
            WriteTlvInt32(buffer, 4, Args);
            WriteTlvInt32(buffer, 5, (int)Time);
            WriteTlvInt32(buffer, 6, Args2);
        }
    }
}
