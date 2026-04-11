using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for level position and transform.
    /// C++ Reader: crygame.dll+sub_10165750 (UnkTlv0103)
    /// C++ Printer: crygame.dll+sub_10165A70
    /// </summary>
    public class TlvLevelPosition : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: CurLevelId = (uint)reader.ReadInt(); break;
                    case 2: Vx = reader.ReadFloat(); break;
                    case 3: Vy = reader.ReadFloat(); break;
                    case 4: Vz = reader.ReadFloat(); break;
                    case 5: Tx = reader.ReadFloat(); break;
                    case 6: Ty = reader.ReadFloat(); break;
                    case 7: Tz = reader.ReadFloat(); break;
                    case 8: Tw = reader.ReadFloat(); break;
                    case 9: HubId = (uint)reader.ReadInt(); break;
                    case 10: NpcId = (uint)reader.ReadInt(); break;
                    case 11: PreLevelId = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, (int)CurLevelId);
            WriteTlvFloat(writer, 2, Vx);
            WriteTlvFloat(writer, 3, Vy);
            WriteTlvFloat(writer, 4, Vz);
            WriteTlvFloat(writer, 5, Tx);
            WriteTlvFloat(writer, 6, Ty);
            WriteTlvFloat(writer, 7, Tz);
            WriteTlvFloat(writer, 8, Tw);
            WriteTlvInt(writer, 9, (int)HubId);
            WriteTlvInt(writer, 10, (int)NpcId);
            WriteTlvInt(writer, 11, (int)PreLevelId);
        }
    }
}
