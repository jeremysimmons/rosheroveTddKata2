using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalcProgram;

namespace TddKata
{
    [TestClass]
    public class StringCalcProgramTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Program_ReturnCode_Is_Normally_0()
        {
            int exitCode = Program.Main(new[] { "1,2,3" });
            Assert.AreEqual(0, exitCode);
        }

        [TestMethod]
        public void Program_Prints_Results()
        {
            StringBuilder stdOutBuffer = new StringBuilder();
            StringWriter stdOutWriter = new StringWriter(stdOutBuffer);
            Console.SetOut(stdOutWriter);
            Program.Main(new[] { "1,2,3" });
            StringAssert.StartsWith(stdOutBuffer.ToString(), "The result is 6");
        }

        [TestMethod]
        public void Program_Prompts_For_Additional_Input()
        {
            // Arrange
            // ======================
            // Input is a single newline, which should end the program
            string inputs = @"4,2,4
10,5,20";
            TextReader reader = new StringReader(inputs);
            Console.SetIn(reader); // simulated input

            // Record the output in a buffer
            StringBuilder stdOutBuffer = new StringBuilder();
            TextWriter stdOutWriter = new StringWriter(stdOutBuffer);
            Console.SetOut(stdOutWriter); // recorded output

            // Act
            // ======================
            Program.Main(new[] { "1,2,3" });
            TestContext.WriteLine("Program Output\n" + new string('-', 40) + Environment.NewLine + stdOutBuffer);

            // Assert
            // ======================
            string[] outputLines = stdOutBuffer.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
         
            Assert.AreEqual("The result is 6", outputLines[0]);
            Assert.AreEqual("another input please", outputLines[1]);
            Assert.AreEqual("The result is 10", outputLines[2]);
            Assert.AreEqual("another input please", outputLines[3]);
            Assert.AreEqual("The result is 35", outputLines[4]);
            Assert.AreEqual("another input please", outputLines[5]);

            int countAnotherinput = outputLines.Count(x => x.Equals("another input please"));
            Assert.AreEqual(3, countAnotherinput);
        }
    }
}
