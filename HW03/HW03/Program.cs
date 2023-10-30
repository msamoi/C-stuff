
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
            keys.EncryptBlocks();
            break;
        case "D":
            keys.DecryptBlocks();
            break;
        case "X":
            break;
    }
} while (userChoice != "X");