using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommsModule
{
    public class ServerUDPDiscoverable
    {
        DataSerializer mDataSerializer;
        const int Port = 2501;
        bool started;
        Socket server;
        EndPoint tempRemoteEP;

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
                try {
                    IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                    tempRemoteEP = (EndPoint)sender;
                    byte[] buffer = new byte[1000];
                    //Recive message from anyone.
                    //Server could crash here if there is another server
                    //on the network listening at the same port.
                    server.ReceiveFrom(buffer, ref tempRemoteEP);

                    Console.Write("Server got '" + buffer[0] + "' from " + tempRemoteEP.ToString() + Environment.NewLine);

                    Console.Write("Sending '2' to " + tempRemoteEP.ToString() + Environment.NewLine);

                    //Replay to client
                    server.SendTo(new byte[] { 2 }, tempRemoteEP);
                }catch(Exception e) { 
                    //Esto está así de mal, porque al cerrar el socket (tras cerrar la app), se lanza una excepción y hay que capturarla, ya que la unica
                    //finalidad aquí es cerrar el socket debidamente.
                }
            }
        }

        public void stop()
        {
            started = false;
            server.Close(); //Esto provoca una excepción en el bucle de "descubrimiento" y hace que el Thread muera.
        }

        public class Dummy : IAsyncResult
        {
            public object AsyncState
            {
                get
                {
                    return null;
                }
            }

            public WaitHandle AsyncWaitHandle
            {
                get
                {
                    return null;
                }
            }

            public bool CompletedSynchronously
            {
                get
                {
                    return true;
                }
            }

            public bool IsCompleted
            {
                get
                {
                    return true;
                }
            }
        }

    }
}
