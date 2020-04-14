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
        public ActionResult Index()
        {
            if (Session["accessLevel"] != null)
            {
                return View(db.Users.Where(x => x.UserLevel == 2).ToList());
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["accessLevel"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Accounts/Create
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
            if (Session["accessLevel"] == null)
            {
                if (ModelState.IsValid)
                {
                    user.UserLevel = 2;
                    user.Password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(user.Password));
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Login");
                }

                return View(user);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["accessLevel"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
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
            if (Session["accessLevel"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("UserManagement", "Admin");
                }
                return View(user);
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["accessLevel"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
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
            if (Session["accessLevel"] != null)
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("UserManagement", "Admin");
            }

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
