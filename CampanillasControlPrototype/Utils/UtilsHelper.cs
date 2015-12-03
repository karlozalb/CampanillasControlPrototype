using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampanillasControlPrototype
{
    public static class UtilsHelper
    {

        public static TimeSpan FIRST_HOUR;
        public static TimeSpan SECOND_HOUR;
        public static TimeSpan THIRD_HOUR;
        public static TimeSpan FOURTH_HOUR;
        public static TimeSpan FIFTH_HOUR;
        public static TimeSpan SIXTH_HOUR;
        public static TimeSpan FINISH_HOUR;
        public static TimeSpan PRE_SPARE_TIME;
        public static TimeSpan TRANS_GUARDIA1, TRANS_GUARDIA2;

        public static bool firstTime = true;

        public static int GT1 = 0,FIRST=1,SECOND=2,THIRD=3,SPARE=4,FOURTH=5,FIFTH=6,SIXTH=7,GT2=8,OFFTIME=9;

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
                case DayOfWeek.Saturday:
                    intToday = 6;
                    break;
                case DayOfWeek.Sunday:
                    intToday = 7;
                    break;
            }

            return intToday;
        }

        public static string getPrettyCurrentDay(DateTime ptoday)
        {
            string stringToday = "";

            switch (ptoday.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    stringToday = "Lunes ";
                    break;
                case DayOfWeek.Tuesday:
                    stringToday = "Martes ";
                    break;
                case DayOfWeek.Wednesday:
                    stringToday = "Miércoles ";
                    break;
                case DayOfWeek.Thursday:
                    stringToday = "Jueves ";
                    break;
                case DayOfWeek.Friday:
                    stringToday = "Viernes ";
                    break;
                default:
                    stringToday = "No lectivo ";
                    break;
            }

            return stringToday + ptoday.ToShortDateString();
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
            DateTime baseDate = Convert.ToDateTime("01/01/2001");

            switch (ptime)
            {
                case 0:
                    return baseDate + TRANS_GUARDIA1;
                case 1: //First class' time
                    return baseDate + FIRST_HOUR;
                case 2: //Second class' time
                    return baseDate + SECOND_HOUR;
                case 3: //Third class' time
                    return baseDate + THIRD_HOUR;
                case 4: //First class' time
                    return baseDate + PRE_SPARE_TIME;
                case 5: //First class' time
                    return baseDate + FOURTH_HOUR;
                case 6: //First class' time
                    return baseDate + FIFTH_HOUR;
                case 7: //First class' time
                    return baseDate + SIXTH_HOUR;
                case 8: //First class' time
                    return baseDate + TRANS_GUARDIA2;
            }

            return Convert.ToDateTime(FINISH_HOUR);
        }

        public static string getPrettyCurrentHour(int ptime)
        {
            switch (ptime)
            {
                case 0: //GT1
                    return "GT1";
                case 1: //First class' time
                    return "Primera hora";
                case 2: //Second class' time
                    return "Segunda hora";
                case 3: //Third class' time
                    return "Tercera hora";
                case 4: //Spare time
                    return "Recreo";
                case 5: //First class' time
                    return "Cuarta hora";
                case 6: //First class' time
                    return "Quinta hora";
                case 7: //First class' time
                    return "Sexta hora";
                case 8: //First class' time
                    return "GT2";
            }

            return "Hora no lectiva";
        }

        public static string getPrettyShortCurrentHour(int ptime)
        {
            switch (ptime)
            {
                case 0: //GT1
                    return "GT1";
                case 1: //First class' time
                    return "1ª";
                case 2: //Second class' time
                    return "2ª";
                case 3: //Third class' time
                    return "3ª";
                case 4: //Spare time
                    return "R.";
                case 5: //First class' time
                    return "4ª";
                case 6: //First class' time
                    return "5ª";
                case 7: //First class' time
                    return "6ª";
                case 8: //First class' time
                    return "GT2";
            }

            return "";
        }

        public static int scheduleTimeToNeededTime(int phour)
        {
            if (phour <= 3)
            {
                return phour;
            }
            else
            {
                return phour + 1;
            }            
        }

        public static void resetHours()
        {
            FIRST_HOUR = Convert.ToDateTime("08:15 AM").TimeOfDay;
            SECOND_HOUR = Convert.ToDateTime("09:15 AM").TimeOfDay;
            THIRD_HOUR = Convert.ToDateTime("10:15 AM").TimeOfDay;
            FOURTH_HOUR = Convert.ToDateTime("11:45 AM").TimeOfDay;
            FIFTH_HOUR = Convert.ToDateTime("12:45 PM").TimeOfDay;
            SIXTH_HOUR = Convert.ToDateTime("13:45 PM").TimeOfDay;
            FINISH_HOUR = Convert.ToDateTime("14:45 PM").TimeOfDay;

            PRE_SPARE_TIME = Convert.ToDateTime("11:15 AM").TimeOfDay;

            TRANS_GUARDIA1 = Convert.ToDateTime("07:45 AM").TimeOfDay;
            TRANS_GUARDIA2 = Convert.ToDateTime("15:15 PM").TimeOfDay;
        }

        public static int getCurrentIntHour(DateTime pcurrenttime)
        {
            TimeSpan currentTimeSpan = pcurrenttime.TimeOfDay;

            if (currentTimeSpan >= TRANS_GUARDIA1 && currentTimeSpan <= FIRST_HOUR)
            {
                return GT1;
            }
            else if (currentTimeSpan >= FIRST_HOUR && currentTimeSpan <= SECOND_HOUR)
            {
                return FIRST;
            }
            else if (currentTimeSpan >= SECOND_HOUR && currentTimeSpan <= THIRD_HOUR)
            {
                return SECOND;
            }
            else if (currentTimeSpan >= THIRD_HOUR && currentTimeSpan <= PRE_SPARE_TIME)
            {
                return THIRD;
            }
            else if (currentTimeSpan >= PRE_SPARE_TIME && currentTimeSpan <= FOURTH_HOUR)
            {
                return SPARE;
            }
            else if (currentTimeSpan >= FOURTH_HOUR && currentTimeSpan <= FIFTH_HOUR)
            {
                return FOURTH;
            }
            else if (currentTimeSpan >= FIFTH_HOUR && currentTimeSpan <= SIXTH_HOUR)
            {
                return FIFTH;
            }
            else if (currentTimeSpan >= SIXTH_HOUR && currentTimeSpan <= FINISH_HOUR)
            {
                return SIXTH;
            }
            else if (currentTimeSpan >= SIXTH_HOUR && currentTimeSpan <= TRANS_GUARDIA2)
            {
                return GT2;
            }
            else if (currentTimeSpan >= TRANS_GUARDIA2)
            {
                return OFFTIME; //Fuera de hora lectiva.
            }

             return -1; //Out of time!
        }

        internal static bool isPostSchoolHour(int phour)
        {
            return phour >= OFFTIME;
        }
    }    
}
