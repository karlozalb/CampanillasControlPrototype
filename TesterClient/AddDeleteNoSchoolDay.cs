using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommsModule;
using SimpleThreadSafeCall;

namespace TesterClient
{
    public partial class AddDeleteNoSchoolDay : Form
    {
        private Client mClient;
        private ReportGenerator mReportGenerator;

        public AddDeleteNoSchoolDay()
        {
            InitializeComponent();
        }

        public AddDeleteNoSchoolDay(Client pclient, ReportGenerator preportgenerator)
        {
            mClient = pclient;
            mReportGenerator = preportgenerator;

            InitializeComponent();
        }

        public void setNoSchoolDaysList(List<DateTime> plist)
        {
            listBoxNoSchoolDays.SafeInvoke(d => d.Items.Clear());
            monthCalendar.SafeInvoke(d => d.RemoveAllBoldedDates());

            if (plist != null && plist.Count > 0)
            {

                foreach (DateTime date in plist)
                {
                    listBoxNoSchoolDays.SafeInvoke(d => d.Items.Add(date));
                    monthCalendar.SafeInvoke(d => d.AddBoldedDate(date));
                    monthCalendar.SafeInvoke(d => d.UpdateBoldedDates());
                }
            }

            monthCalendar.SafeInvoke(d => d.Invalidate());
        }

        private void monthCalendar_MouseDown(object sender, MouseEventArgs e)
        {
            MonthCalendar.HitTestInfo info = monthCalendar.HitTest(e.Location);
            if (info.HitArea == MonthCalendar.HitArea.Date)
            {
                if (monthCalendar.BoldedDates.Contains(info.Time))
                    monthCalendar.RemoveBoldedDate(info.Time);
                else
                    monthCalendar.AddBoldedDate(info.Time);
                monthCalendar.UpdateBoldedDates();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SerializableAddDayMessage dayMessage = new SerializableAddDayMessage();

            foreach (DateTime day in monthCalendar.BoldedDates)
            {
                dayMessage.mDays.Add(day);
            }

            mClient.SendMessageAsyncForResponse(dayMessage);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxNoSchoolDays.SelectedItem != null)
            {
                SerializableDeleteDayMessage deleteDayMessage = new SerializableDeleteDayMessage();

                foreach (Object item in listBoxNoSchoolDays.SelectedItems)
                {
                    deleteDayMessage.mDays.Add((DateTime)item);
                }

                mClient.SendMessageAsyncForResponse(deleteDayMessage);
            }
            else
            {
                MessageBox.Show("Seleccione un día a borrar de la lista derecha por favor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            mReportGenerator.clearAddDeleteNoSchoolDaysForm();
            this.Close();
            this.Dispose();
        }
    }
}
