using StringCalculator;
using StringCalculator.Exceptions;
using System;
using Xunit;

namespace StringCalculatorTests
{
    //*********************************************************************************************************************************************/
    // These are unit test for individual methods in Calculator.cs.  
    // If you want to validate the assignment by requirements, see AssignmentFunctionalityTests.cs
    //*********************************************************************************************************************************************/


    public class CalculatorUnitTest
    {
        Calculator calc;

        public CalculatorUnitTest()
        {
            calc = new Calculator();
        }

        [Fact]
        public void SumInput_ArrayOfNumberStrings_ReturnsSum()
        {
            //Arrange
            string[] numbers = new string[] { "1", "2", "3" };

            //Act
            int sum = calc.SumInput(numbers);

            //Assert
            Assert.Equal(6, sum);
        }

        /// <summary>
        /// Tests that only integer numbers can be passed in.
        /// Letters, decimals, non defined delimiters should all throw FormatException.
        /// </summary>
        [Fact]
        public void SumInput_NonNumeric_FormatExceptionThrown()
        {
            //Arrange
            string[] numbers = new string[] { "1", "2", "bad data" };

            //Act and Assert
            var ex = Assert.Throws<FormatException>(() => calc.SumInput(numbers));
        }

        /// <summary>
        /// Tests that only positive integer numbers can be passed in.
        /// Negative numbers will throw an NegativeIntegerException and will be listed in the message
        [Fact]
        public void SumInput_Negatives_NegativeIntegerExceptionThrown()
        {
            //Arrange
            string[] numbers = new string[] { "1", "2", "-3" };
            string negativeList = "-3";

            //Act
            var ex = Assert.Throws<NegativeIntegerException>(() => calc.SumInput(numbers));

            //Assert
            Assert.Contains("Detected invalid input, negative numbers are not allowed.  Negative Detected:", ex.Message);
            Assert.Contains(negativeList, ex.Message);
        }

        /// <summary>
        /// Tests that the default delimiters of , and \n will separate the numbers into an array of strings
        /// </summary>
        [Fact]
        public void ParseNumbers_WithoutCustomDelimiters_ReturnsStringArrayOfNumbers()
        {
            //Act
            string[] set1 = calc.ParseNumbers("1,2,3");
            string[] set2 = calc.ParseNumbers("1\n2\n3");
            string[] set3 = calc.ParseNumbers("1,2\n3");

            //Assert
            Assert.Equal("1", set1[0]);
            Assert.Equal("2", set1[1]);
            Assert.Equal("3", set1[2]);

            Assert.Equal("1", set2[0]);
            Assert.Equal("2", set2[1]);
            Assert.Equal("3", set2[2]);

            Assert.Equal("1", set3[0]);
            Assert.Equal("2", set3[1]);
            Assert.Equal("3", set3[2]);
        }

        /// <summary>
        /// Tests that a single custom delimiter will separate the numbers into an array of strings
        /// The Delimiter can be multiple chars long.
        /// </summary>
        [Fact]
        public void ParseNumbers_WithCustomDelimiters_ReturnsStringArrayOfNumbers()
        {
            //Act
            string[] set1 = calc.ParseNumbers("//;\n1;2;3");
            string[] set2 = calc.ParseNumbers("//&\n1&2\n3");
            string[] set3 = calc.ParseNumbers("//**\n1**2\n3");

            //Assert
            Assert.Equal("1", set1[0]);
            Assert.Equal("2", set1[1]);
            Assert.Equal("3", set1[2]);

            Assert.Equal("1", set2[0]);
            Assert.Equal("2", set2[1]);
            Assert.Equal("3", set2[2]);

            Assert.Equal("1", set3[0]);
            Assert.Equal("2", set3[1]);
            Assert.Equal("3", set3[2]);
        }
        
        /// <summary>
        /// Tests retrieval of a single custom delimiter
        /// returns null if no custom delimiter is found, otherwise should return one delimiter
        /// </summary>
        /// <param name="numberString"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData("//;\n1;2", ";")]
        [InlineData("//;\n1;2\n4", ";")]
        [InlineData("1,2\n4", null)]  
        public void GetCustomDelimiter_SingleDelimter_ReturnsDelimiter(string numberString, string expected)
        {
            //Act
            string delimiter = calc.GetCustomDelimiter(numberString);

            //Assert
            Assert.Equal(expected, delimiter);
        }

        /// <summary>
        /// Tests the Add method in general
        /// More in depth testing per requirement of assignment is in AssignmentFunctionalityTests.cs
        /// </summary>
        /// <param name="numberString"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData("", 0)]
        [InlineData("4", 4)]
        [InlineData("1,22", 23)]
        [InlineData("//&\n1\n2&3,4,5", 15)]
        public void Add_NumberString_ReturnsSum(string numberString, int expected)
        {
            //Act
            int sum = calc.Add(numberString);

            //Assert
            Assert.Equal(expected, sum);
        }

        /// <summary>
        /// Tests that only integer numbers can be passed in.
        /// Letters, decimals, non defined delimiters should all throw FormatException.
        /// </summary>
        [Theory]
        [InlineData("1;-2")]
        [InlineData("1.00001")]
        [InlineData("//;\n1,2\n3;b")]
        [InlineData("//*\n-1\nc*d")]
        public void Add_NonNumeric_FormatExceptionThrown(string numberString)
        {
            //Act and Assert
            var ex = Assert.Throws<FormatException>(() => calc.Add(numberString));
        }

        /// <summary>
        /// Tests that only positive integer numbers can be passed in.
        /// Negative numbers will throw an NegativeIntegerException and will be listed in the message
        [Theory]
        [InlineData("1,-2,-2,-2", "-2 -2 -2")]
        [InlineData("//&\n1\n2&-3,4\n-5\n-98", "-3 -5 -98")]
        [InlineData("1,2\n-3,-4", "-3 -4")]
        [InlineData("1\n-2,-3,-4", "-3 -4")]
        [InlineData("-1\n-2,-3\n-4,-99", "-1 -2 -3 -4 -99")]
        public void Add_Negatives_NegativeIntegerExceptionThrown(string numberString, string negativeList)
        {
            //Act
            var ex = Assert.Throws<NegativeIntegerException>(() => calc.Add(numberString));

            //Assert
            Assert.Contains("Detected invalid input, negative numbers are not allowed.  Negative Detected:", ex.Message);
            Assert.Contains(negativeList, ex.Message);
        }        
    }
}
