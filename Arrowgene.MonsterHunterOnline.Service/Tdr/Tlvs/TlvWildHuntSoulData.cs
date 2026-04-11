using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for wild hunt soul data with long/int/byte arrays.
    /// C++ Reader: crygame.dll+sub_102482A0 (UnkTlv0294)
    /// C++ Printer: crygame.dll+sub_10248E70
    /// </summary>
    public class TlvWildHuntSoulData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxArrayElements = 20000;

        /// <summary>
        /// Red soul.
        /// Field ID: 1
        /// </summary>
        public ulong RedSoul { get; set; }

        /// <summary>
        /// Yellow soul.
        /// Field ID: 2
        /// </summary>
        public ulong YellowSoul { get; set; }

        /// <summary>
        /// Red soul all.
        /// Field ID: 3
        /// </summary>
        public ulong RedSoulAll { get; set; }

        /// <summary>
        /// Yellow soul all.
        /// Field ID: 4
        /// </summary>
        public ulong YellowSoulAll { get; set; }

        /// <summary>
        /// Phase.
        /// Field ID: 5
        /// </summary>
        public int Phase { get; set; }

        /// <summary>
        /// Activity.
        /// Field ID: 6
        /// </summary>
        public int Activity { get; set; }

        /// <summary>
        /// Instance count.
        /// Field ID: 7
        /// </summary>
        public int InstCount { get; set; }

        /// <summary>
        /// Instance UIDs (long array).
        /// Field ID: 8
        /// </summary>
        public long[] InstUid { get; set; }

        /// <summary>
        /// Instance guild IDs (long array).
        /// Field ID: 9
        /// </summary>
        public long[] InstGuild { get; set; }

        /// <summary>
        /// Instance camp data (byte array).
        /// Field ID: 10
        /// </summary>
        public byte[] InstCamp { get; set; }

        /// <summary>
        /// Red count.
        /// Field ID: 11
        /// </summary>
        public int RedCount { get; set; }

        /// <summary>
        /// Yellow count.
        /// Field ID: 12
        /// </summary>
        public int YellowCount { get; set; }

        /// <summary>
        /// Applied yellow soul all.
        /// Field ID: 13
        /// </summary>
        public ulong ApplyYellowSoulAll { get; set; }

        /// <summary>
        /// Applied red soul all.
        /// Field ID: 14
        /// </summary>
        public ulong ApplyRedSoulAll { get; set; }

        /// <summary>
        /// Active red count.
        /// Field ID: 15
        /// </summary>
        public int ActiveRedCount { get; set; }

        /// <summary>
        /// Active yellow count.
        /// Field ID: 16
        /// </summary>
        public int ActiveYellowCount { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: RedSoul = (ulong)reader.ReadLong(); break;
                    case 2: YellowSoul = (ulong)reader.ReadLong(); break;
                    case 3: RedSoulAll = (ulong)reader.ReadLong(); break;
                    case 4: YellowSoulAll = (ulong)reader.ReadLong(); break;
                    case 5: Phase = reader.ReadInt(); break;
                    case 6: Activity = reader.ReadInt(); break;
                    case 7: InstCount = reader.ReadInt(); break;
                    case 8: InstUid = ReadTlvLongArray(reader); break;
                    case 9: InstGuild = ReadTlvLongArray(reader); break;
                    case 10:
                        int byteLen = reader.ReadInt();
                        if (byteLen > 0 && byteLen <= MaxArrayElements)
                            InstCamp = reader.ReadBytes(byteLen);
                        break;
                    case 11: RedCount = reader.ReadInt(); break;
                    case 12: YellowCount = reader.ReadInt(); break;
                    case 13: ApplyYellowSoulAll = (ulong)reader.ReadLong(); break;
                    case 14: ApplyRedSoulAll = (ulong)reader.ReadLong(); break;
                    case 15: ActiveRedCount = reader.ReadInt(); break;
                    case 16: ActiveYellowCount = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((InstUid?.Length ?? 0) > MaxArrayElements)
                throw new InvalidDataException($"[TlvWildHuntSoulData] InstUid exceeds the maximum of {MaxArrayElements} elements.");
            if ((InstGuild?.Length ?? 0) > MaxArrayElements)
                throw new InvalidDataException($"[TlvWildHuntSoulData] InstGuild exceeds the maximum of {MaxArrayElements} elements.");
            if ((InstCamp?.Length ?? 0) > MaxArrayElements)
                throw new InvalidDataException($"[TlvWildHuntSoulData] InstCamp exceeds the maximum of {MaxArrayElements} elements.");

            WriteTlvLong(writer, 1, (long)RedSoul);
            WriteTlvLong(writer, 2, (long)YellowSoul);
            WriteTlvLong(writer, 3, (long)RedSoulAll);
            WriteTlvLong(writer, 4, (long)YellowSoulAll);
            WriteTlvInt(writer, 5, Phase);
            WriteTlvInt(writer, 6, Activity);
            WriteTlvInt(writer, 7, InstCount);
            WriteTlvLongArray(writer, 8, InstUid);
            WriteTlvLongArray(writer, 9, InstGuild);
            WriteTlvByteArray(writer, 10, InstCamp, InstCamp?.Length ?? 0);
            WriteTlvInt(writer, 11, RedCount);
            WriteTlvInt(writer, 12, YellowCount);
            WriteTlvLong(writer, 13, (long)ApplyYellowSoulAll);
            WriteTlvLong(writer, 14, (long)ApplyRedSoulAll);
            WriteTlvInt(writer, 15, ActiveRedCount);
            WriteTlvInt(writer, 16, ActiveYellowCount);
        }
    }
}
