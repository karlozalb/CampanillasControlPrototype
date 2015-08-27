using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using SimpleThreadSafeCall;
using CommsModule;
using System.Configuration;
using System.Threading;

namespace CampanillasControlPrototype
{
    class MainController
    {

        DataBaseController mDBController;
        ParadoxDBController mPXDBController;
        AccessScheduleDBController mAccessDBController;

        List<PersonalNode> mPersonal; //This list will contain real-time information about teachers.

        private int currentYear, currentDay, currentMonth;

        private int MINUTES_THRESHOLD = 10; //If the same person clock in in less than THRESHOLD, the system ignores it.

        private int CURRENT_INT_HOUR = 0;
        private int CURRENT_INT_DAY = 0;

        private System.Timers.Timer mTaskTimer;

        private MainWindow mMainWindow;

        Process mExternalProcess;
        private bool mExternalApplicationStarted;
        private string mSystempinExePath;

        public MainController(MainWindow pmainwindow)
        {
            mSystempinExePath = ConfigurationManager.AppSettings["systempinexepath"];

            mMainWindow = pmainwindow;

            UtilsHelper.resetHours();

            updateDay();
            CURRENT_INT_HOUR = UtilsHelper.getCurrentIntHour(DateTime.Now);
            updateHourOnGUI();
            CURRENT_INT_HOUR = -1; //Hago esto para que luego al iniciarse la tarea se vuelva a actualizar, y evitar posibles problemas raros.

            //We obtain the teachers' timetable.
            mAccessDBController = new AccessScheduleDBController();

            //Main database controller.
            mDBController = new DataBaseController();
            //SOLO PARA TESTING mDBController.saveIdAbrevTESTINGData(mAccessDBController.getDictionary());

            //Paradox (SystemPin) database controller.
            mPersonal = new List<PersonalNode>();
            mPXDBController = new ParadoxDBController();
            //mPXDBController.getAllTeachers(mPersonal); //Normalmente obtendremos el personal de los datos de la base de datos Paradox.
            mAccessDBController.getAllTeachersFromAccessTestData(mPersonal); //Solo para las pruebas
            mDBController.createTeachersTables(mPersonal);

            setPersonalTodayHours();  

            currentYear = DateTime.Now.Year;
            currentMonth = DateTime.Now.Month;
            currentDay = DateTime.Now.Day;

            mTaskTimer = new System.Timers.Timer(5000);

            mTaskTimer.Elapsed += new ElapsedEventHandler(startTask);
            mTaskTimer.Enabled = true; // Enable it           

            //mPXDBController.insertTestData();
            //startTask(null,null);
        }

        public void startSystemPin()
        {
            mExternalProcess = new Process();
            mExternalProcess.StartInfo.FileName = mSystempinExePath;
            mExternalProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            mExternalProcess.Start();
            Thread.Sleep(10000);
            if (!mExternalProcess.HasExited) mExternalProcess.Kill();
            mExternalProcess = null;
        }


        /*
        * 1 - Obtener el día actual.
        * 2 - Comprobar si el día ha cambiado
        * 2.1 - Si el día, ha cambiado, establecemos dia "current" como día actual previamente obtenido.
        * 3 - Por cada miembro del personal:
        * 3.1 - Obtenemos de la base de datos de Marcajes los fichajes del día actual.
        * 3.2 - Por cada marcaje:
        * 3.2.1 - Añadimos el marcaje al miembro del personal actual.Si el valor devuelto tras añadir el marcaje es true (no existia anteriormente ese marcaje)
        * 3.2.1.1 - Guardamos el marcaje actual en la base de datos del profesor en cuestión, para que quede registrado.
        *
        * NOTAS: Actualmente no tengo en cuenta que un profesor cumpla, por ejemplo, la primera hora, luego fiche diciendo que se va
        * luego vuelva a fichar al volver, etc. Es decir, tengo que tener en cuenta fichajes pares e impares.
        */

        public void startTask(object sender, ElapsedEventArgs e)
        {
            //startSystemPin();

            if (CURRENT_INT_DAY == 0)
            {
                Debug.WriteLine("Día no lectivo");
                return;
            }

            int newCurrentHour = UtilsHelper.getCurrentIntHour(DateTime.Now);

            int personalSize = mPersonal.Count;

            //If we have a hour change, we clear all the messages showed.
            if (newCurrentHour != CURRENT_INT_HOUR)
            {
                CURRENT_INT_HOUR = newCurrentHour;
                updateHourOnGUI();
                for (int i = 0; i < personalSize; i++)
                {
                    clearMissingMessageIfNeeded(i);
                    clearAccumulatedAbsenceIfNeeded(i);
                }
            }

            //Debug.WriteLine("Hora int actual: "+ CURRENT_INT_HOUR);

            //Get this present day
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            //If the current day has changed, we update our vars and then we clear the worker clock-ins.
            if (hasDayChanged())
            {
                currentYear = DateTime.Now.Year;
                currentMonth = DateTime.Now.Month;
                currentDay = DateTime.Now.Day;

                UtilsHelper.resetHours();

                updateDay();

                for (int i = 0; i < personalSize; i++) mPersonal[i].clearCheckins();

                setPersonalTodayHours();
            }

            for (int i=0;i<personalSize;i++)
            {
                //Debug.WriteLine("Checkeando persona: "+ mPersonal[i].getName());

                //We get the clock-ins from the DB
                List<DateTime> times = mPXDBController.getCheckIns(mPersonal[i].getId(),currentDay,currentMonth,currentYear);

                //Debug.WriteLine("Checkeando clock-ins...");
                //For each clock in, we add it to the worker's clock in list.
                foreach (DateTime checkInTime in times)
                {
                    if (mPersonal[i].addClockIn(checkInTime,MINUTES_THRESHOLD))
                    {
                        PersonalNode.HourNode personNewClockInTime = mPersonal[i].getLastModifiedHourNode();

                        if (personNewClockInTime != null)
                        {
                            //Debug.WriteLine("Añadido nuevo marcaje de " + mPersonal[i].getId() + " en " + personNewClockInTime.mClockInTime);

                            //We store the data in the teacher's specific table, providing the delay time (even if it's negative). 
                            mDBController.registerClockIn(mPersonal[i].getId(), personNewClockInTime.mClockInTime, personNewClockInTime.mActualHour, personNewClockInTime.mDelay);

                            clearMissingMessageIfNeeded(i);
                            clearAccumulatedAbsenceIfNeeded(i);
                        }
                    }                   
                }

                PersonalNode.HourNode currentHourHourNode = mPersonal[i].getHourNodeByInt(CURRENT_INT_HOUR);

                if (currentHourHourNode != null && !currentHourHourNode.mAlreadyChecked)
                {
                    //Person missing, notify the GUI.
                    //Debug.WriteLine(mPersonal[i].getName()+" no está en aula.");

                    if (mPersonal[i].hasAccummulatedAbsence(CURRENT_INT_HOUR))
                    {
                        clearMissingMessageIfNeeded(i);
                        if (mPersonal[i].getAccumulatedAbsenceMessage() == null)
                        {
                            mPersonal[i].addAccumulatedAbsenceMessage(mPersonal[i].getName() + " no está presente.");
                            addItemToAccummulatedAbsenceGUIList(mPersonal[i].getAccumulatedAbsenceMessage());
                        }
                    }
                    else
                    {
                        if (mPersonal[i].getMissingMessage() == null)
                        {
                            string[] classRoomAndSubject = mAccessDBController.getClassRoomAndSubject(mPersonal[i].getId(), CURRENT_INT_DAY, CURRENT_INT_HOUR);

                            mPersonal[i].addMissingMessage(mPersonal[i].getName() + " - Aula: " + classRoomAndSubject[0] + " - Asignatura:" + classRoomAndSubject[1]);
                            addItemToGUIList(mPersonal[i].getMissingMessage());
                        }
                    }                    
                }

            }
        }

        public void  clearMissingMessageIfNeeded(int pindex)
        {
            if (mPersonal[pindex].getMissingMessage() != null)
            {
                removeItemFromGUIList(mPersonal[pindex].getMissingMessage());
                mPersonal[pindex].clearMissingMessage();
            }
        }

        public void clearAccumulatedAbsenceIfNeeded(int pindex)
        {
            if (mPersonal[pindex].getAccumulatedAbsenceMessage() != null)
            {
                removeItemFromAccummulatedAbsenceGUIList(mPersonal[pindex].getAccumulatedAbsenceMessage());
                mPersonal[pindex].clearAccumulatedAbsenceMessage();
            }
        }

        public bool hasDayChanged()
        {
            return !(currentYear == DateTime.Now.Year && currentMonth == DateTime.Now.Month && currentDay == DateTime.Now.Day);
        }

        public void updateDay()
        {
            CURRENT_INT_DAY = UtilsHelper.getCurrentDay(DateTime.Now.DayOfWeek);
            mMainWindow.getDayLabel().SafeInvoke(d => d.Text = UtilsHelper.getPrettyCurrentDay(DateTime.Now.DayOfWeek));
        }

        public void updateHourOnGUI()
        {
            mMainWindow.getHourLabel().SafeInvoke(d => d.Text = UtilsHelper.getPrettyCurrentHour(CURRENT_INT_HOUR));
        }

        public void setPersonalTodayHours()
        {
            if (CURRENT_INT_DAY == 0)
            {
                Debug.WriteLine("Día no lectivo");
                return;
            }

            /*for (int i = 0; i < mPersonal.Count; i++)
            {
                mPersonal[i].clearHours();
                mPersonal[i].addHours(mDBController.getTeacherSchedule(mPersonal[i].getId(),CURRENT_INT_DAY));
            }*/

            for (int i = 0; i < mPersonal.Count; i++)
            {
                mPersonal[i].clearHours();

                PersonSchedule pschedule;

                if (mAccessDBController.getDictionary().TryGetValue(mPersonal[i].getId(), out pschedule))
                {
                    SortedList<int,NodePersonSchedule> todayTimetable;
                    if (pschedule.TIMETABLE.TryGetValue(CURRENT_INT_DAY, out todayTimetable))
                    {
                        foreach(KeyValuePair<int,NodePersonSchedule> entry in todayTimetable)
                        {
                            mPersonal[i].addHour(entry.Key);
                        }
                    }                       
                }
            }
        }

        public void addItemToGUIList(PersonalNode.MissingMessage pmessage)
        {
            mMainWindow.getPersonalListBox().SafeInvoke(d => d.Items.Add(pmessage));
        }

        public void removeItemFromGUIList(PersonalNode.MissingMessage pmessage)
        {
            mMainWindow.getPersonalListBox().SafeInvoke(d => d.Items.Remove(pmessage));
        }

        public void addItemToAccummulatedAbsenceGUIList(PersonalNode.MissingMessage pmessage)
        {
            mMainWindow.getPersonalAccummulatedAbsenceListBox().SafeInvoke(d => d.Items.Add(pmessage));
        }

        public void removeItemFromAccummulatedAbsenceGUIList(PersonalNode.MissingMessage pmessage)
        {
            mMainWindow.getPersonalAccummulatedAbsenceListBox().SafeInvoke(d => d.Items.Remove(pmessage));
        }

        public void stopTask()
        {
            mTaskTimer.Enabled = false;
            mTaskTimer.Stop();
            if (mExternalProcess!=null && mExternalProcess.HasExited)
            {
                mExternalProcess.Kill();
            }
        }

        /**
                NETWORK PART
        */

        public SerializableTeacherData getTeacherData(int pteacherid,DateTime pinit,DateTime pend)
        {
            return mDBController.getTeacherData(pteacherid, pinit,pend);
        }

        public SerializableTeacherList getTeacherList()
        {
            return mDBController.getTeacherList();
        }
    }
}
