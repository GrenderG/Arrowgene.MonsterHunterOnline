using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for gathering info.
    /// C++ Reader: crygame.dll+sub_101FF3E0 (UnkTlv0210)
    /// C++ Printer: crygame.dll+sub_101FF650
    /// </summary>
    public class TlvGatherInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Current experience.
        /// Field ID: 1
        /// </summary>
        public int CurExp { get; set; }

        /// <summary>
        /// Level (short).
        /// Field ID: 2
        /// </summary>
        public short Level { get; set; }

        /// <summary>
        /// Gather count.
        /// Field ID: 3
        /// </summary>
        public short GatherCount { get; set; }

        /// <summary>
        /// Last refresh time.
        /// Field ID: 4
        /// </summary>
        public int LastRefreshTime { get; set; }

        /// <summary>
        /// Pet ID.
        /// Field ID: 5
        /// </summary>
        public int PetId { get; set; }

        /// <summary>
        /// Gather level.
        /// Field ID: 6
        /// </summary>
        public short GatherLevel { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: CurExp = reader.ReadInt(); break;
                    case 2: Level = reader.ReadShort(); break;
                    case 3: GatherCount = reader.ReadShort(); break;
                    case 4: LastRefreshTime = reader.ReadInt(); break;
                    case 5: PetId = reader.ReadInt(); break;
                    case 6: GatherLevel = reader.ReadShort(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, CurExp);
            WriteTlvShort(writer, 2, Level);
            WriteTlvShort(writer, 3, GatherCount);
            WriteTlvInt(writer, 4, LastRefreshTime);
            WriteTlvInt(writer, 5, PetId);
            WriteTlvShort(writer, 6, GatherLevel);
        }
    }
}
