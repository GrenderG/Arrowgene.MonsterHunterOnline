using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for passerby role info.
    /// C++ Reader: crygame.dll+sub_1016C710 (UnkTlv0113)
    /// C++ Printer: crygame.dll+sub_1016C990
    /// </summary>
    public class TlvPasserbyInfo : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;

        /// <summary>
        /// Role database ID.
        /// Field ID: 1
        /// </summary>
        public ulong RoleDbId { get; set; }

        /// <summary>
        /// Level.
        /// Field ID: 2
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Role name.
        /// Field ID: 3
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// Meet way.
        /// Field ID: 4
        /// </summary>
        public byte MeetWay { get; set; }

        /// <summary>
        /// HR Level.
        /// Field ID: 5
        /// </summary>
        public int HrLevel { get; set; }

        /// <summary>
        /// Server ID.
        /// Field ID: 6
        /// </summary>
        public int SvrId { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(RoleName) && Encoding.UTF8.GetByteCount(RoleName) >= MaxNameLength)
                throw new InvalidDataException($"[TlvPasserbyInfo] RoleName exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvInt64(buffer, 1, (long)RoleDbId);
            WriteTlvInt32(buffer, 2, Level);
            WriteTlvString(buffer, 3, RoleName);
            WriteTlvByte(buffer, 4, MeetWay);
            WriteTlvInt32(buffer, 5, HrLevel);
            WriteTlvInt32(buffer, 6, SvrId);
        }
    }
}
