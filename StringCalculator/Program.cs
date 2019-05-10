using System;

namespace StringCalculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter string and press Enter to coninue...");
            string enteredString = Console.ReadLine();

            Console.WriteLine($"\nCalling Add method with input string: {enteredString}\n");
            Calculator calc = new Calculator();

            int sum = calc.Add(enteredString);
            Console.WriteLine($"The sum is: {sum}\n");

            Console.WriteLine("Press Enter key to exit");
            Console.ReadLine();
        }
    }
}
