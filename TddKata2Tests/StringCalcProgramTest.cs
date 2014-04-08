using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalcProgram;

namespace TddKata
{
    [TestClass]
    public class StringCalcProgramTest
    {
        [TestMethod]
        public void Program_ReturnCode_Is_Normally_0()
        {
            int exitCode = Program.Main(new []{"1,2,3"});
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
    }
}
