using StringCalculator;
using Xunit;

namespace StringCalculatorTests
{
    public class CalculatorUnitTest
    {
        Calculator calc;

        public CalculatorUnitTest()
        {
            calc = new Calculator();
        }

        /// <summary>
        /// Tests functionality in requirement 1.
        /// 1.	Create a simple String calculator with a method int Add(string numbers) 
        /// 1.	The method can take 0, 1 or 2 numbers, and will return their sum(for an empty string it will return 0) for example “” or “1” or “1,2”//
        /// </summary>
        [Fact]
        public void TestFunctionality1()
        {
            int sum = calc.Add("");
            Assert.Equal(0, sum);

            sum = calc.Add("1");
            Assert.Equal(1, sum);

            sum = calc.Add("1,2");
            Assert.Equal(3, sum);
        }

        /// <summary>
        /// Tests functionality in requirement 2.  No changes to the Add method were needed so we are just adding tests to prove it works for empty and unlimited strings.
        /// 2.	Start with the simplest test case of an empty string and move to 1 and two numbers
        /// 2.	Allow the Add method to handle an unknown amount of numbers
        /// </summary>
        [Theory]
        [InlineData("", 0)]
        [InlineData("1", 1)]
        [InlineData("1,2", 3)]
        [InlineData("1,2,3", 6)]
        [InlineData("1,2,3,4", 10)]
        [InlineData("1,2,3,4,5", 15)]
        [InlineData("1,2,3,4,5,6", 21)]
        [InlineData("1,2,3,4,5,6,7", 28)]
        [InlineData("1,2,3,4,5,6,7,8", 36)]
        public void TestFunctionality2(string numberString, int expected)
        {
            int sum = calc.Add(numberString);
            Assert.Equal(expected, sum);
        }

    }
}
