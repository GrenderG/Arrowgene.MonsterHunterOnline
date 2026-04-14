using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for channel name with flags.
    /// C++ Reader: crygame.dll+sub_10167140 (UnkTlv0106)
    /// C++ Printer: crygame.dll+sub_101674B0
    /// </summary>
    public class TlvChannelNameFlags : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 21;

        /// <summary>
        /// Channel name.
        /// Field ID: 1
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Channel flags.
        /// Field ID: 2
        /// </summary>
        public uint ChannelFlags { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvChannelNameFlags] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvString(buffer, 1, Name);
            WriteTlvInt32(buffer, 2, (int)ChannelFlags);
        }
    }
}
