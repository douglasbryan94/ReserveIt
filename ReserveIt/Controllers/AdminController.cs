using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReserveIt.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserManagement()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReservationManagement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReservationManagement(DateTime startDate, DateTime endDate)
        {
            Models.AdminReservationSearchData data = new Models.AdminReservationSearchData(startDate, endDate);

            return View(data);
        }

        public ActionResult HotelManagement()
        {
            return View();
        }
    }
}