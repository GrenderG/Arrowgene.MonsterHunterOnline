using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for role display names.
    /// C++ Reader: crygame.dll+sub_101514C0 (UnkTlv0080)
    /// C++ Printer: crygame.dll+sub_10151890
    /// </summary>
    public class TlvRoleNames : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;

        /// <summary>
        /// Character name.
        /// Field ID: 1
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Guild name.
        /// Field ID: 2
        /// </summary>
        public string Guild { get; set; } = string.Empty;

        /// <summary>
        /// Clan name.
        /// Field ID: 3
        /// </summary>
        public string Clan { get; set; } = string.Empty;

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvRoleNames] Name exceeds or equals the maximum of {MaxNameLength} bytes.");
            if (!string.IsNullOrEmpty(Guild) && Encoding.UTF8.GetByteCount(Guild) >= MaxNameLength)
                throw new InvalidDataException($"[TlvRoleNames] Guild exceeds or equals the maximum of {MaxNameLength} bytes.");
            if (!string.IsNullOrEmpty(Clan) && Encoding.UTF8.GetByteCount(Clan) >= MaxNameLength)
                throw new InvalidDataException($"[TlvRoleNames] Clan exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvString(buffer, 1, Name);
            WriteTlvString(buffer, 2, Guild);
            WriteTlvString(buffer, 3, Clan);
        }
    }
}
