using System;
using System.Collections.Generic;

namespace ZPersianDateTime
{
    public class EqualityComparer : IEqualityComparer<PersianDate>
    {
        public bool Equals(PersianDate x, PersianDate y) => x.Equals(y);

        public int GetHashCode(PersianDate obj) => HashCode.Combine(obj.Year, obj.Month, obj.Day);
    }
}