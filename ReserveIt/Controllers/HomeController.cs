using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReserveIt.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Models.DTO.HotelShort> hotelShorts;

            using (Models.ReserveItEntities ent = new ReserveIt.Models.ReserveItEntities())
            {
                hotelShorts = ent.Hotels.Select(x => new ReserveIt.Models.DTO.HotelShort()
                {
                    HotelID = x.HotelID,
                    StreetAddress = x.StreetAddress,
                    CityAddress = x.CityAddress,
                    StateAddress = x.StateAddress,
                    Description = x.Description
                }).ToList();
            }

            return View(hotelShorts);
        }
    }
}