using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;


namespace MDBDatabaseConverter
{
    class Program
    {
        private const string DB_PATH = "D:\\Documents\\Visual Studio 2015\\Projects\\CampanillasControlPrototype\\CampanillasControlPrototype\\";
        private const string DB_NAME = "ControlDB.mdf";

        private string fullConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"" + DB_PATH + DB_NAME + "\";Integrated Security=True";

        static string ACCESS_connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Documents\\Visual Studio 2015\\Projects\\CampanillasControlPrototype\\CampanillasControlPrototype\\estructura.mdb;";

        static Dictionary<string, Horario> horariosProfesores;

        static void Main(string[] args)
        {
            horariosProfesores = new Dictionary<string, Horario>();

            using (OleDbConnection MyConn = new OleDbConnection(ACCESS_connectionString))
            {
                MyConn.Open();
                OleDbCommand Cmd = new OleDbCommand("SELECT Profesorado,Dia,Tramohorario,Asignatura,Aula FROM Horarios;", MyConn); ;
                OleDbDataReader ObjReader = Cmd.ExecuteReader();
                while (ObjReader.Read())
                {
                    string profesor = ObjReader[0].ToString();
                    string dia = ObjReader[1].ToString();
                    string hora = ObjReader[2].ToString();
                    string asignatura = ObjReader[3].ToString();
                    string aula = ObjReader[4].ToString();

                    if (profesor.Length > 0 && dia.Length > 0 && hora.Length > 0) addData(profesor,dia,hora,asignatura,aula);
                }
            }
            Console.ReadLine();
            Console.Write("Finalizado.");
        }

        public static void addData(string pprof,string pdia,string phora,string pasignatura,string paula)
        {
            Horario h;

            if (horariosProfesores.TryGetValue(pprof,out h))
            {
                h.addHora(stringToDia(pdia), stringToHora(phora), paula, pasignatura);
            }
            else
            {
                h = new Horario();
                h.tempAbrev = pprof;
                h.addHora(stringToDia(pdia), stringToHora(phora), paula, pasignatura);
                horariosProfesores.Add(pprof,h);
            }
        }

        public static int stringToHora(string phora)
        {
            int ret = -1;

            if (phora.Contains("hora"))
            {
                ret = Convert.ToInt32(phora[0].ToString());
            }

            if (ret == -1) Console.WriteLine("CUIDADO: -1 DETECTADO EN stringToDia");

            return ret;
        }

        public static int stringToDia(string pdia)
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

            Console.WriteLine("CUIDADO: -1 DETECTADO EN stringToDia");

            return -1;
        }
    }

    class Horario
    {
        public string tempAbrev;
        public SortedList<int, SortedList<int, NodoHorario>> HORARIO;

        public Horario()
        {
            HORARIO = new SortedList<int, SortedList<int, NodoHorario>>();
        }

        public void addHora(int pdia,int phora,string paula,string pasignatura)
        {
            SortedList<int, NodoHorario> value;

            if (HORARIO.TryGetValue(pdia, out value))
            {
                NodoHorario nodoHorario;

                if (!value.TryGetValue(phora,out nodoHorario))
                {
                    value.Add(phora, new NodoHorario(paula, pasignatura));
                }                
            }
            else
            {
                value = new SortedList<int, NodoHorario>();
                value.Add(phora, new NodoHorario(paula, pasignatura));
                HORARIO.Add(pdia,value);
            }
        }
    }

    class NodoHorario
    {
        public string AULA;
        public string ASIGNATURA;

        public NodoHorario(string paula,string pasignatura)
        {
            AULA = paula;
            ASIGNATURA = pasignatura;
        }
    }
}
