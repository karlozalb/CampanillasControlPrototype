using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using CommsModule;
using System.IO;

namespace CampanillasControlPrototype
{
    class DataBaseController
    {
        private SqlConnection myConnection;

        private string DB_PATH = "D:\\Documents\\Visual Studio 2015\\Projects\\CampanillasControlPrototype\\CampanillasControlPrototype\\";
        private const string DB_NAME = "ControlDB.mdf";

        private string fullConnectionString;


        public DataBaseController()
        {
            DB_PATH = Directory.GetCurrentDirectory();
            fullConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"" + DB_PATH +"\\"+ DB_NAME + "\";Integrated Security=True";
        }

        /// <summary>
        /// This method checks the existence of one table per teacher, and if it doesn't exist, it creates it.
        /// </summary>
        public void createTeachersTables(List<PersonalNode> ppersonal)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                foreach (PersonalNode person in ppersonal)
                {
                    //Check the existence asking to the DB schema.
                    SqlCommand tableExists = new SqlCommand("select case when exists((select * from information_schema.tables where table_name = 'Profesor_" + person.getId() + "_Marcajes')) then 1 else 0 end", conn);

                    //If the table "Profesor_ID" doesn't exist, I create it.
                    if ((int)tableExists.ExecuteScalar() == 0) 
                    {
                        createTeacherTable(person.getId());
                    }
                } 
            }
        }

        /// <summary>
        /// Creates a new table for each teacher (if it doesn't exist)
        /// </summary>
        public void createTeacherTable(int pid)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand("CREATE TABLE [dbo].[Profesor_"+pid+"_Marcajes] ( " +
                                                    "[Id]                INT      IDENTITY (1, 1) NOT NULL," +
                                                    "[Fecha]             DATE     NULL," +
                                                    "[Hora entrada]      TIME(7) NULL," +
                                                    "[Hora entrada real] TIME(7) NULL," +
                                                    "[Minutos Retraso]   INT      NULL," +
                                                    "PRIMARY KEY CLUSTERED([Id] ASC));", conn);

                command.ExecuteNonQuery();
            }
        }

        /*public void registerClockIn(int pteacherid,DateTime pclockintime,DateTime pactualtime,TimeSpan delay)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();
                
                //OJITO, ESTO NO ESTÁ ACABADO, ESTO ESTÁ ASI PORQUE NO PUEDO PROBARLO EN EL PORTATIL
                SqlCommand command = new SqlCommand("INSERT INTO Profesor_"+pteacherid+"_Marcajes (Fecha,\"Hora entrada\",\"Hora entrada real\",\"Minutos Retraso\") VALUES (convert(datetime,'"+ DateTime.Now.ToShortDateString() + "',105),'" + pclockintime.ToString("HH:mm")+"','"+pactualtime.ToString("HH:mm")+ "'," + delay.Minutes+")", conn);

                command.ExecuteNonQuery();
            }
        }*/

        public void registerClockIn(int pteacherid, DateTime pclockintime, DateTime pactualtime, TimeSpan delay)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Profesor_" + pteacherid + "_Marcajes WHERE Fecha = convert(datetime, '" + DateTime.Now.ToShortDateString() + "', 105);", conn);
                
                bool add = true;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine(reader[0]+" "+reader[1]);

                        TimeSpan dbTime = ((TimeSpan)reader[2]);
                        TimeSpan ddTimeDifference = TimeSpan.Parse(pclockintime.ToShortTimeString()) - dbTime;

                        if (ddTimeDifference.Minutes == 0)
                        {
                            //Duplicate! we don't add anything.
                            add = false;
                        }
                    }
                }

                //OJITO, ESTO NO ESTÁ ACABADO, ESTO ESTÁ ASI PORQUE NO PUEDO PROBARLO EN EL PORTATIL
                if (add)
                {
                    command = new SqlCommand("INSERT INTO Profesor_" + pteacherid + "_Marcajes (Fecha,\"Hora entrada\",\"Hora entrada real\",\"Minutos Retraso\") VALUES (convert(datetime,'" + DateTime.Now.ToShortDateString() + "',105),'" + pclockintime.ToString("HH:mm") + "','" + pactualtime.ToString("HH:mm") + "'," + (delay.Hours*60+delay.Minutes) + ")", conn);
                    command.ExecuteNonQuery();
                }
            }
        }


        public string getTeacherSchedule(int pteacherid,int pintday)
        {      

            string hoursToCheckIn = "";

            //With this SQL process, we obtain the entrance time for the teacher specified by pteacherid.
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Horarios WHERE id='" + pteacherid + "'", conn);


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hoursToCheckIn = (string)reader[pintday];                        
                    }
                }
            }

            return hoursToCheckIn;
        }

        public void saveIdAbrevTESTINGData(Dictionary<string, PersonSchedule> personaldictionary)
        {
            int firstTestingId = 880;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                foreach (KeyValuePair<string, PersonSchedule> entry in personaldictionary)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO TestingIDAbrevTable (Id,Nombre) VALUES ("+firstTestingId + ",'"+entry.Key+"');", conn);

                    command.ExecuteNonQuery();

                    firstTestingId++;
                }                
            }           
        }

        public void getAllTeachersFromAccessTestData(List<PersonalNode> ppersonal)
        {
            //SQL query.
            string query = "SELECT Id,Nombre FROM TestingIDAbrevTable;";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand(query, conn);


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine("("+ reader[1].ToString().Trim() + ")");
                        ppersonal.Add(new PersonalNode((int)reader[0], reader[1].ToString().Trim()));
                    }
                }
            }           
        }

        public SerializableTeacherData getTeacherData(int pteacherid,DateTime pinit,DateTime pend)
        {
            string query = "SELECT * FROM Profesor_"+pteacherid+"_Marcajes WHERE MONTH(Fecha) >= "+ pinit.Month+ " AND YEAR(Fecha) >= "+pinit.Year+" AND DAY(Fecha) >= "+pinit.Day+ " AND MONTH(Fecha) <= " + pend.Month + " AND YEAR(Fecha) <= " + pend.Year + " AND DAY(Fecha) <= " + pend.Day + ";";

            SerializableTeacherData teacherData = new SerializableTeacherData();
            teacherData.mId = pteacherid;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teacherData.addData(Convert.ToDateTime(reader[1]), (TimeSpan)reader[2], (TimeSpan)reader[3], (int)reader[4]);                      
                    }
                }
            }

            return teacherData;
        }

        public SerializableTeacherList getTeacherList()
        {
            string query = "SELECT * FROM TestingIDAbrevTable;";

            SerializableTeacherList teacherList = new SerializableTeacherList();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SerializableTeacherList.TeacherData newNode = new SerializableTeacherList.TeacherData();

                        newNode.mId = (int)reader[0];
                        newNode.mTeacherName = (string)reader[1];

                        teacherList.add(newNode);
                    }
                }
            }

            return teacherList;
        }

        /*public void registerClockIn(int pteacherid)
        {
            //We get the current day of the week in order to get the time the teacher should have clocked in.
            int day = UtilsHelper.getCurrentDay(DateTime.Now.DayOfWeek);

            //We get the current time to save it in the DB.
            string timeNow = DateTime.Now.ToString("HH:mm:ss tt");

            //With this SQL process, we obtain the entrance time for the teacher specified by pteacherid.
            using (SqlConnection conn = new SqlConnection()) {
               conn.ConnectionString = fullConnectionString;
               conn.Open();

               SqlCommand command = new SqlCommand("SELECT * FROM Profesores WHERE id='"+pteacherid+"'", conn);

               string hoursToCheckIn = "";

               using (SqlDataReader reader = command.ExecuteReader())
               {
                   while (reader.Read())
                   {
                        hoursToCheckIn = (string)reader[day];
                   }
               }
           }
        }*/
    }
   
}
