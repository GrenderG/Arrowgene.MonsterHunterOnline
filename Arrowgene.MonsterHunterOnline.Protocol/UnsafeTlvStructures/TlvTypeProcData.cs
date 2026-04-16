using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for type with process data.
    /// C++ Reader: crygame.dll+sub_101514C0 (UnkTlv0081)
    /// C++ Printer: crygame.dll+sub_10152C90
    /// </summary>
    public class TlvTypeProcData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxProcDataLength = 256;

        /// <summary>
        /// Type.
        /// Field ID: 1
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Process data length (derived from ProcData).
        /// Field ID: 2
        /// </summary>
        public int ProcLen => ProcData?.Length ?? 0;

        /// <summary>
        /// Process data bytes.
        /// Field ID: 3
        /// </summary>
        public byte[] ProcData { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((ProcData?.Length ?? 0) > MaxProcDataLength)
                throw new InvalidDataException($"[TlvTypeProcData] ProcData exceeds the maximum length of {MaxProcDataLength} bytes.");

            WriteTlvInt32(buffer, 1, Type);
            WriteTlvInt32(buffer, 2, ProcLen);
            WriteTlvByteArr(buffer, 3, ProcData);
        }
    }
}
