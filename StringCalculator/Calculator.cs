using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using StringCalculator.Exceptions;

namespace StringCalculator
{
    public class Calculator
    {
        public Calculator()
        {
            //emtpy constructor for now
        }

        /// <summary>  
        ///  Takes in a string as described in assignment .
        ///  Calls ParseNumbers method to separate the string it into a list of numbers based off of delimiters.
        ///  Calls SumInput to validate numbers and calculate a sum.
        /// </summary>
        /// <param name="numberString">String consisting of delimiter information and numbers to sum</param>
        /// <returns>The sum of the numbers entered</returns>
        public int Add(string numberString)
        {
            int sum = 0;
            if (!string.IsNullOrEmpty(numberString))  //non-empty string
            {
                string[] numbers = ParseNumbers(numberString);

                try
                {
                    sum = SumInput(numbers);
                }
                catch (Exception)
                {
                    throw;  //rethrow exception up to the caller while preserving the stack trace
                }
            }
            return sum;
        }

        /// <summary>
        /// Uses delimiters to brake the numbers down to an array of numbers.
        /// Figures out if any custom delimiters are being defined and usees those in additon to the default delimiter of ',' and "\n".
        /// </summary>
        /// <param name="numberString">String consisting of delimiter information and numbers.</param>
        /// <returns>An array of strings representing the numbers.</returns>
        public string[] ParseNumbers(string numberString)
        {
            int startOfNumberString = 0;
            List<string> delimiterList = new List<string>();
            delimiterList.Add(",");
            delimiterList.Add("\n");

            string[] customDelimiters = GetCustomDelimiters(numberString);
            foreach (string customDelimiter in customDelimiters)
            {
                delimiterList.Add(customDelimiter);
                startOfNumberString = numberString.IndexOf("\n") + 1;  //used to remove the custom delimiter portion of string
            }         

            //string customDelimiter = GetCustomNonBracketedDelimiter(numberString);
            /*if (customDelimiter != null)
            {
                delimiterList.Add(customDelimiter);
                startOfNumberString = numberString.IndexOf("\n") + 1;  //used to remove the custom delimiter portion of string
            }*/

            return numberString.Substring(startOfNumberString).Split(delimiterList.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        public string[] GetCustomDelimiters(string numberString)
        {
            //use regex to pull just the portion of the string with delimiters
            //This is the area between the opening // and the first \n
            string delimiterDefinedPattern = @"^//(\[?.*\]?)\n";
            Match delimitersFound = Regex.Match(numberString, delimiterDefinedPattern);

            if (delimitersFound.Success)
            {
                int delimiterCharLength = delimitersFound.Value.Length - 3;  //subtract 3 to account for the // and \n
                string delimiterPortion = delimitersFound.Value.Substring(2, delimiterCharLength);
                                
                //use second regex to determine if there is a matchig set of open and close brackets
                string bracketSetPattern = @"^(\[.*\])";
                Match bracketedDelimitersFound = Regex.Match(delimiterPortion, bracketSetPattern);

                
                if (bracketedDelimitersFound.Success)
                {
                    //todo for now
                    return new string[] { "todo" };
                }
                else
                {
                    //handle non-bracketed (single) delimiter
                    return new string[] { delimiterPortion };
                }
            }
            else
            {
                return new string[] {};
            }
        }

        /// <summary>
        /// Finds custom delimiter in string array.  Currently only supports one delimiter.
        /// </summary>
        /// <param name="numberString">String consisting of delimiter information and numbers.</param>
        /// <returns>The custom delimiter if found, otherwise returns null.</returns>
        public string GetCustomNonBracketedDelimiter(string numberString)
        {
            int endDelimiterMarker = 0;

            //determine if custom delimiters are defined based off the beginning of the string
            if (numberString.Length >= 2 && numberString.Substring(0,2) == "//")
            {
                endDelimiterMarker = numberString.IndexOf("\n");
                if (endDelimiterMarker != -1)
                {
                    return numberString.Substring(2, endDelimiterMarker - 2);
                }
            }
            return null;
        }

        /// <summary>
        /// Calculates sum of an array of numbers.
        /// Will throw FormatException if it receive a non integer in the array.
        /// Will throw NegativeIntegerException if it receives one or more negative integers in the array.
        /// </summary>
        /// <param name="numbers">String array representation of numbers to sum.</param>
        /// <returns>Sum as an int.</returns>
        public int SumInput(string[] numbers)
        {
            int curNum = 0;
            int sum = 0;
            List<string> negativeInts = new List<string>();

            //Calculate sum
            foreach (var number in numbers)
            {
                curNum = Int32.Parse(number);
                if (curNum < 0)
                {
                    negativeInts.Add(number);
                }

                //numbers that are 1001 or greater will be ignored according to requirement 6
                if (curNum <= 1000)  
                {
                    sum = sum += curNum;
                }                
            }
            
            //Detect negative numbers
            if (negativeInts.Count > 0)
            {
                string errorMsg = "Detected invalid input, negative numbers are not allowed.  Negative Detected: ";
                string negativeMsg = string.Join(" ", negativeInts.ToArray());
                errorMsg = errorMsg + negativeMsg;                
                throw new NegativeIntegerException(errorMsg);
            }
            return sum;
        }
    }
}
