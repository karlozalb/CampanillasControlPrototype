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
        AddRemoveSubstituteForm mAddSubstituteForm;
        AddDeleteNoSchoolDay mAddDeleteNoSchoolDay;

        SerializableAdList mCurrentAdList;
        List<DateTime> mNoSchoolDaysList;

        ExcelExporter mExcelExporter;
        SerializableTeacherList mTeachersList;

        bool mToSendToAddSubstituteForm = false;

        bool mTransactionInProgress = false;

        public ReportGenerator()
        {            
            Login_Dialog_Form1 NewLogin = new Login_Dialog_Form1();
            /*DialogResult Result = NewLogin.ShowDialog();
            switch (Result)
            {
                case DialogResult.OK:
                    startReportGenerator();
                    break;
                case DialogResult.Cancel:
                    this.Close();
                    break;
            }*/

            startReportGenerator();
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
        }  

        private void buttonGetClockIns_Click(object sender, EventArgs e)
        {
            if (!mTransactionInProgress)
            {
                mTransactionInProgress = true;
                dataGridFichajes.Rows.Clear();

                SerializableGetTeacherDataMessage message = new SerializableGetTeacherDataMessage();

                if (checkDates() && comboBoxProfesor.SelectedItem != null)
                {

                    message.teacherID = Convert.ToInt32(((SerializableTeacherList.TeacherData)comboBoxProfesor.SelectedItem).mId);
                    message.init = dateTimeInit.Value;
                    message.end = dateTimeEnd.Value;

                    tabPanel.SelectTab(0);

                    if (!mClient.SendMessageAsyncForResponse(message))
                    {
                        showConnectionErrorMessage();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un profesor para obtener los datos por favor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mTransactionInProgress = false;
                }
            }
        }

        private void showConnectionErrorMessage()
        {
            MessageBox.Show("Ha habido un error de conexión con aplicación central en la red local (la que muestra los datos de ausencias), asegúrese de que está conectado a la misma red que la aplicación central y vuelva a intentarlo.", "Error de conexión a la aplicación central", MessageBoxButtons.OK, MessageBoxIcon.Error);
            mTransactionInProgress = false;
        }

        internal void clearAddDeleteNoSchoolDaysForm()
        {
            mAddDeleteNoSchoolDay = null;
        }

        private void buttonGetFaltas_Click(object sender, EventArgs e)
        {
            if (!mTransactionInProgress)
            {
                mTransactionInProgress = true;

                dataGridFaltas.Rows.Clear();

                tabPanel.SelectTab(1);
                SerializableGetMissingTeachersMessage message = new SerializableGetMissingTeachersMessage();
                message.init = dateTimeInit.Value;
                message.end = dateTimeEnd.Value;

                if (!mClient.SendMessageAsyncForResponse(message))
                {
                    showConnectionErrorMessage();
                }
            }
        }

        private void buttonMarkClockIns_Click(object sender, EventArgs e)
        {
            if (!mTransactionInProgress)
            {
                mTransactionInProgress = true;

                int treshold = 0;

                try
                {
                    treshold = Convert.ToInt32(textBoxDelay.Text);
                }
                catch (FormatException formatexception)
                {
                    MessageBox.Show("Número de marca incorrecto, asegúrese de que no ha introducido caracteres o espacios en blanco.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mTransactionInProgress = false;
                    return;
                }

                DataGridViewRowCollection rows = dataGridFichajes.Rows;


                for (int i = 0; i < rows.Count; i++)
                {
                    try
                    {
                        if (Convert.ToInt32(rows[i].Cells[3].FormattedValue) >= treshold)
                        {
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
                mTransactionInProgress = false;
            }
        }       

        private void añadirAnuncioToolStripMenuItem_Click(object sender, EventArgs e)
        {           

                if (mAddAnuncioForm == null)
                {
                    mAddAnuncioForm = new AddAnuncioForm(mClient,this);
                    mAddAnuncioForm.Show();
                }           
        }

        private void borrarAnuncioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!mTransactionInProgress)
            {
                if (mDeleteForm == null)
                {
                    if (!mClient.SendMessageAsyncForResponse(new SerializableGetAdList()))
                    {
                        showConnectionErrorMessage();
                    }
                }
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
            mDeleteForm.ShowDialog();
        }

        /// <summary>
        /// Envía al servidor un mensaje de borrado de anuncio.
        /// </summary>
        /// <param name="padid"></param>
        public void deleteAd(int padid)
        {
            if (!mClient.SendMessageAsyncForResponse(new SerializableDeleteAd(padid)))
            {
                showConnectionErrorMessage();
            }
            
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
                            if (comboBoxProfesor.SelectedItem == null)
                            {
                                MessageBox.Show("Seleccione un profesor para obtener los datos por favor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                mExcelExporter.toXLS("Fichajes de " + ((SerializableTeacherList.TeacherData)comboBoxProfesor.SelectedItem).mTeacherName + " desde " + dateTimeInit.Value.ToShortDateString() + " hasta " + dateTimeEnd.Value.ToShortDateString(), "Fichajes", dataGridFichajes, saveFileDialog1.FileName);
                            }
                        }
                        else if (tabPanel.SelectedIndex == 1)
                        {
                            mExcelExporter.toXLS("Faltas desde " + dateTimeInit.Value.ToShortDateString() + " hasta " + dateTimeEnd.Value.ToShortDateString(), "Faltas", dataGridFaltas, saveFileDialog1.FileName);
                        }
                        else if (tabPanel.SelectedIndex == 2)
                        {
                            mExcelExporter.toXLS("Fichajes impares desde " + dateTimeInit.Value.ToShortDateString() + " hasta " + dateTimeEnd.Value.ToShortDateString(), "Fichajes impares", dataGridViewOddClockins, saveFileDialog1.FileName);
                        }
                        else if (tabPanel.SelectedIndex == 3)
                        {
                            mExcelExporter.toXLS("Fichajes tardíos desde " + dateTimeInit.Value.ToShortDateString() + " hasta " + dateTimeEnd.Value.ToShortDateString(), "Fichajes tardíos", dataGridFichajesTarde, saveFileDialog1.FileName);
                        }
                        else if (tabPanel.SelectedIndex == 4)
                        {
                            mExcelExporter.toXLS("Faltas por horas desde " + dateTimeInit.Value.ToShortDateString() + " hasta " + dateTimeEnd.Value.ToShortDateString(), "Faltas por horas", dataGridFaltasPorHoras, saveFileDialog1.FileName);
                        }
                        else
                        {
                            MessageBox.Show("Esta hoja de datos no es exportable.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void oddClockinsButton_Click(object sender, EventArgs e)
        {
            if (!mTransactionInProgress)
            {
                mTransactionInProgress = true;

                SerializableGetBadClockInsTeachersListMessage message = new SerializableGetBadClockInsTeachersListMessage();

                message.mInit = dateTimeInit.Value;
                message.mEnd = dateTimeEnd.Value;

                dataGridViewOddClockins.SafeInvoke(d => d.Rows.Clear());
                tabPanel.SelectTab(2);

                if (!mClient.SendMessageAsyncForResponse(message))
                {
                    showConnectionErrorMessage();
                }
            }
        }

        /// <summary>
        /// Este método recibe una lista de profesores que han faltado en un intervalo de tiempo específico para mostrarlo en la interfaz.
        /// </summary>
        /// <param name="presponse"></param>
        public void missingTeachersListReceived(SerializableMissingTeachersList presponse)
        {
           try {
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
                            MessageBox.Show("Datos de faltas obtenidos con éxito.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No hay datos para el periodo seleccionado.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }catch (Exception e){
                    MessageBox.Show("Hubo un error en la obtención de los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                mTransactionInProgress = false;
            }
        }

        public void oddClockinsTeacherListReceived(SerializableTeacherDataList presponse)
        {
                try
                {
                    if (presponse.mTeacherDataList != null && presponse.mTeacherDataList.Count > 0)
                    {
                            int currentRowIndex = 0;

                            foreach (SerializableTeacherData entry in presponse.mTeacherDataList)
                            {
                                DateTime currentDateTime = entry.mClockins[0].day;

                                bool firstRow = true;

                                foreach (SerializableTeacherData.ClockInDataNode ciEntry in entry.mClockins)
                                {
                                    string[] row = new string[6];

                                    if (firstRow)
                                    {
                                        firstRow = false;
                                        row[0] = entry.mName;
                                    }
                                    else
                                    {
                                        row[0] = "";
                                    }

                                    if (DateTime.Compare(currentDateTime, ciEntry.day) != 0)
                                    {
                                        currentDateTime = ciEntry.day;
                                        currentRowIndex++;
                                        dataGridViewOddClockins.SafeInvoke(d => d.Rows.Add(new string[6]));
                                    }

                                    row[1] = ciEntry.day.ToShortDateString();
                                    row[2] = ciEntry.entranceHour.ToString();
                                    if (ciEntry.isClockIn)
                                    {
                                        row[3] = ciEntry.actualEntranceHour.ToString();
                                        row[4] = ciEntry.delayMinutes.ToString();
                                        row[5] = "Entrada";
                                    }
                                    else
                                    {
                                        row[3] = "";
                                        row[4] = "";
                                        row[5] = "Salida";
                                    }
                                    dataGridViewOddClockins.SafeInvoke(d => d.Rows.Add(row));

                                    if (ciEntry.isClockIn)
                                    {
                                        dataGridViewOddClockins.SafeInvoke(d => d.Rows[currentRowIndex].Cells[5].Style.BackColor = Color.YellowGreen);
                                    }
                                    else
                                    {
                                        dataGridViewOddClockins.SafeInvoke(d => d.Rows[currentRowIndex].Cells[5].Style.BackColor = Color.Yellow);
                                    }

                                    currentRowIndex++;
                                }
                            }
                            MessageBox.Show("Datos de fichajes impares obtenidos con éxito.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No hay datos para el periodo seleccionado.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Hubo un error en la obtención de los datos de fichajes impares.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                mTransactionInProgress = false;
            }
        }

        private void añadirSustitutoToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            mToSendToAddSubstituteForm = true;
            if (!mClient.SendMessageAsyncForResponse(new SerializableGetTeachersListDataMessage()))
            {
                showConnectionErrorMessage();
            }
        }

        public void addTeacherListDataToGUI(SerializableTeacherList pteacherlist)
        {
            mTeachersList = pteacherlist;

            comboBoxProfesor.SafeInvoke(d => d.Items.Clear());
            foreach (SerializableTeacherList.TeacherData entry in pteacherlist.mTeachers)
            {
                comboBoxProfesor.SafeInvoke(d => d.Items.Add(entry));
            }

            if (mToSendToAddSubstituteForm)
            {
                mToSendToAddSubstituteForm = false;
                showSubstituteForm();
            }
        }

        public void showSubstituteForm()
        {
            if (this.InvokeRequired)
            { // check if on UI thread
                this.BeginInvoke(new MethodInvoker(this.showSubstituteForm)); // we need to create the new form on the UI thread
                return;
            }

            if (mAddSubstituteForm == null)
            {
                mAddSubstituteForm = new AddRemoveSubstituteForm(mClient,this);
                mAddSubstituteForm.setTeacherLists(mTeachersList);
                mAddSubstituteForm.ShowDialog();
            }
        }

        public void clearAddDeleteSubstituteForm()
        {
            mAddSubstituteForm = null;
        }

        private void buttonGetLateClockIns_Click_1(object sender, EventArgs e)
        {
            if (!mTransactionInProgress)
            {
                if (checkDates())
                {
                    tabPanel.SelectTab(3);

                    SerializableGetLateClockInsListMessage message = new SerializableGetLateClockInsListMessage();
                    message.mInit = dateTimeInit.Value;
                    message.mEnd = dateTimeEnd.Value;

                    try
                    {
                        message.mDelay = Convert.ToInt32(textBoxDelay.Text);
                    }
                    catch (FormatException formatexception)
                    {
                        MessageBox.Show("Número de marca incorrecto, asegúrese de que no ha introducido caracteres o espacios en blanco.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        mTransactionInProgress = false;
                        return;
                    }

                    dataGridFichajes.SafeInvoke(d => d.Rows.Clear());

                    if (!mClient.SendMessageAsyncForResponse(message))
                    {
                        showConnectionErrorMessage();
                    }
                }
            }
        }

        public void addTeachersDataToGUI(SerializableTeacherData pteacherdata)
        {
            try { 
                if (pteacherdata.mClockins != null && pteacherdata.mClockins.Count > 0)
                {
                        int currentRowIndex = 0;
                        DateTime currentDateTime = pteacherdata.mClockins[0].day;

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
                        MessageBox.Show("Datos de fichajes obtenidos con éxito.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No hay datos para el periodo seleccionado.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error en la obtención de los datos de fichajes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                mTransactionInProgress = false;
            }
        }

        public void addSubstituteListToGUI(SerializableSubstitutionList presponse)
        {
           if (mAddSubstituteForm != null)
            {
                mAddSubstituteForm.setSubstituteList(presponse);
            }
        }

        private void añadirDíaNoLectivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mAddDeleteNoSchoolDay == null)
            {
                if (!mClient.SendMessageAsyncForResponse(new SerializableGetNoSchoolDaysMessage()))
                {
                    showConnectionErrorMessage();
                }
            }
        }

        public void addLateClockinsToGUI(SerializableLateClockInsList presponse)
        {
            try {
                if (presponse.mTeacherList != null && presponse.mTeacherList.Count > 0)
                {
                        int currentRowIndex = 0;

                        foreach (SerializableTeacherData entry in presponse.mTeacherList)
                        {
                            DateTime currentDateTime = entry.mClockins[0].day;

                            bool firstRow = true;

                            foreach (SerializableTeacherData.ClockInDataNode ciEntry in entry.mClockins)
                            {
                                string[] row = new string[6];

                                if (firstRow)
                                {
                                    firstRow = false;
                                    row[0] = entry.mName;
                                }
                                else
                                {
                                    row[0] = "";
                                }

                                if (DateTime.Compare(currentDateTime, ciEntry.day) != 0)
                                {
                                    currentDateTime = ciEntry.day;
                                    currentRowIndex++;
                                    dataGridFichajesTarde.SafeInvoke(d => d.Rows.Add(new string[6]));
                                }

                                row[1] = ciEntry.day.ToShortDateString();
                                row[2] = ciEntry.entranceHour.ToString();
                                if (ciEntry.isClockIn)
                                {
                                    row[3] = ciEntry.actualEntranceHour.ToString();
                                    row[4] = ciEntry.delayMinutes.ToString();
                                    row[5] = "Entrada";
                                }
                                else
                                {
                                    row[3] = "";
                                    row[4] = "";
                                    row[5] = "Salida";
                                }
                                dataGridFichajesTarde.SafeInvoke(d => d.Rows.Add(row));

                                if (ciEntry.isClockIn)
                                {
                                    dataGridFichajesTarde.SafeInvoke(d => d.Rows[currentRowIndex].Cells[4].Style.BackColor = Color.YellowGreen);
                                }
                                else
                                {
                                    dataGridFichajesTarde.SafeInvoke(d => d.Rows[currentRowIndex].Cells[4].Style.BackColor = Color.Yellow);
                                }

                                currentRowIndex++;
                            }
                        }
                        MessageBox.Show("Datos de fichajes tardíos obtenidos con éxito.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }                
                 else
                {
                        MessageBox.Show("No hay datos para el periodo seleccionado.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                    MessageBox.Show("Hubo un error en la obtención de los datos de fichajes tardíos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                mTransactionInProgress = false;
            }
        }

        public bool checkDates()
        {
            if (DateTime.Compare(dateTimeInit.Value, dateTimeEnd.Value) <= 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("La fecha de inicio no puede ser posterior a la de fin en el intervalo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void addNoSchoolDaysToGUI(SerializableNoSchoolDaysList presponse)
        {
            if (mAddDeleteNoSchoolDay == null)
            {
                mNoSchoolDaysList = presponse.mDaysList;
                showNoSchoolDaysForm();
            }
            else
            {
                mAddDeleteNoSchoolDay.setNoSchoolDaysList(presponse.mDaysList);
            }
        }

        private void buttonFaltasPorHoras_Click(object sender, EventArgs e)
        {
            if (!mTransactionInProgress)
            {
                mTransactionInProgress = true;
                if (checkDates())
                {
                    tabPanel.SelectTab(4);

                    SerializableGetMissesPerHourMessage message = new SerializableGetMissesPerHourMessage();
                    message.mInit = dateTimeInit.Value;
                    message.mEnd = dateTimeEnd.Value;                

                    dataGridFaltasPorHoras.SafeInvoke(d => d.Rows.Clear());

                    if (!mClient.SendMessageAsyncForResponse(message))
                    {
                        showConnectionErrorMessage();
                    }
                }
            }
        }       

        private void tabPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabPanel.SelectedIndex == 4)
            {
                tablePanelLeyenda.Visible = true;
            }
            else
            {
                tablePanelLeyenda.Visible = false;
            }
        }

        private void buttonGetFaltasCompletasIndiv_Click(object sender, EventArgs e)
        {
            if (checkDates() && comboBoxProfesor.SelectedItem != null)
            {
                if (!mTransactionInProgress)
                {
                    mTransactionInProgress = true;

                    dataGridFaltas.Rows.Clear();

                    tabPanel.SelectTab(1);
                    SerializableGetMissingTeachersMessage message = new SerializableGetMissingTeachersMessage();
                    message.init = dateTimeInit.Value;
                    message.end = dateTimeEnd.Value;
                    message.teacherId = Convert.ToInt32(((SerializableTeacherList.TeacherData)comboBoxProfesor.SelectedItem).mId);

                    if (!mClient.SendMessageAsyncForResponse(message))
                    {
                        showConnectionErrorMessage();
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un profesor para obtener los datos por favor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mTransactionInProgress = false;
            }           
        }

        private void buttonGetFaltasHorasIndiv_Click(object sender, EventArgs e)
        {
            if (checkDates() && comboBoxProfesor.SelectedItem != null)
            {
                if (!mTransactionInProgress)
                {
                    mTransactionInProgress = true;
                    if (checkDates())
                    {
                        tabPanel.SelectTab(4);

                        SerializableGetMissesPerHourMessage message = new SerializableGetMissesPerHourMessage();
                        message.mInit = dateTimeInit.Value;
                        message.mEnd = dateTimeEnd.Value;
                        message.teacherId = Convert.ToInt32(((SerializableTeacherList.TeacherData)comboBoxProfesor.SelectedItem).mId);

                        dataGridFaltasPorHoras.SafeInvoke(d => d.Rows.Clear());

                        if (!mClient.SendMessageAsyncForResponse(message))
                        {
                            showConnectionErrorMessage();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un profesor para obtener los datos por favor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mTransactionInProgress = false;
            }            
        }

        public void showNoSchoolDaysForm()
        {
            if (this.InvokeRequired)
            { // check if on UI thread
                this.BeginInvoke(new MethodInvoker(this.showNoSchoolDaysForm)); // we need to create the new form on the UI thread
                return;
            }

            mAddDeleteNoSchoolDay = new AddDeleteNoSchoolDay(mClient, this);
            mAddDeleteNoSchoolDay.setNoSchoolDaysList(mNoSchoolDaysList);
            mAddDeleteNoSchoolDay.ShowDialog();
        }

        public void addTeachersMissesPerHourListToGUI(SerializableTeachersMissesPerHourList presponse)
        {
            try { 
                if (presponse.mMissesList != null && presponse.mMissesList.Count > 0)
                {
                        int currentRowIndex = 0;

                        foreach (SerializableTeachersMissesPerHourList.MissesPerHourTeacherNode entry in presponse.mMissesList)
                        {
                            bool firstRow = true;

                            foreach (KeyValuePair<DateTime, byte[]> entryDateHours in entry.mDateHourValues)
                            {
                                string[] row = new string[11];

                                if (firstRow)
                                {
                                    firstRow = false;
                                    row[0] = entry.mTeacherName;
                                }
                                else
                                {
                                    row[0] = "";
                                }                       

                                row[1] = entryDateHours.Key.ToShortDateString();
                                dataGridFaltasPorHoras.SafeInvoke(d => d.Rows.Add(row));

                                for (int i=0;i< entryDateHours.Value.Length; i++)
                                {
                                    Color c = Color.Black;

                                    switch (entryDateHours.Value[i])
                                    {
                                        case SerializableTeachersMissesPerHourList.C_FALTA_COMPLETA:
                                            c = Color.Red;
                                            break;
                                        case SerializableTeachersMissesPerHourList.C_LLEGA_TARDE:
                                            c = Color.LightBlue;
                                            break;
                                        case SerializableTeachersMissesPerHourList.C_NO_NECESARIA_PRESENCIA:
                                            c = Color.Gray;
                                            break;
                                        case SerializableTeachersMissesPerHourList.C_PRESENTE:
                                            c = Color.Blue;
                                            break;
                                        case SerializableTeachersMissesPerHourList.C_SE_VA_ANTES:
                                            c = Color.Green;
                                            break;
                                        case SerializableTeachersMissesPerHourList.C_SE_VA_ANTES_Y_LLEGA_TARDE:
                                            c = Color.Indigo;
                                            break;
                                    }

                                    dataGridFaltasPorHoras.SafeInvoke(d => d.Rows[currentRowIndex].Cells[i+2].Style.BackColor = c);
                                }                    

                                currentRowIndex++;
                            }
                        }
                        MessageBox.Show("Datos de faltas por horas obtenidos con éxito.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No hay datos para el periodo seleccionado.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                    MessageBox.Show("Hubo un error en la obtención de los datos de faltas por hora.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                mTransactionInProgress = false;
            }
        }
    }
}
