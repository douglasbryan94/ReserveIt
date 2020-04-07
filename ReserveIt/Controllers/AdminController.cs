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
            if (Session["accessLevel"] != null)
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ReservationManagement()
        {
            if (Session["accessLevel"] != null)
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ReservationManagement(DateTime startDate, DateTime endDate)
        {
            if (Session["accessLevel"] != null)
            {
                Models.AdminReservationSearchData data = new Models.AdminReservationSearchData(startDate, endDate);
                return View(data);
            }

            return RedirectToAction("Index");
        }

        public ActionResult HotelManagement()
        {
            if (Session["accessLevel"] != null)
            {
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}