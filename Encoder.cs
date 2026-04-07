using System.Buffers.Binary;
using System.Text;

namespace LinkShortener.Utils;

public static class Encoder
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private const int Lenght = 5;

    public static string GetEncoded(ulong number)
    {
        var bytes = new byte[8];
        BinaryPrimitives.WriteUInt64LittleEndian(bytes, number);
        var base62 = new StringBuilder();

        for (int i = 0; i < Lenght; i++)
        {
            var index = bytes[i] % (uint)Chars.Length;
            base62.Append(Chars[(int)index]);
        }

        return base62.ToString();
    }
}
