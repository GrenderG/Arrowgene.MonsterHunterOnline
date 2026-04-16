using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for group ID with name.
    /// C++ Reader: crygame.dll+sub_1016D790 (UnkTlv0115)
    /// C++ Printer: crygame.dll+sub_1016D890
    /// </summary>
    public class TlvGroupIdName : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxGroupNameLength = 32;

        /// <summary>
        /// Group ID.
        /// Field ID: 1
        /// </summary>
        public byte GroupId { get; set; }

        /// <summary>
        /// Group name.
        /// Field ID: 2
        /// </summary>
        public string GroupName { get; set; } = string.Empty;

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(GroupName) && Encoding.UTF8.GetByteCount(GroupName) >= MaxGroupNameLength)
                throw new InvalidDataException($"[TlvGroupIdName] GroupName exceeds or equals the maximum of {MaxGroupNameLength} bytes.");

            WriteTlvByte(buffer, 1, GroupId);
            WriteTlvString(buffer, 2, GroupName);
        }
    }
}
