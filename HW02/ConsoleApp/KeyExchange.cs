
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
        ulong[] pubNumbers = { 0, 0 };
        var rand = new Random();
        ulong limit = (ulong)Math.Sqrt(ulong.MaxValue);
        pubNumbers[0] = rand.GetRandomPrime(limit);
        pubNumbers[1] = rand.FindPrimitive(pubNumbers[0]);
        Console.WriteLine("P is " + pubNumbers[0] + " and G is " + pubNumbers[1]);
        ulong[] privNumbers = { rand.NextLong(limit), rand.NextLong(limit) };
        Console.WriteLine("a is " + privNumbers[0] + " and b is " + privNumbers[1]);
        var publicKeys = GeneratePublicKeys(pubNumbers, privNumbers);
        Console.WriteLine("Public keys: x is " + publicKeys[0] + " and y is " + publicKeys[1]);
        GenerateSymmetricKey(pubNumbers[0], publicKeys, privNumbers);
    }

    private static void GetSymmetricKeyByInput()
    {
        ulong limit = (ulong)Math.Sqrt(ulong.MaxValue);
        var pubNumbers = GetPublicNumbers(limit);
        var privNumbers = GetPrivateNumbers(limit);
        var publicKeys = GeneratePublicKeys(pubNumbers, privNumbers);
        GenerateSymmetricKey(pubNumbers[0], publicKeys, privNumbers);
    }

    private static ulong[] GetPublicNumbers(ulong max)
    {
        ulong p, g;
        while (true)
        {
            Console.WriteLine("Enter the prime number P");
            p = InOut.GetNumberFromUser();
            if (!Primes.IsPrime(p)) continue;
            if (p > max) continue;
            break;
        }

        while (true)
        {
            Console.WriteLine("Enter the primitive root G");
            g = InOut.GetNumberFromUser();
            if (!Primes.IsPrimitiveRoot(p, g)) continue;
            break;
        }

        ulong[] outvar = { p, g };
        return outvar;
    }

    private static ulong[] GetPrivateNumbers(ulong max)
    {
        ulong a, b;
        while (true)
        {
            Console.WriteLine("Enter the private number for the first person (a):");
            a = InOut.GetNumberFromUser();
            if (a > max) continue;
            break;
        }

        while (true)
        {
            Console.WriteLine("Enter the private number for the second person (b):");
            b = InOut.GetNumberFromUser();
            if (b > max) continue;
            break;
        }
        
        ulong[] outvar = { a, b };
        return outvar;
    }

    private static ulong[] GeneratePublicKeys(IReadOnlyList<ulong> pubNum, IReadOnlyList<ulong> privNum)
    {
        var x = Rand.Power(pubNum[1], privNum[0], pubNum[0]);
        var y = Rand.Power(pubNum[1], privNum[1], pubNum[0]);
        
        ulong[] outvar = { x, y };
        return outvar;
    }

    private static void GenerateSymmetricKey(ulong p, IReadOnlyList<ulong> pubKeys, IReadOnlyList<ulong> privNum)
    {
        var symmKey1 = Rand.Power(pubKeys[1], privNum[0], p);
        var symmKey2 = Rand.Power(pubKeys[0], privNum[1], p);

        if (symmKey1.Equals(symmKey2))
        {
            Console.WriteLine("The symmetric key is " + symmKey1 + ".");
        }
    }
}