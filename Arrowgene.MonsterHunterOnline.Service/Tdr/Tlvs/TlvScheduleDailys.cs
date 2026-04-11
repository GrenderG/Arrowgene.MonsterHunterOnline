using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for schedule + dailys container.
    /// C++ Reader: crygame.dll+sub_10228680 (UnkTlv0261)
    /// C++ Printer: crygame.dll+sub_10228A90
    /// </summary>
    public class TlvScheduleDailys : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Schedule data. Field ID: 1</summary>
        public TlvRefreshTimeGroup Schedule { get; set; } = new();

        /// <summary>Dailys data. Field ID: 2</summary>
        public TlvDailys Dailys { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Schedule = ReadTlvObject<TlvRefreshTimeGroup>(reader); break;
                    case 2: Dailys = ReadTlvObject<TlvDailys>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvObject(writer, 1, Schedule);
            WriteTlvObject(writer, 2, Dailys);
        }
    }
}
