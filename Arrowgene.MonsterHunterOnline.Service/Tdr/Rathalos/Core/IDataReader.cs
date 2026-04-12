using System;
using System.IO;

namespace Rathalos.Core.Utils.IO
{
	public enum Endian
	{
		Big,
		Little
    }
    public interface IDataReader : IDisposable
	{
		long Position
		{
			get;
		}

		long BytesAvailable
		{
			get;
		}

		short ReadVarShort();
		ushort ReadVarUShort();
		int ReadVarInt();
		uint ReadVarUInt();
		long ReadVarLong();
		ulong ReadVarULong();

		/// <summary>
		///   Read a Short from the Buffer
		/// </summary>
		/// <returns></returns>
		short ReadShort(Endian endian = Endian.Big);

		/// <summary>
		///   Read a int from the Buffer
		/// </summary>
		/// <returns></returns>
		int ReadInt(Endian endian = Endian.Big);

		/// <summary>
		///   Read a long from the Buffer
		/// </summary>
		/// <returns></returns>
		Int64 ReadLong(Endian endian = Endian.Big);


		/// <summary>
		///   Read a UShort from the Buffer
		/// </summary>
		/// <returns></returns>
		ushort ReadUShort(Endian endian = Endian.Big);

		/// <summary>
		///   Read a int from the Buffer
		/// </summary>
		/// <returns></returns>
		UInt32 ReadUInt(Endian endian = Endian.Big);

		/// <summary>
		///   Read a long from the Buffer
		/// </summary>
		/// <returns></returns>
		UInt64 ReadULong(Endian endian = Endian.Big);

		/// <summary>
		///   Read a byte from the Buffer
		/// </summary>
		/// <returns></returns>
		byte ReadByte();
		sbyte ReadSByte();

		/// <summary>
		///   Returns N bytes from the buffer
		/// </summary>
		/// <param name = "n">Number of read bytes.</param>
		/// <returns></returns>
		byte[] ReadBytes(int n);

        /// <summary>
        ///   Returns N bytes from the buffer
        /// </summary>
        /// <param name = "n">Number of read bytes.</param>
        /// <returns></returns>
        byte[] ReadBytes(uint n);

        /// <summary>
        ///   Read a Boolean from the Buffer
        /// </summary>
        /// <returns></returns>
        Boolean ReadBoolean();

		/// <summary>
		///   Read a Char from the Buffer
		/// </summary>
		/// <returns></returns>
		Char ReadChar();

		/// <summary>
		///   Read a Double from the Buffer
		/// </summary>
		/// <returns></returns>
		Double ReadDouble(Endian endian = Endian.Big);

		/// <summary>
		///   Read a Float from the Buffer
		/// </summary>
		/// <returns></returns>
		float ReadFloat(Endian endian = Endian.Big);

		/// <summary>
		///   Read a string from the Buffer
		/// </summary>
		/// <returns></returns>
		string ReadUTF();

		/// <summary>
		///   Read a string from the Buffer
		/// </summary>
		/// <returns></returns>
		string ReadUTFBytes(ushort len);


		void Seek(long offset, SeekOrigin seekOrigin);
		void SkipBytes(int n);
	}
}