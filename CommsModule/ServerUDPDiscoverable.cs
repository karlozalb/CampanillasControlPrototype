using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    public class ServerUDPDiscoverable
    {
        DataSerializer mDataSerializer;
        const int Port = 2501;
        bool started;
        Socket server;

        public ServerUDPDiscoverable()
        {
            mDataSerializer = new DataSerializer();
        }

        public void startServer()
        {
            started = true;

            server = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);
            Console.Write("Running UDP server..." + Environment.NewLine);
            server.Bind(new IPEndPoint(IPAddress.Any, Port));

            while (started)
            {
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint tempRemoteEP = (EndPoint)sender;
                byte[] buffer = new byte[1000];
                //Recive message from anyone.
                //Server could crash here if there is another server
                //on the network listening at the same port.
                server.ReceiveFrom(buffer, ref tempRemoteEP);

                Console.Write("Server got '" + buffer[0] + "' from " + tempRemoteEP.ToString() + Environment.NewLine);

                Console.Write("Sending '2' to " + tempRemoteEP.ToString() + Environment.NewLine);

                //Replay to client
                server.SendTo(new byte[] { 2 },tempRemoteEP);
            }
        }

        public void stop()
        {
            started = false;
        }

    }
}
