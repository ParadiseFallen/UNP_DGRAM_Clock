using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UNP.Packet;
using UNP.Transforms;

namespace DGRAM_Server_Clock
{
    class Server : IDisposable
    {
        #region Data
        public Socket Socket { get; private set; } = null;
        public bool IsActive { get; set; } = true;
        #endregion
        public Server()
        {
        }
        #region Methods
        public void Start()
        {
            //if (Socket is null)
            //    throw new Exception("Socket is null.");
            //Console.WriteLine($"Server runing on : { Socket.LocalEndPoint.ToString()}");

            UdpClient client = new UdpClient();
            IPEndPoint point = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10303);
            do
            {
                var byteArr = new NetPacket()
                {
                    PacketType = NetPacket.NetPacketType.PUT,
                    Message = new Message()
                    {
                        Data = DateTime.Now
                    }
                }.PacketToBytesTransform();

                client.Send(byteArr, byteArr.Length, point);
                
                Console.WriteLine($"SendTime : {DateTime.Now}");
                Thread.Sleep(1000);

            } while (true);
            client.Close();

        }

        #endregion

        public void Dispose()
        {
            Socket.Shutdown(SocketShutdown.Both);
            Socket.Close();
        }
    }
}
