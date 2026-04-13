using System.IO;
using System.Text;

/// <summary>
/// MHO user.cfg obfuscation codec.
///
/// ConfigApp.exe uses a range-based arithmetic substitution cipher
/// on each byte independently. The transform is self-inverse
/// (encode == decode) and preserves non-alphanumeric characters.
///
///   Digits  0-9 (0x30-0x39) ↔ 0x80-0x89  via  0xB9 - b
///   Lower   a-z (0x61-0x7A) ↔ 0x8C-0xA5  via (0x106 - b) & 0xFF
///   Upper   A-Z (0x41-0x5A) ↔ 0xB4-0xCD  via (0x10E - b) & 0xFF
///   Other                                      unchanged
/// </summary>
public static class UserCfgCipher
{
    private static readonly byte[] _lut;

    static UserCfgCipher()
    {
        _lut = new byte[256];
        for (int i = 0; i < 256; i++)
            _lut[i] = (byte)i;

        for (int c = '0'; c <= '9'; c++)
            _lut[c] = (byte)(0xB9 - c);

        for (int c = 'a'; c <= 'z'; c++)
            _lut[c] = (byte)((0x106 - c) & 0xFF);

        for (int c = 'A'; c <= 'Z'; c++)
            _lut[c] = (byte)((0x10E - c) & 0xFF);

        // Fill inverse mappings (encoded byte → plaintext byte)
        // Since the cipher is self-inverse within each range,
        // we just need to map the encoded ranges back the same way.
        for (int c = 0x80; c <= 0x89; c++)
            _lut[c] = (byte)(0xB9 - c);

        for (int c = 0x8C; c <= 0xA5; c++)
            _lut[c] = (byte)((0x106 - c) & 0xFF);

        for (int c = 0xB4; c <= 0xCD; c++)
            _lut[c] = (byte)((0x10E - c) & 0xFF);
    }

    /// <summary>
    /// Transforms a byte buffer in-place. Since the cipher is self-inverse,
    /// this single method handles both encoding and decoding.
    /// </summary>
    public static void Transform(byte[] data, int offset, int count)
    {
        for (int i = offset; i < offset + count; i++)
            data[i] = _lut[data[i]];
    }

    public static byte[] Transform(byte[] data)
    {
        var result = new byte[data.Length];
        for (int i = 0; i < data.Length; i++)
            result[i] = _lut[data[i]];
        return result;
    }

    public static string DecodeFile(string path)
    {
        byte[] raw = File.ReadAllBytes(path);
        byte[] decoded = Transform(raw);
        return Encoding.ASCII.GetString(decoded);
    }

    public static void EncodeFile(string plaintext, string path)
    {
        byte[] raw = Encoding.ASCII.GetBytes(plaintext);
        byte[] encoded = Transform(raw);
        File.WriteAllBytes(path, encoded);
    }
}
