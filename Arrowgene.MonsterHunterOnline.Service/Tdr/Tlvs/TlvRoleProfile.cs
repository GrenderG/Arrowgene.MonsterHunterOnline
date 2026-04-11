using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (Character / Role Profile Summary).
    /// C++ Reader: crygame.dll+sub_10119210
    /// C++ Printer: crygame.dll+sub_101195F0
    /// </summary>
    public class TlvRoleProfile : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded String Boundaries ---
        public const int MaxHunterStarLen = 128; // < 0x80
        public const int MaxNameLen = 32;        // < 0x20
        public const int MaxNoteLen = 256;       // < 0x100

        public TlvUserInfo Role { get; set; } = new TlvUserInfo();
        public int Level { get; set; }
        public string HunterStar { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Icon { get; set; }
        public string Note { get; set; } = string.Empty;
        public int Gold { get; set; }
        public int BindGold { get; set; }
        public int HRLevel { get; set; } // Hunter Rank

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
                        // Assuming your base implementation has a standard Deserialize method:
                        Role = ReadTlvObject<TlvUserInfo>(reader);
                        break;
                    case 2:
                        Level = reader.ReadInt();
                        break;
                    case 3:
                        HunterStar = reader.ReadUTF();
                        break;
                    case 4:
                        Name = reader.ReadUTF();
                        break;
                    case 5:
                        Icon = reader.ReadInt();
                        break;
                    case 6:
                        Note = reader.ReadUTF();
                        break;
                    case 7:
                        Gold = reader.ReadInt();
                        break;
                    case 8:
                        BindGold = reader.ReadInt();
                        break;
                    case 9:
                        HRLevel = reader.ReadInt();
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
            if (!string.IsNullOrEmpty(HunterStar) && Encoding.UTF8.GetByteCount(HunterStar) >= MaxHunterStarLen)
                throw new InvalidDataException($"[TlvRoleProfile] HunterStar exceeds maximum of {MaxHunterStarLen} bytes.");
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLen)
                throw new InvalidDataException($"[TlvRoleProfile] Name exceeds maximum of {MaxNameLen} bytes.");
            if (!string.IsNullOrEmpty(Note) && Encoding.UTF8.GetByteCount(Note) >= MaxNoteLen)
                throw new InvalidDataException($"[TlvRoleProfile] Note exceeds maximum of {MaxNoteLen} bytes.");

            // --- SERIALIZATION ---

            // Field 1: Nested Object Serialization
            if (Role != null)
            {
                writer.WriteVarUInt((1 << 4) | 5); // Field 1, WireType 5
                long lenPos = writer.ReserveInt();
                long startPos = writer.Position;
                
                WriteTlvObject(writer, 1, Role);
                
                writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
            }

            WriteTlvInt(writer, 2, Level);
            WriteTlvString(writer, 3, HunterStar);
            WriteTlvString(writer, 4, Name);
            WriteTlvInt(writer, 5, Icon);
            WriteTlvString(writer, 6, Note);
            WriteTlvInt(writer, 7, Gold);
            WriteTlvInt(writer, 8, BindGold);
            WriteTlvInt(writer, 9, HRLevel);
        }
    }
}