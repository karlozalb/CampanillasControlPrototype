using CommsModule;
using SimpleThreadSafeCall;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesterClient
{
    public partial class ReportGenerator : Form, ICommClientController
    {
        Client mClient;

        int gridViewLeft, gridViewRight, gridViewUpDown;

        public ReportGenerator()
        {
            Login_Dialog_Form1 NewLogin = new Login_Dialog_Form1();
            DialogResult Result = NewLogin.ShowDialog();
            switch (Result)
            {
                case DialogResult.OK:
                    startReportGenerator();
                    break;
                case DialogResult.Cancel:
                    this.Close();
                    break;
            }            
        }

        public void startReportGenerator()
        {
            InitializeComponent();

            GoFullscreen(false);

            gridViewLeft = dataGridView1.Left;
            gridViewRight = this.Width - (dataGridView1.Left + dataGridView1.Width);
            gridViewUpDown = this.Height - dataGridView1.Height;

            mClient = new Client(this);
            if (mClient.discoverServer())
            {
                mClient.SendMessageAsync(new SerializableGetTeachersListDataMessage());
            }
            else
            {
                MessageBox.Show("No se encuentra la aplicación central en la red local (la que muestra los datos de ausencias), asegúrese de que está conectado a la misma red que la aplicación central y vuelva a intentarlo.", "Error de conexión a la aplicación central", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public void addTeacherListDataToGUI(SerializableTeacherList pteacherlist)
        {
            foreach (SerializableTeacherList.TeacherData entry in pteacherlist.mTeachers)
            {
                comboBox1.SafeInvoke(d => d.Items.Add(entry));
            }
        }

        public void addTeachersDataToGUI(SerializableTeacherData pteacherdata)
        {
            foreach (SerializableTeacherData.ClockInDataNode entry in pteacherdata.mClockins)
            {
                string[] row = new string[4];

                row[0] = entry.day.ToShortDateString();
                row[1] = entry.entranceHour.ToString();
                row[2] = entry.actualEntranceHour.ToString();
                row[3] = entry.delayMinutes.ToString();

                dataGridView1.SafeInvoke(d => d.Rows.Add(row));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            SerializableGetTeacherDataMessage message = new SerializableGetTeacherDataMessage();

            message.teacherID = Convert.ToInt32(((SerializableTeacherList.TeacherData)comboBox1.SelectedItem).mId);
            message.init = dateTimePicker1.Value;
            message.end = dateTimePicker2.Value;

            mClient.SendMessageAsync(message);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int treshold = 0;

            try {
                treshold = Convert.ToInt32(textBox3.Text);
            }
            catch (FormatException formatexcept)
            {
                MessageBox.Show("Número de marca incorrecto, asegúrese de que no ha introducido caracteres o espacios en blanco.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataGridViewRowCollection rows = dataGridView1.Rows;

            for (int i = 0; i < rows.Count; i++)
            {
                if (Convert.ToInt32(rows[i].Cells[3].FormattedValue) >= treshold) {
                    rows[i].Cells[3].Style.BackColor = Color.RosyBrown;
                }
                else
                {
                    rows[i].Cells[3].Style.BackColor = Color.White;
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ReportGenerator_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = this.Width - (gridViewLeft + gridViewRight);
            dataGridView1.Height = this.Height - 2 * gridViewRight;
        }
       

        private void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            }
        }
    }
}
