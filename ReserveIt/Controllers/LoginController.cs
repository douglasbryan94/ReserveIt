using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ReserveIt.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (Session["accessLevel"] == null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login(string email, string password)
        {
            if (Session["accessLevel"] == null)
            {
                using (Models.ReserveItEntities ent = new Models.ReserveItEntities())
                {
                    Models.User result = ent.VerifyUserLogin(email, Convert.ToBase64String(Encoding.UTF8.GetBytes(password))).FirstOrDefault();

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
                    }
                }
            }

            return RedirectToAction("Index", "Login");
        }

        public ActionResult AdminLogin(string email, string password)
        {
            if (Session["accessLevel"] == null)
            {
                using (Models.ReserveItEntities ent = new Models.ReserveItEntities())
                {
                    Models.User result = ent.VerifyUserLogin(email, Convert.ToBase64String(Encoding.UTF8.GetBytes(password))).FirstOrDefault();

                    if (result != null)
                    {
                        if (result.UserLevel == 1)
                        {
                            /*Session["userID"] = result.UserID;
                            Session["email"] = result.Email;
                            Session["password"] = result.Password;*/
                            Session["accessLevel"] = result.UserLevel;
                            /*Session["firstName"] = result.Firstname;
                            Session["middleName"] = result.Middlename;
                            Session["lastName"] = result.Lastname;*/

                            return RedirectToAction("UserManagement", "Admin");
                        }
                    }
                }
            }

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Logout()
        {
            if (Session["accessLevel"] != null)
            {
                Session.Clear();
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult AdminLogout()
        {
            if (Session["accessLevel"] != null)
            {
                Session.Clear();
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}