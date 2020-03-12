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
            return View();
        }

        public ActionResult Login(string email, string password)
        {
            using (Models.ReserveItEntities ent = new Models.ReserveItEntities())
            {
                Models.User result = ent.VerifyUserLogin(email, password).FirstOrDefault();

                if (result != null)
                {
                    Session["userID"] = result.UserID;
                    Session["email"] = result.Email;
                    Session["password"] = result.Password;
                    Session["accessLevel"] = result.UserLevel;
                    Session["firstName"] = result.Firstname;
                    Session["middleName"] = result.Middlename;
                    Session["lastName"] = result.Lastname;
                    Session["streetAddress"] = result.StreetAddress;
                    Session["cityAddress"] = result.CityAddress;
                    Session["stateAddress"] = result.StateAddress;
                    Session["countryAddress"] = result.CountryAddress;
                    Session["zipAddress"] = result.ZIPAddress;
                    Session["phone"] = result.Phone;

                    return RedirectToAction("Index", "Main");
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}