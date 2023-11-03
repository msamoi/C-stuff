
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
        
        const string testString = "Aia!saiasadu.";
        var encTest = Encrypt(testString);
        if (encTest == null) return;

        ulong d = 0;
        
        for (ulong i = 0; i < ulong.MaxValue; i++)
        {
            var decTest = Decrypt(encTest, i);
            if (decTest == testString)
            {
                d = i;
                break;
            }
        }

        var decText = Decrypt(toDecrypt, d);
        if (decText == null) return;
        Console.WriteLine("The private key is " + d + ".\nThe encrypted text is " + decText);
    }

    private static string? Encrypt(in string toEncrypt)
    {
        var bytes = Encoding.UTF8.GetBytes(toEncrypt);
        var encryptedBlocks = new List<string>();

        var blockSize = _n.ToString().Length / 3 - 1;
        
        switch (blockSize)
        {
            case < 1:
                blockSize = 1;
                break;
            case > 6:
                blockSize = 6;
                break;
        }

        for (var i = 0; i < bytes.Length; i += blockSize)
        {
            // feed the string into EncryptBlock 6 bytes at a time
            var tmp = i + blockSize > bytes.Length
                ? EncryptBlock(bytes[i..]) // if there are less than 6 bytes remaining, just encrypt the remainder
                : EncryptBlock(bytes[i..(i + blockSize)]); // if there are more than 6 bytes remaining, encrypt the next 6
            if (tmp == null)
            {
                Console.WriteLine("Encryption failed!");
                return null;
            }
            encryptedBlocks.Add(tmp);
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
        
        return outString;
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
    
    private static string? EncryptBlock(in IEnumerable<byte> bytes)
    {
        var toNumber = "1";
        
        foreach (var cByte in bytes)
        {
            switch (cByte)
            {
                case < 10:
                    toNumber += "00";
                    break;
                case < 100:
                    toNumber += "0";
                    break;
            }

            toNumber += cByte.ToString();
        }

        if (!ulong.TryParse(toNumber, out var textNum))
        {
            Console.WriteLine("Error parsing input!");
            return null;
        }

        if (textNum > _n)
        {
            Console.WriteLine("Number to encrypt cannot be bigger than n!");
            return null;
        }
        var outVar = Math.ModPow(textNum, _e, _n);
        return outVar.ToString("X");
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
}