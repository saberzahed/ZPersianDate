using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ZPersianDateTime.Test
{
    [TestClass]
    public class ZPersianDateTimeTest
    {
        [TestMethod]
        public void AssignDateToPersianDate()
        {
            /// 2019-01-13 ~ 1397-10-23
            PersianDate persianDate = new DateTime(2019, 01, 13);
            Assert.AreEqual(persianDate.Year, 1397);
            Assert.AreEqual(persianDate.Month, 10);
            Assert.AreEqual(persianDate.Day, 23);
        }

        [TestMethod]
        public void AssignPersianDateToDate()
        {
            /// 2019-01-13 ~ 1397-10-23
            DateTime date = new PersianDate(1397,10,23);
            Assert.AreEqual(date.Year, 2019);
            Assert.AreEqual(date.Month, 01);
            Assert.AreEqual(date.Day, 13);
        }


        [TestMethod]
        public void BeginOfMonthTest()
        {
            ///  1397-10-23 ~ 1397-10-01
            var date = new PersianDate(1397, 10, 23);
            var bom = date.BeginOfMonth();

            Assert.AreEqual(bom.Year,1397);
            Assert.AreEqual(bom.Month,10);
            Assert.AreEqual(bom.Day,1);
        }

        [TestMethod]
        public void EndOfMonthTest()
        {
            ///  1397-10-23 ~ 1397-10-30
            var date = new PersianDate(1397, 10, 23);
            var bom = date.EndOfMonth();

            Assert.AreEqual(bom.Year, 1397);
            Assert.AreEqual(bom.Month, 10);
            Assert.AreEqual(bom.Day, 30);
        }


        [TestMethod]
        public void BeginOfWeekTest()
        {
            ///  1397-10-23 ~ 1397-10-01
            var date = new PersianDate(1397, 10, 23);
            var bom = date.BeginOfWeek();

            Assert.AreEqual(bom.Year, 1397);
            Assert.AreEqual(bom.Month, 10);
            Assert.AreEqual(bom.Day, 22);
        }

        [TestMethod]
        public void EndOfWeekTest()
        {
            ///  1397-10-23 ~ 1397-10-30
            var date = new PersianDate(1397, 10, 23);
            var bom = date.EndOfWeek();

            Assert.AreEqual(bom.Year, 1397);
            Assert.AreEqual(bom.Month, 10);
            Assert.AreEqual(bom.Day, 28);
        }


        [TestMethod]
        public void BeginOfYearTest()
        {
            ///  1397-10-23 ~ 1397-10-01
            var date = new PersianDate(1397, 1, 1);
            var bom = date.BeginOfYear();

            Assert.AreEqual(bom.Year, 1397);
            Assert.AreEqual(bom.Month, 1);
            Assert.AreEqual(bom.Day, 1);
        }

        [TestMethod]
        public void EndOfYearTest()
        {
            ///  1397-10-23 ~ 1397-10-30
            var date = new PersianDate(1397, 10, 23);
            var bom = date.EndOfYear();

            Assert.AreEqual(bom.Year, 1397);
            Assert.AreEqual(bom.Month, 12);
            Assert.AreEqual(bom.Day, 29);
        }



        [TestMethod]
        public void EqualAndGotoWeekTest()
        {
            var date = new PersianDate(1397, 1, 1).GotoWeeks(2);
            var date2 = new PersianDate(1397, 1, 15);

            Assert.IsTrue(date.Equals(date2));
        }

        [TestMethod]
        public void EqualAndGotoDayTest()
        {
            var date = new PersianDate(1397, 1, 1).GotoDays(10);
            var date2 = new PersianDate(1397, 1, 11);

            Assert.IsTrue(date.Equals(date2));
        }

        [TestMethod]
        public void EqualAndGotoMonthTest()
        {
            var date = new PersianDate(1397, 1, 1).GotoMonths(1);
            var date2 = new PersianDate(1397, 2, 1);

            Assert.IsTrue(date.Equals(date2));
        }

        [TestMethod]
        public void EqualAndGotoYearTest()
        {
            var date = new PersianDate(1397, 1, 1).GotoYears(1);
            var date2 = new PersianDate(1398, 1, 1);

            Assert.IsTrue(date.Equals(date2));
        }


        [TestMethod]
        public void IsHolidayTest()
        {
            var date = new PersianDate(1397, 11, 22);
            
            Assert.IsTrue(date.IsHoliday);
        }

    }
}
