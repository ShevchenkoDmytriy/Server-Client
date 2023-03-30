using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    public partial class MainWindow : Window
    {
        public static string message;
        int n ;
        public MainWindow()
        {
            Button buttonOk = new Button();
          
            InitializeComponent();
            try
            {
                buttonOk.Click += But1_Click;
                SendMessageFromSocket(11000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }

        void SendMessageFromSocket(int port)
        {
            

            byte[] buffer = new byte[1024];


            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint iPEndPoint = new IPEndPoint(ipAddr, port);


            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


            sender.Connect(iPEndPoint);

          lab1.Content += $"Сокет соединяется с {sender.RemoteEndPoint.ToString()}";
            byte[] msg = Encoding.UTF8.GetBytes(message);


            int byteSend = sender.Send(msg);

            int byteRecv = sender.Receive(buffer);


            lab1.Content += $"Ответ от сервера: {Encoding.UTF8.GetString(buffer, 0, byteRecv)}";
            if (message.IndexOf("<TheEnd>") == -1)
            {
                SendMessageFromSocket(port);
            }
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

        private void But1_Click(object sender, RoutedEventArgs e)
        {
            lab1.Content += Text1.Text;
            message = Text1.Text;
        }
    }
}
