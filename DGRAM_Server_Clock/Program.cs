using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace DGRAM_Server_Clock
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var s = new Server())
                s.Start();
        }
    }
}
