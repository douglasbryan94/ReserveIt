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
    public class RoomsController : Controller
    {
        private ReserveItEntities db = new ReserveItEntities();

        // GET: Rooms
        [HttpGet]
        public ActionResult Index(int hotelID)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                var rooms = db.Rooms.Include(r => r.Hotel).Include(r => r.RoomType).Where(r => r.HotelID == hotelID);
                TempData["Hotel"] = db.Hotels.Where(h => h.HotelID == hotelID).First();
                TempData.Keep();

                return View(rooms.ToList());
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                ViewBag.RoomTypeID = new SelectList(db.RoomTypes, "RoomTypeID", "RoomTypeDescription");

                int roomNumber = db.Rooms.Where(r => r.HotelID == id).Count() + 1;

                return View(new Room() { HotelID = id, RoomNumber = roomNumber });
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "HotelID,RoomTypeID,RoomNumber,CurrentRate", Exclude = "RoomID")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { hotelID = room.HotelID });
        }

        // GET: Rooms/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 1)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Room room = db.Rooms.Find(id);
                if (room == null)
                {
                    return HttpNotFound();
                }
                ViewBag.HotelID = new SelectList(db.Hotels, "HotelID", "StreetAddress", room.HotelID);
                ViewBag.RoomTypeID = new SelectList(db.RoomTypes, "RoomTypeID", "RoomTypeDescription", room.RoomTypeID);
                return View(room);
            }

            return RedirectToAction("Index", "Admin");
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomID,HotelID,RoomTypeID,RoomNumber,CurrentRate")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { hotelID = room.HotelID });
            }
            ViewBag.HotelID = new SelectList(db.Hotels, "HotelID", "StreetAddress", room.HotelID);
            ViewBag.RoomTypeID = new SelectList(db.RoomTypes, "RoomTypeID", "RoomTypeDescription", room.RoomTypeID);
            return View(room);
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
