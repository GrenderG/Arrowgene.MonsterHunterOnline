using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for basic role info.
    /// C++ Reader: crygame.dll+sub_1016D110 (UnkTlv0114)
    /// C++ Printer: crygame.dll+sub_1016D370
    /// </summary>
    public class TlvBasicRoleInfo : Structure, ITlvStructure
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
        /// HR Level.
        /// Field ID: 4
        /// </summary>
        public int HrLevel { get; set; }

        /// <summary>
        /// Server ID.
        /// Field ID: 5
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
                throw new InvalidDataException($"[TlvBasicRoleInfo] RoleName exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvInt64(buffer, 1, (long)RoleDbId);
            WriteTlvInt32(buffer, 2, Level);
            WriteTlvString(buffer, 3, RoleName);
            WriteTlvInt32(buffer, 4, HrLevel);
            WriteTlvInt32(buffer, 5, SvrId);
        }
    }
}
