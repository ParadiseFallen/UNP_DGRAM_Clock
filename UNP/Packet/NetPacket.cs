using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace UNP.Packet
{
    /// <summary>
    /// Wrap of data. Request and etc.
    /// </summary>
    [Serializable]
    public class NetPacket
    {
        #region ENUMS
        /// <summary>
        /// Type of packet
        /// </summary>
        public enum NetPacketType
        {
            CLEAR,
            CONNECT, DISCONECT, SERVICE,
            GET, POST, PUT, DELETE,
            RESPONSE, OTHER,
            TRANSFORM_ERROR
        }
        #endregion

        #region Data
        /// <summary>
        /// Response on packet : 
        /// </summary>
        public NetPacket ResponseOn { get; set; } = null;
        /// <summary>
        /// Type of this packet
        /// </summary>
        public NetPacketType PacketType { get; set; } = NetPacketType.CLEAR;
        /// <summary>
        /// Message
        /// </summary>
        public Message Message { get; set; } = null;
        #endregion

        
    }
}
