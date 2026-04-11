using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for cat cuisine state.
    /// C++ Reader: crygame.dll+sub_10171710 (UnkTlv0122)
    /// C++ Printer: crygame.dll+sub_101717D0
    /// </summary>
    public class TlvCatCuisineState : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Cat cuisine ID.
        /// Field ID: 1
        /// </summary>
        public int CatCuisineId { get; set; }

        /// <summary>
        /// Cuisine state.
        /// Field ID: 2
        /// </summary>
        public uint State { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: CatCuisineId = reader.ReadInt(); break;
                    case 2: State = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, CatCuisineId);
            WriteTlvInt(writer, 2, (int)State);
        }
    }
}
