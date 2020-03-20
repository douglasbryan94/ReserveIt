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
                    TempData["checkIn"] = CheckInDate.ToShortDateString();
                    TempData["checkOut"] = CheckOutDate.ToShortDateString();
                    TempData["lengthOfStay"] = (CheckOutDate - CheckInDate).Days;

                    TempData.Keep();

                    return View();
                }
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult ConfirmReservation(string roomType)
        {
            if (Session["userID"] != null)
            {
                //TempData["roomID"] = Util.SQLConnection.GetAvailableRoomID(TempData, roomType);

                TempData.Keep();

                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SubmitReservation()
        {
            if (Session["userID"] != null)
            {
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