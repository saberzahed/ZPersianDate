using System;
using System.Globalization;
using System.Linq;

namespace ZPersianDateTime
{
    public struct PersianDate
    {
        private static readonly PersianCalendar Calendar = new();

        /// <summary>
        /// Create instance of Persian Date 
        /// </summary>
        /// <param name="year">Year of Jalali, between 1 to 9378</param>
        /// <param name="month">Month , Between 1 to 12 </param>
        /// <param name="day">Day , Between 1 to 31 ></param>
        public PersianDate(int year, int month, int day) : this()
        {
            if (year >= 1 && year <= 9378 && month >= 1 && month <= 12 && day >= 1 && day <= 31)
            {
                var date = Calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
                _init(date, ref this);
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
        public string NameOfDays => Constants.NameOfDays[(int)DayOfWeek];

        /// <summary>
        ///   Gets the month'name of the date represented by this instance.
        /// </summary>
        public string NameOfMonth => Constants.NameOfMonths[Month - 1];


        /// <summary>
        ///   Get half of the year represented by this instance.
        /// </summary>
        public HalfOfYear HalfOfSeason =>
            Season switch
            {
                Season.Spring or Season.Summer => HalfOfYear.First,
                Season.Autumn or Season.Winter => HalfOfYear.Second,
                _ => HalfOfYear.First
            };

        /// <summary>
        ///    Begin of Season of this instance
        /// </summary>
        /// <returns>Return new instance of Persian Date</returns>
        public PersianDate BeginOfSeason(bool newInstance = false) =>
            newInstance ? _getBeginOfSeason() : Copy().Change(day: 1, month: Constants.FirsOfSeason[Month]);

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
        public PersianDate GotoDays(int day = 1) => _goto(this, day, 0, 0);

        /// <summary>
        ///  Add/Subtract months to get new PersianDate
        /// </summary>
        /// <param name="months">months can negative and posstive</param>
        /// <returns>new PersianDate</returns>
        public PersianDate GotoMonths(int months = 1) => _goto(this, 0, months, 0);

        /// <summary>
        ///  Add/Subtract years to get new PersianDate
        /// </summary>
        /// <param name="years">years can negative and posstive</param>
        /// <returns>new PersianDate</returns>
        public PersianDate GotoYears(int years = 1) => _goto(this, 0, 0, years);


        /// <summary>
        ///  Change PersianDate via new value without create new instance 
        /// </summary>
        /// <param name="year">year can be null</param>
        /// <param name="month">year can be null</param>
        /// <param name="day">year can be null</param>
        /// <returns>this instance</returns>
        public PersianDate Change(int? year = null, int? month = null, int? day = null)
        {
            _init(new PersianDate((year ?? Year), (month ?? Month), (day ?? Day)), ref this);
            return this;
        }

        /// <summary>
        /// Clone of this instance 
        /// </summary>
        /// <returns>new PersianDate with this instance's values</returns>
        public PersianDate Copy()
        {
            var clone = new PersianDate();
            _init(this, ref clone);
            return clone;
        }


        /// <returns>The result like as yyyy/mm/dd format </returns>
        public override string ToString() => ToString("yyyy/mm/dd");

        /// <summary>
        /// Show formatted string of the instance 
        /// </summary>
        /// <returns>The result like as yyyymmdd format </returns>
        public string ToShortString() => ToString("yyyymmdd");


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
        public static PersianDate Parse(string date, char sep = char.MinValue)
        {
            try
            {
                int year;
                int month;
                int day;

                if (sep == char.MinValue)
                {
                    var dt = date.Replace("-", "")
                        .Replace("/", "").AsSpan();

                    if (!int.TryParse(dt, out _))
                        throw new FormatException($"{date} is invalid format  for parse.");

                    if (!(int.TryParse(dt.Slice(0, 4), out year) &&
                          int.TryParse(dt.Slice(4, 2), out month) &&
                          int.TryParse(dt.Slice(6, 2), out day)))
                        throw new FormatException($"{date} is invalid format  for parse.");
                }
                else
                {
                    var split = date.Split(sep);
                    if (split.Length != 3)
                        throw new FormatException($"{date} is invalid format  for parse.");

                    if (!(int.TryParse(split[0], out year) &&
                          int.TryParse(split[1], out month) &&
                          int.TryParse(split[2], out day)))
                        throw new FormatException($"{date} is invalid format  for parse.");
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
                output = default;
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
                   Year == obj.Year;
        }


        public static implicit operator PersianDate(DateTime date)
        {
            var convertedDate = new PersianDate
            {
                Year = Calendar.GetYear(date),
                Month = Calendar.GetMonth(date),
                Day = Calendar.GetDayOfMonth(date),
                DayOfWeek = _mapPersianDayOfWeek(Calendar.GetDayOfWeek(date)),
                IsLeapYear = Calendar.IsLeapYear(Calendar.GetYear(date)),
                DayOfYear = Calendar.GetDayOfYear(date),
                _date = date,
            };

            var (isHoliday, description) = _holiday(convertedDate);
            convertedDate.IsHoliday = isHoliday;
            convertedDate.HolidayDescription = description;

            return convertedDate;
        }

        public static implicit operator DateTime(PersianDate date) => date._date;

        #region Private Section

        private static PersianDayOfWeek _mapPersianDayOfWeek(DayOfWeek day)
        {
            return day switch
            {
                System.DayOfWeek.Saturday => PersianDayOfWeek.Shanbe,
                System.DayOfWeek.Sunday => PersianDayOfWeek.YekShanbe,
                System.DayOfWeek.Monday => PersianDayOfWeek.DoShanbe,
                System.DayOfWeek.Tuesday => PersianDayOfWeek.SeShanbe,
                System.DayOfWeek.Wednesday => PersianDayOfWeek.ChaharShanbe,
                System.DayOfWeek.Thursday => PersianDayOfWeek.PanjShanbe,
                System.DayOfWeek.Friday => PersianDayOfWeek.Jome,
                _ => PersianDayOfWeek.Shanbe
            };
        }

        private Season _getSeason()
        {
            return Month switch
            {
                >= 1 and <= 3 => Season.Spring,
                >= 4 and <= 6 => Season.Summer,
                >= 7 and <= 9 => Season.Autumn,
                >= 10 and <= 12 => Season.Winter,
                _ => Season.Spring
            };
        }

        private static (bool IsHoliday, string Description ) _holiday(PersianDate dt)
        {
            var isHoliday = dt.DayOfWeek == PersianDayOfWeek.Jome;
            var description = isHoliday ? Constants.NameOfDays[(int)PersianDayOfWeek.Jome] : string.Empty;

            var result = Constants.Holiday.FirstOrDefault(x => x.Month == dt.Month && x.Day == dt.Day);

            isHoliday |= result is not null;
            description += result?.Description;

            return (isHoliday, description);
        }


        private PersianDate _getBeginOfSeason()
        {
            return Season switch
            {
                Season.Spring => new PersianDate(Year, 1, 1),
                Season.Summer => new PersianDate(Year, 3, 1),
                Season.Autumn => new PersianDate(Year, 7, 1),
                Season.Winter => new PersianDate(Year, 10, 1),
                _ => default
            };
        }

        private PersianDate _getEndOfSeason()
        {
            return Season switch
            {
                Season.Spring => new PersianDate(Year, 3, 31),
                Season.Summer => new PersianDate(Year, 6, 31),
                Season.Autumn => new PersianDate(Year, 9, 30),
                Season.Winter => new PersianDate(Year, 12, IsLeapYear ? 30 : 29),
                _ => default
            };
        }

        private PersianDate _goto(PersianDate date, int days, int months, int years)
        {
            var dt = Calendar.AddYears(date._date, years);
            dt = Calendar.AddMonths(dt, months);
            dt = Calendar.AddDays(dt, days);
            return dt;
        }

        private static void _init(DateTime date, ref PersianDate outDate)
        {
            outDate.Year = Calendar.GetYear(date);
            outDate.Month = Calendar.GetMonth(date);
            outDate.Day = Calendar.GetDayOfMonth(date);
            outDate.DayOfWeek = _mapPersianDayOfWeek(Calendar.GetDayOfWeek(date));
            outDate.IsLeapYear = Calendar.IsLeapYear(outDate.Year);
            outDate.DayOfYear = Calendar.GetDayOfYear(date);
            outDate._date = date;

            var (isHoliday, description) = _holiday(outDate);
            outDate.IsHoliday = isHoliday;
            outDate.HolidayDescription = description;
        }

        #endregion
    }
}