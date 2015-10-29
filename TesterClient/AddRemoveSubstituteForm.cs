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
    public partial class AddRemoveSubstituteForm : Form
    {
        Client mClient;
        ReportGenerator mMainController;

        public AddRemoveSubstituteForm(Client pclient,ReportGenerator pmaincontroller)
        {
            mClient = pclient;
            mMainController = pmaincontroller;            

            InitializeComponent();
        }

        public void setTeacherLists(SerializableTeacherList pteacherslist)
        {
            foreach (SerializableTeacherList.TeacherData entry in pteacherslist.mTeachers)
            {
                comboBoxSubstitute.SafeInvoke(d => d.Items.Add(entry));
                comboBoxMissing.SafeInvoke(d => d.Items.Add(entry));
            }
            mClient.SendMessageAsyncForResponse(new SerializableGetSubstituteListMessage());
        }

        public void setSubstituteList(SerializableSubstitutionList plist)
        {
            listBoxSubstitute.SafeInvoke(d => d.Items.Clear());
            foreach (SerializableSubstituteTeacherNode s in plist.mSubstitutesNodes)
            {
                listBoxSubstitute.SafeInvoke(d => d.Items.Add(s));
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            mMainController.clearAddDeleteSubstituteForm();
            this.Close();
            this.Dispose();
        }

        private void addSubstituteButton_Click(object sender, EventArgs e)
        {
            SerializableAddSubstituteTeacherMessage message = new SerializableAddSubstituteTeacherMessage();

            message.mMissingTeacherId = ((SerializableTeacherList.TeacherData)comboBoxMissing.SelectedItem).mId;
            message.mSubstituteTeacherId = ((SerializableTeacherList.TeacherData)comboBoxSubstitute.SelectedItem).mId;

            mClient.SendMessageAsyncForResponse(message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBoxSubstitute.SelectedItem != null)
            {
                SerializableDeleteSubstituteMessage message = new SerializableDeleteSubstituteMessage();

                SerializableSubstituteTeacherNode subsNode = (SerializableSubstituteTeacherNode)listBoxSubstitute.SelectedItem;

                message.mMissingId = subsNode.mMissingId;
                message.mSubstituteId = subsNode.mSubstituteId;

                mClient.SendMessageAsyncForResponse(message);
            }
            else
            {
                MessageBox.Show("Seleccione una pareja de la lista por favor.", "Error de borrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
