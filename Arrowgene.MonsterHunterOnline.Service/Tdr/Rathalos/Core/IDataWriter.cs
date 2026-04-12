using System;

namespace Rathalos.Core.Utils.IO
{
	public interface IDataWriter
	{
		byte[] Data
		{
			get;
		}

		long Position
		{
			get;
		}

		void WriteVarShort(short @short);
		void WriteVarUShort(ushort @short);
		void WriteVarInt(int @int);
		void WriteVarUInt(uint @int);
		void WriteVarLong(long @long);
		void WriteVarULong(ulong @long);

		/// <summary>
		///   Write a Short into the buffer
		/// </summary>
		/// <returns></returns>
		void WriteShort(short @short, Endian endian = Endian.Big);

		/// <summary>
		///   Write a int into the buffer
		/// </summary>
		/// <returns></returns>
		void WriteInt(int @int, Endian endian = Endian.Big);

		/// <summary>
		///   Write a long into the buffer
		/// </summary>
		/// <returns></returns>
		void WriteLong(Int64 @long, Endian endian = Endian.Big);

		/// <summary>
		///   Write a UShort into the buffer
		/// </summary>
		/// <returns></returns>
		void WriteUShort(ushort @ushort, Endian endian = Endian.Big);

		/// <summary>
		///   Write a int into the buffer
		/// </summary>
		/// <returns></returns>
		void WriteUInt(UInt32 @uint, Endian endian = Endian.Big);

		/// <summary>
		///   Write a long into the buffer
		/// </summary>
		/// <returns></returns>
		void WriteULong(UInt64 @ulong, Endian endian = Endian.Big);

		/// <summary>
		///   Write a byte into the buffer
		/// </summary>
		/// <returns></returns>
		void WriteByte(byte @byte);
		void WriteSByte(sbyte @byte);

		/// <summary>
		///   Write a Float into the buffer
		/// </summary>
		/// <returns></returns>
		void WriteFloat(float @float, Endian endian = Endian.Big);

		/// <summary>
		///   Write a Boolean into the buffer
		/// </summary>
		/// <returns></returns>
		void WriteBoolean(Boolean @bool);

		/// <summary>
		///   Write a Char into the buffer
		/// </summary>
		/// <returns></returns>
		void WriteChar(Char @char);

		/// <summary>
		///   Write a Double into the buffer
		/// </summary>
		void WriteDouble(Double @double, Endian endian = Endian.Big);

		/// <summary>
		///   Write a string into the buffer
		/// </summary>
		/// <returns></returns>
		void WriteUTF(string str);

		/// <summary>
		///   Write a string into the buffer
		/// </summary>
		/// <returns></returns>
		void WriteUTFBytes(string str);

		/// <summary>
		///   Write a bytes array into the buffer
		/// </summary>
		/// <returns></returns>
		void WriteBytes(byte[] data);

		long ReserveInt();
        void WriteIntAtPosition(int value, long position);

        void Clear();
		void Seek(int offset);
	}
}