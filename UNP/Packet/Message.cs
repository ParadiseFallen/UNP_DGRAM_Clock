using System;

namespace UNP.Packet
{
    /// <summary>
    /// Message with data
    /// </summary>
    [Serializable]
    public class Message
    {
        /// <summary>
        /// Type of data
        /// </summary>
        public Type DataType => Data.GetType();
        /// <summary>
        /// Message data
        /// </summary>
        public object Data { get; set; }
    }

}
