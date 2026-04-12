#region License GNU GPL
// BigEndianReader.cs
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
	public class BufferReader : IDataReader, IDisposable
	{
		public const int INT_SIZE = 32;
		public const int SHORT_SIZE = 16;
		public const int SHORT_MIN_VALUE = -0x8000;
		public const int SHORT_MAX_VALUE = 0x7FFF;
		public const int USHORT_MAX_VALUE = 0x10000;
		public const int CHUNCK_BIT_SIZE = 7;
		public static readonly int MAX_ENCODING_LENGHT = (int)Math.Ceiling((double)INT_SIZE / CHUNCK_BIT_SIZE);
		public const int MASK_10000000 = 0x80;
		public const int MASK_01111111 = 0x7F;

		#region Properties

		private BinaryReader _reader;

		/// <summary>
		///   Gets availiable bytes number in the buffer
		/// </summary>
		public long BytesAvailable
		{
			get { return _reader.BaseStream.Length - _reader.BaseStream.Position; }
		}

		public long Position
		{
			get
			{
				return _reader.BaseStream.Position;
			}
			set
			{
				Seek(value, SeekOrigin.Begin);
			}
		}


		public Stream BaseStream
		{
			get
			{
				return _reader.BaseStream;
			}
		}

		#endregion

		#region Initialisation

		/// <summary>
		///   Initializes a new instance of the <see cref = "BufferReader" /> class.
		/// </summary>
		public BufferReader()
		{
			_reader = new BinaryReader(new MemoryStream(), Encoding.UTF8);
		}

		/// <summary>
		///   Initializes a new instance of the <see cref = "BufferReader" /> class.
		/// </summary>
		/// <param name = "stream">The stream.</param>
		public BufferReader(Stream stream)
		{
			_reader = new BinaryReader(stream, Encoding.UTF8);
		}

		/// <summary>
		///   Initializes a new instance of the <see cref = "BufferReader" /> class.
		/// </summary>
		/// <param name = "tab">Memory buffer.</param>
		public BufferReader(byte[] tab)
		{
			_reader = new BinaryReader(new MemoryStream(tab), Encoding.UTF8);
		}

		#endregion

		#region Private Methods

		/// <summary>
		///   Read bytes in big endian format
		/// </summary>
		/// <param name = "count"></param>
		/// <returns></returns>
		private byte[] ReadBigEndianBytes(int count)
		{
			var bytes = new byte[count];
			int i;
			for (i = count - 1; i >= 0; i--)
				bytes[i] = (byte)BaseStream.ReadByte();
			return bytes;
		}

		private byte[] ReadLittleEndianBytes(int count)
		{
			var bytes = new byte[count];
			int i;
			for (i = 0; i < count; i++)
				bytes[i] = (byte)BaseStream.ReadByte();
			return bytes;
        }

        #endregion

        #region Public Method

        public int ReadVarInt()
		{
            uint rawValue = ReadVarUInt();

            // 2. ZigZag Decode
            // Standard 32-bit formula: (rawValue >> 1) ^ -(rawValue & 1)
            // This cleanly transforms the unsigned bits back into the correct negative/positive integer.
            return (int)(rawValue >> 1) ^ -(int)(rawValue & 1);
        }

		public uint ReadVarUInt()
		{
            uint value = 0;
            int shift = 0;

            while (true)
            {
                // ReadByte automatically throws EndOfStreamException if we run out of data
                byte b = ReadByte();

                // Overflow check: A 32-bit integer can only hold up to 5 bytes in LEB128 format.
                // The C++ logic `(v10 > 0xFFFFFFFF >> v9)` perfectly translates to checking
                // if the current byte's bits would exceed the maximum capacity of a uint.
                if (shift >= 35 || (b & 0x7F) > (uint.MaxValue >> shift))
                {
                    throw new InvalidDataException("Malformed VarInt: Value exceeds 32-bit capacity.");
                }

                // Append the lower 7 bits to our value
                value |= (uint)(b & 0x7F) << shift;

                // If the highest bit is 0, this is the last byte of the VarInt
                if ((b & 0x80) == 0)
                {
                    return value;
                }

                shift += 7;
            }
        }

		public short ReadVarShort()
		{
            ushort rawValue = ReadVarUShort();

            // 2. ZigZag Decode and return directly
            return (short)((rawValue >> 1) ^ -(rawValue & 1));
        }

		public ushort ReadVarUShort()
		{
            ushort value = 0;
            int shift = 0;

            while (true)
            {
                // ReadByte automatically throws EndOfStreamException if we run out of data
                byte b = ReadByte();

                // Overflow check: If shifting this byte exceeds 16 bits, the data is invalid/corrupted
                if ((b & 0x7F) > (0xFFFF >> shift))
                {
                    throw new InvalidDataException("Malformed VarInt: Value exceeds 16-bit capacity.");
                }

                // Append the lower 7 bits to our value
                value |= (ushort)((b & 0x7F) << shift);

                // If the highest bit is 0, this is the last byte of the VarInt
                if ((b & 0x80) == 0)
                {
                    return value;
                }

                shift += 7;
            }
        }

		public long ReadVarLong()
		{
            ulong rawValue = ReadVarULong();
            // 64-bit ZigZag Decode
            return (long)(rawValue >> 1) ^ -(long)(rawValue & 1);
        }

		public ulong ReadVarULong()
		{
            ulong value = 0;
            int shift = 0;

            while (true)
            {
                // ReadByte automatically throws EndOfStreamException if we run out of data
                byte b = ReadByte();

                // Overflow check: A 64-bit integer can take up to 10 bytes in LEB128 format.
                // The logic perfectly mimics `(v13 & 0x7Fu) > 0xFFFFFFFFFFFFFFFFuLL >> v4`
                if (shift >= 70 || (ulong)(b & 0x7F) > (ulong.MaxValue >> shift))
                {
                    throw new InvalidDataException("Malformed VarULong: Value exceeds 64-bit capacity.");
                }

                // Append the lower 7 bits to our value
                value |= (ulong)(b & 0x7F) << shift;

                // If the highest bit is 0, this is the last byte of the VarInt
                if ((b & 0x80) == 0)
                {
                    return value;
                }

                shift += 7;
            }
        }

		/// <summary>
		///   Read a Short from the Buffer
		/// </summary>
		/// <returns></returns>
		public short ReadShort(Endian endian = Endian.Big)
		{
			return BitConverter.ToInt16(endian == Endian.Big ? ReadBigEndianBytes(2) : ReadLittleEndianBytes(2), 0);
		}

		/// <summary>
		///   Read a int from the Buffer
		/// </summary>
		/// <returns></returns>
		public int ReadInt(Endian endian = Endian.Big)
		{
			return BitConverter.ToInt32(endian == Endian.Big ? ReadBigEndianBytes(4) : ReadLittleEndianBytes(4), 0);
		}

		/// <summary>
		///   Read a long from the Buffer
		/// </summary>
		/// <returns></returns>
		public long ReadLong(Endian endian = Endian.Big)
		{
			return BitConverter.ToInt64(endian == Endian.Big ? ReadBigEndianBytes(8) : ReadLittleEndianBytes(8), 0);
		}

		/// <summary>
		///   Read a Float from the Buffer
		/// </summary>
		/// <returns></returns>
		public float ReadFloat(Endian endian = Endian.Big)
		{
			return BitConverter.ToSingle(endian == Endian.Big ? ReadBigEndianBytes(4) : ReadLittleEndianBytes(4), 0);
		}

		/// <summary>
		///   Read a UShort from the Buffer
		/// </summary>
		/// <returns></returns>
		public ushort ReadUShort(Endian endian = Endian.Big)
		{
			return BitConverter.ToUInt16(endian == Endian.Big ? ReadBigEndianBytes(2) : ReadLittleEndianBytes(2), 0);
		}

		/// <summary>
		///   Read a int from the Buffer
		/// </summary>
		/// <returns></returns>
		public uint ReadUInt(Endian endian = Endian.Big)
		{
			return BitConverter.ToUInt32(endian == Endian.Big ? ReadBigEndianBytes(4) : ReadLittleEndianBytes(4), 0);
		}

		/// <summary>
		///   Read a long from the Buffer
		/// </summary>
		/// <returns></returns>
		public ulong ReadULong(Endian endian = Endian.Big)
		{
			return BitConverter.ToUInt64(endian == Endian.Big ? ReadBigEndianBytes(8) : ReadLittleEndianBytes(8), 0);
		}

		/// <summary>
		///   Read a byte from the Buffer
		/// </summary>
		/// <returns></returns>
		public byte ReadByte()
		{
			return _reader.ReadByte();
		}

		public sbyte ReadSByte()
		{
			return _reader.ReadSByte();
		}

		/// <summary>
		///   Returns N bytes from the buffer
		/// </summary>
		/// <param name = "n">Number of read bytes.</param>
		/// <returns></returns>
		public byte[] ReadBytes(int n)
		{
			return _reader.ReadBytes(n);
		}

        /// <summary>
        ///   Returns N bytes from the buffer
        /// </summary>
        /// <param name = "n">Number of read bytes.</param>
        /// <returns></returns>
        public byte[] ReadBytes(uint n)
        {
            return ReadBytes((int)n);
        }

        /// <summary>
        /// Returns N bytes from the buffer
        /// </summary>
        /// <param name = "n">Number of read bytes.</param>
        /// <returns></returns>
        public BufferReader ReadBytesInNewBigEndianReader(int n)
		{
			return new BufferReader(_reader.ReadBytes(n));
		}

		/// <summary>
		///   Read a Boolean from the Buffer
		/// </summary>
		/// <returns></returns>
		public bool ReadBoolean()
		{
			return _reader.ReadByte() == 1;
		}

		/// <summary>
		///   Read a Char from the Buffer
		/// </summary>
		/// <returns></returns>
		public char ReadChar()
		{
			return (char)ReadByte();
		}

		/// <summary>
		///   Read a Double from the Buffer
		/// </summary>
		/// <returns></returns>
		public double ReadDouble(Endian endian = Endian.Big)
		{
			return BitConverter.ToDouble(ReadBigEndianBytes(8), 0);
		}

		/// <summary>
		///   Read a string from the Buffer
		/// </summary>
		/// <returns></returns>
		public string ReadUTF()
		{
			int length = ReadInt();

			byte[] bytes = ReadBytes(length);
			return Encoding.UTF8.GetString(bytes).Replace("\0", "");
		}

		/// <summary>
		///   Read a string from the Buffer
		/// </summary>
		/// <returns></returns>
		public string ReadUTFBytes(ushort len)
		{
			byte[] bytes = ReadBytes(len);

			return Encoding.UTF8.GetString(bytes);
		}

		/// <summary>
		///   Read a string from the Buffer
		/// </summary>
		/// <returns></returns>
		public string ReadUTFBytes(int len)
		{
			byte[] bytes = ReadBytes(len);

			return Encoding.UTF8.GetString(bytes);
		}

		/// <summary>
		///   Skip bytes
		/// </summary>
		/// <param name = "n"></param>
		public void SkipBytes(int n)
		{
			int i;
			for (i = 0; i < n; i++)
			{
				_reader.ReadByte();
			}
		}


		public void Seek(long offset, SeekOrigin seekOrigin)
		{
			_reader.BaseStream.Seek(offset, seekOrigin);
		}

		/// <summary>
		///   Add a bytes array to the end of the buffer
		/// </summary>
		public void Add(byte[] data, int offset, int count)
		{
			long pos = _reader.BaseStream.Position;

			_reader.BaseStream.Position = _reader.BaseStream.Length;
			_reader.BaseStream.Write(data, offset, count);
			_reader.BaseStream.Position = pos;
		}

		#endregion

		#region Dispose

		/// <summary>
		///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			if (_reader == null)
				return;

			_reader.BaseStream?.Dispose();
			_reader.Dispose();
			_reader = null;
		}

		#endregion
	}
}