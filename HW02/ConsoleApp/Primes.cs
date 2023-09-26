namespace ConsoleApp;

public class Primes
{
    public static long[] GetPrimes(long toFactor)
    {
        long[] outvar = { 0, 0 };
        var boundary = Math.Sqrt(toFactor);
        for (long i = 2; i < boundary; i++)
        {
            if (!IsPrime(i)) continue;
            Console.WriteLine(i);
            if (toFactor % i == 0)
            {
                outvar[0] = i;
                outvar[1] = toFactor / i;
                break;
            }
        }

        return outvar;
    }
    
    private static bool IsPrime(long number)
    {
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (long)Math.Floor(Math.Sqrt(number));
          
        for (long i = 3; i <= boundary; i += 2)
            if (number % i == 0)
                return false;
    
        return true;        
    }
}