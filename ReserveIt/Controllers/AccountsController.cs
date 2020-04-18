using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReserveIt.Models;

namespace ReserveIt.Controllers
{
    public class AccountsController : Controller
    {
        private ReserveItEntities db = new ReserveItEntities();

        // GET: Accounts
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                return View(db.Users.ToList());
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult UserDetails()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        // GET: Accounts/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (id == null)
                {
                    return RedirectToAction("UserManagement", "Admin");
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return RedirectToAction("UserManagement", "Admin");
                }
                return View(user);
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Accounts/Create
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["accessLevel"] == null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Email,Password,UserLevel,Firstname,Middlename,Lastname,StreetAddress,CityAddress,StateAddress,CountryAddress,ZIPAddress,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserLevel = 2;
                user.Password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(user.Password));
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }

            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult AdminCreate()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                ViewBag.UserLevel = new SelectList(db.UserLevels, "UserLevelID", "UserLevelDescription");
                return View();
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminCreate([Bind(Include = "Email,Password,UserLevel,Firstname,Middlename,Lastname,StreetAddress,CityAddress,StateAddress,CountryAddress,ZIPAddress,Phone", Exclude = "UserID")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(user.Password));
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("UserManagement", "Admin");
            }

            return RedirectToAction("AdminCreate");
        }

        [HttpGet]
        public ActionResult ModifyPersonal(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                return View(db.Users.Find(id));
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult ModifyPersonal([Bind(Include = "UserID,Email,Password,UserLevel,Firstname,Middlename,Lastname,StreetAddress,CityAddress,StateAddress,CountryAddress,ZIPAddress,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                Session["firstName"] = user.Firstname;
                Session["middleName"] = user.Middlename;
                Session["lastName"] = user.Lastname;
                Session["streetAddress"] = user.StreetAddress;
                Session["cityAddress"] = user.CityAddress;
                Session["stateAddress"] = user.StateAddress;
                Session["countryAddress"] = user.CountryAddress;
                Session["zipAddress"] = user.ZIPAddress;

                return RedirectToAction("UserDetails");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult ModifyContact(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                return View(db.Users.Find(id));
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult ModifyContact([Bind(Include = "UserID,Email,Password,UserLevel,Firstname,Middlename,Lastname,StreetAddress,CityAddress,StateAddress,CountryAddress,ZIPAddress,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                Session["email"] = user.Email;
                Session["phone"] = user.Phone;

                return RedirectToAction("UserDetails");
            }
            return View(user);
        }

        // GET: Accounts/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (id == null)
                {
                    return RedirectToAction("UserManagement", "Admin");
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return RedirectToAction("UserManagement", "Admin");
                }
                return View(user);
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Email,Password,UserLevel,Firstname,Middlename,Lastname,StreetAddress,CityAddress,StateAddress,CountryAddress,ZIPAddress,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserManagement", "Admin");
            }

            return View(user);
        }

        // GET: Accounts/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (id == null)
                {
                    return RedirectToAction("UserManagement", "Admin");
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return RedirectToAction("UserManagement", "Admin");
                }
                return View(user);
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("UserManagement", "Admin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
