using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReserveIt.Models
{
    public class ReservationSearchData
    {
        public int HotelID { set; get; }
        public string HotelStreetAddress { set; get; }
        public string HotelCityAddress { set; get; }
        public string HotelStateAddress { set; get; }
        public ReservationDates Dates { set; get; }
        public string RoomTypeID { set; get; }
        public string RoomTypeDescription { set; get; }
        public int RoomID { set; get; }

        public Reservation ToEntity()
        {
            Reservation temp = new Reservation();

            temp.RoomID = RoomID;
            temp.CheckIn = Dates.CheckIn;
            temp.CheckOut = Dates.CheckOut;

            return temp;
        }

        public class ReservationDates
        {
            [Display(Name = "Check In Date")]
            [DataType(DataType.Date)]
            [Required(ErrorMessage = "Please enter a Check In Date")]
            public DateTime CheckIn { get; set; }

            [Display(Name = "Check Out Date")]
            [DataType(DataType.Date)]
            [Required(ErrorMessage = "Please enter a Check Out Date")]
            public DateTime CheckOut { get; set; }
        }
    }
}