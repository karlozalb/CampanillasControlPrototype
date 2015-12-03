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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        Server mServer;
        ServerUDPDiscoverable mServerUDP;
        MainController mMainController;
        Thread serverThread;
        Thread serverUDPThread;

        public CommsController(MainController pmaincontroller)
        {
            mMainController = pmaincontroller;

            mServer = new Server(this);
            //mServerUDP = new ServerUDPDiscoverable();
            serverThread = new Thread(new ThreadStart(mServer.startServer));
            serverThread.Start();
            /*serverUDPThread = new Thread(new ThreadStart(mServerUDP.startServer));
            serverUDPThread.Start();*/
        }

        public SerializableTeacherData getTeacherInfo(int pteacherid, DateTime pinit, DateTime pend)
        {            
            log.Info("info - cliente solicita información de profesor: "+ pteacherid);

            SerializableTeacherData teacherData = mMainController.getTeacherData(pteacherid, pinit, pend);

            return teacherData;
        }

        public SerializableTeacherList getTeachersList()
        {
            log.Info("info - cliente solicita lista de profesores");

            SerializableTeacherList teacherList = mMainController.getTeacherList();

            return teacherList;
        }

        public void addNewAd(SerializableAd pnewad)
        {
            log.Info("info - cliente solicita añadir nuevo anuncio");

            mMainController.addNewAd(pnewad);
        }

        public void stopServer()
        {
            mServer.stop();
            //mServerUDP.stop();
        }

        public SerializableAdList getAdList()
        {
            log.Info("info - cliente solicita lista de anuncios");

            return mMainController.getAdList();
        }

        public void deleteAd(SerializableDeleteAd messageReceived)
        {
            log.Info("info - cliente solicita borrar anuncio");

            mMainController.deleteAd(messageReceived.mId);
        }

        public SerializableMissingTeachersList getMissingTeachers(SerializableGetMissingTeachersMessage messageReceived)
        {
            log.Info("info - cliente solicita lista de profesores con faltas de día completo");
            if (messageReceived.teacherId != 0)
            {
                return mMainController.getMissingTeachers(messageReceived.init, messageReceived.end, messageReceived.teacherId);
            }
            else
            {
                return mMainController.getMissingTeachers(messageReceived.init, messageReceived.end);
            }
        }

        public SerializableTeacherDataList getBadClockInsTeachersData(SerializableGetBadClockInsTeachersListMessage messageReceived)
        {
            log.Info("info - cliente solicita lista de profesores con fichajes impares");

            return mMainController.getBadClockInsTeachersData(messageReceived.mInit, messageReceived.mEnd);
        }

        public void addSubstitute(SerializableAddSubstituteTeacherMessage messageReceived)
        {
            log.Info("info - cliente solicita añadir sustituto");

            mMainController.addSubstitute(messageReceived.mMissingTeacherId, messageReceived.mSubstituteTeacherId);
        }

        public SerializableSubstitutionList getSubstituteList()
        {
            log.Info("info - cliente solicita lista de sustitutos");

            return mMainController.getSubstituteList();
        }

        public void deleteSubstitute(SerializableDeleteSubstituteMessage messageReceived)
        {
            log.Info("info - cliente solicita lista borrar sustituto");

            mMainController.deleteSubstitute(messageReceived.mSubstituteId, messageReceived.mMissingId);
        }

        public SerializableLateClockInsList getLateClockInsList(SerializableGetLateClockInsListMessage messageReceived)
       {
            log.Info("info - cliente solicita lista de profesores con retrasos");

            return mMainController.getLateClockinsList(messageReceived.mInit,messageReceived.mEnd,messageReceived.mDelay);
        }

        public SerializableNoSchoolDaysList getNoSchoolDaysList()
        {
            log.Info("info - cliente solicita lista de días no lectivos");

            return mMainController.getNoSchoolDaysList();
        }

        public void addNoShoolDay(SerializableAddDayMessage messageReceived)
        {
            log.Info("info - cliente solicita añadir día no lectivo");

            foreach (DateTime date in messageReceived.mDays)
            {
                mMainController.addNoSchoolDay(date);
            }
        }

        public void deleteNoShoolDay(SerializableDeleteDayMessage messageReceived)
        {
            log.Info("info - cliente solicita borrar día no lectivo");

            foreach (DateTime date in messageReceived.mDays)
            {
                mMainController.deleteNoSchoolDay(date);
            }
        }

        public SerializableTeachersMissesPerHourList getMissesPerHourList(SerializableGetMissesPerHourMessage messageReceived)
        {
            log.Info("info - cliente solicita lista de faltas por horas");
            if (messageReceived.teacherId != 0)
            {
                return mMainController.getMissesPerHourList(messageReceived.mInit, messageReceived.mEnd, messageReceived.teacherId);
            }
            else
            {
                return mMainController.getMissesPerHourList(messageReceived.mInit, messageReceived.mEnd);
            }
        }
    }
}
