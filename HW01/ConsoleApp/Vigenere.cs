using System.Text;

namespace ConsoleApp;

public static class Vigenere
{
    public static void DoVigenere()
    {
        string userChoice;
        do
        {
            Console.WriteLine("Encrypt, Decrypt or eXit (E, D, X)? : ");
            userChoice = InOut.GetChoice();
            switch (userChoice)
            {
                case "E":
                    Encrypt();
                    break;
                case "D":
                    Decrypt();
                    break;
                case "X":
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    break;
            }
        } while (userChoice != "X");
    }

    private static void Encrypt()
    {
        Console.Write("Enter string to encrypt: ");
        string toEncrypt = InOut.GetStringInput();
        Console.Write("Enter encryption key: ");
        string key = InOut.GetStringInput();
        var keyBytes = Encoding.UTF8.GetBytes(key);

        var textBytes = Encoding.UTF8.GetBytes(toEncrypt);
        for (var i = 0; i < textBytes.Length; i++)
        {
            textBytes[i] = (byte)((textBytes[i] + keyBytes[i % keyBytes.Length]) % 256);
        }
        
        Console.WriteLine($"Vigenere encrypted text with key {key}");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(Convert.ToBase64String(textBytes));
        Console.WriteLine("----------------------------------------------"); 
    }
    
    private static void Decrypt()
    {
        Console.Write("Enter encrypted text to decrypt: ");
        var encryptedText = InOut.GetStringInput();
        var encryptedBytes = Convert.FromBase64String(encryptedText);

        Console.Write("Enter key to decrypt with: ");
        var key = InOut.GetStringInput();
        var keyBytes = Encoding.UTF8.GetBytes(key);

        for (var i = 0; i < encryptedBytes.Length; i++)
        {
            encryptedBytes[i] = (byte) (((encryptedBytes[i] - keyBytes[i % keyBytes.Length]) + 256) % 256);
        }
        Console.WriteLine($"Cesar decrypted text from {encryptedText} with key {key}");
        Console.WriteLine(Encoding.UTF8.GetString(encryptedBytes));
    }
}