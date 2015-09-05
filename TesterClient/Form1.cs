using CommsModule;
using SimpleThreadSafeCall;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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

        DeleteAnuncioForm mDeleteForm;
        AddAnuncioForm mAddAnuncioForm;
        SerializableAdList mCurrentAdList;

        ExcelExporter mExcelExporter;

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

            gridViewLeft = tabPanel.Left;
            gridViewRight = this.Width - (tabPanel.Left + tabPanel.Width);
            gridViewUpDown = this.Height - tabPanel.Height;

            mClient = new Client(this, (string)ConfigurationManager.AppSettings["serverip"],Convert.ToInt32(ConfigurationManager.AppSettings["serverport"]));

            mExcelExporter = new ExcelExporter();

            if (!mClient.SendMessageAsyncForResponse(new SerializableGetTeachersListDataMessage()))
            {
                MessageBox.Show("No se encuentra la aplicación central en la red local (la que muestra los datos de ausencias), asegúrese de que está conectado a la misma red que la aplicación central y vuelva a intentarlo.", "Error de conexión a la aplicación central", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            /*if (mClient.discoverServer())
            {
                mClient.SendMessageAsync(new SerializableGetTeachersListDataMessage());
            }
            else
            {
               
            }*/
        }

        public void addTeacherListDataToGUI(SerializableTeacherList pteacherlist)
        {
            foreach (SerializableTeacherList.TeacherData entry in pteacherlist.mTeachers)
            {
                comboBoxProfesor.SafeInvoke(d => d.Items.Add(entry));
            }
        }

        public void addTeachersDataToGUI(SerializableTeacherData pteacherdata)
        {
            if (pteacherdata.mClockins != null && pteacherdata.mClockins.Count > 0)
            {
                int currentRowIndex = 0;
                DateTime currentDateTime = pteacherdata.mClockins[0].day;
                bool boldLine;

                foreach (SerializableTeacherData.ClockInDataNode entry in pteacherdata.mClockins)
                {
                    string[] row = new string[5];

                    if (DateTime.Compare(currentDateTime, entry.day) != 0)
                    {
                        currentDateTime = entry.day;
                        currentRowIndex++;
                        dataGridFichajes.SafeInvoke(d => d.Rows.Add(new string[5]));
                    }

                    row[0] = entry.day.ToShortDateString();
                    row[1] = entry.entranceHour.ToString();
                    if (entry.isClockIn)
                    {
                        row[2] = entry.actualEntranceHour.ToString();
                        row[3] = entry.delayMinutes.ToString();
                        row[4] = "Entrada";
                    }
                    else
                    {
                        row[2] = "";
                        row[3] = "";
                        row[4] = "Salida";
                    }
                    dataGridFichajes.SafeInvoke(d => d.Rows.Add(row));

                    if (entry.isClockIn)
                    {
                        dataGridFichajes.SafeInvoke(d => d.Rows[currentRowIndex].Cells[4].Style.BackColor = Color.YellowGreen);
                    }
                    else
                    {
                        dataGridFichajes.SafeInvoke(d => d.Rows[currentRowIndex].Cells[4].Style.BackColor = Color.Yellow);
                    }                                      

                    currentRowIndex++;
                }
            }
            else
            {
                MessageBox.Show("No hay datos para el periodo seleccionado.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonGetClockIns_Click(object sender, EventArgs e)
        {
            dataGridFichajes.Rows.Clear();

            SerializableGetTeacherDataMessage message = new SerializableGetTeacherDataMessage();

            if (comboBoxProfesor.SelectedItem != null)
            {
                message.teacherID = Convert.ToInt32(((SerializableTeacherList.TeacherData)comboBoxProfesor.SelectedItem).mId);
                message.init = dateTimeInit.Value;
                message.end = dateTimeEnd.Value;

                tabPanel.SelectTab(0);

                mClient.SendMessageAsyncForResponse(message);
            }
            else
            {
                MessageBox.Show("Seleccione un profesor para obtener los datos por favor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonGetFaltas_Click(object sender, EventArgs e)
        {
            dataGridFaltas.Rows.Clear();

            tabPanel.SelectTab(1);
            SerializableGetMissingTeachersMessage message = new SerializableGetMissingTeachersMessage();
            message.init = dateTimePickerFaltas1.Value;
            message.end = dateTimePickerFaltas2.Value;

            mClient.SendMessageAsyncForResponse(message);
        }

        private void buttonMarkClockIns_Click(object sender, EventArgs e)
        {
            int treshold = 0;

            try {
                treshold = Convert.ToInt32(textBox3.Text);
            }
            catch (FormatException formatexception)
            {
                MessageBox.Show("Número de marca incorrecto, asegúrese de que no ha introducido caracteres o espacios en blanco.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataGridViewRowCollection rows = dataGridFichajes.Rows;

            
                for (int i = 0; i < rows.Count; i++)
                {
                try
                    {
                        if (Convert.ToInt32(rows[i].Cells[3].FormattedValue) >= treshold) {
                            rows[i].Cells[3].Style.BackColor = Color.RosyBrown;
                        }
                        else
                        {
                            rows[i].Cells[3].Style.BackColor = Color.White;
                        }
                    }
                    catch (FormatException exception)
                    {
                        //No hago nada, esto está para filtrar espacios en blanco que obviamente dan error.
                    }
            }
            
        }       

        private void añadirAnuncioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mAddAnuncioForm == null)
            {
                mAddAnuncioForm = new AddAnuncioForm(mClient);
                mAddAnuncioForm.Show();
            }
        }

        private void borrarAnuncioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mDeleteForm == null)
            {
                mClient.SendMessageAsyncForResponse(new SerializableGetAdList());
            }
        }

        public void deleteAdResponseReceived(SerializableAdList plist)
        {
            mCurrentAdList = plist;
            showDeleteForm();
        }

        public void showDeleteForm()
        {
            if (this.InvokeRequired)
            { // check if on UI thread
                this.BeginInvoke(new MethodInvoker(this.showDeleteForm)); // we need to create the new form on the UI thread
                return;
            }

            mDeleteForm = new DeleteAnuncioForm(this);
            mDeleteForm.setAdList(mCurrentAdList);
            mDeleteForm.Show();
        }

        /// <summary>
        /// Envía al servidor un mensaje de borrado de anuncio.
        /// </summary>
        /// <param name="padid"></param>
        public void deleteAd(int padid)
        {
            mClient.SendMessageAsyncForResponse(new SerializableDeleteAd(padid));
        }


        /// <summary>
        /// Gestiona el redimensionado de la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportGenerator_Resize(object sender, EventArgs e)
        {
            tabPanel.Width = this.Width - (gridViewLeft + gridViewRight);
            tabPanel.Height = this.Height - 2 * gridViewRight;
        }
       

        /// <summary>
        /// Gestiona el modo de la ventana (fullscreen, normal).
        /// </summary>
        /// <param name="fullscreen"></param>
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

        /// <summary>
        /// Abre un FileDialog para guardarlo posteriormente en CSV.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportarEnXLSExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel XLSX|*.xlsx|PDF|*.pdf";
            saveFileDialog1.Title = "Exportar informe";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {                
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        if (tabPanel.SelectedIndex == 0)
                        {
                            mExcelExporter.toXLS("Fichajes de " + ((SerializableTeacherList.TeacherData)comboBoxProfesor.SelectedItem).mTeacherName + " desde " + dateTimeInit.Value.ToShortDateString() + " hasta " + dateTimeEnd.Value.ToShortDateString(),"Fichajes",dataGridFichajes, saveFileDialog1.FileName);
                        } //
                        else if (tabPanel.SelectedIndex == 1)
                        {
                            mExcelExporter.toXLS("Faltas desde " + dateTimePickerFaltas1.Value.ToShortDateString() + " hasta " + dateTimePickerFaltas2.Value.ToShortDateString(), "Faltas", dataGridFaltas, saveFileDialog1.FileName);
                        }
                        break;
                    case 2:                        
                        break;
                    case 3:                       
                        break;
                }                
            }
        }

        /// <summary>
        /// Muestra un mensaje y cierra dialogos abiertos.
        /// </summary>
        /// <param name="ptext"></param>
        public void showMessage(string ptext)
        {
            MessageBox.Show(ptext,"Info:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            closeDeleteForm();
            closeAddForm();
        }     

        /*
        * Cerrar dialogos cuando ha habido error o ha habido éxito.
        */

        public void closeDeleteForm()
        {
            if (mDeleteForm != null)
            {
                mDeleteForm.SafeInvoke(d => d.Close());
                mDeleteForm.SafeInvoke(d => d.Dispose());
                mDeleteForm = null;
            }
        }

        public void closeAddForm()
        {
            if (mAddAnuncioForm != null)
            {
                mAddAnuncioForm.SafeInvoke(d => d.Close());
                mAddAnuncioForm.SafeInvoke(d => d.Dispose());
                mAddAnuncioForm = null;
            }
        }

        /// <summary>
        /// Este método recibe una lista de profesores que han faltado en un intervalo de tiempo específico para mostrarlo en la interfaz.
        /// </summary>
        /// <param name="presponse"></param>
        public void missingTeachersListReceived(SerializableMissingTeachersList presponse)
        {
            if (presponse.mMissingList != null && presponse.mMissingList.Count > 0)
            {
                foreach (SerializableMissingTeachersList.TeacherMissingNode entry in presponse.mMissingList)
                {
                    string[] row = new string[2];

                    row[0] = entry.NAME;
                    row[1] = entry.DAYS[0].ToShortDateString();

                    dataGridFaltas.SafeInvoke(d => d.Rows.Add(row));

                    if (entry.DAYS.Count > 1)
                    {
                        for (int i = 1; i < entry.DAYS.Count; i++)
                        {
                            string[] newRow = new string[2];
                            row[0] = "";
                            row[1] = entry.DAYS[i].ToShortDateString();
                            dataGridFaltas.SafeInvoke(d => d.Rows.Add(row));
                        }
                    }                   
                }
            }
            else
            {
                MessageBox.Show("No hay datos para el periodo seleccionado.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
