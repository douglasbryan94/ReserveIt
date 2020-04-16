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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserManagement()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ReservationManagement()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ReservationManagement(Models.AdminReservationSearchData adminReservationSearchData)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                return View(adminReservationSearchData);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult HotelManagement()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}