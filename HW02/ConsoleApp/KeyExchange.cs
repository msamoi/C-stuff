
namespace ConsoleApp;

public static class KeyExchange
{
    public static void Init()
    {
        string userChoice;
        do
        {
            Console.WriteLine("[I]nput values yourself or generate [R]andom ones?");
            userChoice = InOut.GetChoice();
            switch (userChoice)
            {
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
        const ulong maxPubNumber = 100; 
        // pubNumbers[0] = rand.GetRandomPrime(maxPubNumber);
        pubNumbers[0] = 31;
        pubNumbers[1] = rand.FindPrimitive(pubNumbers[0]);
        var maxPrivNumber = (ulong)Math.Floor(Math.Log(ulong.MaxValue, pubNumbers[1]));
        ulong[] privNumbers = { rand.NextLong(maxPrivNumber), rand.NextLong(maxPrivNumber) };
        var publicKeys = GeneratePublicKeys(pubNumbers, privNumbers);
        GenerateSymmetricKey(pubNumbers[0], publicKeys, privNumbers);
    }

    private static void GetSymmetricKeyByInput()
    {
        const ulong maxPubNumber = 100;
        var pubNumbers = GetPublicNumbers(maxPubNumber);
        ulong maxPrivNumber = (ulong)Math.Floor(Math.Log(ulong.MaxValue, pubNumbers[1])); 
        var privNumbers = GetPrivateNumbers(maxPrivNumber);
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

    private static double[] GeneratePublicKeys(IReadOnlyList<ulong> pubNum, IReadOnlyList<ulong> privNum)
    {
        var x = Math.Pow(pubNum[1], privNum[0]) % pubNum[0];
        var y = Math.Pow(pubNum[1], privNum[1]) % pubNum[0];
        double[] outvar = { x, y };
        return outvar;
    }

    private static void GenerateSymmetricKey(ulong p, IReadOnlyList<double> pubKeys, IReadOnlyList<ulong> privNum)
    {
        var symmKey1 = Math.Pow(pubKeys[1], privNum[0]) % p;
        var symmKey2 = Math.Pow(pubKeys[0], privNum[1]) % p;

        if (symmKey1.Equals(symmKey2))
        {
            Console.WriteLine("The symmetric key is " + symmKey1 + ".");
        }
    }
}