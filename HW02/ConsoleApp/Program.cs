// See https://aka.ms/new-console-template for more information

using ConsoleApp;

string userChoice;
do
{
    Console.WriteLine("[D]iffie-Hellman, [F]actorize p1*p2 or E[X]it?");
    userChoice = InOut.GetChoice();
    switch (userChoice)
    {
        case "D":
            KeyExchange.Init();
            break;
        case "F":
            Primes.GetPrimes();
            break;
        case "X":
            break;
    }
} while (userChoice != "X");


// Project for the Diffie-Hellman algorithm implementation + prime number finder