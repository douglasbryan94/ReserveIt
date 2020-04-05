using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReserveIt.Models
{
    public class AdminReservationSearchData
    {
        public DateTime CheckIn { get; }
        public DateTime CheckOut { get; }

        public AdminReservationSearchData(DateTime start, DateTime end)
        {
            CheckIn = start;
            CheckOut = end;
        }
    }
}