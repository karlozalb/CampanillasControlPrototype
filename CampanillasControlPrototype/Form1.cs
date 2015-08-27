using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CampanillasControlPrototype
{
    public partial class MainWindow : Form
    {

        private int surroundingPaddingY,surroundingPaddingX;
        private int initialWidth, initialHeight;

        MainController mMainController;
        CommsController mCommsController;

        private void MainWindow_Load(object sender, EventArgs e)
        {
            showCurrentTime();

            mMainController = new MainController(this);
            mCommsController = new CommsController(mMainController);
        }

        private void dateTimer_Tick(object sender, EventArgs e)
        {
            showCurrentTime();
        }

        public MainWindow()
        {
            InitializeComponent();
           
            surroundingPaddingX = (this.Width - mainSplitContainer.Width) / 2;
            surroundingPaddingY = (this.Height - mainSplitContainer.Height) / 2;
        }       

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            mainSplitContainer.Width = this.Width - surroundingPaddingX * 2;
            mainSplitContainer.Height = this.Height - surroundingPaddingY * 2;

            int labelWidth = this.Width / 3;

            dayLabel.Width = labelWidth;
            hourLabel.Width = labelWidth;
            timeLabel.Width = labelWidth;
        }

        private void showCurrentTime()
        {
            timeLabel.Text = DateTime.Now.ToString("HH:mm:ss tt");
        }       

        public void addItemToList(string pitem)
        {
            missingPersonalListBox.Items.Add(pitem);
        }        

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mMainController != null) mMainController.stopTask();
            if (mCommsController != null) mCommsController.stopServer();
        }

        public ListBox getPersonalListBox()
        {
            return missingPersonalListBox;
        }

        public ListBox getPersonalAccummulatedAbsenceListBox()
        {
            return accummulatedAbsenceListBox;
        }

        public Label getDayLabel()
        {
            return dayLabel;
        }

        public Label getHourLabel()
        {
            return hourLabel;
        }

    }
}
