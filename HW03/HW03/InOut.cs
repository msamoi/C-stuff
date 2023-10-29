namespace HW03;

public static class InOut
{
    public static ulong GetNumberFromUser()
    {
        do
        {
            var input = Console.ReadLine();
            if (ulong.TryParse(input, out var outvar)) return outvar;
            Console.WriteLine("Input is not a number!");
        } while (true);
    }

    public static string GetChoice()
    {
        var userChoice = Console.ReadLine()?.ToUpper().Trim();
        return string.IsNullOrEmpty(userChoice) ? "" : userChoice;
    }

    public static string GetStringInput()
    {
        do
        {
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input)) return input;
            Console.WriteLine("String cannot be empty!");
        } while (true);
    }

    public static ulong[] GetUlongArrayInput()
    {
        ulong[] outVar;
        bool success;
        do
        {
            var input = GetStringInput().Split(",");
            outVar = new ulong[input.Length];
            success = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (!ulong.TryParse(input[i], out var tmp))
                {
                    Console.WriteLine("Error parsing the encrypted text!");
                    success = false;
                    break;
                }

                outVar[i] = tmp;
            }
        } while (success == false);
        
        return outVar;
    }

}