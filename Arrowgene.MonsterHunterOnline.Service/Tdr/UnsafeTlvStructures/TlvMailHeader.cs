using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for mail header info.
    /// C++ Reader: crygame.dll+sub_102349C0 (UnkTlv0277)
    /// C++ Printer: crygame.dll+sub_10234E70
    /// </summary>
    public class TlvMailHeader : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;

        /// <summary>
        /// Source UID.
        /// Field ID: 1
        /// </summary>
        public ulong SrcUid { get; set; }

        /// <summary>
        /// Order.
        /// Field ID: 2
        /// </summary>
        public byte Order { get; set; }

        /// <summary>
        /// Destination UID.
        /// Field ID: 3
        /// </summary>
        public ulong DstUid { get; set; }

        /// <summary>
        /// Destination name.
        /// Field ID: 4
        /// </summary>
        public string DstName { get; set; } = string.Empty;

        /// <summary>
        /// Destination server.
        /// Field ID: 5
        /// </summary>
        public uint DstSvr { get; set; }

        /// <summary>
        /// Create time.
        /// Field ID: 6
        /// </summary>
        public uint CreateTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(DstName) && Encoding.UTF8.GetByteCount(DstName) >= MaxNameLength)
                throw new InvalidDataException($"[TlvMailHeader] DstName exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvInt64(buffer, 1, (long)SrcUid);
            WriteTlvByte(buffer, 2, Order);
            WriteTlvInt64(buffer, 3, (long)DstUid);
            WriteTlvString(buffer, 4, DstName);
            WriteTlvInt32(buffer, 5, (int)DstSvr);
            WriteTlvInt32(buffer, 6, (int)CreateTime);
        }
    }
}
