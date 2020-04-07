﻿using System;
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
    public class ReservationsController : Controller
    {
        private ReserveItEntities db = new ReserveItEntities();

        // GET: Reservations
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["accessLevel"] != null)
            {
                var reservations = db.Reservations.Include(r => r.User);
                return View(reservations.ToList());
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public ActionResult Index(AdminReservationSearchData data)
        {
            if (Session["accessLevel"] != null)
            {
                int stayLength = (data.CheckOut - data.CheckIn).Days;
                var reservations = db.Reservations.Where(x => x.CheckIn == data.CheckIn && x.StayLength == stayLength).Include(r => r.User);
                return View(reservations.ToList());
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["accessLevel"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                return View(reservation);
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            if (Session["accessLevel"] != null)
            {
                ViewBag.UserID = new SelectList(db.Users, "UserID", "Email");
                return View();
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationID,UserID,RoomID,CheckIn,StayLength,NightlyRate,DiscountID")] Reservation reservation)
        {
            if (Session["accessLevel"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", reservation.UserID);
                return View(reservation);
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["accessLevel"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", reservation.UserID);
                return View(reservation);
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationID,UserID,RoomID,CheckIn,StayLength,NightlyRate,DiscountID")] Reservation reservation)
        {
            if (Session["accessLevel"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(reservation).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", reservation.UserID);
                return View(reservation);
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["accessLevel"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                return View(reservation);
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["accessLevel"] != null)
            {
                Reservation reservation = db.Reservations.Find(id);
                db.Reservations.Remove(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Admin");
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
