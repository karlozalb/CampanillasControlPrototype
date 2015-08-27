using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TesterClient
{
    public partial class Login_Dialog_Form1 : Form
    {
        public Login_Dialog_Form1()
        {
            InitializeComponent();
        }
        private bool ValidateUsername()
        {
            //TODO: add code to validate User Name here.
            return true;
        }
        private bool ValidatePassword()
        {
            if (!ValidateUsername())
            {
                MessageBox.Show("Wrong Username", "Invalid Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                if (false)
                {
                    MessageBox.Show("Wrong Password", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    //TODO: add code to validate password.
                    return true;
                }
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidatePassword())
            {
                txtUserName.Clear();
                txtPassword.Clear();
                return;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Clear();
            txtPassword.Clear();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
