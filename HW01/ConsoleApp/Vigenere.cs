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
        var toEncrypt = InOut.GetStringInput();
        
        Console.Write("Enter encryption key: ");
        var key = InOut.GetStringInput();
        var keyBytes = Encoding.UTF8.GetBytes(key);

        var encText = Crypto.Encrypt(toEncrypt, keyBytes); 
        
        Console.WriteLine($"Vigenere encrypted text with key {key}");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(encText);
        Console.WriteLine("----------------------------------------------"); 
    }
    
    private static void Decrypt()
    {
        Console.Write("Enter encrypted text to decrypt: ");
        var encryptedText = InOut.GetStringInput();

        Console.Write("Enter key to decrypt with: ");
        var key = InOut.GetStringInput();
        var keyBytes = Encoding.UTF8.GetBytes(key);

        var decText = Crypto.Decrypt(encryptedText, keyBytes);


        Console.WriteLine($"Vigenere decrypted text from {encryptedText} with key {key}");
        Console.WriteLine(decText);
    }
}