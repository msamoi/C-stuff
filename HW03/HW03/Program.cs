
using HW03;

var r = new Random();
var keys = new Rsa();
keys.GenerateRandomKeys(r);
string userChoice;
do
{
    Console.WriteLine("RSA:\n[E]ncrypt, [D]ecrypt or E[X]it?");
    userChoice = InOut.GetChoice();
    switch (userChoice)
    {
        case "E":
            keys.Encrypt();
            break;
        case "D":
            keys.Decrypt();
            break;
        case "X":
            break;
    }
} while (userChoice != "X");