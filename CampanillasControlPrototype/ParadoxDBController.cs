using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampanillasControlPrototype
{
    /// <summary>
    /// This class provide methods to deal with a Paradox database.
    /// 
    /// </summary>
    class ParadoxDBController
    {     
        //Connection instance.
        private OleDbConnection mConnection;         

        //DATABASE CONNECTION STRING
        const string mConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Paradox 7.x;Data Source=G:\SystemPin\PresenciaPin\db;";

        public ParadoxDBController()
        {
            mConnection = new OleDbConnection(mConnectionString);
            //getCheckIns(879,21,7,2015);
        }

        /// <summary>
        /// Gets the today's check ins of a specific teacher (id and date provided) 
        /// </summary>
        /// <param name="pteacherid">id that identifies a teacher</param>
        /// <param name="pday">day</param>
        /// <param name="pmonth">month</param>
        /// <param name="pyear">year</param>
        /// <returns></returns>
        public List<DateTime> getCheckIns(int pteacherid,int pday,int pmonth,int pyear)
        {
            //SQL query.
            string query = "SELECT Fecha,Hora FROM Marcajes WHERE Persona=" + pteacherid + " AND year(Fecha) = "+pyear+" AND month(Fecha) = "+pmonth+" AND  day(Fecha) = "+pday+" ; ";

            //Get the rows that match with the query.
            DataTable returnedData = selectCommand(query);
            
            //This two lists stores the check ins of a specific day.
            List<DateTime> times = new List<DateTime>();

            //Iterating the data row to get the times.
            foreach (DataRow row in returnedData.Rows)
            {
                String[] splitDate = row["Fecha"].ToString().Split(' ');
                String[] splitTime = row["Hora"].ToString().Split(' ');

                times.Add(Convert.ToDateTime(splitDate[0] + " " + splitTime[1]));
            }

            return times;
        }

        /// <summary>
        /// Get and return the teacher's name specified by a provided id pteacherid.
        /// </summary>
        /// <param name="pteacherid"></param>
        /// <returns></returns>
        public String getTeacher(int pteacherid)
        {
            //SQL query.
            string query = "SELECT Nombre FROM Personal WHERE Id=" + pteacherid + " ; ";

            //Get the rows that match with the query.
            DataTable returnedData = selectCommand(query);

            return returnedData.Rows[0]["Nombre"].ToString();          
        }

        public void getAllTeachers(List<PersonalNode> ppersonal)
        {
            //SQL query.
            string query = "SELECT Id,Nombre FROM Personal;";

            //Get the rows that match with the query.
            DataTable returnedData = selectCommand(query);

            foreach (DataRow row in returnedData.Rows)
            {
                ppersonal.Add(new PersonalNode((int)row["Id"],row["Nombre"].ToString()));
            }
        }

        /// <summary>
        /// Generic method to execute SELECT SQL commands.
        /// </summary>
        /// <param name="pquery"></param>
        /// <returns></returns>
        public DataTable selectCommand(String pquery)
        {
            Debug.WriteLine("Consulta: "+ pquery);


            DataTable dt = new DataTable();

            using (OleDbConnection conn = new OleDbConnection())
            {
                conn.ConnectionString = mConnectionString;
                conn.Open();

                OleDbCommand cmd = new OleDbCommand(pquery, conn);

                OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
               
                adap.Fill(dt);
            }    

            return dt;
        }      
        
        public void insertTestData()
        {
            string date = DateTime.Now.ToShortDateString();
            string time = DateTime.Now.ToShortTimeString();

            OleDbCommand cmd = new OleDbCommand("INSERT INTO Marcajes (Persona,Fecha,Hora,Terminal,Modificado,ModoIdentificacion) VALUES (888,'"+ date + "','07:58',1,true,0);" , mConnection);

            OleDbCommand cmd2 = new OleDbCommand("INSERT INTO Marcajes (Persona,Fecha,Hora,Terminal,Modificado,ModoIdentificacion) VALUES (889,'" + date + "','08:40',1,true,0);", mConnection);

            OleDbCommand cmd3 = new OleDbCommand("INSERT INTO Marcajes (Persona,Fecha,Hora,Terminal,Modificado,ModoIdentificacion) VALUES (890,'" + date + "','07:58',1,true,0);", mConnection);
            OleDbCommand cmd31 = new OleDbCommand("INSERT INTO Marcajes (Persona,Fecha,Hora,Terminal,Modificado,ModoIdentificacion) VALUES (890,'" + date + "','09:20',1,true,0);", mConnection);
            OleDbCommand cmd32 = new OleDbCommand("INSERT INTO Marcajes (Persona,Fecha,Hora,Terminal,Modificado,ModoIdentificacion) VALUES (890,'" + date + "','10:15',1,true,0);", mConnection);

            OleDbCommand cmd4 = new OleDbCommand("INSERT INTO Marcajes (Persona,Fecha,Hora,Terminal,Modificado,ModoIdentificacion) VALUES (891,'" + date + "','10:20',1,true,0);", mConnection);

            mConnection.Open();
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd31.ExecuteNonQuery();
            cmd32.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            mConnection.Close();
        } 
    }
}
