using System;
using Xunit;

namespace ZPersianDateTime.Test
{
    public class ZPersianDateTimeTest
    {
        [Fact]
        public void AssignDateToPersianDate()
        {

            /// 2019-01-13 ~ 1397-10-23
            PersianDate persianDate = new DateTime(2019, 01, 13);
            Assert.Equal(1397, persianDate.Year);
            Assert.Equal(10, persianDate.Month);
            Assert.Equal(23, persianDate.Day);
        }

        [Fact]
        public void AssignPersianDateToDate()
        {
            /// 2019-01-13 ~ 1397-10-23
            DateTime date = (DateTime)new PersianDate(1397,10,23);
            Assert.Equal(2019, date.Year);
            Assert.Equal(01, date.Month);
            Assert.Equal(13, date.Day);
        }


        [Fact]
        public void BeginOfMonthTest()
        {
            ///  1397-10-23 ~ 1397-10-01
            var date = new PersianDate(1397, 10, 23);
            var bom = date.BeginOfMonth();

            Assert.Equal(1397,bom.Year);
            Assert.Equal(10,bom.Month);
            Assert.Equal(1,bom.Day);
        }

        [Fact]
        public void EndOfMonthTest()
        {
            ///  1397-10-23 ~ 1397-10-30
            var date = new PersianDate(1397, 10, 23);
            var bom = date.EndOfMonth();

            Assert.Equal(1397, bom.Year);
            Assert.Equal(10, bom.Month);
            Assert.Equal(30, bom.Day);
        }


        [Fact]
        public void BeginOfWeekTest()
        {
            ///  1397-10-23 ~ 1397-10-01
            var date = new PersianDate(1397, 10, 23);
            var bom = date.BeginOfWeek();

            Assert.Equal(1397, bom.Year);
            Assert.Equal(10, bom.Month);
            Assert.Equal(22, bom.Day);
        }

        [Fact]
        public void EndOfWeekTest()
        {
            ///  1397-10-23 ~ 1397-10-30
            var date = new PersianDate(1397, 10, 23);
            var bom = date.EndOfWeek();

            Assert.Equal(1397, bom.Year);
            Assert.Equal(10, bom.Month);
            Assert.Equal(28, bom.Day);
        }


        [Fact]
        public void BeginOfYearTest()
        {
            ///  1397-10-23 ~ 1397-10-01
            var date = new PersianDate(1397, 1, 1);
            var bom = date.BeginOfYear();

            Assert.Equal(1397, bom.Year);
            Assert.Equal(1, bom.Month);
            Assert.Equal(1, bom.Day);
        }

        [Fact]
        public void EndOfYearTest()
        {
            ///  1397-10-23 ~ 1397-10-30
            var date = new PersianDate(1397, 10, 23);
            var bom = date.EndOfYear();

            Assert.Equal(1397, bom.Year);
            Assert.Equal(12, bom.Month);
            Assert.Equal(29, bom.Day);
        }



        [Fact]
        public void EqualAndGotoWeekTest()
        {
            var date = new PersianDate(1397, 1, 1).GotoWeeks(2);
            var date2 = new PersianDate(1397, 1, 15);

            Assert.True(date.Equals(date2));
        }

        [Fact]
        public void EqualAndGotoDayTest()
        {
            var date = new PersianDate(1397, 1, 1).GotoDays(10);
            var date2 = new PersianDate(1397, 1, 11);

            Assert.True(date.Equals(date2));
        }

        [Fact]
        public void EqualAndGotoMonthTest()
        {
            var date = new PersianDate(1397, 1, 1).GotoMonths(1);
            var date2 = new PersianDate(1397, 2, 1);

            Assert.True(date.Equals(date2));
        }

        [Fact]
        public void EqualAndGotoYearTest()
        {
            var date = new PersianDate(1397, 1, 1).GotoYears(1);
            var date2 = new PersianDate(1398, 1, 1);

            Assert.True(date.Equals(date2));
        }


        [Fact]
        public void IsHolidayTest()
        {
            var date = new PersianDate(1397, 11, 22);
            
            Assert.True(date.IsHoliday);
        }

    }
}
