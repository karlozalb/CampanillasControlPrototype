using CommsModule;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CampanillasControlPrototype
{
    class CommsController : ICommController
    {
        Server mServer;
        ServerUDPDiscoverable mServerUDP;
        MainController mMainController;
        Thread serverThread;
        Thread serverUDPThread;

        public CommsController(MainController pmaincontroller)
        {
            mMainController = pmaincontroller;

            mServer = new Server(this);
            mServerUDP = new ServerUDPDiscoverable();
            serverThread = new Thread(new ThreadStart(mServer.startServer));
            serverThread.Start();
            serverUDPThread = new Thread(new ThreadStart(mServerUDP.startServer));
            serverUDPThread.Start();
        }

        public SerializableTeacherData getTeacherInfo(int pteacherid, DateTime pinit, DateTime pend)
        {
            SerializableTeacherData teacherData = mMainController.getTeacherData(pteacherid, pinit, pend);

            return teacherData;
        }

        public SerializableTeacherList getTeachersList()
        {
            SerializableTeacherList teacherList = mMainController.getTeacherList();

            return teacherList;
        }

        public void addNewAd(SerializableAd pnewad)
        {
            mMainController.addNewAd(pnewad);
        }

        public void stopServer()
        {
            mServer.stop();
            mServerUDP.stop();
        }

        public SerializableAdList getAdList()
        {
            return mMainController.getAdList();
        }

        public void deleteAd(SerializableDeleteAd messageReceived)
        {
            mMainController.deleteAd(messageReceived.mId);
        }

        public SerializableMissingTeachersList getMissingTeachers(SerializableGetMissingTeachersMessage messageReceived)
        {
            return mMainController.getMissingTeachers(messageReceived.init, messageReceived.end);
        }
    }
}
