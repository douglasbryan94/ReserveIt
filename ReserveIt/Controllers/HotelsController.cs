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
    public class HotelsController : Controller
    {
        private ReserveItEntities db = new ReserveItEntities();

        // GET: Hotels
        public ActionResult Index()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                return View(db.Hotels.Include(x => x.Manager).ToList());
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Hotel hotel = db.Hotels.Find(id);
                if (hotel == null)
                {
                    return RedirectToAction("Index");
                }
                return View(hotel);
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Hotels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Hotel hotel = db.Hotels.Find(id);
                if (hotel == null)
                {
                    return RedirectToAction("Index");
                }

                var managerData = db.Managers.Select(m => new
                {
                    m.ManagerID,
                    FullName = m.Firstname + " " + m.Lastname
                }).ToList();

                ViewBag.ManagerID = new SelectList(managerData, "ManagerID", "FullName");
                return View(hotel);
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HotelID,ManagerID,MaxCapacity,StreetAddress,CityAddress,StateAddress,CountryAddress,ZIPAddress,Phone,Description")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Hotel hotel = db.Hotels.Find(id);
                if (hotel == null)
                {
                    return RedirectToAction("Index");
                }
                return View(hotel);
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            db.Hotels.Remove(hotel);
            db.SaveChanges();
            return RedirectToAction("Index");
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
