using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CampanillasControlPrototype;

namespace CampanillasTestCases
{
    [TestClass]
    public class DataBaseHelperTestCases
    {
        [TestMethod]
        public void getCurrentDayTest()
        {
            DataBaseDataHelper dataHelper = new DataBaseDataHelper();

            Assert.AreEqual(1,dataHelper.getCurrentDay(DayOfWeek.Monday));
            Assert.AreEqual(2, dataHelper.getCurrentDay(DayOfWeek.Tuesday));
            Assert.AreEqual(3, dataHelper.getCurrentDay(DayOfWeek.Wednesday));
            Assert.AreEqual(4, dataHelper.getCurrentDay(DayOfWeek.Thursday));
            Assert.AreEqual(5, dataHelper.getCurrentDay(DayOfWeek.Friday));
        }

        [TestMethod]
        public void getDateTimeByEntranceTimeTest()
        {
            DataBaseDataHelper dataHelper = new DataBaseDataHelper();

            Assert.AreEqual(Convert.ToDateTime("08:00 AM"), dataHelper.getDateTimeByEntranceTime(1));
            Assert.AreEqual(Convert.ToDateTime("09:00 AM"), dataHelper.getDateTimeByEntranceTime(2));
            Assert.AreEqual(Convert.ToDateTime("10:00 AM"), dataHelper.getDateTimeByEntranceTime(3));
            Assert.AreEqual(Convert.ToDateTime("11:30 AM"), dataHelper.getDateTimeByEntranceTime(4));
            Assert.AreEqual(Convert.ToDateTime("12:30 PM"), dataHelper.getDateTimeByEntranceTime(5));
            Assert.AreEqual(Convert.ToDateTime("13:30 PM"), dataHelper.getDateTimeByEntranceTime(6));
        }

        [TestMethod]
        public void isOnTimeTest()
        {
            DataBaseDataHelper dataHelper = new DataBaseDataHelper();

            Assert.IsTrue(dataHelper.isOnTime(Convert.ToDateTime("07:58 AM"),1));
            Assert.IsFalse(dataHelper.isOnTime(Convert.ToDateTime("08:02 AM"), 1));

            Assert.IsTrue(dataHelper.isOnTime(Convert.ToDateTime("08:58 AM"), 2));
            Assert.IsFalse(dataHelper.isOnTime(Convert.ToDateTime("11:21 AM"), 2));

            Assert.IsTrue(dataHelper.isOnTime(Convert.ToDateTime("09:58 AM"), 3));
            Assert.IsFalse(dataHelper.isOnTime(Convert.ToDateTime("12:02 PM"), 3));

        }

    }
}
