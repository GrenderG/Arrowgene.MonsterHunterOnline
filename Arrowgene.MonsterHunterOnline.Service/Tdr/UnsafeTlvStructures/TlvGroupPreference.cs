using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for group preference data.
    /// C++ Reader: crygame.dll+sub_1022A470 (UnkTlv0264)
    /// C++ Printer: crygame.dll+sub_1022A820
    /// </summary>
    public class TlvGroupPreference : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxPreferDataLength = 30;

        /// <summary>
        /// Group ID.
        /// Field ID: 1
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Prefer count (derived from PreferData).
        /// Field ID: 2
        /// </summary>
        public int PreferNum => PreferIds?.Length ?? 0;

        /// <summary>
        /// Preference ID bytes.
        /// Field ID: 3
        /// </summary>
        public byte[] PreferIds { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((PreferIds?.Length ?? 0) > MaxPreferDataLength)
                throw new InvalidDataException($"[TlvGroupPreference] PreferIds exceeds the maximum length of {MaxPreferDataLength} bytes.");

            WriteTlvInt32(buffer, 1, GroupId);
            WriteTlvInt32(buffer, 2, PreferNum);
            WriteTlvByteArr(buffer, 3, PreferIds);
        }
    }
}
