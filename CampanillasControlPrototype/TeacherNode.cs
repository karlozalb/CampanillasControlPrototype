using System;
using System.Collections.Generic;
using System.Linq;

namespace CampanillasControlPrototype
{
    public class PersonalNode
    {
        private int ID;
        private string NAME;
        private List<DateTime> checkIns;
        private List<HourNode> todaysHours;
        HourNode mLastModifiedHourNode;
        public MissingMessage mThisPersonMissingMessage, mThisPersonAccumulatedAbsenceMessage;

        public PersonalNode(int pid,string pname)
        {
            ID = pid;
            NAME = pname;
            checkIns = new List<DateTime>();
            todaysHours = new List<HourNode>();
        }

        /// <summary>
        /// This method adds a new clock in to this teacher, avoiding duplicates.
        /// It returns true if the date is new (and need to be added here and inside the DB).
        /// </summary>
        /// <param name="pdate">date to add in string format</param>
        public bool addClockIn(DateTime pdate,int ptreshold)
        {
            bool add = true;

            foreach (DateTime date in checkIns)
            {
                if (DateTime.Compare(date, pdate) == 0 && (pdate - date).Minutes < ptreshold)
                {
                    add = false;
                    break;
                }
            }

            if (add)
            {
                checkIns.Add(pdate);
                if (checkIns.Count % 2 == 0) //even clock in, the person is exiting.
                {
                    registerClockOut(pdate);
                }
                else //odd clock in, the person is entering
                {
                    registerClockIn(pdate);
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

        public void registerClockIn(DateTime pdate)
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
                if (!h.mAlreadyChecked)
                {
                    h.mAlreadyChecked = true;
                    h.clockIn(pdate);
                    mLastModifiedHourNode = h;
                    break;
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
        }

        /// <summary>
        /// Destroy the checkin list (normally when the day changes).
        /// </summary>
        public void clearCheckins()
        {
            checkIns.Clear();
        }

        /// <summary>
        /// Destroy the checkin list (normally when the day changes).
        /// </summary>
        public void clearHours()
        {
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
            return mLastModifiedHourNode;
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

        public class HourNode
        {
            public int mHour; // 1 to 6 format time when the person should have arrived. 
            public bool mAlreadyChecked; //Flag to set this node as checked or not, to avoid rechecking it later.
            public DateTime mActualHour; // ACTUAL Time when the person should have arrived.
            public DateTime mClockInTime; // Exact time when the person arrived.
            public TimeSpan mDelay; // mClockInTime - mActualHour


            public HourNode(int hour)
            {
                mHour = hour;
                mAlreadyChecked = false;

                mActualHour = UtilsHelper.getDateTimeByEntranceTime(hour);
            }

            public void clockIn(DateTime pclockin)
            {
                mClockInTime = pclockin;
                mDelay = pclockin - mActualHour;
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
    }

    
}
