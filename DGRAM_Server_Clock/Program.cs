using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace DGRAM_Server_Clock
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> arg = new List<string>();
            if (args.Length > 0)
                arg.AddRange(args[0].Split(":"));
            else
            {
                arg.Add("127.0.0.1");
                arg.Add("10303");
            }

            Socket socket = new Socket(
                System.Net.Sockets.AddressFamily.InterNetwork,
                System.Net.Sockets.SocketType.Dgram,
                System.Net.Sockets.ProtocolType.Udp);
            socket.Bind(new System.Net.IPEndPoint(
                System.Net.IPAddress.Parse(arg[0]),
                int.Parse(arg[1]) ));

            using (var s = new Server(socket))
                s.Start();
        }
    }
}
