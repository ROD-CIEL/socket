using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace socketUDP
{
    public partial class Form1 : Form
    {
        private Socket socket; // Cr�ation d'un socket
        private IPEndPoint remoteEndPoint; // Point de terminaison de l'h�te
        private IPEndPoint localEndPoint; // Point de terminaison local

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Changer le titre de la fen�tre
            this.Text = "Communication par socket UDP";

            // Cr�ation d'un socket
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);

            localEndPoint = new IPEndPoint(IPAddress.Any, 8081);

            IPEndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);

            socket.Bind(localEndPoint);
        }

        // Envoi d'un datagramme
        private void SendMessage()
        {
            // Mise en forme du message � envoyer
            var msg = Encoding.ASCII.GetBytes("Texte � envoyer");

            socket.SendTo(msg, remoteEndPoint);
        }

        // R�ception d'un datagramme
        private void ReceiveMessage()
        {
            var buffer = new byte[1024];

            EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);
            int receivedBytes = socket.ReceiveFrom(buffer, ref senderEndPoint);

            // Afficher le message
            string message = Encoding.ASCII.GetString(buffer, 0, receivedBytes);
            MessageBox.Show("Re�u: " + message);
        }

        // Fonction pour fermer le socket
        private void CloseSocket()
        {
            socket.Close();
        }
    }
}