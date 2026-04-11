using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (Container for Items Used in Battle).
    /// C++ Reader: crygame.dll+sub_10112E50
    /// C++ Printer: crygame.dll+sub_101130B0
    /// </summary>
    public class TlvItemBattleUseList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxItemBattleUse = 256;

        public List<TlvItemBattleUseCount> ItemBattleUse { get; set; } = new List<TlvItemBattleUseCount>();

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
                        // Field 1 is the [itemBattleUseCount]
                        // Read and discard it, relying solely on the parsed list's count.
                        reader.ReadInt();
                        break;

                    case 2:
                        // Field 2 is the actual list of TlvItemUseCount objects (WireType 5)
                        ItemBattleUse = ReadTlvList<TlvItemBattleUseCount>(reader);
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
            if (ItemBattleUse.Count > MaxItemBattleUse)
                throw new InvalidDataException($"[TlvItemBattleUseList] ItemBattleUse count ({ItemBattleUse.Count}) exceeds maximum of {MaxItemBattleUse}.");

            // --- SERIALIZATION ---

            // Re-inject the Count directly as Field 1
            WriteTlvInt(writer, 1, ItemBattleUse.Count);

            // Write the length-delimited list as Field 2
            WriteTlvList(writer, 2, ItemBattleUse);
        }
    }
}