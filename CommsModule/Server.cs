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
    public class Server
    {
        public class StateObject
        {
            public Socket connection = null;

            // Note that I use a very small buffer size
            // for this example. Normally you'd like a much
            // larger buffer. But this small buffer size nicely
            // demonstrates getting the entire message in multiple
            // pieces.
            public const int bufferSize = 512;
            public byte[] buffer = new byte[bufferSize];
            public int expectedMessageLength = 0;
            public int receivedMessageLength = 0;
            public byte[] message = null;
        }

        static ManualResetEvent acceptDone = new ManualResetEvent(false);
        const int listenPort = 2500;
        DataSerializer mDataSerializer;
        IPEndPoint localEndPoint;

        ICommController mCommController;

        public bool started;

        public Server(ICommController pcommcontroller)
        {
            mDataSerializer = new DataSerializer();
            mCommController = pcommcontroller;
        }

        public void startServer()
        {
            localEndPoint = new IPEndPoint(IPAddress.Any, listenPort);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            started = true;

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (started)
                {
                    acceptDone.Reset();

                    Console.Out.WriteLine("Listening on port {0}", listenPort);
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    acceptDone.WaitOne();
                }

                listener.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void stop()
        {
            started = false;
            acceptDone.Set();
        }

        void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                acceptDone.Set();

                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);

                StateObject state = new StateObject();
                state.connection = handler;

                handler.BeginReceive(state.buffer, 0, StateObject.bufferSize,
                    SocketFlags.None, new AsyncCallback(ReadCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void ReadCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
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
                        Array.ConstrainedCopy(state.buffer, 4, state.message, 0, Math.Min(StateObject.bufferSize - 4, state.expectedMessageLength - state.receivedMessageLength));
                        state.receivedMessageLength += read - 4;
                    }
                    else
                    {
                        Array.ConstrainedCopy(state.buffer, 0, state.message, state.receivedMessageLength, Math.Min(StateObject.bufferSize, state.expectedMessageLength - state.receivedMessageLength));
                        state.receivedMessageLength += read;
                    }

                    // Check if we received the entire message. If not
                    // continue listening, else close the connection
                    // and reconstruct the message.
                    if (state.receivedMessageLength < state.expectedMessageLength)
                    {
                        handler.BeginReceive(state.buffer, 0, StateObject.bufferSize,
                            SocketFlags.None, new AsyncCallback(ReadCallback), state);
                    }
                    else
                    {
                        /*handler.Shutdown(SocketShutdown.Both);
                        handler.Close();*/

                        Console.Out.WriteLine("Received message: \n");

                        Object messageReceived = mDataSerializer.deserialize(state.message);

                        processMessage(handler,messageReceived);
                    }                        
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void processMessage(Socket handler,object messageReceived)
        {
            if (messageReceived is SerializableGetTeacherDataMessage)
            {
                SerializableGetTeacherDataMessage msg = (SerializableGetTeacherDataMessage)messageReceived;
                SerializableTeacherData tdata = mCommController.getTeacherInfo(msg.teacherID, msg.init, msg.end);

                byte[] response = mDataSerializer.serialize(tdata);

                sendBackToClient(handler, response);
            }
            else if (messageReceived is SerializableGetTeachersListDataMessage)
            {
                SerializableGetTeachersListDataMessage msg = (SerializableGetTeachersListDataMessage)messageReceived;
                SerializableTeacherList tdata = mCommController.getTeachersList();

                byte[] response = mDataSerializer.serialize(tdata);

                sendBackToClient(handler, response);
            }
            else if (messageReceived is SerializableServerDiscoveryDataMessage)
            {
                sendBackToClient(handler, mDataSerializer.serialize(new SerializableServerDiscoveryDataMessage()));
            }
            else if (messageReceived is SerializableAd)
            {
                mCommController.addNewAd((SerializableAd)messageReceived);
                sendBackToClient(handler, mDataSerializer.serialize(new SerializableGenericOKMessage()));
            }
            else if (messageReceived is SerializableGetAdList)
            {
                SerializableAdList adList = mCommController.getAdList();
                sendBackToClient(handler, mDataSerializer.serialize(adList));
            }
            else if (messageReceived is SerializableDeleteAd)
            {
                mCommController.deleteAd((SerializableDeleteAd)messageReceived);
                sendBackToClient(handler, mDataSerializer.serialize(new SerializableGenericOKMessage()));
            }
            else if (messageReceived is SerializableGetMissingTeachersMessage)
            {
                SerializableMissingTeachersList teachersList = mCommController.getMissingTeachers((SerializableGetMissingTeachersMessage)messageReceived);
                sendBackToClient(handler, mDataSerializer.serialize(teachersList));
            }
            else if (messageReceived is SerializableGetBadClockInsTeachersListMessage)
            {
                SerializableTeacherDataList teachersList = mCommController.getBadClockInsTeachersData((SerializableGetBadClockInsTeachersListMessage)messageReceived);
                sendBackToClient(handler, mDataSerializer.serialize(teachersList));
            }
            else if (messageReceived is SerializableAddSubstituteTeacherMessage)
            {
                mCommController.addSubstitute((SerializableAddSubstituteTeacherMessage)messageReceived);
                SerializableSubstitutionList subsList = mCommController.getSubstituteList();
                sendBackToClient(handler, mDataSerializer.serialize(subsList));
            }
            else if (messageReceived is SerializableGetSubstituteListMessage)
            {
                SerializableSubstitutionList subsList = mCommController.getSubstituteList();
                sendBackToClient(handler, mDataSerializer.serialize(subsList));
            }
            else if (messageReceived is SerializableDeleteSubstituteMessage)
            {
                mCommController.deleteSubstitute((SerializableDeleteSubstituteMessage)messageReceived);
                SerializableSubstitutionList subsList = mCommController.getSubstituteList();
                sendBackToClient(handler, mDataSerializer.serialize(subsList));
            }
            else if (messageReceived is SerializableGetLateClockInsListMessage)
            {
                SerializableLateClockInsList lateClockinsList = mCommController.getLateClockInsList((SerializableGetLateClockInsListMessage)messageReceived);
                sendBackToClient(handler, mDataSerializer.serialize(lateClockinsList));
            }
            else if (messageReceived is SerializableGetNoSchoolDaysMessage)
            {
                SerializableNoSchoolDaysList noSchoolDays = mCommController.getNoSchoolDaysList();
                sendBackToClient(handler, mDataSerializer.serialize(noSchoolDays));
            }
            else if (messageReceived is SerializableDeleteDayMessage)
            {
                mCommController.deleteNoShoolDay((SerializableDeleteDayMessage)messageReceived);
                SerializableNoSchoolDaysList noSchoolDays = mCommController.getNoSchoolDaysList();
                sendBackToClient(handler, mDataSerializer.serialize(noSchoolDays));
            }
            else if (messageReceived is SerializableAddDayMessage)
            {
                mCommController.addNoShoolDay((SerializableAddDayMessage)messageReceived);
                SerializableNoSchoolDaysList noSchoolDays = mCommController.getNoSchoolDaysList();
                sendBackToClient(handler, mDataSerializer.serialize(noSchoolDays));
            }
            else if (messageReceived is SerializableGetMissesPerHourMessage)
            {
                SerializableTeachersMissesPerHourList teachersList = mCommController.getMissesPerHourList((SerializableGetMissesPerHourMessage)messageReceived);
                sendBackToClient(handler, mDataSerializer.serialize(teachersList));
            }

        }
    

        private void SendCallBack(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytes = client.EndSend(ar);

                Console.Out.WriteLine("A total of {0} bytes were sent to the server", bytes);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
        }

        private void sendBackToClient(Socket connection, byte[] pmessage)
        {
            // We store how much data the server should expect
            // in the first 4 bytes of the data we're going to send
            byte[] head = BitConverter.GetBytes(pmessage.Length);

            byte[] total = new byte[pmessage.Length + head.Length];
            head.CopyTo(total, 0);
            pmessage.CopyTo(total, head.Length);

            connection.BeginSend(total, 0, total.Length, SocketFlags.None, new AsyncCallback(SendCallBack), connection);
        }

    }
}
