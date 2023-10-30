namespace HW03;

public class Math
{
    public static ulong ModPow(ulong x, ulong y, ulong p)
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
    
    private static ulong Gcf(ulong a, ulong b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static ulong Lcm(ulong a, ulong b)
    {
        return (a / Gcf(a, b)) * b;
    }

    public static class ModInv
    {
        public static Int128 x, y;
        public static Int128 GcdExtended(ulong a, ulong b)
        {
            
            if (a == 0) {
                x = 0;
                y = 1;
                return b;
            }
            
            Int128 gcd = GcdExtended(b % a, a);
            Int128 x1 = x;
            Int128 y1 = y;
            
            x = (y1 - (b / a) * x1);
            y = x1;
 
            return gcd;
        }
 
        // Function to find modulo inverse of a
        public static ulong ModInverse(ulong A, ulong M)
        {
            var g = GcdExtended(A, M);
            Int128 res = 0;
            if (g != 1)
                Console.Write("Inverse doesn't exist");
            else {
 
                // M is added to handle negative x
                res = (x % M + M) % M;
            }
            return (ulong)res;
        }
    }

}