// See https://aka.ms/new-console-template for more information


// input: utf7 text
// input to cesar: utf7 bytes, shift amount
// do cesar
// cesar output - shifted bytes
// convert cesar output to text - base63


// decrypt
// input: base63 text, shift amount
// decode base63 to bytes
// shift
// convert bytes to utf7
// output


using ConsoleApp;

string userChoice; 
do
{
    Console.Write("Cesar, Vigenere or eXit (C, V, X)? : ");
    userChoice = InOut.GetChoice();
    switch (userChoice)
    {
        case "C":
            Cesar.DoCesar();
            break;
        case "V":
            Vigenere.DoVigenere();
            break;
        case "X":
            break;
        default:
            Console.WriteLine("Invalid input!");
            break;
    }
} while (userChoice != "X");