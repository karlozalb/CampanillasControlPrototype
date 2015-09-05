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
    public partial class DeleteAnuncioForm : Form
    {

        private SerializableAdList mAdList;
        private ReportGenerator mReportGenerator;

        public DeleteAnuncioForm(ReportGenerator prepgen)
        {
            mReportGenerator = prepgen;

            InitializeComponent();                    
        }

        public void setAdList(SerializableAdList plist)
        {
            foreach (SerializableAd ad in plist.mAdList)
            {
                adListComboBox.SafeInvoke(d => d.Items.Add(ad));
            }
        }

        private void adListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            adText.Text = ((SerializableAd)adListComboBox.SelectedItem).mText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (adListComboBox.SelectedItem == null)
            {
                mReportGenerator.showMessage("No hay ningún anuncio seleccionado.");
            }
            else
            {
                mReportGenerator.deleteAd(((SerializableAd)adListComboBox.SelectedItem).mId);
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            mReportGenerator.closeDeleteForm();
        }
    }
}
