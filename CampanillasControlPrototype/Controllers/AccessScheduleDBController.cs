using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;

namespace CampanillasControlPrototype
{
    public class AccessScheduleDBController
    {
        static string mACCESSDBconnectionString;

        Dictionary<int, PersonSchedule> teachersTimeTables;

        public AccessScheduleDBController()
        {
            mACCESSDBconnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+Directory.GetCurrentDirectory()+"\\DB\\estructura.mdb;";

            //mACCESSDBconnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Documents\\Visual Studio 2015\\Projects\\CampanillasControlPrototype\\CampanillasControlPrototype\\DB\\estructura.mdb;";


            teachersTimeTables = new Dictionary<int, PersonSchedule>();

            using (OleDbConnection MyConn = new OleDbConnection(mACCESSDBconnectionString))
            {
                MyConn.Open();
                OleDbCommand Cmd = new OleDbCommand("SELECT IdProfesor,Profesorado,Dia,Tramohorario,Asignatura,Aula,Sede FROM Horarios;", MyConn); ;
                OleDbDataReader ObjReader = Cmd.ExecuteReader();
                while (ObjReader.Read())
                {
                    string profesorid = ObjReader[0].ToString();
                    string profesor = ObjReader[1].ToString();
                    string dia = ObjReader[2].ToString();
                    string hora = ObjReader[3].ToString();
                    string asignatura = ObjReader[4].ToString();
                    string aula = ObjReader[5].ToString();
                    string sede = ObjReader[6].ToString();

                    if (profesor.Length > 0 && dia.Length > 0 && hora.Length > 0) addData(profesorid,profesor, dia, hora, asignatura, aula,sede);
                }
            }
        }

        public void setHeadQuarters(List<PersonalNode> ppersonal)
        {
            foreach (PersonalNode p in ppersonal)
            {
                PersonSchedule pschedule;
                if (teachersTimeTables.TryGetValue(p.getId(),out pschedule))
                {
                    p.HEADQUARTERS = pschedule.HEADQUARTERS;
                }
            }
        }

        public void DEBUGCOPYFROMPARADOXTOACCESS(List<PersonalNode> ppersonallist)
        {
            using (OleDbConnection MyConn = new OleDbConnection(mACCESSDBconnectionString))
            {
                MyConn.Open();

                foreach (PersonalNode p in ppersonallist)
                {
                    OleDbCommand Cmd = new OleDbCommand("UPDATE Horarios SET Profesorado = \""+ p.getName() + "\" WHERE IdProfesor = "+p.getId()+";", MyConn); ;
                    Cmd.ExecuteNonQuery();
                }

            }
        }

        public void updateTeacherId()
        {
            using (OleDbConnection MyConn = new OleDbConnection(mACCESSDBconnectionString))
            {
                MyConn.Open();
                OleDbCommand Cmd = new OleDbCommand("UPDATE Horarios SET IdProfesor = (IdProfesor - 880) WHERE IdProfesor > 100;", MyConn); ;
                Cmd.ExecuteNonQuery();                
            }
        }

        public void getAllTeachersFromAccessTestData(List<PersonalNode> ppersonal)
        {
            foreach (KeyValuePair<int,PersonSchedule> entry in teachersTimeTables)
            {
                ppersonal.Add(new PersonalNode(entry.Key, entry.Value.NAME.Trim()));
            }
        }

        public void addData(string pprofid,string pprof, string pdia, string phora, string pasignatura, string paula,string psede)
        {
            PersonSchedule h;

            int profId = -1;

            try {
                profId = Convert.ToInt32(pprofid);
            }catch(FormatException fe)
            {               
                return;
            }

            if (teachersTimeTables.TryGetValue(profId, out h))
            {
                h.addHour(stringToDay(pdia), stringToHour(phora), paula, pasignatura);
            }
            else
            {
                h = new PersonSchedule();

                if (psede.Equals("IES", StringComparison.InvariantCultureIgnoreCase))
                {
                    h.HEADQUARTERS = PersonSchedule.IES;
                }
                else if (psede.Equals("PTA", StringComparison.InvariantCultureIgnoreCase))
                {
                    h.HEADQUARTERS = PersonSchedule.PTA;
                }

                h.NAME = pprof;
                h.addHour(stringToDay(pdia), stringToHour(phora), paula, pasignatura);
                teachersTimeTables.Add(profId, h);
            }
        }

        public int stringToHour(string phora)
        {
            int ret = -1;

            if (phora.ToUpper().Contains("HORA")){
                ret = UtilsHelper.scheduleTimeToNeededTime(Convert.ToInt32(phora[0].ToString()));
            }else if(phora.ToUpper().Contains("RECREO")){
                ret = UtilsHelper.SPARE;
            }else if (phora.ToUpper().Contains("TRANS. 1")){
                ret = UtilsHelper.GT1;
            }else if (phora.ToUpper().Contains("TRANS. 2")){
                ret = UtilsHelper.GT2;
            }

            return ret;
        }

        public int stringToDay(string pdia)
        {
            if (string.Equals(pdia, "lunes", StringComparison.OrdinalIgnoreCase))
            {
                return 1;
            }
            else if (string.Equals(pdia, "martes", StringComparison.OrdinalIgnoreCase))
            {
                return 2;
            }
            else if (string.Equals(pdia, "miércoles", StringComparison.OrdinalIgnoreCase))
            {
                return 3;
            }
            else if (string.Equals(pdia, "jueves", StringComparison.OrdinalIgnoreCase))
            {
                return 4;
            }
            else if (string.Equals(pdia, "viernes", StringComparison.OrdinalIgnoreCase))
            {
                return 5;
            }

            return -1;
        }

        public Dictionary<int, PersonSchedule> getDictionary()
        {
            return teachersTimeTables;
        }

        public string[] getClassRoomAndSubject(int pid,int pday,int phour)
        {
            string[] results = new string[2];

            PersonSchedule schedule;

            if (teachersTimeTables.TryGetValue(pid, out schedule))
            {
                SortedList<int,NodePersonSchedule> thisDaySchedule;

                if (schedule.TIMETABLE.TryGetValue(pday, out thisDaySchedule))
                {
                    NodePersonSchedule hourSchedule;
                    if (thisDaySchedule.TryGetValue(phour, out hourSchedule))
                    {
                        results[0] = hourSchedule.CLASSROOM;
                        results[1] = hourSchedule.SUBJECT;
                    }
                }
            }
            return results;
        }         
    }

    public class PersonSchedule
    {
        public string NAME;
        public int HEADQUARTERS;
        public SortedList<int, SortedList<int, NodePersonSchedule>> TIMETABLE;

        public const int IES = 1, PTA = 2;

        public PersonSchedule()
        {
            TIMETABLE = new SortedList<int, SortedList<int, NodePersonSchedule>>();
        }

        public void addHour(int pday, int phour, string pclassroom, string psubject)
        {
            if (pday != -1 && phour != -1)
            {
                SortedList<int, NodePersonSchedule> value;

                if (TIMETABLE.TryGetValue(pday, out value))
                {
                    NodePersonSchedule nodoHorario;

                    if (!value.TryGetValue(phour, out nodoHorario))
                    {
                        value.Add(phour, new NodePersonSchedule(pclassroom, psubject));
                    }
                }
                else
                {
                    value = new SortedList<int, NodePersonSchedule>();
                    value.Add(phour, new NodePersonSchedule(pclassroom, psubject));
                    TIMETABLE.Add(pday, value);
                }
            }
        }
    }

    public class NodePersonSchedule
    {
        public string CLASSROOM;
        public string SUBJECT;

        public NodePersonSchedule(string paula, string pasignatura)
        {
            CLASSROOM = paula;
            SUBJECT = pasignatura;
        }
    }
}



