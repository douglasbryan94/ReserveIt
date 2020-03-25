using System;
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
            if (Session["userID"] != null)
            {
                Models.Hotel hotel = new Models.Hotel();

                using (Models.ReserveItEntities db = new Models.ReserveItEntities())
                {
                    hotel = db.Hotels.Where(x => x.HotelID == hotelId).First();
                }

                TempData["hotelID"] = hotel.HotelID;
                TempData["hotelStreetAddress"] = hotel.StreetAddress;
                TempData["hotelCityAddress"] = hotel.CityAddress;
                TempData["hotelStateAddress"] = hotel.StateAddress;

                TempData.Keep();

                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SelectRoomType(DateTime CheckInDate, DateTime CheckOutDate)
        {
            if (Session["userID"] != null)
            {
                if (CheckInDate != null && CheckOutDate != null)
                {
                    TempData["checkIn"] = CheckInDate;
                    TempData["checkOut"] = CheckOutDate;
                    TempData["lengthOfStay"] = (CheckOutDate - CheckInDate).Days;

                    TempData.Keep();

                    return View();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ConfirmReservation(string roomType)
        {
            if (Session["userID"] != null)
            {
                using (Models.ReserveItEntities db = new Models.ReserveItEntities())
                {
                    TempData["roomID"] = db.GetAvailableRoomID((int)TempData["hotelID"], (DateTime)TempData["checkIn"], (DateTime)TempData["checkOut"], roomType).First();
                }

                TempData.Keep();

                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SubmitReservation()
        {
            if (Session["userID"] != null)
            {
                using (Models.ReserveItEntities db = new Models.ReserveItEntities())
                {
                    db.Reservations.Add(new Models.Reservation()
                    {
                        UserID = (int)Session["userId"],
                        RoomID = (int)TempData["roomID"],
                        CheckIn = (DateTime)TempData["checkIn"],
                        StayLength = (int)TempData["lengthOfStay"]
                    });
                    db.SaveChanges();
                }
                // Util.SQLConnection.SubmitReservation(TempData);
                return RedirectToAction("Index", "Main", new { redirectedFromBooking = true });
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CancelReservation()
        {
            if (Session["userID"] != null)
            {
                return RedirectToAction("Index", "Main");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}