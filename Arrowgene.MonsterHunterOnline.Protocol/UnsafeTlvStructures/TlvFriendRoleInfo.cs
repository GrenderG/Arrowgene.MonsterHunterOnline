using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for friend role info.
    /// C++ Reader: crygame.dll+sub_1016B7D0 (UnkTlv0112)
    /// C++ Printer: crygame.dll+sub_1016BD40
    /// </summary>
    public class TlvFriendRoleInfo : Structure, ITlvStructure
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
        /// Group ID.
        /// Field ID: 4
        /// </summary>
        public byte GroupId { get; set; }

        /// <summary>
        /// Friendly value.
        /// Field ID: 5
        /// </summary>
        public uint Friendly { get; set; }

        /// <summary>
        /// Farm point.
        /// Field ID: 6
        /// </summary>
        public int FarmPoint { get; set; }

        /// <summary>
        /// Farm can-be-gathered count.
        /// Field ID: 7
        /// </summary>
        public int FarmCanBeGatheredCount { get; set; }

        /// <summary>
        /// HR level.
        /// Field ID: 8
        /// </summary>
        public int HrLevel { get; set; }

        /// <summary>
        /// Add time.
        /// Field ID: 9
        /// </summary>
        public int AddTime { get; set; }

        /// <summary>
        /// Server ID.
        /// Field ID: 10
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
                throw new InvalidDataException($"[TlvFriendRoleInfo] RoleName exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvInt64(buffer, 1, (long)RoleDbId);
            WriteTlvInt32(buffer, 2, Level);
            WriteTlvString(buffer, 3, RoleName);
            WriteTlvByte(buffer, 4, GroupId);
            WriteTlvInt32(buffer, 5, (int)Friendly);
            WriteTlvInt32(buffer, 6, FarmPoint);
            WriteTlvInt32(buffer, 7, FarmCanBeGatheredCount);
            WriteTlvInt32(buffer, 8, HrLevel);
            WriteTlvInt32(buffer, 9, AddTime);
            WriteTlvInt32(buffer, 10, SvrId);
        }
    }
}
