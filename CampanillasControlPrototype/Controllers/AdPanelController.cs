using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommsModule;
using System.Timers;
using System.Configuration;
using SimpleThreadSafeCall;
using System.Diagnostics;

namespace CampanillasControlPrototype
{
    
    class AdPanelController
    {
        private DataBaseController mDBController;
        private MainWindow mMainWindow;

        List<CurrentAdNode> mCurrentAdNodeList;

        long AD_TIME;
        int mCurrentAdIndex;

        Timer mTaskTimer;

        public AdPanelController(MainWindow pmainwindow,DataBaseController pdatabasecontroller)
        {
            AD_TIME = Convert.ToInt64(ConfigurationManager.AppSettings["adchangetime"]);

            mDBController = pdatabasecontroller;
            mMainWindow = pmainwindow;
            mCurrentAdNodeList = new List<CurrentAdNode>();


            mTaskTimer = new Timer(AD_TIME);

            mTaskTimer.Elapsed += new ElapsedEventHandler(updateDisplayedAd);
            mTaskTimer.Enabled = true; // Enable it    

            mCurrentAdIndex = 0;

            updateAds();

        }

        public void updateAds()
        {
            //We get the ads from the database
            CommsModule.SerializableAdList adlist = mDBController.getAdList();

            //If both lists have the same information, we do nothing
            bool updateNeeded = false;

            if (adlist.mAdList.Count == mCurrentAdNodeList.Count)
            {
                for (int i = 0; i < adlist.mAdList.Count; i++)
                {
                    for (int j = 0; j < mCurrentAdNodeList.Count; j++)
                    {
                        bool exists = false;
                        if (adlist.mAdList[i].mId == mCurrentAdNodeList[j].ID)  //The ad is present in both lists, we do nothing.
                        {
                            exists = true;
                            break;
                        }
                        if (!exists)
                        {
                            updateNeeded = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                updateNeeded = true;
            }

            if (updateNeeded)
            {
                mTaskTimer.Enabled = false;
                resetAdList(adlist,mCurrentAdNodeList);
                mTaskTimer.Enabled = true; 
            }
            
        }

        private void resetAdList(SerializableAdList padlist, List<CurrentAdNode> pcurrentlist)
        {
            pcurrentlist.Clear();

            foreach (SerializableAd entry in padlist.mAdList)
            {
                pcurrentlist.Add(new CurrentAdNode(entry.mId, entry.mText,entry.mDate));
            }
        }

        private void updateDisplayedAd(object sender, ElapsedEventArgs e)
        {
            if (++mCurrentAdIndex > (mCurrentAdNodeList.Count - 1)) mCurrentAdIndex = 0;

            if (mCurrentAdNodeList.Count > 0)
            {
                Debug.WriteLine("Actualizando anuncio a indice: "+ mCurrentAdIndex);
                mMainWindow.getAdLabel().SafeInvoke(d => d.Text = mCurrentAdNodeList[mCurrentAdIndex].TEXT);
                mMainWindow.getAdInfoLabel().SafeInvoke(d => d.Text = (mCurrentAdIndex + 1) + " de " + mCurrentAdNodeList.Count + " - " + mCurrentAdNodeList[mCurrentAdIndex].DATE);
            }
        }

        public void stopTask()
        {
            mTaskTimer.Stop();
            mTaskTimer.Enabled = false;
            mTaskTimer.Dispose();
        }
    }

    //Class to store data for ADs currently showed on screen.
    public class CurrentAdNode
    {
        public int ID;
        public string TEXT;
        public string DATE;

        public CurrentAdNode(int pid,string ptext,string pdate)
        {
            ID = pid;
            TEXT = ptext;
            DATE = pdate;
        }

        public override string ToString()
        {
           return TEXT;            
        }
    }
}
