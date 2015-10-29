using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Threading;
using System.Web;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace CampanillasControlPrototype
{
    public class MySQLDBController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string mDatabaseName = "iescampa_infoprofesores";
        private string mConnectionString = String.Empty;

        private bool mEnabled = false;

        List<MissingNode> mMissingNodes;
        List<MissingPerHourNode> mMissingPerHourNodes;
        bool sendData;

        public MySQLDBController()
        {
            //mConnectionString = string.Format("Server=http://iescampanillas.com; database={0};Port=3306;UID=iesca_infoprof; password=w3bqu3ry", mDatabaseName);

            mMissingNodes = new List<MissingNode>();
            mMissingPerHourNodes = new List<MissingPerHourNode>();

            Thread mainTaskThread = new Thread(new ThreadStart(taskManagement));
            mainTaskThread.Start();
        }                

        public void taskManagement()
        {
            while (true)
            {
                if (sendData)
                {
                    if (mMissingNodes.Count > 0)
                    {
                        MissingNode n = mMissingNodes[0];
                        mMissingNodes.RemoveAt(0);

                        string date = HttpUtility.UrlEncode(n.date.ToShortDateString());
                        string id = n.teacherId.ToString();

                        string url = "https://infoprofesores.iescampanillas.com/add_complete_missing.php";

                        string myParameters = "userid=" + id + "&date=" + date + "&pass=w3bqu3ry";

                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            string HtmlResult = wc.UploadString(url, myParameters);

                            Debug.WriteLine(HtmlResult);
                        }                        
                    }

                    if (mMissingPerHourNodes.Count > 0)
                    {
                        MissingPerHourNode n = mMissingPerHourNodes[0];
                        mMissingPerHourNodes.RemoveAt(0);

                        string date = HttpUtility.UrlEncode(n.date.ToShortDateString());
                        string id = n.teacherId.ToString();
                        string gt1 = n.GT1.ToString();
                        string p = n.P.ToString();
                        string s = n.S.ToString();
                        string t = n.T.ToString();
                        string r = n.R.ToString();
                        string c = n.C.ToString();
                        string q = n.Q.ToString();
                        string sx = n.SX.ToString();
                        string gt2 = n.GT2.ToString();

                        string url = "https://infoprofesores.iescampanillas.com/add_perhour_missing.php";

                        string myParameters = "userid=" + id + "&date=" + date + "&gt1=" + gt1 + "&p=" + p + "&s=" + s + "&t=" + t + "&r=" + r + "&c=" + c + "&q=" + q + "&sx=" + sx + "&gt2=" + gt2 + "&pass=w3bqu3ry";

                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            string HtmlResult = wc.UploadString(url, myParameters);

                            Debug.WriteLine(HtmlResult);
                        }
                    }

                    if (mMissingPerHourNodes.Count == 0 && mMissingNodes.Count == 0) sendData = false;
                }

                Thread.Sleep(500);
            }
        }

        public void startToSendData()
        {
            sendData = true;
        }

        public void sendMissing(int pteacherid, DateTime pdate)
        {
            MissingNode newNode = new MissingNode();
            newNode.teacherId = pteacherid;
            newNode.date = pdate;

            mMissingNodes.Add(newNode);
        }

        public void sendMissingPerHour(int pteacherid, DateTime pdate,int pgt1,int pp,int ps, int pt, int pr, int pc, int pq, int psx, int pgt2)
        {
            MissingPerHourNode newNode = new MissingPerHourNode();

            newNode.teacherId = pteacherid;
            newNode.date = pdate;
            newNode.GT1 = pgt1;
            newNode.P = pp;
            newNode.S = ps;
            newNode.T = pt;
            newNode.R = pr;
            newNode.C = pc;
            newNode.Q = pq;
            newNode.SX = psx;
            newNode.GT2 = pgt2;

            mMissingPerHourNodes.Add(newNode);
        }


        public class MissingNode
        {
            public int teacherId;
            public DateTime date;
        }


        public class MissingPerHourNode
        {
            public int teacherId;
            public DateTime date;
            public int GT1, P, S, T, R, C, Q, SX, GT2;
        }

        /*public bool testConnection()
        {
            using (MySqlConnection conn = new MySqlConnection(mConnectionString))
            {
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        //Conexión de prueba.
                        conn.Open();
                        mEnabled = true;
                        break;
                    }
                    catch (Exception e)
                    {
                        log.Error("info Error: intento " + i + " fallido al conectar con la BD remota MySQL. ¿estás conectado a internet?", e);
                        Thread.Sleep(2000);
                        mEnabled = false; //Deshabilitamos los envios a la base de dato de MySQL ya que no hemos logrado conectar.
                    }
                }
            }
            return mEnabled;
        }

        public void executeQuery(string pquery)
        {
            if (mEnabled)
            {
                using (MySqlConnection conn = new MySqlConnection(mConnectionString))
                {
                    conn.ConnectionString = mConnectionString;
                    conn.Open();

                    MySqlCommand command = new MySqlCommand(pquery, conn);

                    command.ExecuteNonQuery();
                }
            }
            else
            {
                log.Error("info BS MySQL deshabilitada por no haber superado el test de conexión. ¿estás conectado a internet?");
            }
        }*/
    }
}
