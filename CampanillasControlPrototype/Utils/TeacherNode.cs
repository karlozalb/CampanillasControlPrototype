using System;
using System.Collections.Generic;
using System.Linq;

namespace CampanillasControlPrototype
{
    public class PersonalNode
    {
        private int ID;
        private string NAME;
        private List<DateTime> clockIns;
        private List<HourNode> todaysHours;
        HourNode mLastModifiedHourNode;
        public MissingMessage mThisPersonMissingMessage, mThisPersonAccumulatedAbsenceMessage;
        public bool mIsTeacherPresent, mTodayMissingSaved;
        public bool mUnprocessedClockout;

        public PersonalNode(int pid,string pname)
        {
            ID = pid;
            NAME = pname;
            clockIns = new List<DateTime>();
            todaysHours = new List<HourNode>();
            mIsTeacherPresent = false;
        }

        /// <summary>
        /// This method adds a new clock in to this teacher, avoiding duplicates.
        /// It returns true if the date is new (and need to be added here and inside the DB).
        /// </summary>
        /// <param name="pdate">date to add in string format</param>
        public bool addClockIn(DateTime pdate,int ptreshold,int pcurrenthour)
        {
            bool add = true;

            foreach (DateTime date in clockIns)
            {
                if (DateTime.Compare(date, pdate) == 0 && (pdate - date).Minutes < ptreshold)
                {
                    add = false;
                    break;
                }
            }

            if (add)
            {
                clockIns.Add(pdate);
                if (clockIns.Count % 2 == 0) //even clock in, the person is exiting.
                {
                    registerClockOut(pdate);
                    mIsTeacherPresent = false;
                }
                else //odd clock in, the person is entering
                {
                    mIsTeacherPresent = true;
                    registerClockIn(pdate, pcurrenthour);
                }
            }

            return add;
        }

        /// <summary>
        /// This method adds this person working hours to the list
        /// </summary>
        /// <param name="phours"></param>
        public void addHours(string phours)
        {
            //String to int conversion of class' hours.
            if (phours.Contains('-') || phours.Length == 0) return;

            string[] splittedHours = phours.Split(',');
            int[] intHours = new int[splittedHours.Length];

            for (int i = 0; i < splittedHours.Length; i++)
            {
                todaysHours.Add(new HourNode(Convert.ToInt32(splittedHours[i])));
            }
        }

        public void addHour(int phour)
        {
            todaysHours.Add(new HourNode(phour));
        }

        public void registerClockIn(DateTime pdate,int pcurrenthour)
        {
            mLastModifiedHourNode = null;

            //Use cases:
            /*
            * The teacher's entrance time is, for example 8:15 AM, and he arrives:
            * Option A: On time
            *   - We set all hours as checked and we store the clock in in the DB normally (0 or negative delay).
            * Option B: Late/Very late
            *   - We set all hours as checked and we store the clock in in the DB normally (small/big positive delay).
            * Option B: totally missing (at the end of the day)
            *   - We set the person as missing in the DB.
            */

            foreach (HourNode h in todaysHours)
            {
                if (!h.mAlreadyChecked && h.mHour <= pcurrenthour)
                {
                    h.mAlreadyChecked = true;
                    h.clockIn(pdate);
                    mLastModifiedHourNode = h;
                    break;
                }
            }

            foreach (HourNode h in todaysHours)
            {
                if (h.mHour <= pcurrenthour)
                {
                    h.mAlreadyChecked = true;
                }
            }
        }

        public bool hasAccummulatedAbsence(int pcurrenthour)        {

            if (pcurrenthour <= 1) return false;

            int index = 0;

            //Si la hora anterior el profesor no está, o no ha estado en ninguna de las horas anteriores... chungo.

            for (int i = 0; i < todaysHours.Count; i++)
            {
                if (todaysHours[i].mHour == pcurrenthour)
                {
                    index = i;
                    break;
                }
            }

            return index > 0 && !todaysHours[index - 1].mAlreadyChecked || notPresentInEarlyHours(pcurrenthour);           
        }

        private bool notPresentInEarlyHours(int pcurrenthour)
        {
            if (todaysHours.Count > 0 && todaysHours[0].mHour == pcurrenthour) return false;

            for (int i = 0; i < todaysHours.Count; i++)
            {
                if (todaysHours[i].mHour < pcurrenthour && todaysHours[i].mAlreadyChecked)
                {
                    return false;
                }
            }

            return true;
        }      

        public void registerClockOut(DateTime pdate)
        {
            mUnprocessedClockout = true;
        }

        public void setTeacherPresentIfNeeded(int pcurrentHour)
        {          
            HourNode h = getHourNodeByInt(pcurrentHour);
            if (h!=null && mIsTeacherPresent)
            {
                h.mAlreadyChecked = true;
            }
        }

        public bool thereAreNoMoreHoursToday(int pcurrentHour)
        {
            if (todaysHours.Count > 0)
            {
                HourNode h = todaysHours[todaysHours.Count - 1];

                if (h.mHour < pcurrentHour)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public bool notPresentToday()
        {
            foreach (HourNode h in todaysHours)
            {
                if (h.mAlreadyChecked) return false;
            }

            return todaysHours.Count > 0;
        }

        /// <summary>
        /// Destroy the checkin list (normally when the day changes).
        /// </summary>
        public void clearCheckins()
        {
            clockIns.Clear();
        }

        /// <summary>
        /// Destroy the checkin list (normally when the day changes).
        /// </summary>
        public void clearHours()
        {
            mTodayMissingSaved = false;
            mIsTeacherPresent = false;
            todaysHours.Clear();
        }

        public int getId()
        {
            return ID;
        }

        public string getName()
        {
            return NAME;
        }

        public HourNode getLastModifiedHourNode()
        {
            HourNode temp = mLastModifiedHourNode;
            mLastModifiedHourNode = null;
            return temp;
        }

        public bool isThereAnyClockOutUnprocessed()
        {
            return mUnprocessedClockout;
        }

        public DateTime getLastClockOutTime()
        {
            mUnprocessedClockout = false;
            return clockIns.Last<DateTime>();
        }

        public HourNode getHourNodeByInt(int pinthour)
        {
            for (int i = 0; i < todaysHours.Count; i++)
            {
                if (todaysHours[i].mHour == pinthour)
                {
                    return todaysHours[i];
                }
            }

            return null;
        }

        public bool hasHoursToday()
        {
            return todaysHours.Count > 0;
        }

        public void clearMissingMessage()
        {
            mThisPersonMissingMessage = null;
        }

        public void addMissingMessage(string pmessage)
        {
            mThisPersonMissingMessage = new MissingMessage(pmessage);
        }

        public MissingMessage getMissingMessage()
        {
            return mThisPersonMissingMessage;
        }

        public void clearAccumulatedAbsenceMessage()
        {
            mThisPersonAccumulatedAbsenceMessage = null;
        }

        public void addAccumulatedAbsenceMessage(string pmessage)
        {
            mThisPersonAccumulatedAbsenceMessage = new MissingMessage(pmessage);
        }

        public MissingMessage getAccumulatedAbsenceMessage()
        {
            return mThisPersonAccumulatedAbsenceMessage;
        }        

        public override string ToString()
        {
            return ID + " - " + NAME;
        }

        public bool clockInNeededToBeAdded(DateTime pdate,int ptreshold)
        {
            bool add = true;

            foreach (DateTime date in clockIns)
            {
                if (DateTime.Compare(date, pdate) == 0 && (pdate - date).Minutes < ptreshold)
                {
                    add = false;
                    break;
                }
            }
            return add;
        }

        public ClockInDataToStore addClockInv2(DateTime pdate,int pcurrenthour)
        {
            clockIns.Add(pdate);

            if (clockIns.Count % 2 == 0) //even clock in, the person is exiting.
            {
                return registerClockOutv2(pdate, pcurrenthour);
            }
            else //odd clock in, the person is entering
            {
                return registerClockInv2(pdate, pcurrenthour);
            }
        }

        public ClockInDataToStore registerClockInv2(DateTime pdate,int pcurrenthour)
        {
            //Buscamos primera hora no fichada y la establecemos como fichada.
            //Esto puede ser confuso, sobre todo si la persona ficha para salir y luego ficha para entrar. En ese
            //caso, estaría fichando para la siguiente hora con adelanto, pero esto está bien asi teniendo en cuenta
            //que si ficha para salir se desmarcarán todas las horas superiores a la actual como fichadas.
            int i = 0;
            HourNode h = null;

            for (i=0;i<todaysHours.Count;i++)
            {
                h = todaysHours[i];
                if (!h.mAlreadyChecked)
                {
                    h.mAlreadyChecked = true;
                    h.clockIn(pdate);
                    break;
                }
            }

            //Establecemos como fichadas todas las horas anteriores a la hora actual, y la hora actual.
            for (int j = 0; j < todaysHours.Count; j++)
            {
                HourNode temp = todaysHours[j];
                if (!temp.mAlreadyChecked && temp.mHour <= pcurrenthour)
                {
                    temp.mAlreadyChecked = true;                   
                }
            }

            ClockInDataToStore returnData = null;

            if (h == null)
            {
                h = todaysHours[todaysHours.Count - 1];
                //Si estamos aqui es porque el profesor ha fichado en su última hora, despues de que éste haya fichado para salir
                //es decir, entró a cierta hora, y durante su última hora lectiva se fue (clockout) y volvió (clockin).
                returnData = new ClockInDataToStore(pdate, pdate, h.mActualHour, 0, true);
            }
            else
            {
                returnData = new ClockInDataToStore(pdate,h.mClockInTime,h.mActualHour,h.mDelay.Hours*60+h.mDelay.Minutes,true);
            }

            mIsTeacherPresent = true; //El profesor está presente.   

            return returnData;
        }

        public ClockInDataToStore registerClockOutv2(DateTime pdate, int pcurrenthour)
        {
            mIsTeacherPresent = false; //El profesor NO está presente.   

            //Lo importante del marcaje de salida es la fecha y la hora. El delay será 0 y la hora a la que debería haber entrado irrelevante.
            ClockInDataToStore returnData = new ClockInDataToStore(pdate, pdate,Convert.ToDateTime("00:00 AM"),0,false);

            //Establecemos todas las horas SIGUIENTES a la hora actual como no fichadas.
            for (int j = 0; j < todaysHours.Count; j++)
            {
                HourNode h = todaysHours[j];
                if (h.mHour > pcurrenthour)
                {
                    h.mAlreadyChecked = false;
                }
            }

            return returnData;
        }

        public class HourNode
        {
            public int mHour; // 1 to 6 format time when the person should have arrived. 
            public bool mAlreadyChecked; //Flag to set this node as checked or not, to avoid rechecking it later.
            public DateTime mActualHour; // ACTUAL Time when the person should have arrived.
            public DateTime mClockInTime; // Exact time when the person arrived.
            public TimeSpan mDelay; // mClockInTime - mActualHour
            public bool isClockIn;

            public HourNode(int hour)
            {
                mHour = hour;
                mAlreadyChecked = false;

                mActualHour = UtilsHelper.getDateTimeByEntranceTime(hour);
            }

            public void clockIn(DateTime pclockin)
            {
                isClockIn = true;
                mClockInTime = pclockin;
                mDelay = pclockin - mActualHour;                
            }

            public void clockOut(DateTime pclockin)
            {
                isClockIn = false;
                mClockInTime = pclockin;
            }
        }

        public class MissingMessage
        {
            public string missingString;

            public MissingMessage(string pmessage)
            {
                missingString = pmessage;
            }

            public override string ToString()
            {
                return missingString;
            }
        }

        public class ClockInDataToStore
        {
            public bool CLOCK_IN;
            public DateTime DATE;
            public DateTime REAL_ENTRANCE_TIME,ENTRANCE_TIME_SHOULD_HAVE_ARRIVED;
            public int DELAY_IN_MINUTES;

            public ClockInDataToStore(DateTime pdate, DateTime pentrancetime, DateTime pshouldvearrived,int pdelay,bool pclockin)
            {
                CLOCK_IN = pclockin;
                DATE = pdate;
                REAL_ENTRANCE_TIME = pentrancetime;
                ENTRANCE_TIME_SHOULD_HAVE_ARRIVED = pshouldvearrived;
                DELAY_IN_MINUTES = pdelay;
            }
        }
    }

    
}
