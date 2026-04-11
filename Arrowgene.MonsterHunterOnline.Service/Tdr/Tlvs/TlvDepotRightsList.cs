using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for depot rights list.
    /// C++ Reader: crygame.dll+sub_1011E040 (inner of UnkTlv0021)
    /// C++ Printer: crygame.dll+sub_1011E280
    /// </summary>
    public class TlvDepotRightsList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxDepots = 8;

        /// <summary>
        /// Depot count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int Count => Depots?.Count ?? 0;

        /// <summary>
        /// Depot rights entries.
        /// Field ID: 2
        /// </summary>
        public List<TlvDepotRights> Depots { get; set; }

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
                    case 2: Depots = ReadTlvList<TlvDepotRights>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Depots?.Count ?? 0) > MaxDepots)
                throw new InvalidDataException($"[TlvDepotRightsList] Depots exceeds the maximum of {MaxDepots} elements.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Depots);
        }
    }
}
