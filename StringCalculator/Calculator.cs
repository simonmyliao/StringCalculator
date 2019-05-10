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
                string[] numbers = numberString.Split(',');

                foreach (var number in numbers)
                {
                    sum += Int32.Parse(number);
                }

                return sum;
            }
        }
    }
}
