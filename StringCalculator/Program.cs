using System;
using System.Text.RegularExpressions;
using StringCalculator.Exceptions;

namespace StringCalculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter string and press Enter to continue...");
            string enteredString = Console.ReadLine();

            Console.WriteLine($"\nCalling Add method with input string: {enteredString}\n");

            Calculator calc = new Calculator();
            string filteredString = FilterAdditionalBackslash(enteredString);
            
            try
            {
                int sum = calc.Add(filteredString);
                Console.WriteLine($"The sum is: {sum}\n");
            }
            catch (FormatException fex)
            {
                Console.WriteLine("Sum could not be calculated.  Detected string that could not be converted to an integer.  Please check your input is valid.");
                Console.WriteLine(fex.Message);
            }        
            catch (NegativeIntegerException niex)
            {
                Console.WriteLine("Sum could not be calculated.  Detected one or more negative integers.  Please check your input is valid.");
                Console.WriteLine(niex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sum could not be calculated. Please check your input is valid.");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nPress Enter key to exit");
            Console.ReadLine();
        }

        /// <summary>
        /// .Net is adding an additional escape when taking input through Console.Readline that has escaped characters.  
        /// It also removes this extra escape character when displaying back to the user through Console.Writeline.
        /// We need to filter out the additional escaped backslash to support taking newline as part of the string as in the examples of the assignment
        /// </summary>
        /// <param name="enteredString"></param>
        /// <returns>The strings like "\\n" are replaced by "\n"</returns>
        private static string FilterAdditionalBackslash(string enteredString)
        {
            return Regex.Unescape(enteredString);
        }
    }
}
