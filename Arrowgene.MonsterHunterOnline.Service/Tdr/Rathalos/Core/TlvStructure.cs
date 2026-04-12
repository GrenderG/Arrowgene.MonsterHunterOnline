using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes
{
    public enum TlvMagic
    {
        Debug = 0xAF,
        Dynamic = 0xAA,
        Fixed = 0x99,
    }

    public abstract class TlvStructure
    {
        public virtual TlvMagic Magic { get; private set; } = TlvMagic.Fixed;

        public byte[] Pack()
        {
            var writer = new BufferWriter();
            Serialize(writer);
            return writer.Data;
        }

        public void Unpack(byte[] data)
        {
            var reader = new BufferReader(data);
            Deserialize(reader);
        }

        private void Deserialize(IDataReader reader)
        {
            Magic = (TlvMagic)reader.ReadByte();
            if (Magic != TlvMagic.Fixed && Magic != TlvMagic.Dynamic)
                return;

            var contentLength = reader.ReadInt();
            var contentData = reader.ReadBytes(contentLength);
            var contentReader = new BufferReader(contentData);
            DeserializeContent(contentReader);
        }

        protected abstract void DeserializeContent(IDataReader reader);

        private void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)Magic);
            if (Magic != TlvMagic.Fixed && Magic != TlvMagic.Dynamic)
                return;

            var bufferContent = new BufferWriter();
            SerializeContent(bufferContent);

            writer.WriteInt((int)bufferContent.Data.Length + 5);
            writer.WriteBytes(bufferContent.Data);
        }

        protected abstract void SerializeContent(IDataWriter writer);

        #region Helpers to perfectly match C++ `TdrBuf` logic
        protected T ReadTlvObject<T>(IDataReader reader) where T : TlvStructure, new()
        {
            // 1. Read the length prefix (WireType 5 always starts with a 32-bit integer length)
            int byteLen = reader.ReadInt();

            // If the length is 0, the object is completely empty. 
            if (byteLen <= 0)
                return new T();

            // 2. Extract exactly the bytes that belong to this nested object
            byte[] objectBytes = reader.ReadBytes(byteLen);

            // 3. Instantiate the new nested object
            T obj = new T();

            // 4. Create a "scoped" reader so the nested object's while-loop stops exactly at the end of its own bytes!
            // -> REPLACE `YourDataReader` WITH YOUR ACTUAL CLASS THAT IMPLEMENTS `IDataReader`
            using (var memoryStream = new System.IO.MemoryStream(objectBytes))
            using (var scopedReader = new BufferReader(memoryStream))
            {
                // Assuming your base class has a public Deserialize entry point
                obj.Deserialize(scopedReader);
            }

            return obj;
        }

        protected void WriteTlvObject(IDataWriter writer, uint fieldId, TlvStructure obj)
        {
            // If the object is null, we just skip it to save bandwidth (standard Protobuf/TDR behavior)
            if (obj == null)
                return;

            // Write Tag (Field ID shifted left by 4, bitwise OR with WireType 5)
            writer.WriteVarUInt((fieldId << 4) | 5);

            // Reserve 4 bytes for the length prefix
            long lenPos = writer.ReserveInt();
            long startPos = writer.Position;

            // Delegate the actual content serialization to the nested object
            obj.Serialize(writer);

            // Jump back and inject the exact byte length of the serialized object
            writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
        }

        protected void WriteTlvByte(IDataWriter writer, uint fieldId, byte value)
        {
            // Skip default/0 values to save network bandwidth (standard Protobuf/TDR behavior)
            if (value == 0) return;

            // FieldID shifted left by 4, combined with WireType 1 (which the C++ uses for raw bytes)
            writer.WriteVarUInt((fieldId << 4) | 1);
            writer.WriteByte(value);
        }

        protected void WriteTlvShort(IDataWriter writer, uint fieldId, short value, bool force = false)
        {
            if (value == 0 && !force) return; // Skip default values to save bandwidth
            writer.WriteVarUInt((fieldId << 4) | 2); // WireType 2
            writer.WriteShort(value);
        }

        protected void WriteTlvInt(IDataWriter writer, uint fieldId, int value)
        {
            if (value == 0) return;
            writer.WriteVarUInt((fieldId << 4) | 3); // WireType 3
            writer.WriteInt(value);
        }

        protected void WriteTlvFloat(IDataWriter writer, uint fieldId, float value)
        {
            if (value == 0f) return;
            writer.WriteVarUInt((fieldId << 4) | 3); // WireType 3
            writer.WriteFloat(value);
        }

        protected void WriteTlvLong(IDataWriter writer, uint fieldId, long value)
        {
            if (value == 0) return;
            writer.WriteVarUInt((fieldId << 4) | 4); // WireType 4
            writer.WriteLong(value);
        }

        protected void WriteTlvULong(IDataWriter writer, uint fieldId, ulong value)
        {
            if (value == 0) return;
            writer.WriteVarUInt((fieldId << 4) | 4); // WireType 4
            writer.WriteULong(value);
        }

        protected void WriteTlvVarInt(IDataWriter writer, uint fieldId, int value)
        {
            if (value == 0) return; // Skip defaults
            writer.WriteVarUInt((fieldId << 4) | 0); // WireType 0
            writer.WriteVarInt(value);
        }

        protected void WriteTlvVarShort(IDataWriter writer, uint fieldId, short value)
        {
            if (value == 0) return; // Skip defaults
            writer.WriteVarUInt((fieldId << 4) | 0); // WireType 0
            writer.WriteVarShort(value);
        }

        protected void WriteTlvVarUInt(IDataWriter writer, uint fieldId, uint value)
        {
            if (value == 0) return; // Skip defaults
            writer.WriteVarUInt((fieldId << 4) | 0); // WireType 0
            writer.WriteVarUInt(value);
        }

        protected void WriteStrictIntArray(IDataWriter writer, uint fieldId, int[] arr, int fixedLength)
        {
            if (arr == null) return;

            if (arr.Length != fixedLength)
                throw new InvalidDataException($"[CharTlvAttributes] Field {fieldId} array length is {arr.Length}. It MUST be exactly {fixedLength}.");

            writer.WriteVarUInt((fieldId << 4) | 5); // WireType 5 (Length Delimited)

            long lenPos = writer.ReserveInt();
            long startPos = writer.Position;

            for (int i = 0; i < fixedLength; i++)
            {
                writer.WriteInt(arr[i]);
            }

            writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
        }

        protected void WriteStrictFloatArray(IDataWriter writer, uint fieldId, float[] arr, int fixedLength)
        {
            if (arr == null) return;

            if (arr.Length != fixedLength)
                throw new InvalidDataException($"[CharTlvAttributes] Field {fieldId} float array length is {arr.Length}. It MUST be exactly {fixedLength}.");

            writer.WriteVarUInt((fieldId << 4) | 5);

            long lenPos = writer.ReserveInt();
            long startPos = writer.Position;

            for (int i = 0; i < fixedLength; i++)
            {
                writer.WriteFloat(arr[i]);
            }

            writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
        }

        protected short[] ReadTlvShortArray(IDataReader reader)
        {
            int byteLen = reader.ReadInt();
            int count = byteLen / 2;
            short[] arr = new short[count];
            for (int i = 0; i < count; i++) arr[i] = reader.ReadShort();
            return arr;
        }

        protected void WriteTlvShortArray(IDataWriter writer, uint fieldId, short[] arr)
        {
            if (arr == null || arr.Length == 0) return;
            writer.WriteVarUInt((fieldId << 4) | 5); // WireType 5

            long lenPos = writer.ReserveInt();
            long startPos = writer.Position;

            for (int i = 0; i < arr.Length; i++)
                writer.WriteShort(arr[i]);

            writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
        }

        protected short[] ReadTlvVarShortArray(IDataReader reader)
        {
            // 1. Read the Total Byte Length of the array wrapper
            int byteLen = reader.ReadInt();

            // 2. Calculate exactly where this array ends in the stream
            long endPosition = reader.Position + byteLen;
            var list = new List<short>();

            // 3. Keep reading VarShorts until we hit the end position
            while (reader.Position < endPosition)
            {
                list.Add(reader.ReadVarShort());
            }

            return list.ToArray();
        }
        // Helper for VarShort Arrays that respects a specific count
        protected void WriteTlvVarShortArray(IDataWriter writer, uint fieldId, short[] arr)
        {
            if (arr == null || arr.Length == 0) return;

            writer.WriteVarUInt((fieldId << 4) | 5); // WireType 5
            long lenPos = writer.ReserveInt();
            long startPos = writer.Position;

            // Safely loop up to 'count' (ensuring we don't go out of bounds of the actual array)
            for (int i = 0; i < arr.Length; i++)
            {
                writer.WriteVarShort(arr[i]);
            }

            writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
        }

        protected void WriteTlvIntArray(IDataWriter writer, uint fieldId, int[] arr)
        {
            if (arr == null || arr.Length == 0) return;
            writer.WriteVarUInt((fieldId << 4) | 5); // WireType 5

            long lenPos = writer.ReserveInt();
            long startPos = writer.Position;

            for (int i = 0; i < arr.Length; i++)
                writer.WriteInt(arr[i]);

            writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
        }

        protected void WriteTlvFloatArray(IDataWriter writer, uint fieldId, float[] arr)
        {
            if (arr == null || arr.Length == 0) return;
            writer.WriteVarUInt((fieldId << 4) | 5); // WireType 5

            long lenPos = writer.ReserveInt();
            long startPos = writer.Position;

            for (int i = 0; i < arr.Length; i++)
                writer.WriteFloat(arr[i]);

            writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
        }

        // And the readers for the Length-Delimited arrays
        protected int[] ReadTlvIntArray(IDataReader reader)
        {
            int byteLen = reader.ReadInt();
            int count = byteLen / 4;
            int[] arr = new int[count];
            for (int i = 0; i < count; i++) arr[i] = reader.ReadInt();
            return arr;
        }

        protected float[] ReadTlvFloatArray(IDataReader reader)
        {
            int byteLen = reader.ReadInt();
            int count = byteLen / 4;
            float[] arr = new float[count];
            for (int i = 0; i < count; i++) arr[i] = reader.ReadFloat();
            return arr;
        }

        protected byte[] ReadTlvByteArray(IDataReader reader)
        {
            int byteLen = reader.ReadInt();
            return reader.ReadBytes(byteLen);
        }

        protected void WriteTlvByteArray(IDataWriter writer, uint fieldId, byte[] data)
        {
            if (data == null || data.Length == 0) return;
            writer.WriteVarUInt((fieldId << 4) | 5); // WireType 5

            writer.WriteInt(data.Length); // Raw byte arrays often just write length directly
            writer.WriteBytes(data);
        }

        protected void WriteTlvByteArray(IDataWriter writer, uint fieldId, byte[] data, int count)
        {
            if (data == null || count <= 0) return;

            writer.WriteVarUInt((fieldId << 4) | 5); // WireType 5

            long lenPos = writer.ReserveInt();
            long startPos = writer.Position;

            // Safely write only up to 'count'
            for (int i = 0; i < count && i < data.Length; i++)
            {
                writer.WriteByte(data[i]);
            }

            writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
        }

        protected void WriteTlvIntArray(IDataWriter writer, uint fieldId, int[] arr, int count)
        {
            if (arr == null || count <= 0) return;

            writer.WriteVarUInt((fieldId << 4) | 5); // WireType 5 (Length-delimited)

            long lenPos = writer.ReserveInt();
            long startPos = writer.Position;

            // Safely write only up to 'count'
            for (int i = 0; i < count && i < arr.Length; i++)
            {
                writer.WriteInt(arr[i]);
            }

            writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
        }

        protected int[] ReadTlvVarIntArray(IDataReader reader)
        {
            // A Length-Delimited VarInt array
            int byteLen = reader.ReadInt();

            // Create a temporary sub-reader or track start position
            long endPosition = reader.Position + byteLen;
            var list = new List<int>();

            while (reader.Position < endPosition)
            {
                list.Add(reader.ReadVarInt());
            }

            return list.ToArray();
        }

        protected void WriteTlvVarIntArray(IDataWriter writer, uint fieldId, int[] arr)
        {
            if (arr == null || arr.Length == 0) return;
            writer.WriteVarUInt((fieldId << 4) | 5); // WireType 5 (Length-delimited)

            long lenPos = writer.ReserveInt();
            long startPos = writer.Position;

            for (int i = 0; i < arr.Length; i++)
            {
                writer.WriteVarInt(arr[i]);
            }

            writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
        }

        // Fixed-size Int64 Array (Standard for Magic 0x99)
        protected long[] ReadTlvLongArray(IDataReader reader)
        {
            int byteLen = reader.ReadInt();
            int count = byteLen / 8; // Each long is 8 bytes
            long[] arr = new long[count];
            for (int i = 0; i < count; i++) arr[i] = reader.ReadLong();
            return arr;
        }

        protected void WriteTlvLongArray(IDataWriter writer, uint fieldId, long[] arr)
        {
            if (arr == null || arr.Length == 0) return;
            writer.WriteVarUInt((fieldId << 4) | 5); // WireType 5 (Length Delimited)

            long lenPos = writer.ReserveInt();
            long startPos = writer.Position;

            for (int i = 0; i < arr.Length; i++)
                writer.WriteLong(arr[i]);

            writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
        }

        // Variable-size Long Array (Standard for Magic 0xAA)
        protected long[] ReadTlvVarLongArray(IDataReader reader)
        {
            int byteLen = reader.ReadInt();
            long endPosition = reader.Position + byteLen;
            var list = new List<long>();

            while (reader.Position < endPosition)
            {
                list.Add((long)reader.ReadVarULong());
            }

            return list.ToArray();
        }

        protected void WriteTlvVarLongArray(IDataWriter writer, uint fieldId, long[] arr)
        {
            if (arr == null || arr.Length == 0) return;
            writer.WriteVarUInt((fieldId << 4) | 5); // WireType 5

            long lenPos = writer.ReserveInt();
            long startPos = writer.Position;

            for (int i = 0; i < arr.Length; i++)
                writer.WriteVarULong((ulong)arr[i]);

            writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
        }

        protected void WriteTlvList<T>(IDataWriter writer, uint fieldId, List<T> items) where T : TlvStructure
        {
            if (items == null || items.Count == 0) return;
            writer.WriteVarUInt((fieldId << 4) | 5); // WireType 5

            long listLenPos = writer.ReserveInt();
            long listStartPos = writer.Position;

            foreach (var item in items)
            {
                long itemLenPos = writer.ReserveInt();
                long itemStartPos = writer.Position;

                // Let the nested TLV serialize its own content
                item.SerializeContent(writer);

                writer.WriteIntAtPosition((int)(writer.Position - itemStartPos), itemLenPos);
            }

            writer.WriteIntAtPosition((int)(writer.Position - listStartPos), listLenPos);
        }

        protected List<T> ReadTlvList<T>(IDataReader reader) where T : TlvStructure, new()
        {
            var list = new List<T>();

            // 1. Read the total length of the entire list (in bytes)
            int listByteLen = reader.ReadInt();
            long listEndPos = reader.Position + listByteLen;

            // 2. Loop until we've consumed all the bytes for this list
            while (reader.Position < listEndPos)
            {
                // 3. Read the specific length of THIS single item
                int itemByteLen = reader.ReadInt();

                // 4. Extract only this item's bytes and create a temporary reader for it
                byte[] itemData = reader.ReadBytes(itemByteLen);
                var itemReader = new Rathalos.Core.Utils.IO.BufferReader(itemData);

                // 5. Instantiate the TLV object and deserialize it safely
                var item = new T();
                item.DeserializeContent(itemReader);
                list.Add(item);
            }

            return list;
        }

        protected void WriteTlvString(IDataWriter writer, uint fieldId, string value)
        {
            // In TDR/Protobuf, default or empty values are typically omitted to save bandwidth
            if (string.IsNullOrEmpty(value))
                return;

            // Convert the string to UTF-8 bytes
            byte[] stringBytes = System.Text.Encoding.UTF8.GetBytes(value);

            // Write Tag (Field ID shifted left by 4, bitwise OR with WireType 5)
            writer.WriteVarUInt((fieldId << 4) | 5);

            // Reserve 4 bytes for the length prefix
            long lenPos = writer.ReserveInt();
            long startPos = writer.Position;

            // Write the actual string data
            writer.WriteBytes(stringBytes);

            // Jump back and write the exact byte length
            writer.WriteIntAtPosition((int)(writer.Position - startPos), lenPos);
        }
        protected void SkipTlvField(IDataReader reader, uint wireType)
        {
            switch (wireType)
            {
                case 0:
                    // WireType 0: VarInt / VarLong
                    reader.ReadVarULong(); // (Or ReadVarUInt(), they consume the exact same bytes)
                    break;

                case 1:
                    // WireType 1: Fixed 8-bit (1 byte)
                    reader.ReadByte();
                    break;

                case 2:
                    // WireType 2: Fixed 16-bit (2 bytes)
                    reader.ReadShort();
                    break;

                case 3:
                    // WireType 3: Fixed 32-bit (4 bytes)
                    reader.ReadInt();
                    break;

                case 4:
                    // WireType 4: Fixed 64-bit (8 bytes)
                    reader.ReadLong();
                    break;

                case 5:
                    // WireType 5: Length-Delimited
                    // Reads a 4-byte Int32 to get the length, then skips that many bytes
                    int byteLength = reader.ReadInt();

                    // If your reader supports it, use: reader.BaseStream.Position += byteLength;
                    // Otherwise, just read and discard the bytes:
                    reader.ReadBytes(byteLength);
                    break;

                default:
                    throw new InvalidDataException($"[SkipTlvField] Unrecognized WireType: {wireType}. Possible corrupted packet!");
            }
        }
        #endregion

        public static implicit operator byte[](TlvStructure tlv) => tlv.Pack();
        public static implicit operator List<byte>(TlvStructure tlv) => tlv.Pack().ToList();
    }
}
