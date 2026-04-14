using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for level position and transform.
    /// C++ Reader: crygame.dll+sub_10165750 (UnkTlv0103)
    /// C++ Printer: crygame.dll+sub_10165A70
    /// </summary>
    public class TlvLevelPosition : Structure, ITlvStructure
    {
        /// <summary>
        /// Current level ID.
        /// Field ID: 1
        /// </summary>
        public uint CurLevelId { get; set; }

        /// <summary>
        /// Position X.
        /// Field ID: 2
        /// </summary>
        public float Vx { get; set; }

        /// <summary>
        /// Position Y.
        /// Field ID: 3
        /// </summary>
        public float Vy { get; set; }

        /// <summary>
        /// Position Z.
        /// Field ID: 4
        /// </summary>
        public float Vz { get; set; }

        /// <summary>
        /// Target X.
        /// Field ID: 5
        /// </summary>
        public float Tx { get; set; }

        /// <summary>
        /// Target Y.
        /// Field ID: 6
        /// </summary>
        public float Ty { get; set; }

        /// <summary>
        /// Target Z.
        /// Field ID: 7
        /// </summary>
        public float Tz { get; set; }

        /// <summary>
        /// Target W (rotation).
        /// Field ID: 8
        /// </summary>
        public float Tw { get; set; }

        /// <summary>
        /// Hub ID.
        /// Field ID: 9
        /// </summary>
        public uint HubId { get; set; }

        /// <summary>
        /// NPC ID.
        /// Field ID: 10
        /// </summary>
        public uint NpcId { get; set; }

        /// <summary>
        /// Previous level ID.
        /// Field ID: 11
        /// </summary>
        public uint PreLevelId { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)CurLevelId);
            WriteTlvFloat(buffer, 2, Vx);
            WriteTlvFloat(buffer, 3, Vy);
            WriteTlvFloat(buffer, 4, Vz);
            WriteTlvFloat(buffer, 5, Tx);
            WriteTlvFloat(buffer, 6, Ty);
            WriteTlvFloat(buffer, 7, Tz);
            WriteTlvFloat(buffer, 8, Tw);
            WriteTlvInt32(buffer, 9, (int)HubId);
            WriteTlvInt32(buffer, 10, (int)NpcId);
            WriteTlvInt32(buffer, 11, (int)PreLevelId);
        }
    }
}
