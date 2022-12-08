using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Transport;

namespace Program
{
    class Program
    {
        static void Main()
        {
            Transport1 TransportTask = new Transport1();
            TransportTask.Main();
        }
    }

}