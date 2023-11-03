
using System.Globalization;
using System.Text;

namespace HW03;

public struct Rsa
{
    private ulong _p, _q, _n, _lamn, _e, _d;
    private readonly Random _r = new();

    public Rsa()
    {
        _p = 0;
        _q = 0;
        _n = 0;
        _lamn = 0;
        _e = 0;
        _d = 0;
    }

    public void GenerateRandomKeys()
    {
        _p = _r.GetRandomPrime(uint.MaxValue);
        _q = _r.GetRandomPrime(uint.MaxValue);
        _n = _p * _q;
        _lamn = Math.Lcm(_p - 1, _q - 1);
        _e = 65537;
        _d = Math.ModInv.ModInverse(_e, _lamn);
        Console.WriteLine("Generated keys:\n" +
                          $"n: {_n}\n" +
                          $"e: {_e}\n" +
                          $"d: {_d}\n");
    }

    public void GetKeysFromUser()
    {
        var success = false;
        do
        {
            do
            {
                Console.WriteLine("Enter the prime number p:");
                _p = InOut.GetNumberInput();
            } while (!Primes.IsPrime(_p, _r));

            do
            {
                Console.WriteLine("Enter the prime number q:");
                _q = InOut.GetNumberInput();
            } while (!Primes.IsPrime(_q, _r));

            _n = _p * _q;
            if (_n.ToString().Length < 5)
            {
                Console.WriteLine("n is less than 5 digits long. " +
                                  "This is not enough to encrypt text. " +
                                  "Please enter larger values for p and q.");
                continue;
            }

            success = true;
        } while (!success);

        _lamn = Math.Lcm(_p - 1, _q - 1);
        do
        {
            Console.WriteLine("Enter the integer e (must also be prime)");
            _e = InOut.GetNumberInput();
        } while (!Primes.IsPrime(_e, _r));

        _d = Math.ModInv.ModInverse(_e, _lamn);
        Console.WriteLine("Generated keys:\n" +
                          $"n: {_n}\n" +
                          $"e: {_e}\n" +
                          $"d: {_d}\n");
    }

    /*
     The Encrypt function encrypts a string into ciphertext in blocks.
     Each block is 4 bytes long. The input string is deconstructed into bytes, and is then encrypted with the public key
     in 4 byte blocks. The construction of the blocks is detailed above the EncryptBlocks function.

     The encrypted blocks don't have a fixed length. To tackle this, during encryption, the longest encrypted block is
     found and every other block is padded with leading zeros, which are easily lost by just parsing the block as a
     number. The normalized block length is then placed as the first two digits of the whole ciphertext. e.g.
     19(large amount of numbers) would mean that each block is 19 digits long. These two leading digits are trimmed off
     the ciphertext before starting to decrypt blocks, and exist only for the function itself.
     */
    public void Encrypt()
    {
        Console.WriteLine("Enter string to encrypt:");
        var toEncrypt = InOut.GetStringInput();
        var bytes = Encoding.UTF8.GetBytes(toEncrypt);
        var encryptedBlocks = new List<string>();

        // variable block length based on the n value
        // at least 5-digit n values are pretty much mandatory, as encrypting one byte is a 4-digit operation due to how
        // our block encryption works
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
                return;
            }
            encryptedBlocks.Add(tmp);
        }
        
        // find the length of the longest encrypted block
        var maxLen = encryptedBlocks.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur).Length;
        var outString = "";
        
        // set first two characters of the encrypted string to the block length
        if (maxLen < 10) outString += "0";
        outString += maxLen.ToString();
        
        for (var i = 0; i < encryptedBlocks.Count; i++)
        {
            // pad each encrypted block to be the same length
            while (encryptedBlocks[i].Length < maxLen)
            {
                encryptedBlocks[i] = encryptedBlocks[i].Insert(0, "0");
            }
            // append each padded block to the output string
            outString += encryptedBlocks[i];
        }

        Console.WriteLine("Encrypted blocks: ");
        Console.WriteLine(outString);
    }

    /*
     The Decrypt function is just a reversal of the Encrypt function (surprise)
     It takes a string input from the user, which is presumed to be a ciphertext generated by this same program.
     It then isolates the block length variable from the first two digits of the ciphertext, and trims them from the
     string. The string is then fed into the DecryptBlock function block by block, which handles actually decrypting
     each block.
     */
    public void Decrypt()
    {
        Console.WriteLine("Enter encrypted string to decrypt:");
        int blockLength;
        string toDecrypt;
        while (true)
        {
            toDecrypt = InOut.GetStringInput();
            // get the first two characters and write them to blockLength
            if (int.TryParse(toDecrypt.AsSpan(0, 2), out blockLength)) break;
            Console.WriteLine("Error parsing block length!");
        }

        // remove the block length from the string as its no longer needed
        toDecrypt = toDecrypt.Remove(0, 2);

        // store the decrypted text as bytes at first because blocks might not line up with multi-byte characters
        var decryptedBytes = new List<byte>();
        // feed each block into the DecryptBlock function
        for (var i = 0; i <= toDecrypt.Length - blockLength; i += blockLength)
        {
            var decryptedBlock = DecryptBlock(toDecrypt.Substring(i, blockLength));
            if (decryptedBlock == null)
            {
                Console.WriteLine("Decryption failed!");
                return;
            }
            // add each block of decrypted bytes to the list of bytes
            decryptedBytes.AddRange(decryptedBlock);
        }
        
        Console.WriteLine("Decrypted text:");
        // finally convert the entire byte array to a string when the entire structure is complete
        Console.WriteLine(Encoding.UTF8.GetString(decryptedBytes.ToArray()));
    }

    /*
     Functionality of EncryptBlock
     Consider the string "AAAA". The letter A is a single-byte value, and the value representing it is 65. Since we have
     four A-s, the string could be represented as 65,65,65,65. For encryption we consider the entire string a number instead.
     This number would logically be 65656565, but the numeric values of bytes can be up to 3 digits long, so we pad each
     repetition with a 0: 065065065065. Since a number can't start with a zero, we also start this number with a "1"
     by default. Thus, the string "AAAA" will become 1065065065065 for encryption purposes.
     
     In case of multi-byte characters, the blocks and characters might not line up. This along with block byte size
     consistency is the reason for using byte count instead of char count for blocks. When we consider the whole string
     as an array of bytes for everything except displaying it, then we can easily handle multi-byte characters.
     */
    private string? EncryptBlock(IEnumerable<byte> bytes)
    {
        var toNumber = _r.Next(1, 10).ToString();
        
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

    /*
     DecryptBlock takes a string presumed to be a block encrypted by the EncryptBlock function. It decrypts the block,
     parses it as a string, trims the leading "1" added by EncryptBlock, and parses every 3 digits of the decrypted
     number as bytes.
     e.g.: Decrypting the ciphertext yields the number 1065065065065.
     The function will then trim the 1: 065065065065
     It will then create a byte array with a third of the length of the number: 12/3 = 4, thus new byte[4]
     Then the number will be split into pieces of 3 digits, and put into the byte array:
     bytes = [ 65, 65, 65, 65 ]
     which are our original bytes.
     */
    private byte[]? DecryptBlock(string toDecrypt)
    {
        if (!ulong.TryParse(toDecrypt, NumberStyles.HexNumber, null, out var textNum))
        {
            Console.WriteLine("Error parsing input!");
            return null;
        }

        var decNum = Math.ModPow(textNum, _d, _n);
        var toParse = decNum.ToString();

        toParse = toParse.Remove(0, 1);
        var bytes = new byte[toParse.Length / 3];

        for (var i = 3; i <= toParse.Length; i += 3)
        {
            bytes[i / 3 - 1] = (byte)int.Parse(toParse.Substring(i - 3, 3));
        }
        
        return bytes;
    }
}