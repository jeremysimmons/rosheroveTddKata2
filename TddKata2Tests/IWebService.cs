using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TddKata
{
    public interface IWebService
    {
        void Notify(string message);
    }

    public class NullWebService : IWebService
    {
        private static NullWebService defaultInstance;

        private NullWebService()
        {
        }

        public static IWebService Default
        {
            get { return defaultInstance ?? (defaultInstance = new NullWebService()); }
        }

        public void Notify(string message)
        {
        }
    }
}
