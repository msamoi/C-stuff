
namespace ConsoleApp;

public static class KeyExchange
{
    public static void Init()
    {
        string userChoice;
        do
        {
            Console.WriteLine("[I]nput values yourself or generate [R]andom ones, or e[X]it?");
            userChoice = InOut.GetChoice();
            switch (userChoice)
            {
                case "X":
                    break;
                case "I":
                    GetSymmetricKeyByInput();
                    break;
                case "R":
                    GetRandomSymmetricKey();
                    break;
            }
        } while (userChoice != "X");
    }

    private static void GetRandomSymmetricKey()
    {
        const ulong limit = ulong.MaxValue;
        var keys = new Keys(limit); // initialize Keys struct with random values up to specified limit
        Console.WriteLine("P is " + keys.P + " and G is " + keys.G);
        Console.WriteLine("a is " + keys.A + " and b is " + keys.B);
        keys.GeneratePublicKeys();
        Console.WriteLine("x is " + keys.X + " and y is " + keys.Y);
        keys.GenerateSymmetricKey();
        Console.WriteLine("The symmetric key is " + keys.SymmKey);
    }

    private static void GetSymmetricKeyByInput()
    {
        const ulong limit = ulong.MaxValue;
        var keys = new Keys();
        keys.GetPublicNumbers(limit);
        keys.GetPrivateNumbers(limit);
        keys.GeneratePublicKeys();
        keys.GenerateSymmetricKey();
        Console.WriteLine("The symmetric key is " + keys.SymmKey);
    }
}