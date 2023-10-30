/*
Key generation

The keys for the RSA algorithm are generated in the following way:

    Choose two large prime numbers p and q.
        To make factoring harder, p and q should be chosen at random, be both large and have a large difference.[1] For choosing them the standard method is to choose random integers and use a primality test until two primes are found.
        p and q should be kept secret.
    Compute n = pq.
        n is used as the modulus for both the public and private keys. Its length, usually expressed in bits, is the key length.
        n is released as part of the public key.
    Compute λ(n), where λ is Carmichael's totient function. Since n = pq, λ(n) = lcm(λ(p), λ(q)), and since p and q are prime, λ(p) = φ(p) = p − 1, and likewise λ(q) = q − 1. Hence λ(n) = lcm(p − 1, q − 1).
        The lcm may be calculated through the Euclidean algorithm, since lcm(a, b) = |ab|/gcd(a, b).
        λ(n) is kept secret.
    Choose an integer e such that 2 < e < λ(n) and gcd(e, λ(n)) = 1; that is, e and λ(n) are coprime.
        e having a short bit-length and small Hamming weight results in more efficient encryption – the most commonly chosen value for e is 216 + 1 = 65537. The smallest (and fastest) possible value for e is 3, but such a small value for e has been shown to be less secure in some settings.[15]
        e is released as part of the public key.
    Determine d as d ≡ e−1 (mod λ(n)); that is, d is the modular multiplicative inverse of e modulo λ(n).
        This means: solve for d the equation de ≡ 1 (mod λ(n)); d can be computed efficiently by using the extended Euclidean algorithm, since, thanks to e and λ(n) being coprime, said equation is a form of Bézout's identity, where d is one of the coefficients.
        d is kept secret as the private key exponent.
        
*/

using HW03;

var r = new Random();
var keys = new RSA();
keys.GenerateRandomKeys(r);
string userChoice;
do
{
    Console.WriteLine("RSA:\n[E]ncrypt, [D]ecrypt or E[X]it?");
    // keys.MakePresetKeys();
    userChoice = InOut.GetChoice();
    switch (userChoice)
    {
        case "E":
            keys.EncryptBlocks();
            break;
        case "D":
            keys.DecryptBlocks();
            break;
        case "X":
            break;
    }
} while (userChoice != "X");