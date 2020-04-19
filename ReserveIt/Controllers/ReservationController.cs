using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReserveIt.Controllers
{
    public class ReservationController : Controller
    {
        [HttpPost]
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

        [HttpPost]
        public ActionResult SelectRoomType(Models.ReservationSearchData reservationSearchData)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                List<Models.Room> rooms = new Models.ReserveItEntities().Rooms.Where(r => r.HotelID == reservationSearchData.HotelID).ToList();

                foreach (Models.Room item in rooms)
                {
                    using (Models.ReserveItEntities db = new Models.ReserveItEntities())
                    {
                        int response = (int)db.CheckRoomAvailability(reservationSearchData.Dates.CheckIn, reservationSearchData.Dates.CheckOut, item.RoomID).First();

                        if (response == 0)
                        {
                            reservationSearchData.AvailableRooms.Add(item);
                        }
                    }
                }

                reservationSearchData.AvailableRoomTypes = reservationSearchData.AvailableRooms.Select(r => r.RoomType).Distinct().ToList();

                TempData["Model"] = reservationSearchData;
                TempData.Keep();

                return View(reservationSearchData);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ConfirmReservation(string roomType)
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                Models.ReservationSearchData data = (Models.ReservationSearchData)TempData["Model"];
                data.RoomTypeID = roomType;

                data.ReserveRoom = data.AvailableRooms.Where(r => r.RoomTypeID == roomType).First();

                data.RoomTypeDescription = data.AvailableRooms.Where(ar => ar.RoomID == data.ReserveRoom.RoomID).Select(rs => rs.RoomType.RoomTypeDescription).First();

                TempData["Model"] = data;
                TempData.Keep();

                return View(data);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SubmitReservation()
        {
            if (Session["accessLevel"] != null && (int)Session["accessLevel"] == 2)
            {
                Models.Reservation reservation = ((Models.ReservationSearchData)TempData["Model"]).ToEntity();
                System.Net.Mail.MailMessage mail;

                using (Models.ReserveItEntities db = new Models.ReserveItEntities())
                {
                    
                    Models.Room roomDetails = db.Rooms.AsNoTracking().Where(r => r.RoomID == reservation.RoomID).First();
                    reservation.UserID = (int)Session["userID"];

                    db.Reservations.Add(reservation);
                    db.SaveChanges();

                    mail = new System.Net.Mail.MailMessage(
                        "douglas.bryan94@gmail.com",
                        (string)Session["email"],
                        "Reservation Details for #" + reservation.ReservationID,
                        "");

                    mail.Body = "<h1>Thank you for staying with us</h1>We hope you enjoy your stay. We try our utmost to ensure you have a comfortable and enjoyable experience.<br/><br/><h3>Find your Reservation Details below</h3>Reservation #: " + reservation.ReservationID +
                        "<br/>Hotel Location: " + roomDetails.Hotel.StreetAddress + ", " + roomDetails.Hotel.CityAddress + ", " + roomDetails.Hotel.StateAddress + "<br/>Room #: " + roomDetails.RoomNumber + "<br />Check In: " + reservation.CheckIn.ToShortDateString() +
                        "<br/>Check Out: " + reservation.CheckOut.ToShortDateString() + "<br/>Direct Phone #: " + roomDetails.Hotel.Phone;
                    mail.IsBodyHtml = true;
                }

                using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient())
                {
                    client.Credentials = new System.Net.NetworkCredential("douglas.bryan94@gmail.com", "Intelligence94");
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.Send(mail);
                }
            }

            TempData.Clear();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
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