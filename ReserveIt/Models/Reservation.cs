//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReserveIt.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public int ReservationID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> RoomID { get; set; }
        public System.DateTime CheckIn { get; set; }
        public Nullable<int> StayLength { get; set; }
        public System.DateTime CheckOut { get; set; }
        public Nullable<decimal> NightlyRate { get; set; }
    
        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
    }
}
