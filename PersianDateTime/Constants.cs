using System;
using System.Collections.Generic;
using System.Text;

namespace ZPersianDateTime
{
    internal static class Constants
    {
        public static readonly string[] cosntNameOfDays = { "شنبه", "یک شنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه" };

        public static readonly string[] cosntNameOfMonths = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };

        public static readonly short[] cosntFirsOfSeason = { 1, 1, 1, 4, 4, 4, 7, 7, 7, 10, 10, 10 };

        public static readonly List<HolidayDate> constofHoliday = new List<HolidayDate>{
                                new HolidayDate{ DayOfWeek=PersianDayOfWeek.Jome , Description =  "جمعه" },
                                new HolidayDate{ DayOfWeek=PersianDayOfWeek.None, Day=1 , Month= 1 , Description="نوروز"},
                                new HolidayDate{ DayOfWeek=PersianDayOfWeek.None, Day=2 , Month= 1 , Description="نوروز"},
                                new HolidayDate{ DayOfWeek=PersianDayOfWeek.None, Day=3 , Month= 1 , Description="نوروز"},
                                new HolidayDate{ DayOfWeek=PersianDayOfWeek.None, Day=4 , Month= 1 , Description="نوروز"},
                                new HolidayDate{ DayOfWeek=PersianDayOfWeek.None, Day=13 , Month= 1 , Description="روز طبیعت"},
                                new HolidayDate{ DayOfWeek=PersianDayOfWeek.None, Day=30 , Month= 12 , Description="ملی شدن صعنت نفت"},
                                new HolidayDate{ DayOfWeek=PersianDayOfWeek.None, Day=29 , Month= 12 , Description="نوروز"} ,

                                new HolidayDate{ DayOfWeek=PersianDayOfWeek.None, Day=12 , Month= 1 , Description="روز جمهموری اسلامی"},
                                new HolidayDate{ DayOfWeek=PersianDayOfWeek.None, Day=14 , Month= 3 , Description="رحلت آیت الله خمینی"},
                                new HolidayDate{ DayOfWeek=PersianDayOfWeek.None, Day=15 , Month= 3 , Description="قیام ۱۵ خرداد"} ,
                                new HolidayDate{ DayOfWeek=PersianDayOfWeek.None, Day=22 , Month= 11 , Description="پیروزی انقلاب ۵۷"} 
                       };


    }
}
