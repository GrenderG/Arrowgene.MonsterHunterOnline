#region License GNU GPL
// BigEndianWriter.cs
// 
// Copyright (C) 2012 - BehaviorIsManaged
// 
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the Free Software Foundation;
// either version 2 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details. 
// You should have received a copy of the GNU General Public License along with this program; 
// if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
#endregion
using System;
using System.IO;
using System.Text;

namespace Rathalos.Core.Utils.IO
{
    public class BufferWriter : IDataWriter, IDisposable
    {
        public const int INT_SIZE = 32;
        public const int SHORT_SIZE = 16;
        public const int SHORT_MIN_VALUE = -0x8000;
        public const int SHORT_MAX_VALUE = 0x7FFF;
        public const int USHORT_MAX_VALUE = 0xFFFF;
        public const int CHUNCK_BIT_SIZE = 7;
        public static readonly int MAX_ENCODING_LENGHT = (int)Math.Ceiling((double)INT_SIZE / CHUNCK_BIT_SIZE);
        public const int MASK_10000000 = 0x80;
        public const int MASK_01111111 = 0x7F;

        #region Properties

        private BinaryWriter _writer;

        public Stream BaseStream
        {
            get { return _writer.BaseStream; }
        }

        /// <summary>
        ///   Gets available bytes number in the buffer
        /// </summary>
        public long BytesAvailable
        {
            get { return _writer.BaseStream.Length - _writer.BaseStream.Position; }
        }

        public long Position
        {
            get { return _writer.BaseStream.Position; }
            set
            {
                _writer.BaseStream.Position = value;
            }
        }

        public byte[] Data
        {
            get
            {
                var pos = _writer.BaseStream.Position;

                var data = new byte[_writer.BaseStream.Length];
                _writer.BaseStream.Position = 0;
                _writer.BaseStream.Read(data, 0, (int)_writer.BaseStream.Length);

                _writer.BaseStream.Position = pos;

                return data;
            }
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferWriter"/> class.
        /// </summary>
        public BufferWriter()
        {
            _writer = new BinaryWriter(new MemoryStream(), Encoding.UTF8);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferWriter"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public BufferWriter(Stream stream)
        {
            _writer = new BinaryWriter(stream, Encoding.UTF8);
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///   Reverse bytes and write them into the buffer
        /// </summary>
        private void WriteBigEndianBytes(byte[] endianBytes)
        {
            for (int i = endianBytes.Length - 1; i >= 0; i--)
            {
                _writer.Write(endianBytes[i]);
            }
        }

        private void WriteLittleEndianBytes(byte[] endianBytes)
        {
            for (int i = 0; i < endianBytes.Length; i++)
            {
                _writer.Write(endianBytes[i]);
            }
        }

        #endregion

        #region Public Methods

        public void WriteVarInt(int value)
        {
            uint zigzag = (uint)((value << 1) ^ (value >> 31));

            // 2. Write the encoded bits as an unsigned VarInt
            WriteVarUInt(zigzag);
        }

        public void WriteVarUInt(uint value)
        {
            while (value >= 0x80)
            {
                // Write the lower 7 bits, and set the highest bit (0x80) to 1 to indicate continuation
                _writer.Write((byte)((value & 0x7F) | 0x80));
                value >>= 7;
            }

            // Write the final byte with the highest bit set to 0
            _writer.Write((byte)value);
        }

        public void WriteVarShort(short value)
        {
            ushort zigzag = (ushort)((value << 1) ^ (value >> 15));

            // 2. Write the encoded bits as an unsigned VarInt
            WriteVarUShort(zigzag);
        }

        public void WriteVarUShort(ushort value)
        {
            // While the value is 128 or greater, we need more than 1 byte
            while (value >= 0x80)
            {
                // Write the lower 7 bits, and set the highest bit (0x80) to 1 to indicate continuation
                _writer.Write((byte)((value & 0x7F) | 0x80));
                value >>= 7;
            }

            // Write the final byte with the highest bit set to 0
            _writer.Write((byte)value);
        }

        public void WriteVarLong(long value)
        {
            // 64-bit ZigZag Encode
            ulong zigzag = (ulong)((value << 1) ^ (value >> 63));
            WriteVarULong(zigzag);
        }

        public void WriteVarULong(ulong value)
        {
            // While the value needs more than 7 bits to be represented
            while (value >= 0x80)
            {
                // Write the lower 7 bits, and set the highest bit (0x80) to 1 to indicate continuation
                _writer.Write((byte)((value & 0x7F) | 0x80));

                // Shift the 64-bit value right by 7 bits
                value >>= 7;
            }

            // Write the final byte with the highest bit set to 0
            _writer.Write((byte)value);
        }

        /// <summary>
        ///   Write a Short into the buffer
        /// </summary>
        /// <returns></returns>
        public void WriteShort(short @short, Endian endian = Endian.Big)
        {
            if (endian == Endian.Big)
                WriteBigEndianBytes(BitConverter.GetBytes(@short));
            else
                WriteLittleEndianBytes(BitConverter.GetBytes(@short));
        }

        /// <summary>
        ///   Write a int into the buffer
        /// </summary>
        /// <returns></returns>
        public void WriteInt(int @int, Endian endian = Endian.Big)
        {
            if (endian == Endian.Big)
                WriteBigEndianBytes(BitConverter.GetBytes(@int));
            else
                WriteLittleEndianBytes(BitConverter.GetBytes(@int));
        }

        /// <summary>
        ///   Write a long into the buffer
        /// </summary>
        /// <returns></returns>
        public void WriteLong(Int64 @long, Endian endian = Endian.Big)
        {
            if (endian == Endian.Big)
                WriteBigEndianBytes(BitConverter.GetBytes(@long));
            else
                WriteLittleEndianBytes(BitConverter.GetBytes(@long));
        }

        /// <summary>
        ///   Write a UShort into the buffer
        /// </summary>
        /// <returns></returns>
        public void WriteUShort(ushort @ushort, Endian endian = Endian.Big)
        {
            if (endian == Endian.Big)
                WriteBigEndianBytes(BitConverter.GetBytes(@ushort));
            else
                WriteLittleEndianBytes(BitConverter.GetBytes(@ushort));
        }

        /// <summary>
        ///   Write a int into the buffer
        /// </summary>
        /// <returns></returns>
        public void WriteUInt(UInt32 @uint, Endian endian = Endian.Big)
        {
            if (endian == Endian.Big)
                WriteBigEndianBytes(BitConverter.GetBytes(@uint));
            else
                WriteLittleEndianBytes(BitConverter.GetBytes(@uint));
        }

        /// <summary>
        ///   Write a long into the buffer
        /// </summary>
        /// <returns></returns>
        public void WriteULong(UInt64 @ulong, Endian endian = Endian.Big)
        {
            if (endian == Endian.Big)
                WriteBigEndianBytes(BitConverter.GetBytes(@ulong));
            else
                WriteLittleEndianBytes(BitConverter.GetBytes(@ulong));
        }

        /// <summary>
        ///   Write a byte into the buffer
        /// </summary>
        /// <returns></returns>
        public void WriteByte(byte @byte)
        {
            _writer.Write(@byte);
        }

        public void WriteSByte(sbyte @byte)
        {
            _writer.Write(@byte);
        }
        /// <summary>
        ///   Write a Float into the buffer
        /// </summary>
        /// <returns></returns>
        public void WriteFloat(float @float, Endian endian = Endian.Big)
        {
            if (endian == Endian.Big)
                WriteBigEndianBytes(BitConverter.GetBytes(@float));
            else
                WriteLittleEndianBytes(BitConverter.GetBytes(@float));
        }

        /// <summary>
        ///   Write a Boolean into the buffer
        /// </summary>
        /// <returns></returns>
        public void WriteBoolean(Boolean @bool)
        {
            if (@bool)
            {
                _writer.Write((byte)1);
            }
            else
            {
                _writer.Write((byte)0);
            }
        }

        /// <summary>
        ///   Write a Char into the buffer
        /// </summary>
        /// <returns></returns>
        public void WriteChar(Char @char)
        {
            WriteBigEndianBytes(new byte[] { (byte)@char, 0 });
        }

        /// <summary>
        ///   Write a Double into the buffer
        /// </summary>
        public void WriteDouble(Double @double, Endian endian = Endian.Big)
        {
            if (endian == Endian.Big)
                WriteBigEndianBytes(BitConverter.GetBytes(@double));
            else
                WriteLittleEndianBytes(BitConverter.GetBytes(@double));
        }

        /// <summary>
        ///   Write a string into the buffer
        /// </summary>
        /// <returns></returns>
        public void WriteUTF(string str)
        {
            if (!str.EndsWith('\0'))
                str += "\0";

            var bytes = Encoding.UTF8.GetBytes(str);
            var len = bytes.Length;
            WriteInt(len);

            int i;
            for (i = 0; i < len; i++)
                _writer.Write(bytes[i]);
        }

        /// <summary>
        ///   Write a string into the buffer
        /// </summary>
        /// <returns></returns>
        public void WriteUTFBytes(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            var len = bytes.Length;
            int i;
            for (i = 0; i < len; i++)
                _writer.Write(bytes[i]);
        }

        /// <summary>
        ///   Write a bytes array into the buffer
        /// </summary>
        /// <returns></returns>
        public void WriteBytes(byte[] data)
        {
            _writer.Write(data);
        }

        public long ReserveInt()
        {
            // Remember where we are
            long reservedPosition = _writer.BaseStream.Position;

            // Write 4 bytes of dummy data (zeros) to advance the stream
            WriteInt(0);

            return reservedPosition;
        }

        public void WriteIntAtPosition(int value, long position)
        {
            // Remember the current position
            long currentPosition = _writer.BaseStream.Position;
            // Move to the reserved position
            _writer.BaseStream.Seek(position, SeekOrigin.Begin);
            // Write the actual integer value
            WriteInt(value);
            // Move back to the original position
            _writer.BaseStream.Seek(currentPosition, SeekOrigin.Begin);
        }

        public void Seek(int offset)
        {
            Seek(offset, SeekOrigin.Begin);
        }

        public void Seek(int offset, SeekOrigin seekOrigin)
        {
            _writer.BaseStream.Seek(offset, seekOrigin);
        }

        public void Clear()
        {
            _writer = new BinaryWriter(new MemoryStream(), Encoding.UTF8);
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            _writer.Flush();
            _writer.Dispose();
            _writer = null;
        }

        #endregion
    }
}