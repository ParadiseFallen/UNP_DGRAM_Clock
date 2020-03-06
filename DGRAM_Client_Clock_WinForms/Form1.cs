using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNP.Packet;

namespace DGRAM_Client_Clock_WinForms
{
    public partial class Form1 : Form
    {
        Socket socket;
        public Form1()
        {
            InitializeComponent();
            socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.IP);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            //IPTextBox.Text

            Task.Run(() => {
                do
                {
                    lock (socket)
                    {
                        //socket.ReceiveFrom()
                        //NetPacket packet;
                        NetPacket
                        lock (this)
                        {

                        }
                    }  
                } while (true);
            });
        }
    }
}
