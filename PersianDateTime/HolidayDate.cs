namespace ZPersianDateTime
{
    internal class HolidayDate
    {
        public static HolidayDate Create( int month, int day, string description)=>
             new HolidayDate(month, day, description);
    
        private HolidayDate( int month, int day, string description)
        {
            Month = month;
            Day = day;
            Description = description;
        }
        public int Month { get;  }
        public int Day { get;  }
        public string Description { get;  }


    }
}
