using System.Collections.Generic;

namespace ZPersianDateTime
{
    internal static class Constants
    {
        public static readonly string[] NameOfDays =
            { "شنبه", "یک شنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه" };

        public static readonly string[] NameOfMonths =
            { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };

        public static readonly short[] FirsOfSeason = { 1, 1, 1, 4, 4, 4, 7, 7, 7, 10, 10, 10 };

        public static readonly List<HolidayDate> Holiday = new()
        {
            HolidayDate.Create(1, 1, "نوروز"),
            HolidayDate.Create(1, 3, "نوروز"),
            HolidayDate.Create(1, 4, "نوروز"),
            HolidayDate.Create(1, 5, "نوروز"),
            HolidayDate.Create(1, 13, "روز طبیعت"),
            HolidayDate.Create(1, 12, "روز جمهموری اسلامی"),
            HolidayDate.Create(3, 14, "رحلت آیت الله خمینی"),
            HolidayDate.Create(3, 15, "قیام ۱۵ خرداد"),
            HolidayDate.Create(11, 22, "پیروزی انقلاب ۵۷"),
            HolidayDate.Create(12, 29, "ملی شدن صعنت نفت"),
            HolidayDate.Create(12, 30, "نوروز")
        };
    }
}