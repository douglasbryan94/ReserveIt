using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReserveIt.Models.DTO
{
    public class HotelShort
    {
        public int HotelID { set; get; }
        public string StreetAddress { set; get; }
        public string CityAddress { set; get; }
        public string StateAddress { set; get; }
        public string Description { set; get; }
    }
}