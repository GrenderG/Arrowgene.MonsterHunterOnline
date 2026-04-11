using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for operation log entry.
    /// C++ Reader: crygame.dll+sub_10130FB0 (UnkTlv0044)
    /// C++ Printer: crygame.dll+sub_10131250
    /// </summary>
    public class TlvOperationLog : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxNameLength = 32;

        /// <summary>
        /// Operation type.
        /// Field ID: 1
        /// </summary>
        public int OperType { get; set; }

        /// <summary>
        /// Executor name.
        /// Field ID: 2
        /// </summary>
        public string Executor { get; set; } = string.Empty;

        /// <summary>
        /// Being executed name.
        /// Field ID: 3
        /// </summary>
        public string BeExecutored { get; set; } = string.Empty;

        /// <summary>
        /// Arguments value.
        /// Field ID: 4
        /// </summary>
        public int Args { get; set; }

        /// <summary>
        /// Time value.
        /// Field ID: 5
        /// </summary>
        public uint Time { get; set; }

        /// <summary>
        /// Arguments 2 value.
        /// Field ID: 6
        /// </summary>
        public int Args2 { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: OperType = reader.ReadInt(); break;
                    case 2:
                        int execLen = reader.ReadInt();
                        if (execLen > 0 && execLen < MaxNameLength)
                        {
                            byte[] execBytes = reader.ReadBytes(execLen);
                            Executor = Encoding.UTF8.GetString(execBytes).TrimEnd('\0');
                        }
                        break;
                    case 3:
                        int beExecLen = reader.ReadInt();
                        if (beExecLen > 0 && beExecLen < MaxNameLength)
                        {
                            byte[] beExecBytes = reader.ReadBytes(beExecLen);
                            BeExecutored = Encoding.UTF8.GetString(beExecBytes).TrimEnd('\0');
                        }
                        break;
                    case 4: Args = reader.ReadInt(); break;
                    case 5: Time = (uint)reader.ReadInt(); break;
                    case 6: Args2 = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECKS ---
            if (!string.IsNullOrEmpty(Executor) && Encoding.UTF8.GetByteCount(Executor) >= MaxNameLength)
                throw new InvalidDataException($"[TlvOperationLog] Executor exceeds or equals the maximum of {MaxNameLength} bytes.");
            if (!string.IsNullOrEmpty(BeExecutored) && Encoding.UTF8.GetByteCount(BeExecutored) >= MaxNameLength)
                throw new InvalidDataException($"[TlvOperationLog] BeExecutored exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvInt(writer, 1, OperType);
            WriteTlvString(writer, 2, Executor);
            WriteTlvString(writer, 3, BeExecutored);
            WriteTlvInt(writer, 4, Args);
            WriteTlvInt(writer, 5, (int)Time);
            WriteTlvInt(writer, 6, Args2);
        }
    }
}
