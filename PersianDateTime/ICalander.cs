using System;
using System.Collections.Generic;
using System.Text;
using Zahed.PersianDateTime;

namespace PersianDateTime
{
    public interface ICalander
    {
        void NextMonth();
        void PreviousMonth();

        void NextYear();
        void PreviousYear();

        void Today();

    }
}
