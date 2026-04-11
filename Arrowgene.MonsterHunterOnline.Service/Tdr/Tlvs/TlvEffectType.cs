using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for effect type with parameters.
    /// C++ Reader: crygame.dll+sub_101FBB30 (UnkTlv0206)
    /// C++ Printer: crygame.dll+sub_101FBD20
    /// </summary>
    public class TlvEffectType : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Effect type.
        /// Field ID: 1
        /// </summary>
        public int EffectType { get; set; }

        /// <summary>
        /// Parameter 1.
        /// Field ID: 2
        /// </summary>
        public int Param1 { get; set; }

        /// <summary>
        /// Parameter 2.
        /// Field ID: 3
        /// </summary>
        public int Param2 { get; set; }

        /// <summary>
        /// Parameter 3.
        /// Field ID: 4
        /// </summary>
        public int Param3 { get; set; }

        /// <summary>
        /// Parameter 4.
        /// Field ID: 5
        /// </summary>
        public int Param4 { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: EffectType = reader.ReadInt(); break;
                    case 2: Param1 = reader.ReadInt(); break;
                    case 3: Param2 = reader.ReadInt(); break;
                    case 4: Param3 = reader.ReadInt(); break;
                    case 5: Param4 = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, EffectType);
            WriteTlvInt(writer, 2, Param1);
            WriteTlvInt(writer, 3, Param2);
            WriteTlvInt(writer, 4, Param3);
            WriteTlvInt(writer, 5, Param4);
        }
    }
}
