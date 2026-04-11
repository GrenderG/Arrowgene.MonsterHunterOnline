using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for type lock information.
    /// C++ Reader: crygame.dll+sub_101A1A60 (UnkTlv0181)
    /// C++ Printer: crygame.dll+sub_101A1B20
    /// </summary>
    public class TlvTypeLockInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Type identifier.
        /// Field ID: 1
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Lock information.
        /// Field ID: 2
        /// </summary>
        public int LockInfo { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Type = reader.ReadInt(); break;
                    case 2: LockInfo = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Type);
            WriteTlvInt(writer, 2, LockInfo);
        }
    }
}
