using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TddKata1
{
    [TestClass]
    public class CalculationExpressionParserTest
    {
        [TestMethod]
        public void ContainsDelimiterExpression_InvalidWithoutNewline()
        {
            CalculationExpressionParser parser = new CalculationExpressionParser();
            bool negativeResult = parser.ContainsDelimiterExpression("//;1;2");
            Assert.IsFalse(negativeResult);
        }

        [TestMethod]
        public void ContainsDelimiterExpression_SingleCharacterDelimitersExpressions_Valid()
        {
            CalculationExpressionParser parser = new CalculationExpressionParser();
            
            string[] singleCharacterDelimiterPrograms = {
                "//;\n1;2",
                "//,\n1,2"
            };

            foreach (string calcProgram in singleCharacterDelimiterPrograms)
            {
                bool positiveResult = parser.ContainsDelimiterExpression(calcProgram);
                Assert.IsTrue(positiveResult);
            }
        }

        [TestMethod]
        public void Delimiters_DelimiterExpressionWithSingleCharacterIsValid()
        {
            CalculationExpressionParser parser = new CalculationExpressionParser();
            ValidateSingleCharacterDelimiterExpression(parser, ";", "//;\n1;2");
            ValidateSingleCharacterDelimiterExpression(parser, ",", "//,\n1,2");
        }

        private static void ValidateSingleCharacterDelimiterExpression(CalculationExpressionParser parser, string expected, string calcProgram)
        {
            string[] delimiters = parser.Delimiters(calcProgram);
            Assert.IsTrue(delimiters.Contains(expected));
        }

        [TestMethod]
        public void InputInstructions_ValidForOneNumber()
        {
            CalculationExpressionParser parser = new CalculationExpressionParser();
            string singleNumber = parser.InputInstructions("//;\n1");
            Assert.AreEqual("1", singleNumber);
            string multipleNumbers = parser.InputInstructions("//;\n1,2");
            Assert.AreEqual("1,2", multipleNumbers);
        }

        [TestMethod]
        public void InputInstructions_ValidForTwoNumbers()
        {
            CalculationExpressionParser parser = new CalculationExpressionParser();
            string multipleNumbers = parser.InputInstructions("//;\n1,2");
            Assert.AreEqual("1,2", multipleNumbers);
        }

        [TestMethod]
        public void InputInstructions_Valid_WithoutDelimiterExpression()
        {
            CalculationExpressionParser parser = new CalculationExpressionParser();
            string multipleNumbersNoInstructions = parser.InputInstructions("2,1");
            Assert.AreEqual("2,1", multipleNumbersNoInstructions);
        }

        [TestMethod]
        public void Delimiters_IsValidFor_MultiCharacterDelimiters()
        {
            CalculationExpressionParser parser = new CalculationExpressionParser();
            string calcProgram = "//[***]\n1***2***3";
            string delimiter = parser.Delimiters(calcProgram)[0];
            Assert.AreEqual("***", delimiter);

            string instructions = parser.InputInstructions(calcProgram);
            Assert.AreEqual("1***2***3", instructions);
        }

        [TestMethod]
        public void ContainsDelimiterExpression_IsValid_ForMultipleDelimiters()
        {
            string multipleDelimiterProgram = "//[*][%]\n1*2%3";
            CalculationExpressionParser parser = new CalculationExpressionParser();
            bool result = parser.ContainsDelimiterExpression(multipleDelimiterProgram);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Delimiters_HasMultipleCorrectDelimiters()
        {
            string multipleDelimiterProgram = "//[*][%]\n1*2%3";
            CalculationExpressionParser parser = new CalculationExpressionParser();
            string[] result = parser.Delimiters(multipleDelimiterProgram);
            Assert.AreEqual(2, result.Length);
            CollectionAssert.AreEqual(new[]{"*", "%"}, result);
        }

        [TestMethod]
        public void IsMultiDelimiter_MatchesMultipleDelimiters()
        {
            CalculationExpressionParser parser = new CalculationExpressionParser();
            string[] testCases = {
                "[%][*]",
                "[***][%%%]"
            };
            foreach (string validCase in testCases)
            {
                Assert.IsTrue(parser.IsMultiDelimiter(validCase));
            }
        }

        [TestMethod]
        public void IsMultiDelimiter_Single_Delimiters_AreNot_Classified_As_Multiples()
        {
            CalculationExpressionParser parser = new CalculationExpressionParser();
            string[] testCases = {
                "[%]",
                "[*]"
            };
            foreach (string invalidCase in testCases)
            {
                Assert.IsFalse(parser.IsMultiDelimiter(invalidCase));
            }
        }
    }
}