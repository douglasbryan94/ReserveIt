using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ReserveIt.Models
{
    [MetadataType(typeof(HotelMetadata))]
    public partial class Hotel { }

    [MetadataType(typeof(ManagerMetadata))]
    public partial class Manager { }

    [MetadataType(typeof(ReservationMetadata))]
    public partial class Reservation { }

    [MetadataType(typeof(RoomMetadata))]
    public partial class Room { }

    [MetadataType(typeof(RoomTypeMetadata))]
    public partial class RoomType { }

    [MetadataType(typeof(UserMetadata))]
    public partial class User : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (Models.ReserveItEntities db = new ReserveItEntities())
            {
                if (db.Users.Any(u => u.Email == Email))
                {
                    yield return new ValidationResult("A user with that email already exists", new[] { "Email" });
                }
            }
        }
    }

    [MetadataType(typeof(UserLevelMetadata))]
    public partial class UserLevel { }
}