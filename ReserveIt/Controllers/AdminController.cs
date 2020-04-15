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
            if ((int)Session["accessLevel"] == 1)
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ReservationManagement()
        {
            if ((int)Session["accessLevel"] == 1)
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ReservationManagement(Models.AdminReservationSearchData adminReservationSearchData)
        {
            if ((int)Session["accessLevel"] == 1)
            {
                return View(adminReservationSearchData);
            }

            return RedirectToAction("Index");
        }

        public ActionResult HotelManagement()
        {
            if ((int)Session["accessLevel"] == 1)
            {
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}