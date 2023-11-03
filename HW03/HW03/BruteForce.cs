
using System.Globalization;
using System.Text;

namespace HW03;

public static class BruteForce
{
    private static ulong _e, _n;
    public static void Run()
    {
        Console.WriteLine("Enter the modulus n part of public key:");
        _n = InOut.GetNumberInput();
        Console.WriteLine("Enter the e part of the public key:");
        _e = InOut.GetNumberInput();
        Console.WriteLine("Enter the encrypted text to decrypt:");
        var toDecrypt = InOut.GetStringInput();
        var r = new Random();

        ulong p = 0, q = 0;

        var boundary = System.Math.Sqrt(_n);
        for (ulong i = 3; i <= boundary; i += 2)
        {
            if (_n % i != 0) continue;
            if (!Primes.IsPrime(i, r)) continue;
            p = i;
            q = _n / i;
            break;
        }

        var lamn = Math.Lcm(p - 1, q - 1);
        var d = Math.ModInv.ModInverse(_e, lamn);

        var decText = Decrypt(toDecrypt, d);
        if (decText == null) return;
        Console.WriteLine("The private key is " + d + ".\nThe encrypted text is " + decText);
    }

    private static string? Decrypt(in string toDecrypt, ulong d)
    {
        if (!int.TryParse(toDecrypt.AsSpan(0, 2), out var blockLength))
        {
            Console.WriteLine("Could not parse block length!");
            return null;
        }
        
        
        var decryptedBytes = new List<byte>();
        for (var i = 2; i <= toDecrypt.Length - blockLength; i += blockLength)
        {
            var decryptedBlock = DecryptBlock(toDecrypt.Substring(i, blockLength), d);
            if (decryptedBlock == null)
            {
                Console.WriteLine("Decryption failed!");
                return null;
            }
            decryptedBytes.AddRange(decryptedBlock);
        }
        
        return Encoding.UTF8.GetString(decryptedBytes.ToArray());
    }
    
    private static byte[]? DecryptBlock(in string toDecrypt, in ulong d)
    {
        if (!ulong.TryParse(toDecrypt, NumberStyles.HexNumber, null, out var textNum))
        {
            Console.WriteLine("Error parsing input!");
            return null;
        }

        var decNum = Math.ModPow(textNum, d, _n);
        var toParse = decNum.ToString().Remove(0, 1);
        
        var bytes = new byte[toParse.Length / 3];

        for (var i = 3; i <= toParse.Length; i += 3)
        {
            bytes[i / 3 - 1] = (byte)int.Parse(toParse.Substring(i - 3, 3));
        }
        
        return bytes;
    }

    private static IEnumerable<ulong> Range(ulong fromInclusive, ulong toExclusive)
    {
        for (var i = fromInclusive; i < toExclusive; i++) yield return i;
    }

    private static void ParallelFor(ulong fromInclusive, ulong toExclusive, Action<ulong, ParallelLoopState> body)
    {
        Parallel.ForEach(
            Range(fromInclusive, toExclusive),
            new ParallelOptions
            {
                MaxDegreeOfParallelism = -1,
            },
            body);
    }
}