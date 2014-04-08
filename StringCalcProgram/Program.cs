using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddKata;

namespace StringCalcProgram
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if (args == null || args.Length == 0)
                return 1;

            StringCalculator calculator = new StringCalculator();
            int result = calculator.Add(args[0]);

            Console.WriteLine("The result is " + result);

            return 0;
        }
    }
}
