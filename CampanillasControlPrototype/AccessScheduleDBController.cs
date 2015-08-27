using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;

namespace CampanillasControlPrototype
{
    class AccessScheduleDBController
    {
        static string mACCESSDBconnectionString;

        Dictionary<int, PersonSchedule> teachersTimeTables;

        public AccessScheduleDBController()
        {
            mACCESSDBconnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+Directory.GetCurrentDirectory()+"\\estructura.mdb;";

            teachersTimeTables = new Dictionary<int, PersonSchedule>();

            using (OleDbConnection MyConn = new OleDbConnection(mACCESSDBconnectionString))
            {
                MyConn.Open();
                OleDbCommand Cmd = new OleDbCommand("SELECT IdProfesor,Profesorado,Dia,Tramohorario,Asignatura,Aula FROM Horarios;", MyConn); ;
                OleDbDataReader ObjReader = Cmd.ExecuteReader();
                while (ObjReader.Read())
                {
                    string profesorid = ObjReader[0].ToString();
                    string profesor = ObjReader[1].ToString();
                    string dia = ObjReader[2].ToString();
                    string hora = ObjReader[3].ToString();
                    string asignatura = ObjReader[4].ToString();
                    string aula = ObjReader[5].ToString();

                    if (profesor.Length > 0 && dia.Length > 0 && hora.Length > 0) addData(profesorid,profesor, dia, hora, asignatura, aula);
                }
            }
        }

        public void getAllTeachersFromAccessTestData(List<PersonalNode> ppersonal)
        {
            foreach (KeyValuePair<int,PersonSchedule> entry in teachersTimeTables)
            {
                ppersonal.Add(new PersonalNode(entry.Key, entry.Value.NAME.Trim()));
            }
        }

        public void addData(string pprofid,string pprof, string pdia, string phora, string pasignatura, string paula)
        {
            PersonSchedule h;

            int profId = Convert.ToInt32(pprofid);

            if (teachersTimeTables.TryGetValue(profId, out h))
            {
                h.addHour(stringToDay(pdia), stringToHour(phora), paula, pasignatura);
            }
            else
            {
                h = new PersonSchedule();
                h.NAME = pprof;
                h.addHour(stringToDay(pdia), stringToHour(phora), paula, pasignatura);
                teachersTimeTables.Add(profId, h);
            }
        }

        public int stringToHour(string phora)
        {
            int ret = -1;

            if (phora.Contains("hora"))
            {
                ret = Convert.ToInt32(phora[0].ToString());
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

    class PersonSchedule
    {
        public string NAME;
        public SortedList<int, SortedList<int, NodePersonSchedule>> TIMETABLE;

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

    class NodePersonSchedule
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



