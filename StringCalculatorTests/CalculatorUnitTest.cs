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
        public void Test1()
        {
            int sum = calc.Add("");
            Assert.Equal(0, sum);

            sum = calc.Add("1");
            Assert.Equal(1, sum);

            sum = calc.Add("1,2");
            Assert.Equal(3, sum);
        }
    }
}
