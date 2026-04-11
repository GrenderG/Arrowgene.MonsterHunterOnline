using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (User / Character Identity Reference).
    /// C++ Reader: crygame.dll+sub_10118360
    /// C++ Printer: crygame.dll+sub_10118580
    /// </summary>
    public class TlvUserInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxNameLength = 32; // < 0x20

        public string Name { get; set; } = string.Empty;
        public long DbId { get; set; } // Database ID
        public int RtId { get; set; }  // Runtime ID
        public long Uin { get; set; }  // User Identification Number

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
                        int nameLen = reader.ReadInt();
                        Name = Encoding.UTF8.GetString(reader.ReadBytes(nameLen));
                        break;

                    case 2:
                        DbId = reader.ReadLong();
                        break;

                    case 3:
                        RtId = reader.ReadInt();
                        break;

                    case 4:
                        Uin = reader.ReadLong();
                        break;

                    default:
                        SkipTlvField(reader, wireType);
                        break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECKS ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvUserInfo] Name exceeds or equals the strict maximum of {MaxNameLength} bytes.");

            // --- SERIALIZATION ---
            WriteTlvString(writer, 1, Name);
            WriteTlvLong(writer, 2, DbId);
            WriteTlvInt(writer, 3, RtId);
            WriteTlvLong(writer, 4, Uin);
        }
    }
}