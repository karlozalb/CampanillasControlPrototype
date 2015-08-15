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

        private void MainWindow_Load(object sender, EventArgs e)
        {
            showCurrentTime();

            mMainController = new MainController(this);
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
        }

        private void showCurrentTime()
        {
            timeLabel.Text = "Hora actual: " + DateTime.Now.ToString("HH:mm:ss tt");
        }

        public void addItemToList(string pitem)
        {
            personalListBox.Items.Add(pitem);
        }

        public ListBox getPersonalListBox()
        {
            return personalListBox;
        }
        
    }
}
