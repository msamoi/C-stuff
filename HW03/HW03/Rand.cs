namespace HW03;

public static class Rand
{
    public static ulong GetRandomPrime(this Random random, ulong max) // extremely simplistic, but it gets the job done
    {
        while(true)
        {
            var p = random.NextLong(max);
            if (p % 2 == 0) continue;

            if (Primes.IsPrime(p, random)) return p;
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
}