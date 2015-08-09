using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampanillasControlPrototype
{
    class PersonalNode
    {
        private int ID;
        private string NAME;
        private List<DateTime> checkIns;
        private List<HourNode> todaysHours;

        public PersonalNode(int pid,string pname)
        {
            ID = pid;
            NAME = pname;
            checkIns = new List<DateTime>();
            todaysHours = new List<HourNode>();
        }

        /// <summary>
        /// This method adds a new check in to this teacher, avoiding duplicates.
        /// It returns true if the date is new (and need to be added here and inside the DB).
        /// </summary>
        /// <param name="pdate">date to add in string format</param>
        public bool addCheckIn(DateTime pdate)
        {
            bool add = true;

            foreach (DateTime date in checkIns)
            {
                if (DateTime.Compare(date, pdate) == 0)
                {
                    add = false;
                    break;
                }
            }

            if (add)
            {
                checkIns.Add(pdate);
                registerCheckIn(pdate);
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
            string[] splittedHours = phours.Split(',');
            int[] intHours = new int[splittedHours.Length];

            for (int i = 0; i < splittedHours.Length; i++)
            {
                todaysHours.Add(new HourNode(Convert.ToInt32(splittedHours[i])));
            }
        }

        public void registerCheckIn(DateTime pdate)
        {
            foreach (HourNode h in todaysHours)
            {
                if (!h.mAlreadyChecked)
                {

                }
            }
            
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

        private class HourNode
        {
            public int mHour;
            public bool mAlreadyChecked;
            public DateTime mActualHour;
            public DateTime mCheckInTime;           
            
            public HourNode(int hour)
            {
                mHour = hour;
                mAlreadyChecked = false;
            } 
        }
    }

    
}
