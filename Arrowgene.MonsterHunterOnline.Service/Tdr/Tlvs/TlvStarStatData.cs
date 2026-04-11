using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for star points with stat list.
    /// C++ Reader: crygame.dll+sub_10215210 (UnkTlv0233)
    /// C++ Printer: crygame.dll+sub_10215C50
    /// </summary>
    public class TlvStarStatData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxStars = 20;
        public const int MaxStats = 10;

        /// <summary>
        /// Star count (derived from array).
        /// Field ID: 1
        /// </summary>
        public byte StarNum => (byte)(StarList?.Length ?? 0);

        /// <summary>
        /// Star list (byte array).
        /// Field ID: 2
        /// </summary>
        public byte[] StarList { get; set; }

        /// <summary>
        /// Stat count (derived from list).
        /// Field ID: 3
        /// </summary>
        public byte StatNum => (byte)(StatList?.Count ?? 0);

        /// <summary>
        /// Stat list.
        /// Field ID: 4
        /// </summary>
        public List<TlvStatTypeValue> StatList { get; set; }

        /// <summary>
        /// Star points.
        /// Field ID: 5
        /// </summary>
        public uint StarPoints { get; set; }

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
                    case 2:
                        int len2 = reader.ReadInt();
                        if (len2 > 0 && len2 <= MaxStars)
                            StarList = reader.ReadBytes(len2);
                        break;
                    case 3: reader.ReadByte(); break; // statNum, derived
                    case 4: StatList = ReadTlvList<TlvStatTypeValue>(reader); break;
                    case 5: StarPoints = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((StarList?.Length ?? 0) > MaxStars)
                throw new InvalidDataException($"[TlvStarStatData] StarList exceeds the maximum of {MaxStars} elements.");
            if ((StatList?.Count ?? 0) > MaxStats)
                throw new InvalidDataException($"[TlvStarStatData] StatList exceeds the maximum of {MaxStats} elements.");

            WriteTlvByte(writer, 1, StarNum);
            WriteTlvByteArray(writer, 2, StarList, StarNum);
            WriteTlvByte(writer, 3, StatNum);
            WriteTlvList(writer, 4, StatList);
            WriteTlvInt(writer, 5, (int)StarPoints);
        }
    }
}
