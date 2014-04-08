using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TddKata2Tests
{
    interface IWebService
    {
        void Notify(string message);
    }
}
