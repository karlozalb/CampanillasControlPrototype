using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Data.Odbc;

namespace CampanillasControlPrototype
{
    /// <summary>
    /// This class provide methods to deal with a Paradox database.
    /// 
    /// </summary>
    class ParadoxDBController
    {
        


        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //Connection instance.
        private OleDbConnection mConnection;         

        //DATABASE CONNECTION STRING
        string mConnectionString = "";

        Dictionary<int, int> teacherIdConversion;


        public ParadoxDBController()
        {
            getSystemPinPathDB();

            mConnection = new OleDbConnection(mConnectionString);

            mConnection.ConnectionString = mConnectionString;
            mConnection.Open();

            setAdditionalData();
            //getCheckIns(879,21,7,2015);
        }

        void getSystemPinPathDB()
        {
            string mySetting = ConfigurationManager.AppSettings["systempindbfolder"];
            mConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Paradox 7.x;Data Source=" + mySetting + ";";

            //string prueba = "Driver={Microsoft Paradox Driver (*.db )};DriverID=538;Fil=Paradox 7.X;DefaultDir = " + mySetting + "\\; Dbq =  " + mySetting + "\\; CollatingSequence = ASCII; ";
            //string prueba = "Driver={Microsoft Paradox Driver (*.db )};Data Source=" + mySetting + ";Database=" + mySetting+";";

            //string prueba = "Provider=MSDASQL.1;Persist Security Info=False;Mode=Read;Extended Properties='DSN=SystemPin;DBQ=" + mySetting + ";DefaultDir=" + mySetting + ";DriverId=538;FIL=Paradox 7.X;MaxBufferSize=2048;PageTimeout=600;';Initial Catalog=" + mySetting + ";";
            /*string prueba = "DSN=SystemPin2";

            OdbcConnection c = new OdbcConnection(prueba);
            c.Open();

            using (OdbcCommand cmd = new OdbcCommand("SELECT * FROM Personal",c))
            {
                OdbcDataReader reader = cmd.ExecuteReader();
            }*/                        

        }

        public void setAdditionalData()
        {
            teacherIdConversion = new Dictionary<int, int>();

            string query = "SELECT Id,LTRIM(Codigo) FROM Personal;";

            try
            {
                using (OleDbCommand cmd = new OleDbCommand(query, mConnection))
                    {
                        using (OleDbDataAdapter adap = new OleDbDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();

                            adap.Fill(dt);

                            foreach (DataRow d in dt.Rows)
                            {
                                teacherIdConversion.Add(Convert.ToInt32(d[0]), Convert.ToInt32(d[1]));
                            }

                            dt.Dispose();
                            adap.Dispose();
                        }
                    }                
            }
            catch (Exception e)
            {
                log.Error("info error en clausula SELECT sobre la base de datos Paradox. (probable lectura/escritura paralela)\n", e);
            }
        }

        /// <summary>
        /// Gets the today's check ins of a specific teacher (id and date provided) 
        /// </summary>
        /// <param name="pteacherid">id that identifies a teacher</param>
        /// <param name="pday">day</param>
        /// <param name="pmonth">month</param>
        /// <param name="pyear">year</param>
        /// <returns></returns>
        public Dictionary<int, List<DateTime>> getCheckIns(int pday, int pmonth, int pyear)
        {
            string query = "SELECT Persona,Fecha,Hora FROM Marcajes WHERE year(Fecha) = " + pyear + " AND month(Fecha) = " + pmonth + " AND  day(Fecha) = " + pday + " ; ";

             //Get the rows that match with the query.
             Dictionary < int, List < DateTime >> teacherTimes = selectCommandToDictionary(query);               
             return teacherTimes;          
        }

        /// <summary>
        /// Gets the today's check ins of a specific teacher (id and date provided) 
        /// </summary>
        /// <param name="pteacherid">id that identifies a teacher</param>
        /// <param name="pday">day</param>
        /// <param name="pmonth">month</param>
        /// <param name="pyear">year</param>
        /// <returns></returns>
        public List<DateTime> getCheckInsOLD(int pteacherid,int pday,int pmonth,int pyear)
        {
            //We get Codigo to Id from Personal.db table.
            string query = "SELECT Id FROM Personal WHERE LTRIM(Codigo)=\"" + pteacherid + "\";";

            //Get the rows that match with the query.
            DataTable returnedData = selectCommand(query);

            if (returnedData!=null && returnedData.Rows.Count > 0)
            {

                int realTeacherId = (int)returnedData.Rows[0]["Id"];

                returnedData.Dispose();

                //SQL query.
                query = "SELECT Fecha,Hora FROM Marcajes WHERE Persona=" + realTeacherId + " AND year(Fecha) = " + pyear + " AND month(Fecha) = " + pmonth + " AND  day(Fecha) = " + pday + " ; ";

                //Get the rows that match with the query.
                returnedData = selectCommand(query);

                if (returnedData != null)
                {
                    //This two lists stores the check ins of a specific day.
                    List<DateTime> times = new List<DateTime>();

                    //Iterating the data row to get the times.
                    foreach (DataRow row in returnedData.Rows)
                    {
                        String[] splitDate = row["Fecha"].ToString().Split(' ');
                        String[] splitTime = row["Hora"].ToString().Split(' ');

                        times.Add(Convert.ToDateTime(splitDate[0] + " " + splitTime[1]));
                    }

                    returnedData.Dispose();

                    return times;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
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
            string query = "SELECT LTRIM(Codigo),Nombre FROM Personal;";

            //Get the rows that match with the query.
            DataTable returnedData = selectCommand(query);

            foreach (DataRow row in returnedData.Rows)
            {
                ppersonal.Add(new PersonalNode(Convert.ToInt32(row[0].ToString()),row["Nombre"].ToString()));
            }

            returnedData.Dispose();
        }

        /// <summary>
        /// Generic method to execute SELECT SQL commands.
        /// </summary>
        /// <param name="pquery"></param>
        /// <returns></returns>
        /// 

        public Dictionary<int, List<DateTime>> selectCommandToDictionary(String pquery)
        {
            try
            {
                Dictionary<int, List<DateTime>> teacherTimes = new Dictionary<int, List<DateTime>>();

                using (OleDbCommand cmd = new OleDbCommand(pquery, mConnection))
                    {
                        using (OleDbDataAdapter adap = new OleDbDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();

                            adap.Fill(dt);
                            
                            
                            foreach (DataRow d in dt.Rows)
                            {
                                List<DateTime> timesList;

                                int realTeacherId;
                                teacherIdConversion.TryGetValue(Convert.ToInt32(d["Persona"]), out realTeacherId);

                                if (teacherTimes.TryGetValue(realTeacherId, out timesList))
                                {
                                    String[] splitDate = d["Fecha"].ToString().Split(' ');
                                    String[] splitTime = d["Hora"].ToString().Split(' ');

                                    timesList.Add(Convert.ToDateTime(splitDate[0] + " " + splitTime[1]));
                                }
                                else
                                {
                                    String[] splitDate = d["Fecha"].ToString().Split(' ');
                                    String[] splitTime = d["Hora"].ToString().Split(' ');

                                    timesList = new List<DateTime>();
                                    timesList.Add(Convert.ToDateTime(splitDate[0] + " " + splitTime[1]));
                                    teacherTimes.Add(realTeacherId, timesList);
                                }                                
                            }
                            adap.Dispose();
                            dt.Dispose();                       
                        }
                    }

                return teacherTimes;
            }
            catch (Exception e)
            {
                log.Error("info error en clausula SELECT sobre la base de datos Paradox. (probable lectura/escritura paralela)\n", e);
            }
            return null;
        }


        public DataTable selectCommand(String pquery)
        {
            //Debug.WriteLine("Consulta: "+ pquery);

            try {
                using (OleDbConnection conn = new OleDbConnection())
                {
                    DataTable dt = new DataTable();

                    conn.ConnectionString = mConnectionString;
                    conn.Open();

                    using (OleDbCommand cmd = new OleDbCommand(pquery, conn))
                    {
                        using (OleDbDataAdapter adap = new OleDbDataAdapter(cmd))
                        {
                            adap.Fill(dt);                        

                            return dt;
                        }
                    }                    
                }
            }catch (Exception e){
                log.Error("info error en clausula SELECT sobre la base de datos Paradox. (probable lectura/escritura paralela)\n",e);
            }
            return null;            
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
