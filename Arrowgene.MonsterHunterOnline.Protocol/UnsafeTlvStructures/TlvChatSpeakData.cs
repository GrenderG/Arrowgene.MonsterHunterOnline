using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for chat speak settings data.
    /// C++ Reader: crygame.dll+sub_10187AA0 (UnkTlv0153)
    /// C++ Printer: crygame.dll+sub_10188850
    /// </summary>
    public class TlvChatSpeakData : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((AutoSpeak?.Length ?? 0) > MaxAutoSpeak)
                throw new InvalidDataException($"[TlvChatSpeakData] AutoSpeak exceeds the maximum of {MaxAutoSpeak} elements.");
            if ((QuickSpeakIndex?.Length ?? 0) > MaxQuickSpeak)
                throw new InvalidDataException($"[TlvChatSpeakData] QuickSpeakIndex exceeds the maximum of {MaxQuickSpeak} elements.");
            if ((SelfDefContent?.Length ?? 0) > MaxSelfDef)
                throw new InvalidDataException($"[TlvChatSpeakData] SelfDefContent exceeds the maximum of {MaxSelfDef} elements.");

            WriteTlvInt32(buffer, 1, AutoCount);
            WriteTlvInt32Arr(buffer, 2, AutoSpeak);
            WriteTlvInt32(buffer, 3, QuickCount);
            WriteTlvByteArr(buffer, 4, QuickSpeakIndex);
            WriteTlvInt32Arr(buffer, 5, QuickSpeakId);
            WriteTlvByteArr(buffer, 6, QuickSpeakType);
            WriteTlvInt32(buffer, 7, SelfDefCount);
            WriteTlvByteArr(buffer, 8, SelfDefIndex);

            if (SelfDefContent != null && SelfDefContent.Length > 0)
            {
                WriteTlvTag(buffer, 9, TlvType.ID_LENGTH_DELIMITED);
                int lenPos = buffer.Position;
                WriteInt32(buffer, 0);
                int startPos = buffer.Position;
                foreach (var s in SelfDefContent)
                {
                    byte[] strBytes = Encoding.UTF8.GetBytes(s ?? string.Empty);
                    WriteInt32(buffer, strBytes.Length);
                    buffer.WriteBytes(strBytes);
                }
                int endPos = buffer.Position;
                buffer.Position = lenPos;
                WriteInt32(buffer, endPos - startPos);
                buffer.Position = endPos;
            }
        }
    }
}
