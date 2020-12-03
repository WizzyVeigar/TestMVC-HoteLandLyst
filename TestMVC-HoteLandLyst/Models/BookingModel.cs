using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.Models
{
    //component for a full booking
    public class BookingModel
    {
        public Room Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [DisplayName()]
        public decimal ReservationPrice { get; set; }
    }
}
