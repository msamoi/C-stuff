namespace ConsoleApp;

public struct Keys
{
    public ulong P, G, A, B, X, Y, SymmKey;

    public Keys(ulong limit)
    {
        var random = new Random();
        P = random.GetRandomPrime(limit);
        G = random.FindPrimitive(P);
        A = random.NextLong(limit);
        B = random.NextLong(limit);
    }
    
    public void GetPublicNumbers(ulong max)
    {
        while (true)
        {
            Console.WriteLine("Enter the prime number P");
            P = InOut.GetNumberFromUser();
            if (!Primes.IsPrime(P)) continue;
            if (P > max) continue;
            break;
        }

        while (true)
        {
            Console.WriteLine("Enter the primitive root G");
            G = InOut.GetNumberFromUser();
            if (!Primes.IsPrimitiveRoot(P, G)) continue;
            break;
        }
    }

    public void GetPrivateNumbers(ulong max)
    {
        while (true)
        {
            Console.WriteLine("Enter the private number for the first person (a):");
            A = InOut.GetNumberFromUser();
            if (A > max) continue;
            break;
        }

        while (true)
        {
            Console.WriteLine("Enter the private number for the second person (b):");
            B = InOut.GetNumberFromUser();
            if (B > max) continue;
            break;
        }
    }

    public void GeneratePublicKeys()
    {
        X = Rand.Power(G, A, P);
        Y = Rand.Power(G, B, P);
    }

    public void GenerateSymmetricKey()
    {
        var symmKey1 = Rand.Power(Y, A, P);
        var symmKey2 = Rand.Power(X, B, P);

        if (symmKey1 == symmKey2)
        {
            SymmKey = symmKey1;
        }
        else Console.WriteLine("Symmetric key does not match!");
    }
}