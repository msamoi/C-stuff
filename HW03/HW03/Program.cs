
using HW03;


string choice;
do
{
    Console.WriteLine("[R]SA encryption/decryption, [B]rute force decryption or e[X]it?");
    choice = InOut.GetChoice();
    switch (choice)
    {
        case "R":
            var keys = RsaInit();
            if (keys == null) break;
            RsaOps((Rsa)keys);
            break;
        case "B":
            BruteForce.Run();
            break;
        case "X":
            break;
    }
} while (choice != "X");
return;

Rsa? RsaInit()
{
    var r = new Random();
    var keys = new Rsa();
    string userChoice;
    do
    {
        Console.WriteLine("RSA:\n- [G]enerate random keys\n- [I]nput them yourself\n- e[X]it");
        userChoice = InOut.GetChoice();
        switch (userChoice)
        {
            case "G":
                keys.GenerateRandomKeys(r);
                return keys;
            case "I":
                keys.GetKeysFromUser(r);
                return keys;
            case "X":
                break;
        }
    } while (userChoice != "X");
    return null;
}

void RsaOps(Rsa keys)
{
    string userChoice;
    do
    {
        Console.WriteLine("RSA:\n- [E]ncrypt\n- [D]ecrypt\n- E[X]it");
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
}