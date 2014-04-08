using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TddKata2Tests
{
    public interface ILogger
    {
        void Write(string message);
    }

    public class NullLogger : ILogger
    {
        private static NullLogger defaultInstance;

        private NullLogger()
        {
        }

        public static ILogger Default
        {
            get { return defaultInstance ?? (defaultInstance = new NullLogger()); }
        }

        public void Write(string message)
        {
        }
    }
}
