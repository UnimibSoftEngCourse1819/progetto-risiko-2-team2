using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskServer1
{
    class General
    {
        public static void InizializzaServer()
        {
            ServerTCP.initializeNetwork();
            Console.WriteLine("il server è partito, cool");

        }
    }
}
