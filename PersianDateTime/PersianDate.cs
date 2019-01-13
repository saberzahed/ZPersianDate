using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ZPersianDateTime
{
    public class PersianDate
    {
        private static PersianCalendar _calendar = new PersianCalendar();
        private PersianDate()
        {

        }

        /// <summary>
        /// Create instance of Persian Date 
        /// </summary>
        /// <param name="year">Year of Jalali, between 1 to 9378</param>
        /// <param name="month">Month , Between 1 to 12 </param>
        /// <param name="day">Day , Between 1 to 31 ></param>
        public PersianDate(int year, int month, int day)
        {
            if (year >= 1 && year <= 9378 && month >= 1 && Month <= 12 && day >= 1 && day <= 31)
            {
                var date = _calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
                _init(date, this);
            }
            else
                throw new NotSupportedException("Arguments are invalid");
        }



        private DateTime _date;

        /// <summary>
        ///  Gets the Jalali's year component of the date represented by this instance. 
        /// </summary>
        public int Year { get; private set; }


        /// <summary>
        ///  Gets the month component of the date represented by this instance. 
        /// </summary>
        public int Month { get; private set; }


        /// <summary>
        ///   Gets the day of the month represented by this instance.
        /// </summary>
        public int Day { get; private set; }

        /// <summary>
        ///   Gets the day of the week represented by this instance.
        /// </summary>
        public PersianDayOfWeek DayOfWeek { get; set; }

        /// <summary>
        ///   Gets the season represented by this instance.
        /// </summary>
        public Season Season => _getSeason();

        /// <summary>
        ///   Gets the day of the year represented by this instance.
        /// </summary>
        public int DayOfYear { get; private set; }

        /// <summary>
        ///   this day is holiday , true if the day is holiday; otherwise, false.
        /// </summary>
        public bool IsHoliday { get; private set; }

        /// <summary>
        ///   Gets the description of the holiday day represented by this instance.
        /// </summary>
        public string HolidayDescription { get; private set; }



        /// <summary>
        ///   Gets the current date.
        /// </summary>
        public static PersianDate Today => DateTime.Today;

        /// <summary>
        ///  Represents the largest possible value of Persian DateTime. This field is read-only.
        /// </summary>
        public static readonly PersianDate MaxValue = DateTime.MaxValue;

        /// <summary>
        /// Represents the smallest possible value of Persian DateTime. This field is read-only.
        /// </summary>
        public static readonly PersianDate MinValue = DateTime.MinValue.AddYears(622);

        /// <summary>
        ///  Year is a leap year , true if year is a leap year; otherwise, false.
        /// </summary>
        public bool IsLeapYear { get; private set; }

        /// <summary>
        ///   Gets  the day's name of the month represented by this instance.
        /// </summary>
        public string NameOfDays => Constants.cosntNameOfDays[(int)DayOfWeek];

        /// <summary>
        ///   Gets the month'name of the date represented by this instance.
        /// </summary>
        public string NameOfMonth => Constants.cosntNameOfMonths[Month - 1];


        /// <summary>
        ///   Get half of the year represented by this instance.
        /// </summary>
        public HalfOfYear HalfOfSeason
        {
            get
            {

                switch (Season)
                {
                    case Season.Spring:
                    case Season.Summer:
                        return HalfOfYear.First;
                    case Season.Autumn:
                    case Season.Winter:
                        return HalfOfYear.Second;
                    default: return HalfOfYear.First;

                }

            }
        }

        /// <summary>
        ///    Begin of Season of this instance
        /// </summary>
        /// <returns>Return new instance of Persian Date</returns>
        public PersianDate BeginOfSeason() => Copy().Change(day: 1, month: Constants.cosntFirsOfSeason[Month]);
        /// <summary>
        ///    End of Season of this instance
        /// </summary>
        /// <returns>Return new instance of Persian Date</returns>
        public PersianDate EndOfSeason() => _goto(BeginOfSeason(), -1, 3, 0);
        /// <summary>
        ///    Begin of week of this instance
        /// </summary>
        /// <returns>Return new instance of Persian Date</returns>
        public PersianDate BeginOfWeek() => _goto(this, PersianDayOfWeek.Shanbe - DayOfWeek, 0, 0);
        /// <summary>
        ///    Begin of week of this instance
        /// </summary>
        /// <returns>Return new instance of Persian Date</returns>
        public PersianDate EndOfWeek() => _goto(this, PersianDayOfWeek.Jome - DayOfWeek, 0, 0);
        /// <summary>
        ///    Begin of Month of this instance
        /// </summary>
        /// <returns>Return new instance of Persian Date</returns>
        public PersianDate BeginOfMonth() => Copy().Change(day: 1);
        /// <summary>
        ///    Begin of end of this instance
        /// </summary>
        /// <returns>Return new instance of Persian Date</returns>
        public PersianDate EndOfMonth() => _goto(BeginOfMonth(), -1, 1, 0);
        /// <summary>
        ///    Begin of year of this instance
        /// </summary>
        /// <returns>Return new instance of Persian Date</returns>
        public PersianDate BeginOfYear() => Copy().Change(day: 1, month: 1);
        /// <summary>
        ///    Begin of year of this instance
        /// </summary>
        /// <returns>Return new instance of Persian Date</returns>
        public PersianDate EndOfYear() => _goto(BeginOfYear(), -1, 0, 1);


        /// <summary>
        ///  Add/Subtract week to get new PersianDate
        /// </summary>
        /// <param name="week">Week can negative and posstive</param>
        /// <returns>new PersianDate</returns>
        public PersianDate GotoWeeks(int week = 1) => _goto(this, (week * 7), 0, 0);
        /// <summary>
        ///  Add/Subtract day to get new PersianDate
        /// </summary>
        /// <param name="day">day can negative and posstive</param>
        /// <returns>new PersianDate</returns>
        public PersianDate GotoDays(int day = 1) => _goto(this, (int)(day), 0, 0);

        /// <summary>
        ///  Add/Subtract months to get new PersianDate
        /// </summary>
        /// <param name="months">months can negative and posstive</param>
        /// <returns>new PersianDate</returns>
        public PersianDate GotoMonths(int months = 1) => _goto(this, 0, (int)months, 0);

        /// <summary>
        ///  Add/Subtract years to get new PersianDate
        /// </summary>
        /// <param name="years">years can negative and posstive</param>
        /// <returns>new PersianDate</returns>
        public PersianDate GotoYears(int years = 1) => _goto(this, 0, 0, (int)years);


        /// <summary>
        ///  Change PersianDate via new value without create new instance 
        /// </summary>
        /// <param name="year">year can be null</param>
        /// <param name="month">year can be null</param>
        /// <param name="day">year can be null</param>
        /// <returns>this instance</returns>
        public PersianDate Change(int? year = null, int? month = null, int? day = null) => _init(new PersianDate((year ?? Year), (month ?? Month), (day ?? Day)), this);

        /// <summary>
        /// Clone of this instance 
        /// </summary>
        /// <returns>new PersianDate with this instance's values</returns>
        public PersianDate Copy() => _init(this, new PersianDate());



        /// <summary>
        /// Converts the value of the current PersianDate object to its equivalent string
        //     representation using the specified format.
        /// </summary>
        /// <returns>The result like as yyyy/mm/dd format </returns>
        public override string ToString() => ToString("yyyy/mm/dd");

        /// <summary>
        /// Show formatted string of the instance 
        /// </summary>
        /// <returns>The result like as yyyymmdd format </returns>
        public string ToShortString() => ToString("yyyymmdd");

        /// <summary>
        /// Converts the value of the current PersianDate object to its equivalent string
        //     representation using the specified format.
        /// </summary>
        /// <returns>The result like as yyyy MM - DD dd format </returns>
        public string ToLongString() => ToString("yyyy MM - DD dd");


        /// <summary>
        /// Converts the value of the current PersianDate object to its equivalent string  representation using the specified format.
        /// </summary>
        /// <param name="format"> A  custom date and time format string
        ///     yyyy   => Year              .   1397
        ///     yy     => Year              .     97
        ///     MM     => Name Of Month     .    تیر  
        ///     mm     => Month             .     04
        ///     m      => Month             .      4
        ///     DD     => Name Of Days      .   شنبه
        ///     dd     => Day               .     08
        ///     d      => Day               .      8
        /// </param>
        /// <returns> A string representation of value of the current PersianDate object as specified by format. </returns>
        public string ToString(string format)
        {
            return format.Replace("yyyy", Year.ToString())
                   .Replace("yy", Year.ToString().Substring(2))
                   .Replace("MM", NameOfMonth)
                   .Replace("mm", Month.ToString("00"))
                   .Replace("m", Month.ToString())
                   .Replace("DD", NameOfDays)
                   .Replace("dd", Day.ToString("00"))
                   .Replace("d", Day.ToString());
        }

        /// <summary>
        ///     Converts the specified string representation of a date  to its PersianDate 
        ///     equivalent and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="date">A string containing a date  to convert.</param>
        /// <param name="sep">A char containining a seprater of date  to convert. if date has a sepration</param>
        /// <returns> An object that is equivalent to the date  contained in s</returns>
        public static PersianDate Parse(string date, char sep = Char.MinValue)
        {

            try
            {
                var year = 0;
                var month = 0;
                var day = 0;

                if (sep == char.MinValue)
                {
                    var dt = date.Replace("-", "")
                                 .Replace("/", "");

                    if (!int.TryParse(dt, out var cast))
                        throw new FormatException($"{date} is invalid format  for parse.");

                    if (!(int.TryParse(dt.Substring(0, 4), out year) &&
                    int.TryParse(dt.Substring(4, 2), out month) &&
                    int.TryParse(dt.Substring(6, 2), out day))) throw new FormatException($"{date} is invalid format  for parse.");
                }
                else
                {
                    var splitted = date.Split(sep);
                    if (splitted.Length != 3)
                        throw new FormatException($"{date} is invalid format  for parse.");

                    if (!(int.TryParse(splitted[0], out year) &&
                         int.TryParse(splitted[1], out month) &&
                         int.TryParse(splitted[2], out day))) throw new FormatException($"{date} is invalid format  for parse.");

                }
                return new PersianDate(year, month, day);
            }
            catch (Exception)
            {

                throw new FormatException($"{date} is invalid format  for parse.");
            }


        }

        /// <summary>
        ///     Converts the specified string representation of a date  to its PersianDate 
        ///     equivalent and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="date">A string containing a date  to convert.</param>
        /// <param name="output">
        ///    When this method returns, contains the PersianDate value equivalent to the
        ///     date and time contained in s, if the conversion succeeded, or null
        ///     if the conversion failed. This parameter is passed uninitialized
        /// </param>
        /// <param name="sep">A char containining a seprater of date  to convert. if date has a sepration</param>
        /// <returns>true if the s parameter was converted successfully; otherwise, false.</returns>

        public static bool TryParse(string date, out PersianDate output, char sep = char.MinValue)
        {
            try
            {
                output = Parse(date, sep);
                return true;
            }
            catch
            {
                output = null;
                return false;
            }

        }

        /// <summary>
        ///    Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">PersianDate obj </param>
        /// <returns>true if value is an instance of PersianDate and equals the value of this instance; otherwise, false.</returns>
        public bool Equals(PersianDate obj)
        {
            return Day == obj.Day &&
                    Month == obj.Month &&
                    Year== obj.Year;
        }



        public static implicit operator PersianDate(DateTime date) => _init(date, new PersianDate());

        public static implicit operator DateTime(PersianDate date) => date._date;

        #region Private Section
        private static PersianDayOfWeek _mapPersianDayOfWeek(DayOfWeek day)
        {
            switch (day)
            {

                case System.DayOfWeek.Saturday:
                    return PersianDayOfWeek.Shanbe;
                case System.DayOfWeek.Sunday:
                    return PersianDayOfWeek.YekShanbe;
                case System.DayOfWeek.Monday:
                    return PersianDayOfWeek.DoShanbe;
                case System.DayOfWeek.Tuesday:
                    return PersianDayOfWeek.SeShanbe;
                case System.DayOfWeek.Wednesday:
                    return PersianDayOfWeek.ChaharShanbe;
                case System.DayOfWeek.Thursday:
                    return PersianDayOfWeek.PanjShanbe;
                case System.DayOfWeek.Friday:
                    return PersianDayOfWeek.Jome;
                default:
                    return PersianDayOfWeek.Shanbe;
            }
        }

        private Season _getSeason()
        {
            switch (Month)
            {
                case 1:
                case 2:
                case 3:
                    return Season.Spring;
                case 4:
                case 5:
                case 6:
                    return Season.Summer;
                case 7:
                case 8:
                case 9:
                    return Season.Autumn;
                case 10:
                case 11:
                case 12:
                    return Season.Winter;
                default:
                    return Season.Spring;
            }
        }

        private static KeyValuePair<bool, string> _holiday(PersianDate dt)
        {
            var result = Constants.constofHoliday.Where(x => (x.Month == dt.Month && x.Day == dt.Day) || (x.DayOfWeek == dt.DayOfWeek && x.DayOfWeek != PersianDayOfWeek.None));
            return new KeyValuePair<bool, string>(result.Any(), result.Any() ? string.Join(Environment.NewLine, result.Select(x => x.Description)) : "");
        }


        private PersianDate _getBeginOfSeason()
        {
            switch (Season)
            {
                case Season.Spring:
                    return new PersianDate(Year, 1, 1);
                case Season.Summer:
                    return new PersianDate(Year, 3, 1);
                case Season.Autumn:
                    return new PersianDate(Year, 7, 1);
                case Season.Winter:
                    return new PersianDate(Year, 10, 1);
                default:
                    return null;
            }
        }

        private PersianDate _getEndOfSeason()
        {
            switch (Season)
            {
                case Season.Spring:
                    return new PersianDate(Year, 3, 31);
                case Season.Summer:
                    return new PersianDate(Year, 6, 31);
                case Season.Autumn:
                    return new PersianDate(Year, 9, 30);
                case Season.Winter:
                    return new PersianDate(Year, 12, IsLeapYear ? 30 : 29);
                default:
                    return null;
            }
        }






        private PersianDate _goto(PersianDate date, int days, int months, int years)
        {
            var dt = _calendar.AddYears(date._date, years);
            dt = _calendar.AddMonths(dt, months);
            dt = _calendar.AddDays(dt, days);
            return dt;
        }

        private static PersianDate _init(DateTime date, PersianDate outDate)
        {
            outDate.Year = _calendar.GetYear(date);
            outDate.Month = _calendar.GetMonth(date);
            outDate.Day = _calendar.GetDayOfMonth(date);
            outDate.DayOfWeek = _mapPersianDayOfWeek(_calendar.GetDayOfWeek(date));
            outDate.IsLeapYear = _calendar.IsLeapYear(outDate.Year);
            outDate.DayOfYear = _calendar.GetDayOfYear(date);
            outDate._date = date;

            var holiday = _holiday(outDate);
            outDate.IsHoliday = holiday.Key;
            outDate.HolidayDescription = holiday.Value;
            return outDate;

        }


        #endregion


    }


}
