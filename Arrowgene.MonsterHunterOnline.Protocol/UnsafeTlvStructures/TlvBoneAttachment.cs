using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for bone attachment with position/direction.
    /// C++ Reader: crygame.dll+sub_101FDF20 (UnkTlv0209)
    /// C++ Printer: crygame.dll+sub_101FE530
    /// </summary>
    public class TlvBoneAttachment : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Part) && Encoding.UTF8.GetByteCount(Part) >= MaxPartNameLength)
                throw new InvalidDataException($"[TlvBoneAttachment] Part exceeds or equals the maximum of {MaxPartNameLength} bytes.");

            WriteTlvInt32(buffer, 1, BoneId);
            WriteTlvString(buffer, 2, Part);
            WriteTlvFloat(buffer, 3, PosX);
            WriteTlvFloat(buffer, 4, PosY);
            WriteTlvFloat(buffer, 5, PosZ);
            WriteTlvFloat(buffer, 6, DirX);
            WriteTlvFloat(buffer, 7, DirY);
            WriteTlvFloat(buffer, 8, DirZ);
            WriteTlvInt32(buffer, 9, (int)LogicVehicleId);
        }
    }
}
