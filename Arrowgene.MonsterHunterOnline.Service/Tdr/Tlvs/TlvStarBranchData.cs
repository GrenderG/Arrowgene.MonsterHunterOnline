using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for star and branch data.
    /// C++ Reader: crygame.dll+sub_102175D0 (UnkTlv0236)
    /// C++ Printer: crygame.dll+sub_10217B40
    /// </summary>
    public class TlvStarBranchData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxBranches = 10;
        public const int MaxStars = 20;

        /// <summary>
        /// Star count (derived from list).
        /// Field ID: 1
        /// </summary>
        public byte StarNum => (byte)(StarList?.Count ?? 0);

        /// <summary>
        /// Branch count (derived from list).
        /// Field ID: 3
        /// </summary>
        public byte BranchNum => (byte)(BranchList?.Count ?? 0);

        /// <summary>
        /// Branch list.
        /// Field ID: 4
        /// </summary>
        public List<TlvBranchStatsB> BranchList { get; set; }

        /// <summary>
        /// Star list.
        /// Field ID: 5
        /// </summary>
        public List<TlvQualityFinishTimeVar> StarList { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadByte(); break; // starNum, derived
                    case 3: reader.ReadByte(); break; // branchNum, derived
                    case 4: BranchList = ReadTlvList<TlvBranchStatsB>(reader); break;
                    case 5: StarList = ReadTlvList<TlvQualityFinishTimeVar>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((BranchList?.Count ?? 0) > MaxBranches)
                throw new InvalidDataException($"[TlvStarBranchData] BranchList exceeds the maximum of {MaxBranches} elements.");
            if ((StarList?.Count ?? 0) > MaxStars)
                throw new InvalidDataException($"[TlvStarBranchData] StarList exceeds the maximum of {MaxStars} elements.");

            WriteTlvByte(writer, 1, StarNum);
            WriteTlvByte(writer, 3, BranchNum);
            WriteTlvList(writer, 4, BranchList);
            WriteTlvList(writer, 5, StarList);
        }
    }
}
