using System;
using System.Collections.Generic;
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
        public Server(Socket socket)
        {
            Socket = socket;
        }
        #region Methods
        public void Start()
        {
            if (Socket is null)
                throw new Exception("Socket is null.");
            Console.WriteLine($"Server runing on : { Socket.LocalEndPoint.ToString()}");

            //main loop
            do
            {
                Socket.SendTo(new NetPacket()
                {
                    PacketType = NetPacket.NetPacketType.PUT,
                    Message = new Message()
                    {
                        Data = DateTime.Now
                    }
                }.PacketToBytesTransform(),Socket.LocalEndPoint);

                Thread.Sleep(1000);

            } while (IsActive);
        }

        #endregion

        public void Dispose()
        {
            Socket.Shutdown(SocketShutdown.Both);
            Socket.Close();
        }
    }
}
