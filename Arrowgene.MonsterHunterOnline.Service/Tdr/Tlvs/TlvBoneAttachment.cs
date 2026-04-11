using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for bone attachment with position/direction.
    /// C++ Reader: crygame.dll+sub_101FDF20 (UnkTlv0209)
    /// C++ Printer: crygame.dll+sub_101FE530
    /// </summary>
    public class TlvBoneAttachment : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxPartNameLength = 32;

        /// <summary>
        /// Bone ID.
        /// Field ID: 1
        /// </summary>
        public int BoneId { get; set; }

        /// <summary>
        /// Part name.
        /// Field ID: 2
        /// </summary>
        public string Part { get; set; } = string.Empty;

        /// <summary>
        /// Position X.
        /// Field ID: 3
        /// </summary>
        public float PosX { get; set; }

        /// <summary>
        /// Position Y.
        /// Field ID: 4
        /// </summary>
        public float PosY { get; set; }

        /// <summary>
        /// Position Z.
        /// Field ID: 5
        /// </summary>
        public float PosZ { get; set; }

        /// <summary>
        /// Direction X.
        /// Field ID: 6
        /// </summary>
        public float DirX { get; set; }

        /// <summary>
        /// Direction Y.
        /// Field ID: 7
        /// </summary>
        public float DirY { get; set; }

        /// <summary>
        /// Direction Z.
        /// Field ID: 8
        /// </summary>
        public float DirZ { get; set; }

        /// <summary>
        /// Logic vehicle ID.
        /// Field ID: 9
        /// </summary>
        public uint LogicVehicleId { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: BoneId = reader.ReadInt(); break;
                    case 2:
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxPartNameLength)
                        {
                            byte[] nameBytes = reader.ReadBytes(nameLen);
                            Part = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
                        }
                        break;
                    case 3: PosX = reader.ReadFloat(); break;
                    case 4: PosY = reader.ReadFloat(); break;
                    case 5: PosZ = reader.ReadFloat(); break;
                    case 6: DirX = reader.ReadFloat(); break;
                    case 7: DirY = reader.ReadFloat(); break;
                    case 8: DirZ = reader.ReadFloat(); break;
                    case 9: LogicVehicleId = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Part) && Encoding.UTF8.GetByteCount(Part) >= MaxPartNameLength)
                throw new InvalidDataException($"[TlvBoneAttachment] Part exceeds or equals the maximum of {MaxPartNameLength} bytes.");

            WriteTlvInt(writer, 1, BoneId);
            WriteTlvString(writer, 2, Part);
            WriteTlvFloat(writer, 3, PosX);
            WriteTlvFloat(writer, 4, PosY);
            WriteTlvFloat(writer, 5, PosZ);
            WriteTlvFloat(writer, 6, DirX);
            WriteTlvFloat(writer, 7, DirY);
            WriteTlvFloat(writer, 8, DirZ);
            WriteTlvInt(writer, 9, (int)LogicVehicleId);
        }
    }
}
