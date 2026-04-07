namespace LinkShortener.Utils;

public static class Encoder
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private const int Length = 6;

    public static string GetEncoded(ulong number)
    {
        var result = new char[Length];
        for (var i = 0; i < Length; i++)
        {
            var value = number % (ulong)Chars.Length;
            result[i] = Chars[(int)value];
            number /= (ulong)Chars.Length;
        }

        return new string(result);
    }
}
