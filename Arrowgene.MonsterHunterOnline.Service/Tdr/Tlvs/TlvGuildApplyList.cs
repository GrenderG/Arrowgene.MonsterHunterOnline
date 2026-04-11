using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guild apply list.
    /// C++ Reader: crygame.dll+sub_10124190 (UnkTlv0027)
    /// C++ Printer: crygame.dll+sub_10124650
    /// </summary>
    public class TlvGuildApplyList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxApplys = 128;

        /// <summary>
        /// Apply count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int Count => Applys?.Count ?? 0;

        /// <summary>
        /// Apply entries.
        /// Field ID: 2
        /// </summary>
        public List<TlvGuildMemberInfo> Applys { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // count, derived
                    case 2: Applys = ReadTlvList<TlvGuildMemberInfo>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Applys?.Count ?? 0) > MaxApplys)
                throw new InvalidDataException($"[TlvGuildApplyList] Applys exceeds the maximum of {MaxApplys} elements.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Applys);
        }
    }
}
