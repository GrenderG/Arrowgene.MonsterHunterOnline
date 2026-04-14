using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for wild hunt soul data with long/int/byte arrays.
    /// C++ Reader: crygame.dll+sub_102482A0 (UnkTlv0294)
    /// C++ Printer: crygame.dll+sub_10248E70
    /// </summary>
    public class TlvWildHuntSoulData : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((InstUid?.Length ?? 0) > MaxArrayElements)
                throw new InvalidDataException($"[TlvWildHuntSoulData] InstUid exceeds the maximum of {MaxArrayElements} elements.");
            if ((InstGuild?.Length ?? 0) > MaxArrayElements)
                throw new InvalidDataException($"[TlvWildHuntSoulData] InstGuild exceeds the maximum of {MaxArrayElements} elements.");
            if ((InstCamp?.Length ?? 0) > MaxArrayElements)
                throw new InvalidDataException($"[TlvWildHuntSoulData] InstCamp exceeds the maximum of {MaxArrayElements} elements.");

            WriteTlvInt64(buffer, 1, (long)RedSoul);
            WriteTlvInt64(buffer, 2, (long)YellowSoul);
            WriteTlvInt64(buffer, 3, (long)RedSoulAll);
            WriteTlvInt64(buffer, 4, (long)YellowSoulAll);
            WriteTlvInt32(buffer, 5, Phase);
            WriteTlvInt32(buffer, 6, Activity);
            WriteTlvInt32(buffer, 7, InstCount);
            WriteTlvInt64Arr(buffer, 8, InstUid);
            WriteTlvInt64Arr(buffer, 9, InstGuild);
            WriteTlvByteArr(buffer, 10, InstCamp);
            WriteTlvInt32(buffer, 11, RedCount);
            WriteTlvInt32(buffer, 12, YellowCount);
            WriteTlvInt64(buffer, 13, (long)ApplyYellowSoulAll);
            WriteTlvInt64(buffer, 14, (long)ApplyRedSoulAll);
            WriteTlvInt32(buffer, 15, ActiveRedCount);
            WriteTlvInt32(buffer, 16, ActiveYellowCount);
        }
    }
}
