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
        var shiftAmount = InOut.GetShiftAmount();
        string toShift = InOut.GetStringInput();

        var textBytes = Encoding.UTF8.GetBytes(toShift);

        foreach (var textByte in textBytes)
        {
            Console.Write(textByte + " ");
        }

        for (var i = 0; i < textBytes.Count(); i++)
        {
            textBytes[i] = (byte)((textBytes[i] + shiftAmount) % 256);
        }


        Console.WriteLine($"Cesar encrypted text with shift {shiftAmount}");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(Convert.ToBase64String(textBytes));
        Console.WriteLine("----------------------------------------------");
    }


    private static void Decrypt()
    {
        var encryptedText = InOut.GetStringInput();
        var encryptedBytes = Convert.FromBase64String(encryptedText);

        var shiftAmount = InOut.GetShiftAmount();

        for (var i = 0; i < encryptedBytes.Length; i++)
        {
            encryptedBytes[i] = (byte) (((encryptedBytes[i] - shiftAmount) + 256) % 256);
        }
        Console.WriteLine($"Cesar decrypted text from {encryptedText} with shift {shiftAmount}");
        Console.WriteLine(Encoding.UTF8.GetString(encryptedBytes));
    }
}