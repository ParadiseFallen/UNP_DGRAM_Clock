using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UNP.Packet;
using UNP.Transforms;

namespace DGRAM_Client_Clock
{
    class Program
    {
        static void Main(string[] args)
        {
            Transform.OnException += (arr, packet, ex) => { Console.WriteLine(ex); };

            IPEndPoint endPoint = null;
            UdpClient udpClient = new UdpClient(10303);
            do
            {
                NetPacket packet;
                Console.WriteLine("Recive");

                packet = udpClient.Receive(ref endPoint).BytesToPacketTransform();

                Console.WriteLine($"Server time : {packet.Message.Data}");

                 Thread.Sleep(1000);
                //Console.Clear();
            } while (true);
        }
    }
}
