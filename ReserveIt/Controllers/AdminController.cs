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

        public ActionResult UserView()
        {
            using (Models.ReserveItEntities db = new Models.ReserveItEntities())
            {
                return View(db.Users.ToList());
            }
        }
    }
}