using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReserveIt.Models
{
    public class HotelMetadata
    {
        [Required(ErrorMessage = "Please provide a unique hotel ID")]
        public int HotelID { get; set; }

        [Required(ErrorMessage = "Please provide a unique manager ID")]
        public Nullable<int> ManagerID { get; set; }

        [Required(ErrorMessage = "Please provide a maximum capacity")]
        [Display(Name = "Max Capacity")]
        public int MaxCapacity { get; set; }


        [Required(ErrorMessage = "Please provide a Street Address")]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Please provide a City Address")]
        [Display(Name = "City Address")]
        public string CityAddress { get; set; }

        [Required(ErrorMessage = "Please provide a State Address")]
        [Display(Name = "State Address")]
        public string StateAddress { get; set; }

        [Required(ErrorMessage = "Please provide a Country Address")]
        [Display(Name = "Country Address")]
        public string CountryAddress { get; set; }

        [Required(ErrorMessage = "Please provide a ZIP Address")]
        [Display(Name = "ZIP Address")]
        public string ZIPAddress { get; set; }

        [Required(ErrorMessage = "Please provide a Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please provide a description of the Hotel")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }

    public class ManagerMetadata
    {
        public int ManagerID { get; set; }

        [Required(ErrorMessage = "Please enter a First Name")]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Display(Name = "Middle Name")]
        public string Middlename { get; set; }

        [Required(ErrorMessage = "Please enter a Last Name")]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
    }

    public class ReservationMetadata
    {
        public int ReservationID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> RoomID { get; set; }

        [Required(ErrorMessage = "Please specify a check in date")]
        [Display(Name = "Check In Date")]
        [DataType(DataType.Date)]
        public System.DateTime CheckIn { get; set; }

        [Display(Name = "Length of Stay")]
        public Nullable<int> StayLength { get; set; }

        [Required(ErrorMessage = "Please specify a check out date")]
        [Display(Name = "Check In Date")]
        [DataType(DataType.Date)]
        public System.DateTime CheckOut { get; set; }

        [Display(Name = "Nightly Rate")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> NightlyRate { get; set; }
    }

    public class RoomMetadata
    {
        [Required(ErrorMessage = "Please provide a unique room ID")]
        public int RoomID { get; set; }
        public int HotelID { get; set; }
        public string RoomTypeID { get; set; }

        [Display(Name = "Hotel Room #")]
        public int RoomNumber { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Current Rate")]
        public decimal CurrentRate { get; set; }
    }

    public class RoomTypeMetadata
    {
        public string RoomTypeID { get; set; }

        [Display(Name = "Room Type")]
        public string RoomTypeDescription { get; set; }

        [Display(Name = "Max Occupancy")]
        public int MaxOccupancy { get; set; }
    }

    public class UserMetadata
    {
        [Required(ErrorMessage = "Please specify an Email Address")]
        [EmailAddress(ErrorMessage = "Please specify an Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please specify a password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please specify an Access Level")]
        [Display(Name = "Access Level")]
        public int UserLevel { get; set; }

        [Required(ErrorMessage = "Please specify a First Name")]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Display(Name = "Middle Name")]
        public string Middlename { get; set; }

        [Required(ErrorMessage = "Please specify a Last Name")]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Please specify a Street Address")]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Please specify a City Address")]
        [Display(Name = "City Address")]
        public string CityAddress { get; set; }

        [Required(ErrorMessage = "Please specify a State Address")]
        [Display(Name = "State Address")]
        public string StateAddress { get; set; }

        [Required(ErrorMessage = "Please specify a Country Address")]
        [Display(Name = "Country Address")]
        public string CountryAddress { get; set; }

        [Required(ErrorMessage = "Please specify a ZIP Address")]
        [Display(Name = "ZIP Address")]
        public string ZIPAddress { get; set; }

        [Required(ErrorMessage = "Please specify a Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [MinLength(10, ErrorMessage = "Please specify a Phone Number")]
        [MaxLength(11, ErrorMessage = "Please specify a Phone Number")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
    }

    public class UserLevelMetadata
    {
        public int UserLevelID { get; set; }
        
        [Display(Name = "Access Level")]
        public string UserLevelDescription { get; set; }
    }
}