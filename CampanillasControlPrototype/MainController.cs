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

        private int MINUTES_THRESHOLD = 10; //If the same person clock in in less than THRESHOLD, the system ignores it.

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

        public void startTask()
        {
            int personalSize = mPersonal.Count;

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

                for (int i = 0; i < personalSize; i++) mPersonal[i].clearCheckins();               
            }

            for (int i=0;i<personalSize;i++)
            {
                //We get the clock-ins from the DB
                List<DateTime> times = mPXDBController.getCheckIns(mPersonal[i].getId(),currentDay,currentMonth,currentYear);

                //For each clock in, we add it to the worker's clock in list.
                foreach(DateTime checkInTime in times)
                {
                    if (mPersonal[i].addClockIn(checkInTime,MINUTES_THRESHOLD))
                    {
                        PersonalNode.HourNode personNewClockInTime = mPersonal[i].getLastModifiedHourNode();
                        //We store the data in the teacher's specific table, providing the delay time (even if it's negative). 
                        mDBController.registerClockIn(mPersonal[i].getId(), personNewClockInTime.mClockInTime, personNewClockInTime.mActualHour, personNewClockInTime.mDelay);
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
