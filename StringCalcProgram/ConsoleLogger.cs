using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddKata
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger()
        {
        }

        public void SetOut(TextWriter writer)
        {
            Console.SetOut(writer);
        }

        public virtual void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
