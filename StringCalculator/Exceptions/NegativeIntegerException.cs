using System;

namespace StringCalculator.Exceptions
{
    public class NegativeIntegerException : Exception
    {
        /// <summary>
        /// Custom exception for requirement 5
        /// </summary>
        /// <param name="message"></param>
        public NegativeIntegerException(string message) : base(message)
        {
        }
    }
}