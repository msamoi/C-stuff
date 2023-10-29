
using System.Text;

namespace HW03;

public struct RSA
{
    public ulong p, q, n, lamn, e, d;

    public void GenerateRandomKeys(Random r)
    {
        p = r.GetRandomPrime(uint.MaxValue);
        q = r.GetRandomPrime(uint.MaxValue);
        n = p * q;
        lamn = Math.Lcm(p - 1, q - 1);
        e = 65537;
        d = Math.ModInv.ModInverse(e, lamn);
    }

    public void Encrypt()
    {
        Console.WriteLine("Enter the string to encrypt:");
        var toEncrypt = InOut.GetStringInput();
        var bytes = Encoding.UTF8.GetBytes(toEncrypt);

        ulong[] outVar = new ulong[bytes.Length];

        Console.WriteLine("Encrypted text:");
        for (int i = 0; i < bytes.Length; i++)
        {
            if (i > 0) Console.Write(",");
            outVar[i] = Math.Power(bytes[i], e, n);
            Console.Write(outVar[i]);
        }
        Console.WriteLine();
    }

    public void Decrypt()
    {
        Console.WriteLine("Enter the ciphertext to decrypt:");
        var toDecrypt = InOut.GetUlongArrayInput();
        var bytes = new byte[toDecrypt.Length];
        
        for (int i = 0; i < toDecrypt.Length; i++)
        {
            bytes[i] = (byte)Math.Power(toDecrypt[i], d, n);
        }
        
        Console.WriteLine("Decrypted text:");
        Console.WriteLine(Encoding.UTF8.GetString(bytes));
    }
}