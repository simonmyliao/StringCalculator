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

        /// <summary>
        /// Test summing of regular set of integers
        /// No numbers larger then 1000
        /// </summary>
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
        /// Test summing of an empty set of numbers
        /// </summary>
        [Fact]
        public void SumInput_WithEmptyArrayOfNumberStrings_ReturnsSum()
        {
            //Arrange
            string[] numbers = new string[] { };

            //Act
            int sum = calc.SumInput(numbers);

            //Assert
            Assert.Equal(0, sum);
        }

        /// <summary>
        /// Test summing of a set of integers that includes numbers larger then 1000
        /// Numbers larger then 1000 will be ignored.  1000 is okay, 1001 and higher is ignored
        /// Numbers larger then can fit in an int32 are out of the scope of this assignment and wont be handled for.
        /// </summary>
        [Fact]
        public void SumInput_ArrayOfNumberStringsWithGreaterThen1000_ReturnsSum()
        {
            //Arrange
            string[] numbers = new string[] { "99", "1000", "1001", "1003" };

            //Act
            int sum = calc.SumInput(numbers);

            //Assert
            Assert.Equal(1099, sum);
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
        /// The Delimiter can be multiple chars long.  This is not specifically mentioned in the assignment 
        /// but I figure include support for a better usabilty.
        /// </summary>
        [Fact]
        public void ParseNumbers_WithCustomNonBracketedDelimiters_ReturnsStringArrayOfNumbers()
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
        /// Tests that a single custom bracketed delimiter will separate the numbers into an array of strings
        /// The Delimiter can be multiple chars long.
        /// </summary>
        [Fact]
        public void ParseNumbers_WithCustomBracketedDelimiters_ReturnsStringArrayOfNumbers()
        {
            //Act
            string[] set1 = calc.ParseNumbers("//[;]\n1;2;3");
            string[] set2 = calc.ParseNumbers("//[&]\n1&2\n3");
            string[] set3 = calc.ParseNumbers("//[**]\n1**2\n3");

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
        /// Tests that GetDelimiterPortion returns only the section defining the delimiters
        /// </summary>
        /// <param name="numberString"></param>
        /// <param name="expected">The portion of the string defining the delimiters.</param>
        [Theory]
        [InlineData("//&\n1,2&3", "&")]
        [InlineData("//[&]\n1,2&3", "[&]")]
        [InlineData("//[&][*]\n1,2&3,*4", "[&][*]")]
        [InlineData("//[&&&][**]\n1**2&&&3", "[&&&][**]")]
        public void GetDelimiterPortion_WithCustomBracketedDelimiters_ReturnDelimiterString(string numberString, string expected)
        {
            //Act
            string delimiterPortion = calc.GetDelimiterPortion(numberString);

            //Assert
            Assert.Equal(expected, delimiterPortion);
        }

        /// <summary>
        /// Tests retrieval of a single non bracketed custom delimiter
        /// </summary>
        /// <param name="delimiterPortion"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData(";", ";")]
        [InlineData("&", "&")]
        public void GetCustomDelimiter_SingleNonBracketedDelimter_ReturnsDelimiter(string delimiterPortion, string expected)
        {
            //Act
            string[] delimiter = calc.GetCustomDelimiters(delimiterPortion);

            //Assert
            Assert.Equal(expected, delimiter[0]);
        }

        /// <summary>
        /// Tests retrieval of a single bracketed custom delimiter
        /// </summary>
        /// <param name="delimiterPortion"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData("[;]", ";")]
        [InlineData("[&]", "&")]
        [InlineData("[&&]", "&&")]
        public void GetCustomDelimiter_BracketedDelimter_ReturnsDelimiter(string delimiterPortion, string expected)
        {
            //Act
            string[] delimiter = calc.GetCustomDelimiters(delimiterPortion);

            //Assert
            Assert.Equal(expected, delimiter[0]);
        }

        /// <summary>
        /// Tests retrieval of a multiple bracketed custom delimiters
        /// </summary>
        /// <param name="delimiterPortion"></param>
        /// <param name="expectedAt0">The output expected array postion 0</param>
        /// <param name="expectedAt1">The output expected array postion 1</param>
        /// <param name="expectedAt2">The output expected array postion 2</param>
        [Theory]
        [InlineData("[;][^][&]", ";", "^", "&")]
        [InlineData("[&][*][;]", "&", "*", ";")]
        [InlineData("[&&][*][**]", "&&", "*", "**")]
        public void GetCustomDelimiter_MultipleBracketedDelimter_ReturnsDelimiters(string delimiterPortion, string expectedAt0, string expectedAt1, string expectedAt2)
        {
            //Act
            string[] delimiter = calc.GetCustomDelimiters(delimiterPortion);

            //Assert
            Assert.Equal(expectedAt0, delimiter[0]);
            Assert.Equal(expectedAt1, delimiter[1]);
            Assert.Equal(expectedAt2, delimiter[2]);
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
