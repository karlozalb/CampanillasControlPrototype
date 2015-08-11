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
            Assert.AreEqual(1, UtilsHelper.getCurrentDay(DayOfWeek.Monday));
            Assert.AreEqual(2, UtilsHelper.getCurrentDay(DayOfWeek.Tuesday));
            Assert.AreEqual(3, UtilsHelper.getCurrentDay(DayOfWeek.Wednesday));
            Assert.AreEqual(4, UtilsHelper.getCurrentDay(DayOfWeek.Thursday));
            Assert.AreEqual(5, UtilsHelper.getCurrentDay(DayOfWeek.Friday));
        }

        [TestMethod]
        public void getDateTimeByEntranceTimeTest()
        {
            Assert.AreEqual(Convert.ToDateTime("08:00 AM"), UtilsHelper.getDateTimeByEntranceTime(1));
            Assert.AreEqual(Convert.ToDateTime("09:00 AM"), UtilsHelper.getDateTimeByEntranceTime(2));
            Assert.AreEqual(Convert.ToDateTime("10:00 AM"), UtilsHelper.getDateTimeByEntranceTime(3));
            Assert.AreEqual(Convert.ToDateTime("11:30 AM"), UtilsHelper.getDateTimeByEntranceTime(4));
            Assert.AreEqual(Convert.ToDateTime("12:30 PM"), UtilsHelper.getDateTimeByEntranceTime(5));
            Assert.AreEqual(Convert.ToDateTime("13:30 PM"), UtilsHelper.getDateTimeByEntranceTime(6));
        }

        [TestMethod]
        public void isOnTimeTest()
        {
            Assert.IsTrue(UtilsHelper.isOnTime(Convert.ToDateTime("07:58 AM"),1));
            Assert.IsFalse(UtilsHelper.isOnTime(Convert.ToDateTime("08:02 AM"), 1));

            Assert.IsTrue(UtilsHelper.isOnTime(Convert.ToDateTime("08:58 AM"), 2));
            Assert.IsFalse(UtilsHelper.isOnTime(Convert.ToDateTime("11:21 AM"), 2));

            Assert.IsTrue(UtilsHelper.isOnTime(Convert.ToDateTime("09:58 AM"), 3));
            Assert.IsFalse(UtilsHelper.isOnTime(Convert.ToDateTime("12:02 PM"), 3));
        }
    }
}
