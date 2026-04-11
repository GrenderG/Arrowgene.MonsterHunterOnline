using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for ID state with last update.
    /// C++ Reader: crygame.dll+sub_102443F0 (UnkTlv0288)
    /// C++ Printer: crygame.dll+sub_102446E0
    /// </summary>
    public class TlvIdStateUpdate : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// ID.
        /// Field ID: 1
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// State.
        /// Field ID: 2
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// Last update time.
        /// Field ID: 3
        /// </summary>
        public uint LastUpdate { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = (uint)reader.ReadInt(); break;
                    case 2: State = reader.ReadByte(); break;
                    case 3: LastUpdate = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, (int)Id);
            WriteTlvByte(writer, 2, State);
            WriteTlvInt(writer, 3, (int)LastUpdate);
        }
    }
}
