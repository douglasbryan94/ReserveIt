using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReserveIt.Helpers
{
    public static class ReservationHelpers
    {
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day.AddDays(1))
            {
                yield return day;
            }
        }
    }
}