namespace ConsoleApp;

public static class InOut
{
   public static ulong GetNumberFromUser()
   {
      do
      {
         var input = Console.ReadLine();
         if (ulong.TryParse(input, out var outvar)) return outvar;
         Console.WriteLine("Input is not a number!");
      } while (true);
   }
   
   public static string GetChoice()
   {
      var userChoice = Console.ReadLine()?.ToUpper().Trim();
      return String.IsNullOrEmpty(userChoice) ? "" : userChoice;
   }
}