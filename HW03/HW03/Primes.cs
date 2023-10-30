
namespace HW03;

public static class Primes
{
    public static bool IsPrime(ulong n, Random r)
    {
        if (n < 2) return false;
        // first check divisibility by first 10 primes
        ulong[] ar = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };
        foreach (var prime in ar)
        {
            if (n % prime == 0) return false;
        }

        return MilRab(n, 20, r); // only use miller-rabin if number isn't divisible by first primes
    }

    // n is the number to check, k is the certainty to check
    private static bool MilRab(ulong n, ulong k, Random r)
    {
        var s = n - 1;
        while (s % 2 == 0)  s >>= 1;
        
        for (ulong i = 0; i < k; i++)
        {
            var a = r.NextLong(n - 1) + 1;
            var temp = s;
            var mod = Math.ModPow(a, temp, n);
            while (temp != n - 1 && mod != 1 && mod != n - 1)
            {
                mod = (mod * mod) % n;
                temp *= 2;
            }
            if (mod != n - 1 && temp % 2 == 0) return false;
        }
        return true;
    }
}