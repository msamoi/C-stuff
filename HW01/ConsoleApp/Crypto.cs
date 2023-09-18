namespace ConsoleApp;

public static class Crypto
{
    public static string Encrypt(string input, byte[] keyBytes) {
        var textBytes = System.Text.Encoding.UTF8.GetBytes(input);
        for (var i = 0; i<textBytes.Length; i++)
        {
            int offset = keyBytes[i % keyBytes.Length];
            textBytes[i] = (byte)((textBytes[i] + (offset % 26)) % 256);
        }
        return Convert.ToBase64String(textBytes);
    }

    public static string Decrypt(string input, byte[] keyBytes)
    {
        var encryptedBytes = Convert.FromBase64String(input);
        
        for (var i = 0; i < encryptedBytes.Length; i++)
        {
            int offset = keyBytes[i % keyBytes.Length];
            encryptedBytes[i] = (byte) (((encryptedBytes[i] - (offset % 26)) + 256) % 256);
        }
        return System.Text.Encoding.UTF8.GetString(encryptedBytes);
    }
}