using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator
{
    public class Calculator
    {
        public Calculator()
        {
            //emtpy constructor for now
        }

        /// <summary>  
        ///  Takes in a string and separates it into a list of numbers based off a delimiter and returns the sum
        /// </summary>
        public int Add(string numberString)
        {
            if (string.IsNullOrEmpty(numberString))
            {
                return 0;
            }
            else
            {
                int sum = 0;
                int startOfNumberString = 0;
                string customDelimiter = GetCustomDelimiter(numberString);
                List<string> delimiterList = new List<string>();
                delimiterList.Add(",");
                delimiterList.Add("\n");
                if (customDelimiter != null)
                {
                    delimiterList.Add(customDelimiter);
                    startOfNumberString = numberString.IndexOf("\n") + 1;  //used to remove the custom delimiter portion of string
                }

                string[] numbers = numberString.Substring(startOfNumberString).Split(delimiterList.ToArray(), StringSplitOptions.RemoveEmptyEntries);

                foreach (var number in numbers)
                {
                    sum += Int32.Parse(number);
                }

                return sum;
            }
        }

        public string GetCustomDelimiter(string numberString)
        {
            int endDelimiterMarker = 0;

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
    }
}
