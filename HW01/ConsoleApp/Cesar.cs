namespace ConsoleApp;
using System.Text;

public static class Cesar
{
    public static void DoCesar()
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
        Console.WriteLine("Enter text to encrypt: ");
        var toShift = InOut.GetStringInput();
        byte[] shiftAmount =  { InOut.GetShiftAmount() };

        var encText = Crypto.Encrypt(toShift, shiftAmount);
        Console.WriteLine($"Cesar encrypted text with shift {shiftAmount[0]}");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(encText);
        Console.WriteLine("----------------------------------------------");
    }


    private static void Decrypt()
    {
        Console.Write("Enter encrypted text to decrypt: ");
        var encryptedText = InOut.GetStringInput();
        byte[] shiftAmount =  { InOut.GetShiftAmount() };

        var decText = Crypto.Decrypt(encryptedText, shiftAmount);

        Console.WriteLine($"Cesar decrypted text from {encryptedText} with shift {shiftAmount[0]}");
        Console.WriteLine(decText);
    }
}