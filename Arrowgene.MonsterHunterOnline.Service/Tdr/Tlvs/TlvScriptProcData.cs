using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for script/proc/online time data.
    /// C++ Reader: crygame.dll+sub_10158750 (UnkTlv0090)
    /// C++ Printer: crygame.dll+sub_10158990
    /// </summary>
    public class TlvScriptProcData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Fetch procs data. Field ID: 1</summary>
        public TlvProcs FetchProcs { get; set; } = new();

        /// <summary>Script vars data. Field ID: 2</summary>
        public TlvActivityDataList ScriptVars { get; set; } = new();

        /// <summary>Online time data. Field ID: 3</summary>
        public TlvAlarmTimeData OnlineTime { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: FetchProcs = ReadTlvObject<TlvProcs>(reader); break;
                    case 2: ScriptVars = ReadTlvObject<TlvActivityDataList>(reader); break;
                    case 3: OnlineTime = ReadTlvObject<TlvAlarmTimeData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvObject(writer, 1, FetchProcs);
            WriteTlvObject(writer, 2, ScriptVars);
            WriteTlvObject(writer, 3, OnlineTime);
        }
    }
}
