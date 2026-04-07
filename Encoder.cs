using System.Text;

namespace LinkShortener.Utils;

public static class Encoder
{
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string Encode(ulong number)
    {
        if (number == 0)
        {
            return Alphabet[0].ToString();
        }

        var result = new StringBuilder();
        while (number > 0)
        {
            var remainder = (int)(number % (ulong)Alphabet.Length);
            result.Insert(0, Alphabet[remainder]);
            number /= (ulong)Alphabet.Length;
        }

        return result.ToString();
    }
}
