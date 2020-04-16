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
    public class ReservationsController : Controller
    {
        private ReserveItEntities db = new ReserveItEntities();

        // GET: Reservations
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                var reservations = db.Reservations.Include(r => r.User);
                return View(reservations.ToList());
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public ActionResult Index(AdminReservationSearchData data)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
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
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return RedirectToAction("Index");
                }
                return View(reservation);
            }

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult MyReservations()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                int userID = (int)Session["userID"];

                return View(db.Reservations.Where(rs => rs.UserID == userID).ToList());
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditMyReservation(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                if (id == null)
                {
                    return RedirectToAction("MyReservations");
                }
                Reservation reservation = db.Reservations.Find(id);
                return View(reservation);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditMyReservation([Bind(Include = "ReservationID,UserID,RoomID,CheckIn,CheckOut,StayLength,NightlyRate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                Reservation previous = db.Reservations.AsNoTracking().Where(rs => rs.ReservationID == reservation.ReservationID).Include(rs => rs.Room).First();

                for (DateTime date = previous.CheckOut; date.Date < reservation.CheckOut; date = date.AddDays(1))
                {
                    if (db.CheckAvailabilityOnDate(date, previous.Room.HotelID, reservation.RoomID).First() == 1)
                    {
                        return View("ReservationModificationFailure");
                    }
                }

                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("MyReservations");
        }

        public ActionResult CancelMyReservation(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return RedirectToAction("Index");
                }
                return View(reservation);
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("CancelMyReservation")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelMyReservationConfirmed(int id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                Reservation reservation = db.Reservations.Find(id);
                db.Reservations.Remove(reservation);
                db.SaveChanges();
                return RedirectToAction("MyReservations");
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return RedirectToAction("Index");
                }
                return View(reservation);
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationID,UserID,RoomID,CheckIn,CheckOut,StayLength,NightlyRate")] Reservation reservation)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (ModelState.IsValid)
                {
                    Reservation previous = db.Reservations.AsNoTracking().Where(rs => rs.ReservationID == reservation.ReservationID).Include(rs => rs.Room).First();

                    for (DateTime date = previous.CheckOut; date.Date < reservation.CheckOut; date = date.AddDays(1))
                    {
                        if (db.CheckAvailabilityOnDate(date, previous.Room.HotelID, reservation.RoomID).First() == 1)
                        {
                            return View("ReservationModificationFailure");
                        }
                    }

                    db.Entry(reservation).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return View(reservation);
            }

            return RedirectToAction("Index", "Admin");
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return RedirectToAction("Index");
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
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                Reservation reservation = db.Reservations.Find(id);
                db.Reservations.Remove(reservation);
                db.SaveChanges();
                return RedirectToAction("ReservationManagement", "Admin");
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