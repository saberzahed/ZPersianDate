using PersianDateTime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Zahed.PersianDateTime
{
    public class PersianDateExtended: PersianDate
    {
        public PersianDateExtended(int year,int month,int day):base( year,  month,  day)
        {

        }
        public bool IsToday => base.Equals(PersianDate.Today);

    }


}
