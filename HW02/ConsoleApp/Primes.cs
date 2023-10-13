
namespace ConsoleApp;

public static class Primes
{
    public static void GetPrimes()
    {
        Console.WriteLine("Enter p1*p2 to factorize:");
        ulong toFactor = InOut.GetNumberFromUser();
        ulong[] outvar = { 0, 0 };
        var boundary = Math.Sqrt(toFactor);
        if (toFactor % 2 == 0)
        {
            var res = toFactor / 2;
            if (!IsPrime(res))
            {
                Console.WriteLine("Input is not a product of two primes!");
                return;
            }
            outvar[0] = 2;
            outvar[1] = res;
        }
        for (ulong i = 3; i <= boundary; i += 2)
        {
            if (toFactor % i != 0) continue;
            if (!IsPrime(i)) continue;
            outvar[0] = i;
            outvar[1] = toFactor / i;
            break;
        }
        if (outvar[0] == 0 && outvar[1] == 0) Console.WriteLine("Input is not a product of two primes!");
        Console.WriteLine("The prime numbers that make up " + toFactor + " are:\n" +
                          outvar[0] + " and " + outvar[1] + ".");
   }
    
    public static bool IsPrime(ulong number)
    {
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (ulong)Math.Floor(Math.Sqrt(number));
          
        for (ulong i = 3; i <= boundary; i += 2)
            if (number % i == 0)
                return false;
    
        return true;        
    }

    public static bool IsPrimitiveRoot(ulong p, ulong g)
    {
        List<double> mods = new List<double>();
        for (ulong i = 1; i < p; i++)
        {
            var tmp = Math.Pow(g, i) % p;
            if (mods.Contains(tmp)) return false;
            mods.Add(tmp);
        }
        return true;
    }
}