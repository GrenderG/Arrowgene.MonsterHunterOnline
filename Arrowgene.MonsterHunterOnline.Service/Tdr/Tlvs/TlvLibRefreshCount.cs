using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for library refresh count.
    /// C++ Reader: crygame.dll+sub_102243B0 (UnkTlv0256)
    /// C++ Printer: crygame.dll+sub_102246A0
    /// </summary>
    public class TlvLibRefreshCount : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Refresh time.
        /// Field ID: 1
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Library ID.
        /// Field ID: 2
        /// </summary>
        public int Lib { get; set; }

        /// <summary>
        /// Complete count.
        /// Field ID: 3
        /// </summary>
        public int CompleteCount { get; set; }

        /// <summary>
        /// Remain count.
        /// Field ID: 4
        /// </summary>
        public int RemainCount { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: RefreshTime = (uint)reader.ReadInt(); break;
                    case 2: Lib = reader.ReadInt(); break;
                    case 3: CompleteCount = reader.ReadInt(); break;
                    case 4: RemainCount = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, (int)RefreshTime);
            WriteTlvInt(writer, 2, Lib);
            WriteTlvInt(writer, 3, CompleteCount);
            WriteTlvInt(writer, 4, RemainCount);
        }
    }
}
