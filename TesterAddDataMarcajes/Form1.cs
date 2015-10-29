using CampanillasControlPrototype;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace TesterAddDataMarcajes
{
    public partial class Form1 : Form
    {
        TestingParadoxDBController mPDXDB;
        DataBaseController mDBController;

        List<CampanillasControlPrototype.PersonalNode> teachers;

        private int currentSelectedId = 0;

        System.Timers.Timer mTaskTimer;

        public Form1()
        {
            InitializeComponent();

            mPDXDB = new TestingParadoxDBController();
            mDBController = new DataBaseController();

            teachers = new List<CampanillasControlPrototype.PersonalNode>();

            mDBController.getAllTeachersFromAccessTestData(teachers);

            //mPDXDB.getAllTeachers(teachers);

            foreach (CampanillasControlPrototype.PersonalNode p in teachers)
            {
                comboBox1.Items.Add(p);
            }

            //TESTING MASIVO

            mTaskTimer = new System.Timers.Timer(1000);

            mTaskTimer.Elapsed += new ElapsedEventHandler(testTask);
            mTaskTimer.Enabled = true; // Enable it         

            setTime(2015,9,7,8,0,1);
        }

        public struct SystemTime
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Millisecond;
        };

        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
        public extern static bool Win32SetSystemTime(ref SystemTime st);

        public void setTime(ushort pyear,ushort pmonth,ushort pday,ushort phour,ushort pminute,ushort pdayofweek)
        {
            SystemTime st = new SystemTime
            {
                Year = pyear,
                Month = pmonth,
                Day = pday,
                Hour = phour,
                Minute = pminute,
                DayOfWeek = pdayofweek
            };
            Win32SetSystemTime(ref st);

            Debug.WriteLine(DateTime.Now);
        }

        public void testTask(object sender, ElapsedEventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem!=null)
            {
                currentSelectedId = Convert.ToInt32(((CampanillasControlPrototype.PersonalNode)comboBox1.SelectedItem).getId());
            }
            else
            {
                currentSelectedId = Convert.ToInt32(textBox1.Text);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) currentSelectedId = Convert.ToInt32(textBox1.Text);
            mPDXDB.insertTESTClockIn(currentSelectedId);
            MessageBox.Show("Se añadió correctamente.");
        }
    }
}
