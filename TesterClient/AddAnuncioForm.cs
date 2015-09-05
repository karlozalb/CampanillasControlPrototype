using CommsModule;
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
    public partial class AddAnuncioForm : Form
    {
        Client mClient;

        public AddAnuncioForm(Client pclient)
        {
            mClient = pclient;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SerializableAd newAd = new SerializableAd();

            newAd.mText = adText.Text;
            newAd.mDate = DateTime.Now.ToShortDateString();

            mClient.SendMessageAsyncForResponse(newAd);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
