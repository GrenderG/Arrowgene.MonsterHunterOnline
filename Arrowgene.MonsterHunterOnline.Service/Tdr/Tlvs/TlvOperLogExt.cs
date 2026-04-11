using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for extended operation log with long arguments.
    /// C++ Reader: crygame.dll+sub_10234010 (UnkTlv0276)
    /// C++ Printer: crygame.dll+sub_10234380
    /// </summary>
    public class TlvOperLogExt : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Operation type.
        /// Field ID: 1
        /// </summary>
        public int Oper { get; set; }

        /// <summary>
        /// Argument 1.
        /// Field ID: 2
        /// </summary>
        public int Arg1 { get; set; }

        /// <summary>
        /// Argument 2.
        /// Field ID: 3
        /// </summary>
        public int Arg2 { get; set; }

        /// <summary>
        /// Argument 5 (64-bit).
        /// Field ID: 6
        /// </summary>
        public long Arg5 { get; set; }

        /// <summary>
        /// Argument 6 (64-bit).
        /// Field ID: 7
        /// </summary>
        public long Arg6 { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Oper = reader.ReadInt(); break;
                    case 2: Arg1 = reader.ReadInt(); break;
                    case 3: Arg2 = reader.ReadInt(); break;
                    case 6: Arg5 = reader.ReadLong(); break;
                    case 7: Arg6 = reader.ReadLong(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Oper);
            WriteTlvInt(writer, 2, Arg1);
            WriteTlvInt(writer, 3, Arg2);
            WriteTlvLong(writer, 6, Arg5);
            WriteTlvLong(writer, 7, Arg6);
        }
    }
}
