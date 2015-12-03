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
using System.Runtime.Serialization;

namespace CampanillasControlPrototype
{
    class DataBaseController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string DB_PATH = "D:\\Documents\\Visual Studio 2015\\Projects\\CampanillasControlPrototype\\CampanillasControlPrototype\\DB\\";
        private const string DB_NAME = "ControlDB.mdf";

        private string fullConnectionString;

        MySQLDBController mMySQLController;

        public DataBaseController(MySQLDBController pmysqlcontroller)
        {
            mMySQLController = pmysqlcontroller;

            DB_PATH = Directory.GetCurrentDirectory()+"\\DB\\";
            fullConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"" + DB_PATH + DB_NAME + "\";Integrated Security=True";
            log.Info("info cadena de conexión DB SQL Server: "+ fullConnectionString);
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
                        log.Info("info creada tabla de profesor "+ person.getName() + " ID: " + person.getId());
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
                                                    "[MarcajeEntrada]    BIT NULL, "+
                                                    "PRIMARY KEY CLUSTERED([Id] ASC));", conn);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error("error en la creación de la tabla profesor con ID: "+pid,e);                    
                }
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

        public void registerClockIn(int pteacherid, DateTime pclockintime, DateTime pactualtime, int pdelay, bool pisentrance)
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

                        if (ddTimeDifference.Minutes == 0 && ddTimeDifference.Hours == 0)
                        {
                            //Duplicate! we don't add anything.
                            add = false;
                        }
                    }
                }

                if (add)
                {
                    try {
                        command = new SqlCommand("INSERT INTO Profesor_" + pteacherid + "_Marcajes (Fecha,\"Hora entrada\",\"Hora entrada real\",\"Minutos Retraso\",\"MarcajeEntrada\") VALUES (convert(datetime,'" + DateTime.Now.ToShortDateString() + "',105),'" + pclockintime.ToString("HH:mm") + "','" + pactualtime.ToString("HH:mm") + "'," + pdelay + "," + ((pisentrance) ? 1 : 0) + ")", conn);
                        command.ExecuteNonQuery();

                        log.Info("info Registrado nuevo fichaje de ID: " + pteacherid);
                    }catch (Exception e)
                    {
                        log.Error("info error añadiendo fichaje de ID: " + pteacherid, e);
                    }
                }
            }
        }

        internal void deleteSubstitute(int psubstituteid, int pmissingid)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                try {
                    conn.ConnectionString = fullConnectionString;
                    conn.Open();

                    SqlCommand command = new SqlCommand("DELETE FROM Sustitutos WHERE IdProfesorSustituto = " + psubstituteid + " AND IdProfesorASustituir = " + pmissingid + ";", conn);
                    command.ExecuteNonQuery();
                    log.Info("info añadido sustituto con ID: " + psubstituteid + " para profesor " + pmissingid);
                }catch(Exception e)
                {
                    log.Error("info error añadiendo sustituto con ID: " + psubstituteid + " para profesor " + pmissingid,e);
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

        public void saveIdAbrevTESTINGData(List<PersonalNode> ppersonal)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                foreach (PersonalNode entry in ppersonal)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO TestingIDAbrevTable (Id,Nombre) VALUES ("+ entry.getId() + ",'"+entry.getName()+"');", conn);

                    command.ExecuteNonQuery();
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
            string query = "SELECT * FROM Profesor_"+pteacherid+ "_Marcajes WHERE Fecha >= convert(datetime,'"+pinit.ToShortDateString()+"',105) and Fecha <= convert(datetime,'"+pend.ToShortDateString() + "',105);";

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
                        teacherData.addData(Convert.ToDateTime(reader[1]), (TimeSpan)reader[2], (TimeSpan)reader[3], (int)reader[4],(bool)reader[5]);                      
                    }
                }
            }

            return teacherData;
        }

        public SerializableTeacherData getTodayTeacherData(int pteacherid, DateTime pinit)
        {
            string query = "SELECT * FROM Profesor_" + pteacherid + "_Marcajes WHERE Fecha = convert(datetime,'" + pinit.ToShortDateString() + "',105) ORDER BY \"Hora entrada\" ASC;";

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
                        teacherData.addData(Convert.ToDateTime(reader[1]), (TimeSpan)reader[2], (TimeSpan)reader[3], (int)reader[4], (bool)reader[5]);
                    }
                }
            }

            return teacherData;
        }

        public void addTeacherMiss(int pteacherid,DateTime pdate,PersonalNode pteacher)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                //Para evitar duplicados.
                SqlCommand command = new SqlCommand("SELECT * FROM Faltas WHERE TeacherId=" + pteacherid + " AND Fecha=convert(datetime, '" + pdate.ToShortDateString() + "', 105);", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        command = new SqlCommand("SELECT * FROM Profesor_"+ pteacherid + "_Marcajes WHERE Fecha=convert(datetime, '" + pdate.ToShortDateString() + "', 105);", conn);

                        using (SqlDataReader reader2 = command.ExecuteReader())
                        {
                            if (!reader2.HasRows)
                            {
                                try {
                                    reader2.Close();

                                    string sqlQuery = "INSERT INTO Faltas (TeacherId,Fecha) VALUES (" + pteacherid + ", convert(datetime, '" + pdate.ToShortDateString() + "', 105));";

                                    command = new SqlCommand(sqlQuery, conn);
                                    command.ExecuteNonQuery();

                                    //mMySQLController.executeQuery("INSERT INTO Faltas(TeacherId, Fecha) VALUES(" + pteacherid + ", STR_TO_DATE('" + pdate.ToShortDateString() + "', '%d/%m/%Y'));");
                                    mMySQLController.sendMissing(pteacherid,pdate);

                                    log.Info("info guardada falta de día completo de: " + pteacherid);
                                }
                                catch (Exception e)
                                {
                                    log.Error("error al guardar faltas de día completo de " + pteacherid,e);
                                }
                            }
                            else
                            {
                                saveTeacherMissesPerHours(pteacherid, pdate, pteacher);
                            }
                        }
                    }
                }

            }
        }        

        public void saveTeacherMissesPerHours(int pteacherid, DateTime pdate, PersonalNode pteacher)
        {
            bool duplicate = false;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                //Para evitar duplicados.
                SqlCommand command = new SqlCommand("SELECT * FROM FaltasPorHoras WHERE TeacherId=" + pteacherid + " AND Fecha=convert(datetime, '" + pdate.ToShortDateString() + "', 105);", conn);

                using (SqlDataReader reader = command.ExecuteReader()){
                    if (reader.HasRows)
                    {
                        duplicate = true;
                    }
                }

            }

            if (!duplicate)
            {
                int[] values = new int[9];

                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = SerializableTeachersMissesPerHourList.C_NO_NECESARIA_PRESENCIA;
                }

                foreach (PersonalNode.HourNode h in pteacher.getTodaysHours())
                {
                    int numHour = h.mHour;

                    if (h.isTeacherLate() && h.isTeacherLeavingEarly())
                    {
                        values[numHour] = SerializableTeachersMissesPerHourList.C_SE_VA_ANTES_Y_LLEGA_TARDE;
                    }
                    else if (h.isTeacherLate())
                    {
                        values[numHour] = SerializableTeachersMissesPerHourList.C_LLEGA_TARDE;
                    }
                    else if (h.isTeacherLeavingEarly())
                    {
                        values[numHour] = SerializableTeachersMissesPerHourList.C_SE_VA_ANTES;
                    }
                    else if (!h.mAlreadyChecked)
                    {
                        values[numHour] = SerializableTeachersMissesPerHourList.C_FALTA_COMPLETA;
                    }
                    else
                    {
                        values[numHour] = SerializableTeachersMissesPerHourList.C_PRESENTE;
                    }
                }

                using (SqlConnection conn = new SqlConnection())
                {
                    try {
                        conn.ConnectionString = fullConnectionString;
                        conn.Open();

                        string sqlQuery = "INSERT INTO FaltasPorHoras (TeacherId,Fecha,GT1,Primera,Segunda,Tercera,Recreo,Cuarta,Quinta,Sexta,GT2) VALUES (" + pteacherid + ",convert(datetime, '" + pdate.ToShortDateString() + "', 105)," + values[0] + "," + values[1] + "," + values[2] + "," + values[3] + "," + values[4] + "," + values[5] + "," + values[6] + "," + values[7] + "," + values[8] + ");";

                        SqlCommand command = new SqlCommand(sqlQuery, conn);

                        command.ExecuteNonQuery();

                        //mMySQLController.executeQuery("INSERT INTO FaltasPorHoras (TeacherId,Fecha,GT1,Primera,Segunda,Tercera,Recreo,Cuarta,Quinta,Sexta,GT2) VALUES (" + pteacherid + ",STR_TO_DATE('" + pdate.ToShortDateString() + "', '%d/%m/%Y'))," + values[0] + "," + values[1] + "," + values[2] + "," + values[3] + "," + values[4] + "," + values[5] + "," + values[6] + "," + values[7] + "," + values[8] + ");");
                        mMySQLController.sendMissingPerHour(pteacherid, pdate, values[0],values[1],values[2],values[3],values[4],values[5],values[6],values[7],values[8]);

                        log.Info("info guardadas faltas por horas de: "+ pteacherid);
                    }
                    catch (Exception e)
                    {
                        log.Error("info error al guardar faltas por horas de "+ pteacherid + " - "+e);
                    }
                }
            }
        }   

        public SerializableTeachersMissesPerHourList getTeacherMissesPerHourList(List<PersonalNode> ppersonal,DateTime pinit,DateTime pend,int pteacherid)
        {
            SerializableTeachersMissesPerHourList toReturnList = new SerializableTeachersMissesPerHourList();
            
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command;

                if (pteacherid != 0)
                {
                    command = new SqlCommand("SELECT * FROM FaltasPorHoras WHERE TeacherId="+pteacherid+" AND Fecha >= convert(datetime,'" + pinit.ToShortDateString() + "',105) and Fecha <= convert(datetime,'" + pend.ToShortDateString() + "',105) ORDER BY TeacherId,Fecha ASC;", conn);
                }
                else
                {
                    command = new SqlCommand("SELECT * FROM FaltasPorHoras WHERE Fecha >= convert(datetime,'" + pinit.ToShortDateString() + "',105) and Fecha <= convert(datetime,'" + pend.ToShortDateString() + "',105) ORDER BY TeacherId,Fecha ASC;", conn);
                }

                int currentTeacherId = -1;
                SerializableTeachersMissesPerHourList.MissesPerHourTeacherNode teacherNode = null;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (currentTeacherId != (int)reader[0])
                        {
                            currentTeacherId = (int)reader[0];
                            teacherNode = new SerializableTeachersMissesPerHourList.MissesPerHourTeacherNode();
                            teacherNode.mId = currentTeacherId;
                            foreach (PersonalNode p in ppersonal)
                            {
                                if (p.getId() == currentTeacherId)
                                {
                                    teacherNode.mTeacherName = p.getName();
                                    break;
                                }
                            }
                            toReturnList.mMissesList.Add(teacherNode);
                        }

                        byte[] newValues = new byte[9];

                        for (int i = 0; i < 9; i++)
                        {
                            newValues[i] = (byte)reader[i + 2];
                        }                        

                        teacherNode.mDateHourValues.Add(new KeyValuePair<DateTime, byte[]>(Convert.ToDateTime(reader[1]), newValues));
                    }
                }

                return toReturnList;
            }
        }

        public void addAd(string ptext,string pdate)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                try
                {
                    conn.ConnectionString = fullConnectionString;
                    conn.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO Anuncios (texto,fecha) VALUES ('" + ptext + "','"+pdate+"');", conn);

                    command.ExecuteNonQuery();

                    log.Info("info Añadido anuncio \""+ptext+"\" con éxito.");
                }catch(Exception e)
                {
                    log.Error("info error al añadir anuncio \""+ptext+"\"");
                }
            }
        }

        public void deleteAd(int pid)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand("DELETE FROM Anuncios WHERE id="+pid+";", conn);

                command.ExecuteNonQuery();

                log.Info("info Eliminado anuncio "+ pid + " con éxito.");
            }
        }

        public SerializableAdList getAdList()
        {
            string query = "SELECT * FROM Anuncios;";

            SerializableAdList adList = new SerializableAdList();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SerializableAd newNode = new SerializableAd();

                        newNode.mId = (int)reader[0];
                        newNode.mText = (string)reader[1];
                        newNode.mDate = (string)reader[2];
                        adList.mAdList.Add(newNode);
                    }
                }
            }

            return adList;
        }

        public SerializableMissingTeachersList getMissingTeachers(List<PersonalNode> ppersonallist,DateTime pinit,DateTime pend,int pteacherid)
        {
            string query = "";

            if (pteacherid != 0)
            {
                query = "SELECT TeacherId,Fecha FROM Faltas WHERE TeacherId="+pteacherid+" AND Fecha >= convert(datetime,'" + pinit.ToShortDateString() + "',105) and Fecha <= convert(datetime,'" + pend.ToShortDateString() + "',105) ORDER BY TeacherId,Fecha ASC;";
            }
            else
            {
                query = "SELECT TeacherId,Fecha FROM Faltas WHERE Fecha >= convert(datetime,'" + pinit.ToShortDateString() + "',105) and Fecha <= convert(datetime,'" + pend.ToShortDateString() + "',105) ORDER BY TeacherId,Fecha ASC;";
            }


            SerializableMissingTeachersList missingTeachers = new SerializableMissingTeachersList();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    SerializableMissingTeachersList.TeacherMissingNode currentMissingNode = null;
                    int currentId = -1;

                    while (reader.Read())
                    {
                        if (currentId != (int)reader[0])
                        {
                            currentId = (int)reader[0];
                            currentMissingNode = new SerializableMissingTeachersList.TeacherMissingNode();

                            foreach (PersonalNode p in ppersonallist)
                            {
                                if (p.getId() == currentId)
                                {
                                    currentMissingNode.NAME = p.getName();
                                    break;
                                }
                            }

                            missingTeachers.mMissingList.Add(currentMissingNode);
                        }

                        currentMissingNode.DAYS.Add(Convert.ToDateTime(reader[1]));
                    }
                }
            }

            return missingTeachers;
        }

        public SerializableTeacherDataList getAllTeachersOddClockins(List<PersonalNode> ppersonallist, DateTime pinit, DateTime pend)
        {
            SerializableTeacherDataList teacherDataToReturn = new SerializableTeacherDataList();

            foreach (PersonalNode teacher in ppersonallist)
            {
                SerializableTeacherData teacherData = getTeacherData(teacher.getId(), pinit, pend);

                bool firstIt = true;
                DateTime currentDateTime = DateTime.Now;
                int clockInCount = 0;
                List<SerializableTeacherData.ClockInDataNode> tempList = new List<SerializableTeacherData.ClockInDataNode>();

                SerializableTeacherData newTeacherData = new SerializableTeacherData();
                newTeacherData.mId = teacher.getId();
                newTeacherData.mName = teacher.getName();

                foreach (SerializableTeacherData.ClockInDataNode clockindatanote in teacherData.mClockins)
                {
                    if (firstIt)
                    {
                        firstIt = false;
                        currentDateTime = clockindatanote.day;
                    }

                    if (DateTime.Compare(currentDateTime, clockindatanote.day) != 0)
                    {
                        if (clockInCount % 2 != 0)
                        {
                            foreach (SerializableTeacherData.ClockInDataNode clockindatanotetoadd in tempList)
                            {
                                newTeacherData.mClockins.Add(clockindatanotetoadd);
                            }
                        }
                        clockInCount = 0;
                        currentDateTime = clockindatanote.day;
                        tempList.Clear();
                    }
                   
                    tempList.Add(clockindatanote);
                    clockInCount++;
                }

                if (clockInCount % 2 != 0)
                {
                    foreach (SerializableTeacherData.ClockInDataNode clockindatanotetoadd in tempList)
                    {
                        newTeacherData.mClockins.Add(clockindatanotetoadd);
                    }
                }

                if (newTeacherData.mClockins != null && newTeacherData.mClockins.Count > 0) teacherDataToReturn.mTeacherDataList.Add(newTeacherData);
            }

            return teacherDataToReturn;
        }

        public SerializableLateClockInsList getAllTeachersLateClockins(List<PersonalNode> ppersonallist, DateTime pinit, DateTime pend,int pmindelay)
        {
            SerializableLateClockInsList teacherDataToReturn = new SerializableLateClockInsList();

            foreach (PersonalNode teacher in ppersonallist)
            {
                SerializableTeacherData teacherData = getTeacherData(teacher.getId(), pinit, pend);              

                SerializableTeacherData newTeacherData = new SerializableTeacherData();
                newTeacherData.mId = teacher.getId();
                newTeacherData.mName = teacher.getName();

                foreach (SerializableTeacherData.ClockInDataNode clockindatanote in teacherData.mClockins)
                {
                    if (clockindatanote.delayMinutes >= pmindelay && clockindatanote.isClockIn)
                    {
                        newTeacherData.mClockins.Add(clockindatanote);
                    }
                }

                if (newTeacherData.mClockins != null && newTeacherData.mClockins.Count > 0) teacherDataToReturn.mTeacherList.Add(newTeacherData);
            }

            return teacherDataToReturn;
        }


        public List<SerializableSubstituteTeacherNode> getSubstitutes()
        {
            string query = "SELECT * FROM Sustitutos;";

            List<SerializableSubstituteTeacherNode> substituteList = new List<SerializableSubstituteTeacherNode>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SerializableSubstituteTeacherNode newNode = new SerializableSubstituteTeacherNode();

                        newNode.mSubstituteId = (int)reader[0];
                        newNode.mMissingId = (int)reader[1];
                        substituteList.Add(newNode);
                    }
                }
            }

            return substituteList;
        }

        public void addSubstitute(int pmissing, int psubstitute)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                try {
                    conn.ConnectionString = fullConnectionString;
                    conn.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO Sustitutos (IdProfesorSustituto,IdProfesorASustituir) VALUES ('" + psubstitute + "','" + pmissing + "');", conn);

                    command.ExecuteNonQuery();

                    log.Info("info Añadido sustituto ID:" + psubstitute + " para persona ID: " + pmissing);
                }catch (Exception e)
                {
                    log.Error("info error al añadir sustituto ID:" + psubstitute + " para persona ID: " + pmissing,e);
                }
            }
        }

        public void addNoSchoolDay(DateTime pday)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                try {
                    conn.ConnectionString = fullConnectionString;
                    conn.Open();

                    string selectQuery = "SELECT * FROM NoLectivos WHERE Dia = (convert(datetime, '" + pday.ToShortDateString() + "', 105)) ;";
                    SqlCommand selectCommand = new SqlCommand(selectQuery, conn);

                    SqlDataReader reader = selectCommand.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        reader.Close();

                        SqlCommand command = new SqlCommand("INSERT INTO NoLectivos (Dia) VALUES (convert(datetime, '" + pday.ToShortDateString() + "', 105));", conn);

                        command.ExecuteNonQuery();

                        log.Info("info Añadido día no lectivo: "+pday);
                    }
                    else
                    {
                        log.Info("info Añadir día no lectivo: " + pday + "CANCELADO por duplicidad");
                        reader.Close();
                    }
                }catch (Exception e)
                {
                    log.Error("info error al añadir día no lectivo "+pday, e);
                }
            }
        }

        public void removeNoSchoolDay(DateTime pday)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                try {
                    conn.ConnectionString = fullConnectionString;
                    conn.Open();

                    SqlCommand command = new SqlCommand("DELETE FROM NoLectivos WHERE Dia = (convert(datetime, '" + pday.ToShortDateString() + "', 105));", conn);

                    command.ExecuteNonQuery();

                    log.Info("info día no lectivo eliminado: " + pday);
                }
                catch (Exception e)
                {
                    log.Error("info error al eliminar día no lectivo " + pday, e);
                }
            }
        }

        public SerializableNoSchoolDaysList getNoSchoolDayList()
        {
            string query = "SELECT * FROM NoLectivos;";

            SerializableNoSchoolDaysList noSchoolDays = new SerializableNoSchoolDaysList();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        noSchoolDays.mDaysList.Add(Convert.ToDateTime(reader[1]));
                    }
                }
            }

            return noSchoolDays;
        }

        public void DEBUG_DONT_USE_THIS_DELETEALLTABLES()
        {
            FileInfo file = new FileInfo("D:\\Documents\\Visual Studio 2015\\Projects\\CampanillasControlPrototype\\BorrarTablasProfesores.sql");
            string script = file.OpenText().ReadToEnd();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = fullConnectionString;
                conn.Open();

                SqlCommand command = new SqlCommand(script, conn);

                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM Faltas;", conn);

                command.ExecuteNonQuery();
            }
        }        
       
    }
   
}
