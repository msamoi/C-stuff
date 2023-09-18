namespace ConsoleApp;

public static class Crypto
{
    public static string Encrypt(string input, byte[] keyBytes) {
        var textBytes = System.Text.Encoding.UTF8.GetBytes(input);
        for (var i = 0; i<textBytes.Length; i++)
        {
            textBytes[i] = (byte)((textBytes[i] + keyBytes[i % keyBytes.Length]) % 256);
        }
        return Convert.ToBase64String(textBytes);
    }

    public static string Decrypt(string input, byte[] keyBytes)
    {
        var encryptedBytes = Convert.FromBase64String(input);
        for (var i = 0; i < encryptedBytes.Length; i++)
        {
            encryptedBytes[i] = (byte) (((encryptedBytes[i] - keyBytes[i % keyBytes.Length]) + 256) % 256);
        }
        return System.Text.Encoding.UTF8.GetString(encryptedBytes);
    }
}