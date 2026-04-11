using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for a discriminated variant (base or bonus).
    /// Field 1 → single TlvTypedVariant (base).
    /// Field 2 → TlvTypedVariantList (bonus).
    /// C++ Reader: crygame.dll+sub_101AFE20 (UnkTlv0190)
    /// C++ Printer: crygame.dll+sub_101AFFD0
    /// </summary>
    public class TlvBaseOrBonus : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Discriminator (1=base, 2=bonus).
        /// </summary>
        public int Kind { get; set; }

        /// <summary>
        /// Base variant (when Kind == 1).
        /// Field ID: 1
        /// </summary>
        public TlvTypedVariant Base { get; set; } = new();

        /// <summary>
        /// Bonus variant list (when Kind == 2).
        /// Field ID: 2
        /// </summary>
        public TlvTypedVariantList Bonus { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1:
                        Kind = 1;
                        Base = ReadTlvObject<TlvTypedVariant>(reader);
                        break;
                    case 2:
                        Kind = 2;
                        Bonus = ReadTlvObject<TlvTypedVariantList>(reader);
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            switch (Kind)
            {
                case 1: WriteTlvObject(writer, 1, Base); break;
                case 2: WriteTlvObject(writer, 2, Bonus); break;
            }
        }
    }
}
