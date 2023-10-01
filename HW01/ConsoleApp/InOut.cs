using System.Text;

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
                Console.WriteLine($"Cannot convert \"{shiftStr}\" to integer!");
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
            var input = Console.ReadLine();
            if (String.IsNullOrEmpty(input))
            {
                Console.WriteLine("String cannot be empty!");
                continue;
            }

            if (!CheckEncoding(input, Encoding.UTF8))
            {
                Console.WriteLine("String encoding not UTF-8!");
                continue;
            }
            
            return input;
        } while (true);
    }
    
    private static bool CheckEncoding(string value, Encoding encoding) // solution idea from https://stackoverflow.com/questions/66598354/check-if-string-is-encoded-in-utf-8-in-c-sharp
    {
        var charArray = value.ToCharArray();
        var bytes = new byte[charArray.Length];
        for (var i = 0; i < charArray.Length; i++)
        {
            bytes[i] = (byte)charArray[i];
        }
        return string.Equals(encoding.GetString(bytes, 0, bytes.Length), value, StringComparison.InvariantCulture);
    }

    public static string GetChoice()
    {
        var userChoice = Console.ReadLine()?.ToUpper().Trim();
        return String.IsNullOrEmpty(userChoice) ? "" : userChoice;
    }
}