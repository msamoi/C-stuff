namespace ConsoleApp;

public static class InOut
{
    public static byte GetShiftAmount()
    {
        byte outVar;
        do
        {
            Console.Write("Shift amount: ");
            var shiftStr = Console.ReadLine();
            if (!int.TryParse(shiftStr, out var shiftAmount))
            {
                Console.WriteLine($"Cannot convert {shiftStr} to integer!");
                continue;
            }

            if (shiftAmount == 0)
            {
                Console.WriteLine("Shift amount cannot be 0!");
                continue;
            }

            shiftAmount %= 255;
            if (shiftAmount < 0) shiftAmount += 256;
            outVar = (byte)shiftAmount;
            break;
        } while (true);

        return outVar;
    }

    public static string GetStringInput()
    {
        do
        {
            Console.Write("Enter string to encrypt/decrypt: ");
            var input = Console.ReadLine();
            if (String.IsNullOrEmpty(input))
            {
                Console.WriteLine("String cannot be empty!");
                continue;
            }

            return input;
        } while (true);
    }

    public static string GetChoice()
    {
        var userChoice = Console.ReadLine()?.ToUpper().Trim();
        return String.IsNullOrEmpty(userChoice) ? "" : userChoice;
    }
}