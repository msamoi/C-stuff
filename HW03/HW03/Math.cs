namespace HW03;

public static class Math
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
        private static Int128 _x, _y;

        private static Int128 GcdExtended(ulong a, ulong b)
        {
            
            if (a == 0) {
                _x = 0;
                _y = 1;
                return b;
            }
            
            Int128 gcd = GcdExtended(b % a, a);
            Int128 x1 = _x;
            Int128 y1 = _y;
            
            _x = (y1 - (b / a) * x1);
            _y = x1;
 
            return gcd;
        }
 
        // Function to find modulo inverse of a
        public static ulong ModInverse(ulong a, ulong m)
        {
            var g = GcdExtended(a, m);
            Int128 res = 0;
            if (g != 1)
                Console.Write("Inverse doesn't exist");
            else {
 
                // M is added to handle negative x
                res = (_x % m + m) % m;
            }
            return (ulong)res;
        }
    }

}