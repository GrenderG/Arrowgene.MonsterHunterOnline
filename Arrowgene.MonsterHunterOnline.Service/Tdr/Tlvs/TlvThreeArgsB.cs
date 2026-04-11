using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for three int arguments (variant B).
    /// C++ Reader: crygame.dll+sub_1017C260 (UnkTlv0138)
    /// C++ Printer: crygame.dll+sub_1017C5D0
    /// </summary>
    public class TlvThreeArgsB : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Argument 1.
        /// Field ID: 1
        /// </summary>
        public int Arg1 { get; set; }

        /// <summary>
        /// Argument 2.
        /// Field ID: 2
        /// </summary>
        public int Arg2 { get; set; }

        /// <summary>
        /// Argument 3.
        /// Field ID: 3
        /// </summary>
        public int Arg3 { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Arg1 = reader.ReadInt(); break;
                    case 2: Arg2 = reader.ReadInt(); break;
                    case 3: Arg3 = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Arg1);
            WriteTlvInt(writer, 2, Arg2);
            WriteTlvInt(writer, 3, Arg3);
        }
    }
}
