using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for refresh time with group.
    /// C++ Reader: crygame.dll+sub_10226B60 (UnkTlv0258)
    /// C++ Printer: crygame.dll+sub_10226D90
    /// </summary>
    public class TlvRefreshTimeGroup : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Refresh time.
        /// Field ID: 1
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Group.
        /// Field ID: 2
        /// </summary>
        public int Group { get; set; }

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
                    case 2: Group = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, (int)RefreshTime);
            WriteTlvInt(writer, 2, Group);
        }
    }
}
