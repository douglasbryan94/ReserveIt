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
    public class ManagersController : Controller
    {
        private ReserveItEntities db = new ReserveItEntities();

        // GET: Managers
        public ActionResult Index()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                return View(db.Managers.ToList());
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Managers/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (id == null)
                {
                    return RedirectToAction("ManagerManagement", "Admin");
                }
                Manager manager = db.Managers.Find(id);
                if (manager == null)
                {
                    return RedirectToAction("ManagerManagement", "Admin");
                }
                return View(manager);
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Managers/Create
        public ActionResult Create()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                return View();
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Managers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManagerID,Firstname,Middlename,Lastname")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Managers.Add(manager);
                db.SaveChanges();
                return RedirectToAction("ManagerManagement", "Admin");
            }

            return View(manager);
        }

        // GET: Managers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (id == null)
                {
                    return RedirectToAction("ManagerManagement", "Admin");
                }
                Manager manager = db.Managers.Find(id);
                if (manager == null)
                {
                    return RedirectToAction("ManagerManagement", "Admin");
                }
                return View(manager);
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManagerID,Firstname,Middlename,Lastname")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManagerManagement", "Admin");
            }
            return View(manager);
        }

        // GET: Managers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (id == null)
                {
                    return RedirectToAction("ManagerManagement", "Admin");
                }
                Manager manager = db.Managers.Find(id);
                if (manager == null)
                {
                    return RedirectToAction("ManagerManagement", "Admin");
                }
                return View(manager);
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = db.Managers.Find(id);
            db.Managers.Remove(manager);
            db.SaveChanges();
            return RedirectToAction("ManagerManagement", "Admin");
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
