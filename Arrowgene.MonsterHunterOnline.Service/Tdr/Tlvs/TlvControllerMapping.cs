using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for controller button mapping.
    /// C++ Reader: crygame.dll+sub_10169110 (UnkTlv0109)
    /// C++ Printer: crygame.dll+sub_10169A60
    /// </summary>
    public class TlvControllerMapping : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Field ID: 1</summary>
        public byte ButtonUp { get; set; }

        /// <summary>Field ID: 2</summary>
        public byte ButtonLeft { get; set; }

        /// <summary>Field ID: 3</summary>
        public byte ButtonDown { get; set; }

        /// <summary>Field ID: 4</summary>
        public byte ButtonRight { get; set; }

        /// <summary>Field ID: 5</summary>
        public byte StickLeftLeft { get; set; }

        /// <summary>Field ID: 6</summary>
        public byte StickLeftRight { get; set; }

        /// <summary>Field ID: 7</summary>
        public byte StickLeftUp { get; set; }

        /// <summary>Field ID: 8</summary>
        public byte StickLeftDown { get; set; }

        /// <summary>Field ID: 9</summary>
        public byte StickRightLeft { get; set; }

        /// <summary>Field ID: 10</summary>
        public byte StickRightRight { get; set; }

        /// <summary>Field ID: 11</summary>
        public byte StickRightUp { get; set; }

        /// <summary>Field ID: 12</summary>
        public byte StickRightDown { get; set; }

        /// <summary>Field ID: 13</summary>
        public byte ButtonL2 { get; set; }

        /// <summary>Field ID: 14</summary>
        public byte ButtonR2 { get; set; }

        /// <summary>Field ID: 15</summary>
        public byte ButtonL1 { get; set; }

        /// <summary>Field ID: 16</summary>
        public byte ButtonR1 { get; set; }

        /// <summary>Field ID: 17</summary>
        public byte ButtonTriangle { get; set; }

        /// <summary>Field ID: 18</summary>
        public byte ButtonCircle { get; set; }

        /// <summary>Field ID: 19</summary>
        public byte ButtonCross { get; set; }

        /// <summary>Field ID: 20</summary>
        public byte ButtonSquare { get; set; }

        /// <summary>Field ID: 21</summary>
        public byte ButtonSelect { get; set; }

        /// <summary>Field ID: 22</summary>
        public byte ButtonStart { get; set; }

        /// <summary>Field ID: 23</summary>
        public byte ButtonL3 { get; set; }

        /// <summary>Field ID: 24</summary>
        public byte ButtonR3 { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: ButtonUp = reader.ReadByte(); break;
                    case 2: ButtonLeft = reader.ReadByte(); break;
                    case 3: ButtonDown = reader.ReadByte(); break;
                    case 4: ButtonRight = reader.ReadByte(); break;
                    case 5: StickLeftLeft = reader.ReadByte(); break;
                    case 6: StickLeftRight = reader.ReadByte(); break;
                    case 7: StickLeftUp = reader.ReadByte(); break;
                    case 8: StickLeftDown = reader.ReadByte(); break;
                    case 9: StickRightLeft = reader.ReadByte(); break;
                    case 10: StickRightRight = reader.ReadByte(); break;
                    case 11: StickRightUp = reader.ReadByte(); break;
                    case 12: StickRightDown = reader.ReadByte(); break;
                    case 13: ButtonL2 = reader.ReadByte(); break;
                    case 14: ButtonR2 = reader.ReadByte(); break;
                    case 15: ButtonL1 = reader.ReadByte(); break;
                    case 16: ButtonR1 = reader.ReadByte(); break;
                    case 17: ButtonTriangle = reader.ReadByte(); break;
                    case 18: ButtonCircle = reader.ReadByte(); break;
                    case 19: ButtonCross = reader.ReadByte(); break;
                    case 20: ButtonSquare = reader.ReadByte(); break;
                    case 21: ButtonSelect = reader.ReadByte(); break;
                    case 22: ButtonStart = reader.ReadByte(); break;
                    case 23: ButtonL3 = reader.ReadByte(); break;
                    case 24: ButtonR3 = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, ButtonUp);
            WriteTlvByte(writer, 2, ButtonLeft);
            WriteTlvByte(writer, 3, ButtonDown);
            WriteTlvByte(writer, 4, ButtonRight);
            WriteTlvByte(writer, 5, StickLeftLeft);
            WriteTlvByte(writer, 6, StickLeftRight);
            WriteTlvByte(writer, 7, StickLeftUp);
            WriteTlvByte(writer, 8, StickLeftDown);
            WriteTlvByte(writer, 9, StickRightLeft);
            WriteTlvByte(writer, 10, StickRightRight);
            WriteTlvByte(writer, 11, StickRightUp);
            WriteTlvByte(writer, 12, StickRightDown);
            WriteTlvByte(writer, 13, ButtonL2);
            WriteTlvByte(writer, 14, ButtonR2);
            WriteTlvByte(writer, 15, ButtonL1);
            WriteTlvByte(writer, 16, ButtonR1);
            WriteTlvByte(writer, 17, ButtonTriangle);
            WriteTlvByte(writer, 18, ButtonCircle);
            WriteTlvByte(writer, 19, ButtonCross);
            WriteTlvByte(writer, 20, ButtonSquare);
            WriteTlvByte(writer, 21, ButtonSelect);
            WriteTlvByte(writer, 22, ButtonStart);
            WriteTlvByte(writer, 23, ButtonL3);
            WriteTlvByte(writer, 24, ButtonR3);
        }
    }
}
