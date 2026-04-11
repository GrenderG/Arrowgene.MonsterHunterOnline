using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for round score with name.
    /// C++ Reader: crygame.dll+sub_101390F0 (UnkTlv0049)
    /// C++ Printer: crygame.dll+sub_10139290
    /// </summary>
    public class TlvRoundScore : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;

        /// <summary>
        /// Round number.
        /// Field ID: 1
        /// </summary>
        public int Round { get; set; }

        /// <summary>
        /// Score value.
        /// Field ID: 2
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Player name.
        /// Field ID: 3
        /// </summary>
        public string Name { get; set; } = string.Empty;

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Round = reader.ReadInt(); break;
                    case 2: Score = reader.ReadInt(); break;
                    case 3:
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxNameLength)
                        {
                            byte[] nameBytes = reader.ReadBytes(nameLen);
                            Name = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
                        }
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvRoundScore] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvInt(writer, 1, Round);
            WriteTlvInt(writer, 2, Score);
            WriteTlvString(writer, 3, Name);
        }
    }
}
