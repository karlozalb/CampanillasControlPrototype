using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampanillasControlPrototype
{
    class MainController
    {

        DataBaseController mDBController;
        ParadoxDBController mPXDBController;

        List<PersonalNode> mPersonal; //This list will contain real-time information about teachers.

        private int currentYear, currentDay, currentMonth;

        public MainController()
        {
            mDBController = new DataBaseController();

            mPersonal = new List<PersonalNode>();

            mPXDBController = new ParadoxDBController();
            mPXDBController.getAllTeachers(mPersonal);
            mDBController.createTeachersTables(mPersonal);

            currentYear = DateTime.Now.Year;
            currentMonth = DateTime.Now.Month;
            currentDay = DateTime.Now.Day;

            startTask();
        }

        public void startTask()
        {
            int personalSize = mPersonal.Count;

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            if (hasDayChanged())
            {
                currentYear = DateTime.Now.Year;
                currentMonth = DateTime.Now.Month;
                currentDay = DateTime.Now.Day;

                for (int i = 0; i < personalSize; i++) mPersonal[i].clearCheckins();               
            }

            for (int i=0;i<personalSize;i++)
            {
                List<DateTime> times = mPXDBController.getCheckIns(mPersonal[i].getId(),currentDay,currentMonth,currentYear);

                foreach(DateTime checkInTime in times)
                {
                    if (mPersonal[i].addCheckIn(checkInTime))
                    {

                    }
                }
            }
        }

        public bool hasDayChanged()
        {
            return !(currentYear == DateTime.Now.Year && currentMonth == DateTime.Now.Month && currentDay == DateTime.Now.Day);
        }
    }
}
