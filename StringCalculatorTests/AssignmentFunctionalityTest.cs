using StringCalculator;
using StringCalculator.Exceptions;
using System;
using Xunit;

namespace StringCalculatorTests
{

    //*********************************************************************************************************************************************/
    // These are functionality test per requirement in the assignment. 
    // If you want regular unit tests, see CalculatorUnitTests.cs
    //*********************************************************************************************************************************************/

    public class AssignmentFunctionalityTest
    {
        Calculator calc;

        public AssignmentFunctionalityTest()
        {
            calc = new Calculator();
        }

        /// <summary>
        /// Tests functionality in requirement 1.
        /// 1.	Create a simple String calculator with a method int Add(string numbers) 
        /// 1.	The method can take 0, 1 or 2 numbers, and will return their sum(for an empty string it will return 0) for example “” or “1” or “1,2”//
        /// </summary>
        [Theory]
        [InlineData("", 0)]
        [InlineData("1", 1)]
        [InlineData("1,2", 3)]
        public void TestFunctionality1(string numberString, int expected)
        {
            //Act
            int sum = calc.Add(numberString);

            //Assert
            Assert.Equal(expected, sum);
        }

        /// <summary>
        /// Tests functionality in requirement 2.  No changes to the Add method were needed so we are just adding tests to prove it works for empty and unlimited strings.
        /// 2.	Start with the simplest test case of an empty string and move to 1 and two numbers
        /// 2.	Allow the Add method to handle an unknown amount of numbers
        /// </summary>
        [Theory]
        [InlineData("", 0)]
        [InlineData("99", 99)]
        [InlineData("1,2", 3)]
        [InlineData("1,2,3", 6)]
        [InlineData("1,2,3,4", 10)]
        [InlineData("1,2,3,4,5", 15)]
        [InlineData("1,2,3,4,5,6", 21)]
        [InlineData("1,2,3,4,5,6,7", 28)]
        [InlineData("1,2,3,4,5,6,7,8", 36)]
        public void TestFunctionality2(string numberString, int expected)
        {
            //Act
            int sum = calc.Add(numberString);

            //Assert
            Assert.Equal(expected, sum);
        }

        /// <summary>
        /// Tests functionality in requirement 3.  
        /// 3.	Allow the Add method to handle new lines between numbers (instead of commas). 
        ///   1.	the following input is ok:  “1\n2,3”  (will equal 6)
        ///   2.	the following input is NOT ok:  “1,\n” (not need to prove it - just clarifying)
        /// </summary>
        [Theory]
        [InlineData("1\n2", 3)]
        [InlineData("1,2\n3", 6)]
        [InlineData("1\n2,3", 6)]
        [InlineData("1\n2,3\n4", 10)]
        [InlineData("1\n2\n3\n4\n5", 15)]
        public void TestFunctionality3(string numberString, int expected)
        {
            //Act
            int sum = calc.Add(numberString);

            //Assert
            Assert.Equal(expected, sum);
        }

        /// <summary>
        /// Tests functionality in requirement 4.
        /// 4.	Support user providing there own delimiters
        ///   1.	to change a delimiter, the beginning of the string will contain a separate line that looks like this: 
        ///   2.	“//[delimiter]\n[numbers…]” for example “//;\n1;2” should return three where the default delimiter is ‘;’ .
        ///   3.	the first line is optional.all existing scenarios should still be supported
        /// </summary>
        [Theory]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//;\n1,2\n3;4", 10)]
        [InlineData("//*\n1\n2*3", 6)]
        [InlineData("//&\n1\n2&3,4\n5", 15)]
        public void TestFunctionality4(string numberString, int expected)
        {
            //Act
            int sum = calc.Add(numberString);

            //Assert
            Assert.Equal(expected, sum);
        }

        /// <summary>
        /// Tests functionality in requirement 5.
        /// 5.	Calling Add with a negative number will throw an exception “negatives not allowed” - and the 
        /// negative that was passed. If there are multiple negatives, show all of them in the exception message
        /// </summary>
        [Theory]
        [InlineData("1,-2", "-2")]
        [InlineData("//;\n1,2\n3;-4", "-4")]
        [InlineData("//*\n-1\n-2*-3", "-1 -2 -3")]
        [InlineData("//&\n1\n2&-3,4\n-5", "-3 -5")]
        [InlineData("1,2\n-3", "-3")]
        [InlineData("1\n-2,-3", "-3")]
        [InlineData("-1\n-2,-3\n-4", "-1 -2 -3 -4")]
        public void TestFunctionality5(string numberString, string negativeList)
        {
            //Act
            var ex = Assert.Throws<NegativeIntegerException>(() => calc.Add(numberString));

            //Assert
            Assert.Contains("Detected invalid input, negative numbers are not allowed.  Negative Detected:", ex.Message);
            Assert.Contains(negativeList, ex.Message);

        }
    }
}
