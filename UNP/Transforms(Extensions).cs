using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UNP.Packet;

namespace UNP.Transforms
{
    ///////////////////////////////////////////////////////////TODO: Crypto

    /// <summary>
    /// Extension class with packet transforms
    /// </summary>
    public static class Transform
    {
        #region EVENTS
        static public event Action<byte[], NetPacket, Exception> OnException;
        #endregion

        #region TRANSFORMS

        /// <summary>
        /// Serialize packet to byte[]
        /// </summary>
        /// <param name="packet">Packet</param>
        /// <returns>byte aray that can be send</returns>
        static public byte[] PacketToBytesTransform(this NetPacket packet)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ms, packet);
                    return ms.ToArray();
                }
                catch (Exception ex)
                {
                    OnException?.BeginInvoke(ms.ToArray(), packet, ex, null, null);
                }
                return null;
            }
        }

        /// <summary>
        /// Async serialize packet to byte[]
        /// </summary>
        /// <param name="packet">Packet</param>
        /// <returns>Task with byte array on resault that can be send</returns>
        static public async Task<byte[]> PacketToBytesTransformAsync(this NetPacket packet)
        {
            return await Task.Run(() =>
            {
                lock (packet)
                {
                    return PacketToBytesTransform(packet);
                }
            });
        }

        /// <summary>
        /// Deserialize byte[] to NetPacket
        /// </summary>
        /// <param name="ByteArray">byte array that contains net packet data</param>
        /// <returns>NetPacket that can be used</returns>
        static public NetPacket BytesToPacketTransform(this byte[] ByteArray)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    ms.Write(ByteArray, 0, ByteArray.Length);
                    ms.Position = 0;
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(ms) as NetPacket;
                }
                catch (Exception ex)
                {
                    OnException?.BeginInvoke(ByteArray, null, ex, null, null);
                    return new NetPacket() { PacketType = NetPacket.NetPacketType.TRANSFORM_ERROR };
                }
            }
        }

        /// <summary>
        /// Async transform byte[] to NetPacket
        /// </summary>
        /// <param name="ByteArray">byte[] that contains NetPacket data</param>
        /// <returns>Task with NetPacket</returns>
        static public async Task<NetPacket> BytesToPacketTransformAsync(this byte[] ByteArray)
        {
            return await Task.Run(() =>
            {
                lock (ByteArray)
                {
                    return BytesToPacketTransform(ByteArray);
                }
            });
        }

        /// <summary>
        /// Transform data in Socket to NetPacket
        /// </summary>
        /// <param name="ConnectionSocket">Socket that recived NetPacket</param>
        /// <param name="BufferSize">Size of buffer. You can play with this size to improve perfomence</param>
        /// <returns>NetPacket</returns>
        static public NetPacket SocketToPacketTransform(this Socket ConnectionSocket, int BufferSize = 100)
        {
            return BytesToPacketTransform(ReadAllBytes(ConnectionSocket, BufferSize));
        }

        /// <summary>
        /// Async transform data in Socket to NetPacket
        /// </summary>
        /// <param name="ConnectionSocket">Socket that recived NetPacket</param>
        /// <param name="BufferSize">Size of buffer. You can play with this size to improve perfomence</param>
        /// <returns>NetPacket</returns>
        static public async Task<NetPacket> SocketToPacketTransformAsync(this Socket ConnectionSocket, int BufferSize = 100)
        {
            return await
                Task.Run(() =>
                {
                    lock (ConnectionSocket)
                    {
                        return BytesToPacketTransform(ReadAllBytes(ConnectionSocket, BufferSize));
                    }
                });
        }

        /// <summary>
        /// Read all bytes from connection
        /// </summary>
        /// <param name="ConnectionSocket">Connection</param>
        /// <param name="BufferSize">Buffer size</param>
        /// <returns>byte[]</returns>
        static private byte[] ReadAllBytes(Socket ConnectionSocket, int BufferSize = 100)
        {
            try
            {
                List<byte> byteList = new List<byte>();
                do
                {
                    var Buffer = new byte[BufferSize];
                    if (ConnectionSocket.Receive(Buffer) > 0)
                        byteList.AddRange(Buffer);
                } while (ConnectionSocket.Available > 0);

                return byteList.ToArray();
            }
            catch (Exception ex)
            {
                OnException?.BeginInvoke(null, null, ex, null, null);
                return null;
            }
        }
        #endregion
    }
}
