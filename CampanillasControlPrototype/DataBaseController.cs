using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Diagnostics;

namespace CampanillasControlPrototype
{
    class DataBaseController
    {
        private SqlConnection myConnection;

        private const string DB_PATH = "D:\\Documents\\Visual Studio 2015\\Projects\\CampanillasControlPrototype\\CampanillasControlPrototype\\";
        private const string DB_NAME = "ControlDB.mdf";

        private string fullConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\""+DB_PATH+DB_NAME+"\";Integrated Security=True";


        public DataBaseController()
        {
            /*using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Profesores", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Debug.WriteLine("FirstColumn\tSecond Column\t\tThird Column\t\tForth Column\t");
                    while (reader.Read())
                    {
                        Debug.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3}",
                            reader[0], reader[1], reader[2], reader[3]));
                    }
                }
            }*/
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
                    SqlCommand tableExists = new SqlCommand("select case when exists((select * from information_schema.tables where table_name = 'Profesor_" + person.getId() + "')) then 1 else 0 end", conn);

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

                SqlCommand command = new SqlCommand("CREATE TABLE [dbo].[Profesor_"+pid+"] ( " +
                                                    "[Id]                INT      NOT NULL," +
                                                    "[Fecha]             DATE     NULL," +
                                                    "[Hora entrada]      TIME(7) NULL," +
                                                    "[Hora entrada real] TIME(7) NULL," +
                                                    "[Minutos Retraso]   INT      NULL," +
                                                    "PRIMARY KEY CLUSTERED([Id] ASC));", conn);

                command.ExecuteNonQuery();
            }
        }

        public void registerCheckIn(int pteacherid,DateTime pclockintime)
        {
            //We get the current day of the week in order to get the time the teacher should have clocked in.
            int day = DataBaseDataHelper.getCurrentDay(DateTime.Now.DayOfWeek);

            string hoursToCheckIn = "";

            //Firstly we need to check if the person has come late or not.
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Horarios WHERE id='" + pteacherid + "'", conn);


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hoursToCheckIn = (string)reader[day];
                    }
                }                
            }

            //String to int conversion of class' hours.
            string[] splittedHours = hoursToCheckIn.Split(',');
            int[] intHours = new int[splittedHours.Length];

            for (int i = 0; i < splittedHours.Length; i++)
            {
                intHours[i] = Convert.ToInt32(splittedHours[i]);
            }

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Profesor_"+pteacherid+" ", conn);

                command.ExecuteNonQuery();
            }
        }

        public void registerClockIn(int pteacherid)
        {
            //We get the current day of the week in order to get the time the teacher should have clocked in.
            int day = DataBaseDataHelper.getCurrentDay(DateTime.Now.DayOfWeek);

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
        }

    }
   
}
