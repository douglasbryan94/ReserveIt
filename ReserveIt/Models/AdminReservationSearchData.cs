using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReserveIt.Models
{
    public class AdminReservationSearchData
    {
        [Display(Name = "Check In Date")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { set;  get; }

        [Display(Name = "Check Out Date")]
        [DataType(DataType.Date)]
        public DateTime CheckOut { set; get; }
    }
}