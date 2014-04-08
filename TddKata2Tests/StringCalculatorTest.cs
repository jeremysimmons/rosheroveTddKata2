using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TddKata1
{
    [TestClass]
    public class StringCalculatorTest
    {
        private readonly CalculationExpressionParser _calculationExpressionParser = new CalculationExpressionParser();

        [TestMethod]
        public void Add_SumsEmptyString()
        {
            StringCalculator calc = new StringCalculator();
            int result = calc.Add("");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Add_SumsSingleNumber()
        {
            StringCalculator calc = new StringCalculator();
            int result = calc.Add("1");
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Add_SumsTwoNumbers_SeparatedByComma()
        {
            StringCalculator calc = new StringCalculator();
            int result = calc.Add("1,2");
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Add_SumsMultipleNumbers_SeparatedByComma()
        {
            StringCalculator calc = new StringCalculator();
            int result = calc.Add("1,2,3,4");
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void Add_Allows_Comma_Or_Newline_Delimiters()
        {
            StringCalculator calc = new StringCalculator();
            int result = calc.Add("1\n2,3");
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Add_DefaultDelimiterSpecifiedFirstLine()
        {
            StringCalculator calc = new StringCalculator();
            int result = calc.Add("//;\n1;2");
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_With_Negative_Number_Throws_Exception()
        {
            StringCalculator calc = new StringCalculator();
            int result = calc.Add("-1");
        }

        [TestMethod]
        public void Add_With_Negative_Number_Includes_Number_In_Exception_Message()
        {
            StringCalculator calc = new StringCalculator();
            try
            {
                int result = calc.Add("-1");
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("-1"));
            }
        }

        [TestMethod]
        public void Add_With_Multiple_Negative_Numbers_Includes_All_Numbers_In_Exception_Message()
        {
            StringCalculator calc = new StringCalculator();
            try
            {
                int result = calc.Add("-1,1,-2");
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("-1, -2"));
            }
        }

        [TestMethod]
        public void Add_Ingores_Numbers_Larger_Than_1000()
        {
            StringCalculator calc = new StringCalculator();
            int result = calc.Add("2,1001");
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Add_Handles_Multi_Character_Delimiter()
        {
            string calcProgram = "//[***]\n1***2***3";
            StringCalculator calc = new StringCalculator();
            int result = calc.Add(calcProgram);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Add_Handles_Multiple_Single_Character_Delimiters()
        {
            string calcProgram = "//[*][%]\n1*2%3";
            StringCalculator calc = new StringCalculator();
            int result = calc.Add(calcProgram);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Add_Handles_Multiple_Multi_Character_Delimiters()
        {
            string calcProgram = "//[***][%%%]\n1***2%%%3";
            StringCalculator calc = new StringCalculator();
            int result = calc.Add(calcProgram);
            Assert.AreEqual(6, result);
        }
    }
}