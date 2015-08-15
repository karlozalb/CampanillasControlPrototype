using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampanillasControlPrototype
{
    public static class UtilsHelper
    {

        public static DateTime FIRST_HOUR;
        public static DateTime SECOND_HOUR;
        public static DateTime THIRD_HOUR;
        public static DateTime FOURTH_HOUR;
        public static DateTime FIFTH_HOUR;
        public static DateTime SIXTH_HOUR;
        public static DateTime FINISH_HOUR;

        public static bool firstTime = true;

        /// <summary>
        ///  Get the current day as an int (1-Monday...5-Friday).
        /// </summary>
        /// <returns></returns>
        public static int getCurrentDay(DayOfWeek ptoday) {

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
        public static bool isOnTime(DateTime pcurrentTime, int pentrancetime)
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
                    return FIRST_HOUR;
                case 2: //Second class' time
                    return SECOND_HOUR;
                case 3: //Third class' time
                    return THIRD_HOUR;
                case 4: //First class' time
                    return FOURTH_HOUR;
                case 5: //First class' time
                    return FIFTH_HOUR;
                case 6: //First class' time
                    return SIXTH_HOUR;
            }

            return FINISH_HOUR;
        }

        public static void resetHours()
        {
            FIRST_HOUR = Convert.ToDateTime("08:00 AM");
            SECOND_HOUR = Convert.ToDateTime("09:00 AM");
            THIRD_HOUR = Convert.ToDateTime("10:00 AM");
            FOURTH_HOUR = Convert.ToDateTime("11:30 AM");
            FIFTH_HOUR = Convert.ToDateTime("12:30 PM");
            SIXTH_HOUR = Convert.ToDateTime("13:30 PM");
            FINISH_HOUR = Convert.ToDateTime("14:30 PM");
        }

        public static int getCurrentIntHour(DateTime pcurrenttime)
        {  

            if (pcurrenttime >= FIRST_HOUR && pcurrenttime<= SECOND_HOUR)
            {
                return 1;
            }
            else if (pcurrenttime >= SECOND_HOUR && pcurrenttime <= THIRD_HOUR)
            {
                return 2;
            }
            else if (pcurrenttime >= THIRD_HOUR && pcurrenttime <= FOURTH_HOUR)
            {
                return 3;
            }
            else if (pcurrenttime >= FOURTH_HOUR && pcurrenttime <= FIFTH_HOUR)
            {
                return 4;
            }
            else if (pcurrenttime >= FIFTH_HOUR && pcurrenttime <= SIXTH_HOUR)
            {
                return 5;
            }
            else if (pcurrenttime >= SIXTH_HOUR && pcurrenttime <= FINISH_HOUR)
            {
                return 6;
            }

            return -1; //Out of time!
        }
    }    
}
