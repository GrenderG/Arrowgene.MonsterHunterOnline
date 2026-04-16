using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for controller button mapping.
    /// C++ Reader: crygame.dll+sub_10169110 (UnkTlv0109)
    /// C++ Printer: crygame.dll+sub_10169A60
    /// </summary>
    public class TlvControllerMapping : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, ButtonUp);
            WriteTlvByte(buffer, 2, ButtonLeft);
            WriteTlvByte(buffer, 3, ButtonDown);
            WriteTlvByte(buffer, 4, ButtonRight);
            WriteTlvByte(buffer, 5, StickLeftLeft);
            WriteTlvByte(buffer, 6, StickLeftRight);
            WriteTlvByte(buffer, 7, StickLeftUp);
            WriteTlvByte(buffer, 8, StickLeftDown);
            WriteTlvByte(buffer, 9, StickRightLeft);
            WriteTlvByte(buffer, 10, StickRightRight);
            WriteTlvByte(buffer, 11, StickRightUp);
            WriteTlvByte(buffer, 12, StickRightDown);
            WriteTlvByte(buffer, 13, ButtonL2);
            WriteTlvByte(buffer, 14, ButtonR2);
            WriteTlvByte(buffer, 15, ButtonL1);
            WriteTlvByte(buffer, 16, ButtonR1);
            WriteTlvByte(buffer, 17, ButtonTriangle);
            WriteTlvByte(buffer, 18, ButtonCircle);
            WriteTlvByte(buffer, 19, ButtonCross);
            WriteTlvByte(buffer, 20, ButtonSquare);
            WriteTlvByte(buffer, 21, ButtonSelect);
            WriteTlvByte(buffer, 22, ButtonStart);
            WriteTlvByte(buffer, 23, ButtonL3);
            WriteTlvByte(buffer, 24, ButtonR3);
        }
    }
}
