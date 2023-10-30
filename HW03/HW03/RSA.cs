
using System.Globalization;
using System.Text;

namespace HW03;

public struct Rsa
{
    private ulong _p, _q, _n, _lamn, _e, _d;

    public void GenerateRandomKeys(Random r)
    {
        _p = r.GetRandomPrime(uint.MaxValue);
        _q = r.GetRandomPrime(uint.MaxValue);
        _n = _p * _q;
        _lamn = Math.Lcm(_p - 1, _q - 1);
        _e = 65537;
        _d = Math.ModInv.ModInverse(_e, _lamn);
    }

    public void EncryptBlocks()
    {
        Console.WriteLine("Enter string to encrypt:");
        var toEncrypt = InOut.GetStringInput();
        var encryptedBlocks = new List<string>();
        
        for (var i = 0; i < toEncrypt.Length; i += 4)
        {
            encryptedBlocks.Add(i + 4 > toEncrypt.Length
                ? Encrypt(toEncrypt.Substring(i, toEncrypt.Length - i))
                : Encrypt(toEncrypt.Substring(i, 4)));
        }
        
        var maxLen = encryptedBlocks.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur).Length;
        var outString = "";
        if (maxLen < 10) outString += "0";
        outString += maxLen.ToString();
        
        for (var i = 0; i < encryptedBlocks.Count; i++)
        {
            while (encryptedBlocks[i].Length < maxLen)
            {
                encryptedBlocks[i] = encryptedBlocks[i].Insert(0, "0");
            }

            outString += encryptedBlocks[i];
        }

        Console.WriteLine("Encrypted blocks: ");
        Console.WriteLine(outString);
    }

    public void DecryptBlocks()
    {
        Console.WriteLine("Enter encrypted string to decrypt:");
        int blockLength;
        string toDecrypt;
        while (true)
        {
            toDecrypt = InOut.GetStringInput();
            if (int.TryParse(toDecrypt.Substring(0, 2), out blockLength)) break;
            Console.WriteLine("Error parsing block length!");
        }

        toDecrypt = toDecrypt.Remove(0, 2);

        var decryptedText = "";
        for (var i = 0; i <= toDecrypt.Length - blockLength; i += blockLength)
        {
            decryptedText += Decrypt(toDecrypt.Substring(i, blockLength));
        }
        
        Console.WriteLine("Decrypted text:");
        Console.WriteLine(decryptedText);
    }


    private string Encrypt(string toEncrypt)
    {
        var bytes = Encoding.UTF8.GetBytes(toEncrypt);
        var toNumber = "1";
        
        foreach (var cByte in bytes)
        {
            if (cByte < 10) toNumber += "00";
            else if (cByte < 100) toNumber += "0";
            toNumber += cByte.ToString();
        }

        if (!ulong.TryParse(toNumber, out var textNum))
        {
            Console.WriteLine("Error parsing input!");
            return "";
        }

        var outVar = Math.ModPow(textNum, _e, _n);
        return outVar.ToString("X");
    }

    private string Decrypt(string toDecrypt)
    {
        if (!ulong.TryParse(toDecrypt, NumberStyles.HexNumber, null, out var textNum))
        {
            Console.WriteLine("Error parsing input!");
            return "";
        }

        var decNum = Math.ModPow(textNum, _d, _n);
        var toParse = decNum.ToString();

        toParse = toParse.Remove(0, 1);
        var bytes = new byte[toParse.Length / 3];

        for (var i = 3; i <= toParse.Length; i += 3)
        {
            bytes[i / 3 - 1] = (byte)int.Parse(toParse.Substring(i - 3, 3));
        }
        
        return Encoding.UTF8.GetString(bytes);
    }
}