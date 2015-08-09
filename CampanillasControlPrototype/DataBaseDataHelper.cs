using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampanillasControlPrototype
{
    public static class DataBaseDataHelper
    {
        /// <summary>
        ///  Get the current day as an int (1-Monday...5-Friday).
        /// </summary>
        /// <returns></returns>
        public static int getCurrentDay(DayOfWeek ptoday)        {

            int intToday = 0;

            switch (ptoday)
            {
                case DayOfWeek.Monday:
                    intToday = 1;
                    break;
                case DayOfWeek.Tuesday:
                    intToday = 2;
                    break;
                case DayOfWeek.Wednesday:
                    intToday = 3;
                    break;
                case DayOfWeek.Thursday:
                    intToday = 4;
                    break;
                case DayOfWeek.Friday:
                    intToday = 5;
                    break;
            }

            return intToday;
        }

        /// <summary>
        /// Returns true if the current system time is less than the worker entrance's time.
        /// </summary>
        /// <param name="pcurrentTime"></param>
        /// <param name="pentrancetime"></param>
        /// <returns></returns>
        public static bool isOnTime(DateTime pcurrentTime,int pentrancetime)
        {
            return pcurrentTime <= getDateTimeByEntranceTime(pentrancetime);
        }

        /// <summary>
        /// This method converts the time stored in the DB to "real" time.
        /// </summary>
        /// <param name="ptime"></param>
        /// <returns>The actual date</returns>
        public static DateTime getDateTimeByEntranceTime(int ptime)
        {
            switch (ptime)
            {              
                case 1: //First class' time
                    return Convert.ToDateTime("08:00 AM");
                case 2: //Second class' time
                    return Convert.ToDateTime("09:00 AM");
                case 3: //Third class' time
                    return Convert.ToDateTime("10:00 AM");
                case 4: //First class' time
                    return Convert.ToDateTime("11:30 AM");
                case 5: //First class' time
                    return Convert.ToDateTime("12:30 PM");
                case 6: //First class' time
                    return Convert.ToDateTime("13:30 PM");
            }

            return Convert.ToDateTime("00:00 AM");
        }
    }    
}
