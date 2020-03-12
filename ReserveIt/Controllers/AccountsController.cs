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

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Email,Password,Firstname,Middlename,Lastname,StreetAddress,CityAddress,StateAddress,CountryAddress,ZIPAddress,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserLevel = 2;
                db.Users.Add(user);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}