using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.Models
{
    //Final model stored in session for finalizing bookings
    public class FullReservationModel
    {
        /// <summary>
        /// List of rooms customer wants booked
        /// </summary>
        public List<BookingModel> RoomsToBook { get; set; }
        /// <summary>
        /// Customer who booked the rooms
        /// </summary>
        public Customer Customer { get; set; }
    }
}
