
namespace ConsoleApp;

public class KeyExchange
{
    public static void GetSymmetricKey()
    {
        var pubNumbers = GetPublicNumbers();
        var privNumbers = GetPrivateNumbers();
        var publicKeys = GeneratePublicKeys(pubNumbers, privNumbers);
        GenerateSymmetricKey(pubNumbers[0], publicKeys, privNumbers);
    }

    private static ulong[] GetPublicNumbers()
    {
        ulong p, g;
        while (true)
        {
            Console.WriteLine("Enter the prime number P");
            p = InOut.GetNumberFromUser();
            if (!Primes.IsPrime(p)) continue;
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

    private static ulong[] GetPrivateNumbers()
    {
        ulong a, b;
        Console.WriteLine("Enter the private number for the first person (a):");
        a = InOut.GetNumberFromUser();
        Console.WriteLine("Enter the private number for the second person (b):");
        b = InOut.GetNumberFromUser();
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
        var symmKey1 = (ulong)Math.Pow(pubKeys[1], privNum[0]) % p;
        var symmKey2 = (ulong)Math.Pow(pubKeys[0], privNum[1]) % p;

        if (symmKey1 == symmKey2)
        {
            Console.WriteLine("The symmetric key is " + symmKey1 + ".");
        }
    }
}