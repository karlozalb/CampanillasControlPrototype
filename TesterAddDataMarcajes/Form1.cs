using CampanillasControlPrototype;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesterAddDataMarcajes
{
    public partial class Form1 : Form
    {
        ParadoxDBController mPDXDB;

        List<CampanillasControlPrototype.PersonalNode> teachers;

        private int currentSelectedId = 0;

        public Form1()
        {
            InitializeComponent();

            mPDXDB = new ParadoxDBController();

            teachers = new List<CampanillasControlPrototype.PersonalNode>();

            mPDXDB.getAllTeachers(teachers);

            foreach (CampanillasControlPrototype.PersonalNode p in teachers)
            {
                comboBox1.Items.Add(p.getId());
            }
        }        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentSelectedId = Convert.ToInt32(comboBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mPDXDB.insertTESTClockIn(currentSelectedId);
        }
    }
}
