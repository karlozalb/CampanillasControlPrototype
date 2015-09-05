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
        public static DateTime PRE_SPARE_TIME;
        public static DateTime TRANS_GUARDIA1, TRANS_GUARDIA2;

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
            }

            return intToday;
        }

        public static string getPrettyCurrentDay(DayOfWeek ptoday)
        {
            string stringToday = "";

            switch (ptoday)
            {
                case DayOfWeek.Monday:
                    stringToday = "Lunes";
                    break;
                case DayOfWeek.Tuesday:
                    stringToday = "Martes";
                    break;
                case DayOfWeek.Wednesday:
                    stringToday = "Miércoles";
                    break;
                case DayOfWeek.Thursday:
                    stringToday = "Jueves";
                    break;
                case DayOfWeek.Friday:
                    stringToday = "Viernes";
                    break;
            }

            return stringToday;
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
            FIRST_HOUR = Convert.ToDateTime("08:15 AM");
            SECOND_HOUR = Convert.ToDateTime("09:15 AM");
            THIRD_HOUR = Convert.ToDateTime("10:15 AM");
            FOURTH_HOUR = Convert.ToDateTime("11:45 AM");
            FIFTH_HOUR = Convert.ToDateTime("12:45 PM");
            SIXTH_HOUR = Convert.ToDateTime("13:45 PM");
            FINISH_HOUR = Convert.ToDateTime("14:45 PM");

            PRE_SPARE_TIME = Convert.ToDateTime("11:15 AM");

            TRANS_GUARDIA1 = Convert.ToDateTime("07:45 AM");
            TRANS_GUARDIA2 = Convert.ToDateTime("15:15 PM");
        }

        public static int getCurrentIntHour(DateTime pcurrenttime)
        {
            if (pcurrenttime >= TRANS_GUARDIA1 && pcurrenttime <= FIRST_HOUR)
            {
                return GT1;
            }
            else if (pcurrenttime >= FIRST_HOUR && pcurrenttime<= SECOND_HOUR)
            {
                return FIRST;
            }
            else if (pcurrenttime >= SECOND_HOUR && pcurrenttime <= THIRD_HOUR)
            {
                return SECOND;
            }
            else if (pcurrenttime >= THIRD_HOUR && pcurrenttime <= PRE_SPARE_TIME)
            {
                return THIRD;
            }
            else if (pcurrenttime >= PRE_SPARE_TIME && pcurrenttime <= FOURTH_HOUR)
            {
                return SPARE;
            }
            else if (pcurrenttime >= FOURTH_HOUR && pcurrenttime <= FIFTH_HOUR)
            {
                return FOURTH;
            }
            else if (pcurrenttime >= FIFTH_HOUR && pcurrenttime <= SIXTH_HOUR)
            {
                return FIFTH;
            }
            else if (pcurrenttime >= SIXTH_HOUR && pcurrenttime <= FINISH_HOUR)
            {
                return SIXTH;
            }
            else if (pcurrenttime >= SIXTH_HOUR && pcurrenttime <= TRANS_GUARDIA2)
            {
                return GT2;
            }
            else if (pcurrenttime >= TRANS_GUARDIA2)
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
