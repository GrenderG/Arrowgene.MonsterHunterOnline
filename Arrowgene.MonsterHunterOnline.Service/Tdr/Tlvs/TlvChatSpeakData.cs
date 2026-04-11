using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for chat speak settings data.
    /// C++ Reader: crygame.dll+sub_10187AA0 (UnkTlv0153)
    /// C++ Printer: crygame.dll+sub_10188850
    /// </summary>
    public class TlvChatSpeakData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxAutoSpeak = 40;
        public const int MaxQuickSpeak = 20;
        public const int MaxSelfDef = 10;
        public const int MaxSelfDefContentLen = 24;

        /// <summary>
        /// Auto speak count (derived from array).
        /// Field ID: 1
        /// </summary>
        public int AutoCount => AutoSpeak?.Length ?? 0;

        /// <summary>
        /// Auto speak IDs (int array).
        /// Field ID: 2
        /// </summary>
        public int[] AutoSpeak { get; set; }

        /// <summary>
        /// Quick speak count (derived from arrays).
        /// Field ID: 3
        /// </summary>
        public int QuickCount => QuickSpeakIndex?.Length ?? 0;

        /// <summary>
        /// Quick speak indices (byte array).
        /// Field ID: 4
        /// </summary>
        public byte[] QuickSpeakIndex { get; set; }

        /// <summary>
        /// Quick speak IDs (int array).
        /// Field ID: 5
        /// </summary>
        public int[] QuickSpeakId { get; set; }

        /// <summary>
        /// Quick speak types (byte array).
        /// Field ID: 6
        /// </summary>
        public byte[] QuickSpeakType { get; set; }

        /// <summary>
        /// Self-defined speak count (derived from arrays).
        /// Field ID: 7
        /// </summary>
        public int SelfDefCount => SelfDefIndex?.Length ?? 0;

        /// <summary>
        /// Self-defined speak indices (byte array).
        /// Field ID: 8
        /// </summary>
        public byte[] SelfDefIndex { get; set; }

        /// <summary>
        /// Self-defined speak content (string array).
        /// Field ID: 9
        /// </summary>
        public string[] SelfDefContent { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // autoCount, derived
                    case 2: AutoSpeak = ReadTlvIntArray(reader); break;
                    case 3: reader.ReadInt(); break; // quickCount, derived
                    case 4:
                        int len4 = reader.ReadInt();
                        if (len4 > 0 && len4 <= MaxQuickSpeak)
                            QuickSpeakIndex = reader.ReadBytes(len4);
                        break;
                    case 5: QuickSpeakId = ReadTlvIntArray(reader); break;
                    case 6:
                        int len6 = reader.ReadInt();
                        if (len6 > 0 && len6 <= MaxQuickSpeak)
                            QuickSpeakType = reader.ReadBytes(len6);
                        break;
                    case 7: reader.ReadInt(); break; // selfDefCount, derived
                    case 8:
                        int len8 = reader.ReadInt();
                        if (len8 > 0 && len8 <= MaxSelfDef)
                            SelfDefIndex = reader.ReadBytes(len8);
                        break;
                    case 9:
                        int strListLen = reader.ReadInt();
                        if (strListLen > 0)
                        {
                            long endPos = reader.Position + strListLen;
                            var strings = new System.Collections.Generic.List<string>();
                            while (reader.Position < endPos && strings.Count < MaxSelfDef)
                            {
                                int strLen = reader.ReadInt();
                                if (strLen > 0 && strLen <= MaxSelfDefContentLen)
                                {
                                    byte[] strBytes = reader.ReadBytes(strLen);
                                    strings.Add(Encoding.UTF8.GetString(strBytes));
                                }
                                else if (strLen == 0)
                                {
                                    strings.Add(string.Empty);
                                }
                            }
                            SelfDefContent = strings.ToArray();
                        }
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((AutoSpeak?.Length ?? 0) > MaxAutoSpeak)
                throw new InvalidDataException($"[TlvChatSpeakData] AutoSpeak exceeds the maximum of {MaxAutoSpeak} elements.");
            if ((QuickSpeakIndex?.Length ?? 0) > MaxQuickSpeak)
                throw new InvalidDataException($"[TlvChatSpeakData] QuickSpeakIndex exceeds the maximum of {MaxQuickSpeak} elements.");
            if ((SelfDefContent?.Length ?? 0) > MaxSelfDef)
                throw new InvalidDataException($"[TlvChatSpeakData] SelfDefContent exceeds the maximum of {MaxSelfDef} elements.");

            WriteTlvInt(writer, 1, AutoCount);
            WriteTlvIntArray(writer, 2, AutoSpeak, AutoCount);
            WriteTlvInt(writer, 3, QuickCount);
            WriteTlvByteArray(writer, 4, QuickSpeakIndex, QuickCount);
            WriteTlvIntArray(writer, 5, QuickSpeakId, QuickCount);
            WriteTlvByteArray(writer, 6, QuickSpeakType, QuickCount);
            WriteTlvInt(writer, 7, SelfDefCount);
            WriteTlvByteArray(writer, 8, SelfDefIndex, SelfDefCount);

            if (SelfDefContent != null && SelfDefContent.Length > 0)
            {
                writer.WriteVarUInt((9u << 4) | 5);
                long lenPos = writer.ReserveInt();
                long startPos = writer.Position;
                foreach (var s in SelfDefContent)
                {
                    byte[] strBytes = Encoding.UTF8.GetBytes(s ?? string.Empty);
                    writer.WriteInt(strBytes.Length);
                    writer.WriteBytes(strBytes);
                }
                writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
            }
        }
    }
}
