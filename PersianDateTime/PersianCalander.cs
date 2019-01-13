using PersianDateTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zahed.PersianDateTime
{
    public class PersianCalander : ICalander
    {

        private List<PersianDate> _dates;
        private List<PersianDate> _deactive_Dates;

        private PersianDate _selectedDate;
        public List<PersianDate> Dates { get => _dates; private set => _dates = value; }

        public void NextMonth()
        {
            _selectedDate = _selectedDate.GotoMonths(1);
            Dates = _calanderDays(_selectedDate);
        }

        public void NextYear()
        {
            _selectedDate = _selectedDate.GotoYears(1);
        }

        public void PreviousMonth()
        {
            _selectedDate = _selectedDate.GotoMonths(1);
            Dates = _calanderDays(_selectedDate);
        }

        public void PreviousYear()
        {
            throw new NotImplementedException();
        }

        public void Today()
        {
            throw new NotImplementedException();
        }

        public List<PersianDate> _calanderDays(PersianDate date)
        {

            List<PersianDate> cals = new List<PersianDate>();

            var beginOfMonth = date.BeginOfMonth();
            var endOfMonth = date.EndOfMonth();

            var start = beginOfMonth.DayOfWeek - PersianDayOfWeek.Shanbe;
            var finish = PersianDayOfWeek.Jome - endOfMonth.DayOfWeek;


            for (int i = start; i > 0; i--)
            {
                cals.Add(beginOfMonth.GotoDays(-1 * i));
            }

            cals.Add(beginOfMonth);

            for (int i = beginOfMonth.Day; i < endOfMonth.Day; i++)
            {
                cals.Add(beginOfMonth.GotoDays(i));
            }

            for (int i = 1; i <= finish; i++)
            {
                cals.Add(endOfMonth.GotoDays(i));
            }

            return cals;
        }


    }
}
