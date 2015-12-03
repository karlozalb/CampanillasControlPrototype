using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SimpleThreadSafeCall;
using CommsModule;
using System.Drawing;
using System.Windows.Forms;

namespace CampanillasControlPrototype
{
    class MainController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string TEMP_PATH;

        DataBaseController mDBController;
        ParadoxDBController mPXDBController;
        AccessScheduleDBController mAccessDBController;
        AdPanelController mAdPanelController;
        MySQLDBController mMySQLController;

        List<PersonalNode> mPersonal; //This list will contain real-time information about teachers.

        private int currentYear, currentDay, currentMonth;

        private int MINUTES_THRESHOLD = 10; //If the same person clock in in less than THRESHOLD, the system ignores it.

        private int CURRENT_INT_HOUR = 0;
        private int CURRENT_INT_DAY = 0;

        private System.Threading.Timer mTaskTimer;

        private MainWindow mMainWindow;

        private DateTime mCurrentDate,mLastStoredTime;
        private List<DateTime> mNoSchoolDays;

        bool mEnabled = true;

        public MainController(MainWindow pmainwindow)
        {          
            mMainWindow = pmainwindow;            

            UtilsHelper.resetHours();

            updateDay();
            CURRENT_INT_HOUR = UtilsHelper.getCurrentIntHour(DateTime.Now);
            updateHourOnGUI();
            CURRENT_INT_HOUR = -1; //Hago esto para que luego al iniciarse la tarea se vuelva a actualizar, y evitar posibles problemas raros.

            //We obtain the teachers' timetable.
            mAccessDBController = new AccessScheduleDBController();

            //Main database controller.
            mMySQLController = new MySQLDBController();
            //mMySQLController.testConnection();

            mDBController = new DataBaseController(mMySQLController);
            //CUIDADO TESTING!!!!
            //*********************************
            //mDBController.DEBUG_DONT_USE_THIS_DELETEALLTABLES();
            //*********************************

            //Paradox (SystemPin) database controller.
            mPersonal = new List<PersonalNode>();
            mPXDBController = new ParadoxDBController();
            mPXDBController.getAllTeachers(mPersonal); //Normalmente obtendremos el personal de los datos de la base de datos Paradox.

            //mAccessDBController.getAllTeachersFromAccessTestData(mPersonal); //Solo para las pruebas
            mDBController.createTeachersTables(mPersonal);

            //Días no lectivos
            SerializableNoSchoolDaysList noSchoolDayList = mDBController.getNoSchoolDayList();
            mNoSchoolDays = noSchoolDayList.mDaysList;

            processSubstitutes(mPersonal,mDBController.getSubstitutes());

            //mDBController.saveIdAbrevTESTINGData(mPersonal);

            mAdPanelController = new AdPanelController(mMainWindow,mDBController);
            //mAdPanelController.updateAds();

            currentYear = DateTime.Now.Year;
            currentMonth = DateTime.Now.Month;
            currentDay = DateTime.Now.Day;

            mLastStoredTime = Convert.ToDateTime(Properties.Settings.Default.LastCheckedDate.ToShortDateString());
            mCurrentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            if (DateTime.Compare(mLastStoredTime, Convert.ToDateTime("01/01/0001")) == 0 || DateTime.Compare(mLastStoredTime, mCurrentDate) > 0)
            {
                Properties.Settings.Default.LastCheckedDate = DateTime.Now;
                Properties.Settings.Default.Save();
            }
            else
            {                
                //Si las fechas no son iguales quiere decir que hemos apagado el PC antes de que se guarden las faltas.
                while (DateTime.Compare(mLastStoredTime, mCurrentDate) != 0)
                {
                    log.Info("Incongruencia entre fecha actual y fecha guardada: "+ mLastStoredTime +" VS "+ mCurrentDate + " - ACTUALIZANDO.");

                    if (isASchoolDay(mLastStoredTime))
                    {
                        log.Info("Fecha: "+mLastStoredTime+" es día lectivo, calculando faltas de profesores.");
                        setPersonalTodayHours(UtilsHelper.getCurrentDay(mLastStoredTime.DayOfWeek), mLastStoredTime);
                        saveTeacherMisses(mLastStoredTime);
                    }
                    mLastStoredTime = mLastStoredTime.AddDays(1);
                }                
            }

            setPersonalTodayHours(CURRENT_INT_DAY,DateTime.Now);

            /*mTaskTimer = new System.Timers.Timer(50000);

            mTaskTimer.Elapsed += new ElapsedEventHandler(startTaskV2);
            mTaskTimer.Enabled = true;   */

            //mTaskTimer = new System.Threading.Timer(startTaskV2,null,25000,25000);

            //mPXDBController.insertTestData();
            //startTask(null,null);

            //startTaskV2(null,null);

            Thread mainTaskThread = new Thread(new ThreadStart(taskManagement));
            mainTaskThread.Start();
        }

        public void taskManagement()
        {
            while (mEnabled)
            {
                Debug.WriteLine("Ejecutando tarea: " + System.Diagnostics.Process.GetCurrentProcess().WorkingSet64);
                startTaskV2(null);
                Debug.WriteLine("Tarea finalizada tarea: " + System.Diagnostics.Process.GetCurrentProcess().WorkingSet64);
                Thread.Sleep(15000);
            }
        }

        internal SerializableTeachersMissesPerHourList getMissesPerHourList(DateTime pinit, DateTime pend)
        {
            return mDBController.getTeacherMissesPerHourList(mPersonal,pinit,pend,0);
        }

        internal SerializableTeachersMissesPerHourList getMissesPerHourList(DateTime pinit, DateTime pend, int pteacherid)
        {
            return mDBController.getTeacherMissesPerHourList(mPersonal, pinit, pend, pteacherid);
        }

        private bool isASchoolDay(DateTime pday)
        {
            if (pday.DayOfWeek == DayOfWeek.Saturday || pday.DayOfWeek == DayOfWeek.Sunday) return false;

            foreach (DateTime date in mNoSchoolDays)
            {
                if (date.Date == pday.Date) return false;
            }

            return true;
        }

        internal void deleteNoSchoolDay(DateTime date)
        {
            mDBController.removeNoSchoolDay(date);
        }

        internal void addNoSchoolDay(DateTime date)
        {
            mDBController.addNoSchoolDay(date);
        }

        internal SerializableNoSchoolDaysList getNoSchoolDaysList()
        {
            return mDBController.getNoSchoolDayList();
        }

        internal SerializableLateClockInsList getLateClockinsList(DateTime pinit, DateTime pend, int pdelay)
        {
            return mDBController.getAllTeachersLateClockins(mPersonal,pinit,pend,pdelay);
        }

        internal void deleteSubstitute(int psubstituteid, int pmissingid)
        {
            mDBController.deleteSubstitute(psubstituteid, pmissingid);


        }

        internal void addSubstitute(int pmissing, int psubstitute)
        {
            mDBController.addSubstitute(pmissing,psubstitute);
        }

        private void processSubstitutes(List<PersonalNode> ppersonal, List<SerializableSubstituteTeacherNode> psubstitutelist)
        {
            foreach (PersonalNode p in ppersonal)
            {
                foreach (SerializableSubstituteTeacherNode s in psubstitutelist)
                {
                    if (p.getId() == s.mSubstituteId)
                    {
                        p.mMissingTeacherId = s.mMissingId; //A partir de ahora este profesor cogerá el horario del profesor ausente.
                    }else if (p.getId() == s.mMissingId)
                    {
                        p.isBeingReplaced = true; //Esto es para que no se muestren los mensajes del profe ausente en la pantalla.               
                    }
                }
            }
        }

        public SerializableTeacherDataList getBadClockInsTeachersData(DateTime pinit,DateTime pend)
        {
            return mDBController.getAllTeachersOddClockins(mPersonal, pinit,pend);
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
        

        //public void startTaskV2(object sender, ElapsedEventArgs e)
        public void startTaskV2(object pthreaddata)
        {
            if (!isASchoolDay(DateTime.Now) ||  CURRENT_INT_DAY == 6 || CURRENT_INT_DAY == 7)
            {
                Debug.WriteLine("Día no lectivo");
                updateDay();
                return;
            }

            log.Info("info - Ejecutando tarea.");

            int newCurrentHour = UtilsHelper.getCurrentIntHour(DateTime.Now);

            int personalSize = mPersonal.Count;

            //If we have a hour change, we clear all the messages showed.
            if (newCurrentHour != CURRENT_INT_HOUR)
            {
                log.Info("info - Ha habido cambio de hora, actualizando...");

                CURRENT_INT_HOUR = newCurrentHour;
                updateHourOnGUI();
                for (int i = 0; i < personalSize; i++)
                {
                    mPersonal[i].setTeacherPresentIfNeeded(CURRENT_INT_HOUR);
                    clearMissingMessageIfNeeded(i);
                    clearAccumulatedAbsenceIfNeeded(i);                                      
                }

                if (UtilsHelper.isPostSchoolHour(CURRENT_INT_HOUR))
                {
                    saveTeacherMisses(mCurrentDate);
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
                log.Info("info - Ha habido cambio de día, actualizando...");

                currentYear = DateTime.Now.Year;
                currentMonth = DateTime.Now.Month;
                currentDay = DateTime.Now.Day;

                saveTeacherMisses(mCurrentDate);
                mCurrentDate = DateTime.Now;

                UtilsHelper.resetHours();

                updateDay();

                for (int i = 0; i < personalSize; i++) mPersonal[i].clearCheckins();

                setPersonalTodayHours(CURRENT_INT_DAY,DateTime.Now);
            }

            Dictionary<int, List<DateTime>> timesList = mPXDBController.getCheckIns(currentDay, currentMonth, currentYear);

            for (int i = 0; i < personalSize; i++)
            {
                //Si está siendo reemplazado, pasamos de él completamente.
                if (mPersonal[i].isBeingReplaced) continue;

                //We get the clock-ins from the DB
                //List<DateTime> times = mPXDBController.getCheckIns(mPersonal[i].getId(), currentDay, currentMonth, currentYear);
                List<DateTime> times;

                timesList.TryGetValue(mPersonal[i].getId(), out times);

                //Debug.WriteLine("Checkeando clock-ins...");
                //For each clock in, we add it to the worker's clock in list.

                log.Info("info - Checkeando fichajes y mostrando información en la pantalla cuando es necesario.");

                if (times != null)
                {
                    foreach (DateTime checkInTime in times)
                    {
                        if (mPersonal[i].clockInNeededToBeAdded(checkInTime, MINUTES_THRESHOLD))
                        {
                            PersonalNode.ClockInDataToStore data = mPersonal[i].addClockInv2(checkInTime, CURRENT_INT_HOUR);

                            mDBController.registerClockIn(mPersonal[i].getId(), data.REAL_ENTRANCE_TIME, data.ENTRANCE_TIME_SHOULD_HAVE_ARRIVED, data.DELAY_IN_MINUTES, data.CLOCK_IN);
                        }
                    }
                }

                //SOLO MOSTRAMOS EN PANTALLA SI ES PERSONAL DEL IES y NO es del PAS
                if (mPersonal[i].HEADQUARTERS == PersonSchedule.IES && !mPersonal[i].isPASmember)
                {

                    PersonalNode.HourNode currentHourNode = mPersonal[i].getHourNodeByInt(CURRENT_INT_HOUR);
                    if (currentHourNode != null)
                    {
                        /*********************************************************************************
                        ***************************** HORA LECTIVA NORMAL *******************************
                        *********************************************************************************/
                        if (currentHourNode.mHourType == PersonalNode.HourNode.SCHOOL)
                        {
                            if (/*!currentHourNode.mAlreadyChecked*/!mPersonal[i].mIsTeacherPresent)
                            {
                                if (mPersonal[i].hasAccummulatedAbsence(CURRENT_INT_HOUR))
                                {
                                    clearMissingMessageIfNeeded(i);
                                    if (mPersonal[i].getAccumulatedAbsenceMessage() == null)
                                    {
                                        mPersonal[i].addAccumulatedAbsenceMessage(mPersonal[i].getName(), CURRENT_INT_HOUR);
                                        addItemToAccummulatedAbsenceGUIList(mPersonal[i].getAccumulatedAbsenceMessage());
                                    }
                                }
                                else
                                {
                                    if (mPersonal[i].getMissingMessage() == null)
                                    {
                                        string[] classRoomAndSubject = null;

                                        if (mPersonal[i].mMissingTeacherId != -1)
                                        {
                                            classRoomAndSubject = mAccessDBController.getClassRoomAndSubject(mPersonal[i].mMissingTeacherId, CURRENT_INT_DAY, CURRENT_INT_HOUR);
                                        }
                                        else
                                        {
                                            classRoomAndSubject = mAccessDBController.getClassRoomAndSubject(mPersonal[i].getId(), CURRENT_INT_DAY, CURRENT_INT_HOUR);
                                        }

                                        mPersonal[i].addMissingMessage(mPersonal[i].getName() + " - " + classRoomAndSubject[0] + " - " + classRoomAndSubject[1]);
                                        addItemToGUIList(mPersonal[i].getMissingMessage());
                                    }
                                }
                            }
                            else
                            {
                                clearMissingMessageIfNeeded(i);
                                clearAccumulatedAbsenceIfNeeded(i);
                            }
                        }
                        /*********************************************************************************
                       ***************************** HORA DE GUARDIA *******************************
                       *********************************************************************************/
                        else if (currentHourNode.mHourType == PersonalNode.HourNode.GUARD)
                        {
                            if (mPersonal[i].getMissingMessage() == null)
                            {
                                string teacherName = mPersonal[i].getName();

                                //Estoy usando el missing message por no escribir codigo igual pero con otros nombre, pero realmente este profesor puede estar presente, o no.
                                mPersonal[i].addMissingMessage(teacherName);
                                addItemToGuardGUIList(mPersonal[i].getMissingMessage());
                            }

                            if (!mPersonal[i].mIsTeacherPresent)
                            {
                                mPersonal[i].getMissingMessage().setColor(Brushes.Red);
                            }
                            else
                            {
                                mPersonal[i].getMissingMessage().setColor(Brushes.Black);
                            }
                            invalidateGuardListBox();
                        }
                        /*********************************************************************************
                        ***************************** GUARDIA DIRECTIVA *******************************
                        *********************************************************************************/
                        else if (currentHourNode.mHourType == PersonalNode.HourNode.DIR_GUARD)
                        {
                            if (mPersonal[i].getMissingMessage() == null)
                            {
                                string teacherName = mPersonal[i].getName();

                                //Estoy usando el missing message por no escribir codigo igual pero con otros nombre, pero realmente este profesor puede estar presente, o no.
                                mPersonal[i].addMissingMessage(teacherName);
                                addItemToExecutiveGUIList(mPersonal[i].getMissingMessage());
                            }

                            if (!mPersonal[i].mIsTeacherPresent)
                            {
                                mPersonal[i].getMissingMessage().setColor(Brushes.Red);
                            }
                            else
                            {
                                mPersonal[i].getMissingMessage().setColor(Brushes.Blue);
                            }
                            invalidateDirectListBox();
                        }
                        /*********************************************************************************
                        ***************************** FUNCION DIRECTIVA *******************************
                        *********************************************************************************/
                        else if (currentHourNode.mHourType == PersonalNode.HourNode.DIR_STANDARD)
                        {
                            if (mPersonal[i].getMissingMessage() == null)
                            {
                                string teacherName = mPersonal[i].getName();

                                //Estoy usando el missing message por no escribir codigo igual pero con otros nombre, pero realmente este profesor puede estar presente, o no.
                                mPersonal[i].addMissingMessage(teacherName);
                                addItemToExecutiveGUIList(mPersonal[i].getMissingMessage());
                            }

                            if (!mPersonal[i].mIsTeacherPresent)
                            {
                                mPersonal[i].getMissingMessage().setColor(Brushes.Red);
                            }
                            else
                            {
                                mPersonal[i].getMissingMessage().setColor(Brushes.Black);
                            }
                            invalidateDirectListBox();
                        }
                    }
                    else
                    {
                        if (!mPersonal[i].mIsTeacherPresent)
                        {
                            if (mPersonal[i].hasAccummulatedAbsence(CURRENT_INT_HOUR))
                            {
                                clearMissingMessageIfNeeded(i);
                                if (mPersonal[i].getAccumulatedAbsenceMessage() == null)
                                {
                                    mPersonal[i].addAccumulatedAbsenceMessage(mPersonal[i].getName(), CURRENT_INT_HOUR);
                                    addItemToAccummulatedAbsenceGUIList(mPersonal[i].getAccumulatedAbsenceMessage());
                                }
                            }
                        }
                        else
                        {
                            clearAccumulatedAbsenceIfNeeded(i);
                        }
                    }
                }
            }

            timesList.Clear();
        }

        public void saveTeacherMisses(DateTime pdate)
        {
            foreach (PersonalNode p in mPersonal)
            {
                if (p.hasHoursToday())
                {
                    mDBController.addTeacherMiss(p.getId(), pdate, p);
                }
            }

            mMySQLController.startToSendData();

            Properties.Settings.Default.LastCheckedDate = DateTime.Now;
            Properties.Settings.Default.Save();
        }

        public void  clearMissingMessageIfNeeded(int pindex)
        {
            if (mPersonal[pindex].getMissingMessage() != null)
            {
                removeItemFromGUIList(mPersonal[pindex].getMissingMessage());
                removeItemFromExecutiveGUIList(mPersonal[pindex].getMissingMessage());
                removeItemFromGuardGUIList(mPersonal[pindex].getMissingMessage());
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
            mMainWindow.getDayLabel().SafeInvoke(d => d.Text = UtilsHelper.getPrettyCurrentDay(DateTime.Now));
        }

        public void updateHourOnGUI()
        {
            mMainWindow.getHourLabel().SafeInvoke(d => d.Text = UtilsHelper.getPrettyCurrentHour(CURRENT_INT_HOUR));
        }

        public void setPersonalTodayHours(int pday,DateTime pdate)
        {
            if (pday == 0)
            {
                Debug.WriteLine("Día no lectivo");
                return;
            }            

            for (int i = 0; i < mPersonal.Count; i++)
            {
                updateTeacher(pday,i, pdate);
            }
        }       
        
        private void updateTeacher(int pday,int pindex,DateTime pdatetimeformatday)
        {
            mPersonal[pindex].clearHours();

            int idToCheck = mPersonal[pindex].getId();

            if (mPersonal[pindex].mMissingTeacherId != -1)
            {
                //Si el id de profesor faltante es distinto de -1, este profesor se trata de un sustituto, y tenemos que coger el horario
                //del profesor al que sustituye, por eso cambio el ID aquí.
                idToCheck = mPersonal[pindex].mMissingTeacherId;
            }

            PersonSchedule pschedule;

            if (mAccessDBController.getDictionary().TryGetValue(idToCheck, out pschedule))
            {
                mPersonal[pindex].HEADQUARTERS = pschedule.HEADQUARTERS;
                SortedList<int, NodePersonSchedule> todayTimetable;
                if (pschedule.TIMETABLE.TryGetValue(pday, out todayTimetable))
                {
                    foreach (KeyValuePair<int, NodePersonSchedule> entry in todayTimetable)
                    {
                        mPersonal[pindex].addHour(entry.Key, entry.Value.SUBJECT);
                    }
                    //A la hora de obtener clockins pasados, si hacemos uso del ID del profesor sustituto y no del id de obtención del horario.
                    SerializableTeacherData tdata = mDBController.getTodayTeacherData(mPersonal[pindex].getId(), pdatetimeformatday);
                    //Añadido para tener en cuenta apagados de la máquina y pérdida de fichajes en memoria pero no en BD.
                    if (tdata.mClockins != null && tdata.mClockins.Count > 0)
                    {
                        for (int i = UtilsHelper.GT1; i < UtilsHelper.OFFTIME; i++) //Todas las horas del dia
                        {
                            mPersonal[pindex].setTeacherPresentIfNeeded(i);
                            foreach (SerializableTeacherData.ClockInDataNode cidn in tdata.mClockins)
                            {
                                //mPersonal[pindex].forcedAddClockIn(cidn.day);
                                //Supuestamente esto debe dejar los fichajes exactamente como estaban (y los flags de horas faltadas también).
                                DateTime completeDate = Convert.ToDateTime(cidn.day.ToShortDateString() + " " + cidn.entranceHour.ToString());
                                if (UtilsHelper.getCurrentIntHour(completeDate) == i)
                                {
                                    mPersonal[pindex].addClockInv2(completeDate, UtilsHelper.getCurrentIntHour(completeDate));
                                }
                            }
                        }
                    }
                }
            }
        }     

        public void setTeacherLabelColor(PersonalNode.MissingMessage pmessage,Color pcolor)
        {
            ListBox list = mMainWindow.getPersonalListBox();
            int index = list.Items.IndexOf(pmessage);
            list.SafeInvoke(d => d.Items[index]);
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

        public void addItemToGuardGUIList(PersonalNode.MissingMessage pmessage)
        {
            mMainWindow.getGuardPersonalListBox().SafeInvoke(d => d.Items.Add(pmessage));
        }

        public void removeItemFromGuardGUIList(PersonalNode.MissingMessage pmessage)
        {
            mMainWindow.getGuardPersonalListBox().SafeInvoke(d => d.Items.Remove(pmessage));
        }

        public void addItemToExecutiveGUIList(PersonalNode.MissingMessage pmessage)
        {
            mMainWindow.getExecutiveListBox().SafeInvoke(d => d.Items.Add(pmessage));
        }

        public void removeItemFromExecutiveGUIList(PersonalNode.MissingMessage pmessage)
        {
            mMainWindow.getExecutiveListBox().SafeInvoke(d => d.Items.Remove(pmessage));
        }

        public void invalidateDirectListBox()
        {
            mMainWindow.getExecutiveListBox().SafeInvoke(d => d.Invalidate());
        }

        public void invalidateGuardListBox()
        {
            mMainWindow.getGuardPersonalListBox().SafeInvoke(d => d.Invalidate());
        }

        public void stopTask()
        {
            /*mTaskTimer.Enabled = false;
            mTaskTimer.Stop();
            mTaskTimer.Dispose();*/
            mEnabled = false;
            mAdPanelController.stopTask();
            mMySQLController.stopMySQL();
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
            //La versión anterior daba la lista de profesores almacenada en memoria, pero esto no es preciso,
            //ya que si añadimos un profesor sustituto al lector en tiempo real no se manda la lista actualizada.

            SerializableTeacherList teacherList = new SerializableTeacherList();

            //Creamos una nueva lista de personal actualizada obtenida de la BD de SystemPin.
            List<PersonalNode> updatedTeacherList = new List<PersonalNode>();
            mPXDBController.getAllTeachers(updatedTeacherList);

            foreach (PersonalNode teacher in updatedTeacherList)
            {
                SerializableTeacherList.TeacherData newNode = new SerializableTeacherList.TeacherData();

                newNode.mId = teacher.getId();
                newNode.mTeacherName = teacher.getName();

                teacherList.add(newNode);
            }         

            return teacherList;
        }

        public void addNewAd(SerializableAd pnewad)
        {
            mDBController.addAd(pnewad.mText,pnewad.mDate);
            mAdPanelController.updateAds();
        }

        public SerializableAdList getAdList()
        {
            return mDBController.getAdList();
        }

        public SerializableSubstitutionList getSubstituteList()
        {
            List<SerializableSubstituteTeacherNode> subs = mDBController.getSubstitutes();

            foreach (SerializableSubstituteTeacherNode sub in subs)
            {
                foreach (PersonalNode p in mPersonal)
                {
                    if (p.getId() == sub.mSubstituteId)
                    {
                        sub.mSubstituteName = p.getName();
                    }else if (p.getId() == sub.mMissingId)
                    {
                        sub.mMissingName = p.getName();
                    }
                }
            }

            SerializableSubstitutionList subsList = new SerializableSubstitutionList();
            subsList.mSubstitutesNodes = subs;

            return subsList;
        }

        public void deleteAd(int pid)
        {
            mDBController.deleteAd(pid);
            mAdPanelController.updateAds();
        }

        public SerializableMissingTeachersList getMissingTeachers(DateTime pinit, DateTime pend)
        {
            return mDBController.getMissingTeachers(mPersonal,pinit,pend,0);
        }

        public SerializableMissingTeachersList getMissingTeachers(DateTime pinit, DateTime pend,int pteacherid)
        {
            return mDBController.getMissingTeachers(mPersonal, pinit, pend, pteacherid);
        }

        public void updatePersonalList()
        {
            List<PersonalNode> updatedPersonalNodeList = new List<PersonalNode>();
            mPXDBController.getAllTeachers(updatedPersonalNodeList); //Reobtenemos la lista actualizada de personal (esto ya incluirá las nuevas personas añadidas desde Systempin).

            List<SerializableSubstituteTeacherNode> subsList = mDBController.getSubstitutes(); //Obtenemos los sustitutos para asignar luego horario.

            processSubstitutes(updatedPersonalNodeList, subsList);

            //Recorremos la lista de personal ACTUALIZADA en busca de diferencias con la lista de personal ANTIGUA, y añadimos a aquellas nuevas personas a la
            //lista nueva. ¿Por qué no cambiar una lista por otra? Porque la lista antigua contiene información de fichajes y es menos costoso añadir una persona
            //que actualizar una lista completa con información de otra.
            foreach (PersonalNode p1 in updatedPersonalNodeList)
            {
                bool add = true;
                //Recorremos la lista de personal en busca de concidencias
                foreach (PersonalNode p2 in mPersonal)
                {
                    if (p1.getId() == p2.getId())
                    {
                        add = false;
                        break;
                    }
                }

                //Si no hay coincidencia, quiere decir que la lista de personal no incluye al nuevo profesor añadido, luego necesitamos añadirlo a la lista.
                if (add)
                {
                    mPersonal.Add(p1);
                    updateTeacher(CURRENT_INT_DAY,mPersonal.IndexOf(p1),DateTime.Now);                 
                }                
            }            
        }        
    }
}
