using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for chapter progress tracking.
    /// C++ Reader: crygame.dll+sub_1018EF10 (UnkTlv0162)
    /// C++ Printer: crygame.dll+sub_1018F060
    /// </summary>
    public class TlvChapterProgress : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Chapter identifier.
        /// Field ID: 1
        /// </summary>
        public int ChapterId { get; set; }

        /// <summary>
        /// Get reward count.
        /// Field ID: 2
        /// </summary>
        public int GetRewardCount { get; set; }

        /// <summary>
        /// Is not new flag.
        /// Field ID: 3
        /// </summary>
        public byte IsNotNew { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: ChapterId = reader.ReadInt(); break;
                    case 2: GetRewardCount = reader.ReadInt(); break;
                    case 3: IsNotNew = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, ChapterId);
            WriteTlvInt(writer, 2, GetRewardCount);
            WriteTlvByte(writer, 3, IsNotNew);
        }
    }
}
