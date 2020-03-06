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
            Console.Write("Enter ip and port : ");
            var adress = Console.ReadLine();
            Socket socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp);

            EndPoint endPoint = new IPEndPoint(IPAddress.Parse(adress.Split(":")[0]), int.Parse(adress.Split(":")[1]));

            socket.Bind(endPoint);

            do
            {
                NetPacket packet;
                List<byte> byteList = new List<byte>();

                do
                {
                    byte[] buffer = new byte[100];
                    socket.ReceiveFrom(buffer,ref endPoint);
                    byteList.AddRange(buffer);
                } while (socket.Available>0);

                packet = byteList.ToArray().BytesToPacketTransform();

                Console.WriteLine($"Server time : {packet.Message.Data}");
                Thread.Sleep(1000);
                Console.Clear();
            } while (true);
        }
    }
}
