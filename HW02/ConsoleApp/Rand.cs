
namespace ConsoleApp;

public static class Rand 
{
    public static ulong GetRandomPrime(this Random random, ulong max) // extremely simplistic, but it gets the job done
    {
        while(true)
        {
            var p = random.NextLong(max);
            if (p % 2 == 0) continue;

            if (Primes.IsPrime(p)) return p;
        }
    }
    public static ulong NextLong(this Random random, ulong max)
    {
        if (max == 0) return 0;
        
        var buf = new byte[8];
        random.NextBytes(buf);
        var ulongRand = BitConverter.ToUInt64(buf, 0);
        
        return ulongRand % max;
    }

    public static ulong NextLong(this Random random)
    {
        return random.NextLong(ulong.MaxValue);
    }
    
    public static ulong Power(ulong x, ulong y, ulong p)
    {
        UInt128 res = 1;
        
        UInt128 nbase = x;
        
        nbase %= p;
 
        while (y > 0) 
        {
            if (y % 2 == 1) 
            {
                res = (res * nbase) % p;
            }
            y >>= 1; 
            nbase = (nbase * nbase) % p;
        }
        return (ulong)res;
    }

    private static void FindPrimefactors(HashSet<ulong> s, ulong n) 
    {
        while (n % 2 == 0) 
        {
            s.Add(2);
            n /= 2;
        }
        
        for (ulong i = 3; i <= Math.Sqrt(n); i += 2) 
        {
            while (n % i == 0) 
            {
                s.Add(i);
                n /= i;
            }
        }
 
        if (n > 2) 
        {
            s.Add(n);
        }
    }
 
    public static ulong FindPrimitive(this Random random, ulong n) 
    {
        var s = new HashSet<ulong>();
 
        if (Primes.IsPrime(n) == false) 
        {
            return 0;
        }
 
        var phi = n - 1;
 
        FindPrimefactors(s, phi);

        while(true) {
            var randomNum = random.NextLong(phi);
            var flag = s.Any(a => Power(randomNum, phi / a, n) == 1);
            if (flag == false)
            {
                return randomNum;
            }
        }
    }
}