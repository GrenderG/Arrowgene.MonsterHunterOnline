using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for activity data.
    /// C++ Reader: crygame.dll+sub_10155590 (UnkTlv0086)
    /// C++ Printer: crygame.dll+sub_10155D40
    /// </summary>
    public class TlvActivityData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Activity ID.
        /// Field ID: 1
        /// </summary>
        public int ActivityID { get; set; }

        /// <summary>
        /// Last update time.
        /// Field ID: 2
        /// </summary>
        public int LastUpdate { get; set; }

        /// <summary>
        /// Variable data.
        /// Field ID: 5
        /// </summary>
        public TlvVarData Vars { get; set; } = new();

        /// <summary>
        /// Counter data.
        /// Field ID: 6
        /// </summary>
        public TlvCounterDataList Counters { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: ActivityID = reader.ReadInt(); break;
                    case 2: LastUpdate = reader.ReadInt(); break;
                    case 5: Vars = ReadTlvObject<TlvVarData>(reader); break;
                    case 6: Counters = ReadTlvObject<TlvCounterDataList>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, ActivityID);
            WriteTlvInt(writer, 2, LastUpdate);
            WriteTlvObject(writer, 5, Vars);
            WriteTlvObject(writer, 6, Counters);
        }
    }
}
