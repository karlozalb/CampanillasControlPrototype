using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommsModule
{
    public class Client
    {
        //static readonly IPAddress serverIP = IPAddress.Loopback;
        private int mServerPort = 2500;
        const int serverUDPPort = 2501; //Legacy
        IPAddress mServerIp;

        static ManualResetEvent connectDone = new ManualResetEvent(false);
        static ManualResetEvent sendDone = new ManualResetEvent(false);

        DataSerializer mDataSerializer;

        ICommClientController mClientController;

        private int MAX_RETRIES = 10;
        private int currentRetries;

        public Client(ICommClientController pclientcontroller,string pserverip,int pserverport)
        {
            mServerIp = IPAddress.Parse(pserverip);
            mServerPort = pserverport;
            mClientController = pclientcontroller;
            mDataSerializer = new DataSerializer();
        }


        public bool SendMessageAsyncForResponse(Object pmessage)
        {
            try
            {
                if (mServerIp != null)
                {
                    // Initiate connecting to the server
                    Socket connection = Connect();
                    if (connection != null && connection.Connected)
                    {
                        // block this thread until we have connected
                        // normally your program would just continue doing other work
                        // but we've got nothing to do :)
                        if (connectDone.WaitOne(new TimeSpan(0, 0, 3)))
                        {
                            Console.Out.WriteLine("Conectado al servidor.");
                            // Start sending the data
                            SendData(connection, pmessage);
                            if (sendDone.WaitOne(new TimeSpan(0, 0, 3)))
                            {
                                Console.Out.WriteLine("Message successfully sent");
                                return true;
                            }
                        }
                    }
                }
            }
            catch (System.Net.Sockets.SocketException e)
            {
                //No hacemos nada.
            }
            return false;
        }
        

        Socket Connect()
        {
            try
            {
                IPEndPoint serverAddress = new IPEndPoint(mServerIp, mServerPort);
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IAsyncResult result = client.BeginConnect(serverAddress, new AsyncCallback(ConnectCallback), client);

                bool success = result.AsyncWaitHandle.WaitOne(5000, true);

                if (success) {
                    return client;
                } else {
                    client.Close(); return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public bool discoverServer()
        {
            string hostname = System.Net.Dns.GetHostName();
            IPHostEntry allLocalNetworkAddresses = Dns.Resolve(hostname);

            for (int i = 0; i < MAX_RETRIES; i++)
            {
                foreach (IPAddress ip in allLocalNetworkAddresses.AddressList)
                {
                    Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);


                    //Bind on port 0. The OS will give some port between 1025 and 5000.
                    client.Bind(new IPEndPoint(ip, 2501));

                    //Create endpoint, broadcast.
                    IPEndPoint AllEndPoint = new IPEndPoint(IPAddress.Broadcast, serverUDPPort);

                    //Allow sending broadcast messages
                    client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

                    //Send message to everyone on this network
                    client.SendTo(new byte[] { 1 }, AllEndPoint);

                    Console.Write("Client send '1' to " + AllEndPoint.ToString() + Environment.NewLine);

                    try
                    {
                        //Create object for the server.
                        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                        EndPoint tempRemoteEP = (EndPoint)sender;
                        byte[] buffer = new byte[1000];

                        //Recieve from server. Don't wait more than 3000 milliseconds.
                        client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);

                        //Recive message, save wherefrom in tempRemoteIp
                        client.ReceiveFrom(buffer, ref tempRemoteEP);
                        Console.Write("Client got '" + buffer[0] + "' from " +
                        tempRemoteEP.ToString() + Environment.NewLine);

                        //Get server IP (ugly)
                        mServerIp = IPAddress.Parse(tempRemoteEP.ToString().Split(":".ToCharArray(), 2)[0]);

                        //Don't try any more networkss
                        return true;
                    }
                    catch
                    {
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                        Debug.WriteLine("No server answered. Try next network");
                        //Timout. No server answered. Try next network.
                    }
                }
            }         

            return mServerIp != null;
        }

        void SendData(Socket connection, Object pmessage)
        {
            if (mServerIp != null)
            {
                try
                {
                    byte[] bytes = mDataSerializer.serialize(pmessage);

                    // We store how much data the server should expect
                    // in the first 4 bytes of the data we're going to send
                    byte[] head = BitConverter.GetBytes(bytes.Length);

                    byte[] total = new byte[bytes.Length + head.Length];

                    head.CopyTo(total, 0);
                    bytes.CopyTo(total, head.Length);

                    connection.BeginSend(total, 0, total.Length, 0, new AsyncCallback(SendCallBack), connection);
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.Message);
                }
            }
            else
            {
                Console.Out.WriteLine("No conectado al servidor");
            }
        }

        void responseReceived(Object presponse)
        {
            if (presponse is SerializableTeacherData)
            {
                mClientController.addTeachersDataToGUI((SerializableTeacherData)presponse);
            }else if (presponse is SerializableTeacherList)
            {
                mClientController.addTeacherListDataToGUI(((SerializableTeacherList)presponse));
            }else if (presponse is SerializableGenericOKMessage)
            {
                mClientController.showMessage("Operación realizada correctamente.");
            }
            else if (presponse is SerializableAdList)
            {
                mClientController.deleteAdResponseReceived((SerializableAdList)presponse);
            }
            else if (presponse is SerializableMissingTeachersList)
            {
                mClientController.missingTeachersListReceived((SerializableMissingTeachersList)presponse);
            }
            else if (presponse is SerializableTeacherDataList)
            {
                mClientController.oddClockinsTeacherListReceived((SerializableTeacherDataList)presponse);
            }           
            else if (presponse is SerializableSubstitutionList)
            {
                mClientController.addSubstituteListToGUI((SerializableSubstitutionList)presponse);
            }
            else if (presponse is SerializableLateClockInsList)
            {
                mClientController.addLateClockinsToGUI((SerializableLateClockInsList)presponse);
            }
            else if (presponse is SerializableNoSchoolDaysList)
            {
                mClientController.addNoSchoolDaysToGUI((SerializableNoSchoolDaysList)presponse);
            }
            else if (presponse is SerializableTeachersMissesPerHourList)
            {
                mClientController.addTeachersMissesPerHourListToGUI((SerializableTeachersMissesPerHourList)presponse);
            }
        }

        void SendDataForResponse(Socket connection, Object pmessage)
        {
            if (mServerIp != null)
            {
                try
                {
                    byte[] bytes = mDataSerializer.serialize(pmessage);

                    // We store how much data the server should expect
                    // in the first 4 bytes of the data we're going to send
                    byte[] head = BitConverter.GetBytes(bytes.Length);

                    byte[] total = new byte[bytes.Length + head.Length];
                    head.CopyTo(total, 0);
                    bytes.CopyTo(total, head.Length);

                    connection.BeginSend(total, 0, total.Length, 0, new AsyncCallback(SendCallBack), connection);
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.Message);
                }
            }
            else
            {
                Console.Out.WriteLine("No conectado al servidor");
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
        }

        private void SendCallBack(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytes = client.EndSend(ar);

                Console.Out.WriteLine("A total of {0} bytes were sent to the server", bytes);

                Server.StateObject state = new Server.StateObject();
                state.connection = client;

                client.BeginReceive(state.buffer, 0, Server.StateObject.bufferSize,SocketFlags.None, new AsyncCallback(ReadCallback), state);

                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
        }

        void ReadCallback(IAsyncResult ar)
        {
            try
            {
                Server.StateObject state = (Server.StateObject)ar.AsyncState;
                Socket handler = state.connection;

                int read = handler.EndReceive(ar);

                if (read > 0)
                {
                    Console.Out.WriteLine("Read {0} bytes", read);

                    if (state.expectedMessageLength == 0)
                    {
                        // Extract how much data to expect from the first 4 bytes
                        // then configure buffer sizes and copy the already received
                        // part of the message.
                        state.expectedMessageLength = BitConverter.ToInt32(state.buffer, 0);
                        state.message = new byte[state.expectedMessageLength];
                        Array.ConstrainedCopy(state.buffer, 4, state.message, 0, Math.Min(Server.StateObject.bufferSize - 4, state.expectedMessageLength - state.receivedMessageLength));
                        state.receivedMessageLength += read - 4;
                    }
                    else
                    {
                        Array.ConstrainedCopy(state.buffer, 0, state.message, state.receivedMessageLength, Math.Min(Server.StateObject.bufferSize, state.expectedMessageLength - state.receivedMessageLength));
                        state.receivedMessageLength += read;
                    }

                    // Check if we received the entire message. If not
                    // continue listening, else close the connection
                    // and reconstruct the message.
                    if (state.receivedMessageLength < state.expectedMessageLength)
                    {
                        handler.BeginReceive(state.buffer, 0, Server.StateObject.bufferSize,
                            SocketFlags.None, new AsyncCallback(ReadCallback), state);
                    }
                    else
                    {
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();

                        Console.Out.WriteLine("Received message: \n");

                        Object messageReceived = mDataSerializer.deserialize(state.message);

                        responseReceived(messageReceived);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
