﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReserveIt.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult GetDateAndTime(int hotelId)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                Models.ReservationSearchData data;

                using (Models.ReserveItEntities db = new Models.ReserveItEntities())
                {
                    data = db.Hotels.Where(h => h.HotelID == hotelId).Select(h => new Models.ReservationSearchData()
                    {
                        HotelID = h.HotelID,
                        HotelStreetAddress = h.StreetAddress,
                        HotelCityAddress = h.CityAddress,
                        HotelStateAddress = h.StateAddress
                    }).First();
                }

                return View(data);
            }

            return RedirectToAction("Index", "Login");
        }

        public ActionResult SelectRoomType(Models.ReservationSearchData reservationSearchData)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                if (reservationSearchData.Dates.CheckIn != null && reservationSearchData.Dates.CheckOut != null)
                {
                    TempData["Model"] = reservationSearchData;
                    TempData.Keep();

                    return View(reservationSearchData);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ConfirmReservation(string roomType)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                Models.ReservationSearchData data = (Models.ReservationSearchData)TempData["Model"];
                data.RoomTypeID = roomType;

                using (Models.ReserveItEntities db = new Models.ReserveItEntities())
                {
                    data.RoomID = (int)db.GetAvailableRoomID(data.HotelID, data.Dates.CheckIn, data.Dates.CheckOut, data.RoomTypeID).First();
                    data.RoomTypeDescription = db.Rooms.Find(data.RoomID).RoomType.RoomTypeDescription;
                }

                TempData["Model"] = data;
                TempData.Keep();

                return View(data);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SubmitReservation()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                using (Models.ReserveItEntities db = new Models.ReserveItEntities())
                {
                    Models.Reservation reservation = ((Models.ReservationSearchData)TempData["Model"]).ToEntity();
                    reservation.UserID = (int)Session["userID"];

                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                }
            }

            TempData.Clear();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CancelReservation()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}