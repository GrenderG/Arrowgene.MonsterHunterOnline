using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for title unlock information.
    /// C++ Reader: crygame.dll+sub_10174840 (UnkTlv0126)
    /// C++ Printer: crygame.dll+sub_10174900
    /// </summary>
    public class TlvTitleUnlock : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Title ID.
        /// Field ID: 1
        /// </summary>
        public int TitleId { get; set; }

        /// <summary>
        /// Unlock time value.
        /// Field ID: 2
        /// </summary>
        public uint UnlockTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: TitleId = reader.ReadInt(); break;
                    case 2: UnlockTime = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, TitleId);
            WriteTlvInt(writer, 2, (int)UnlockTime);
        }
    }
}
