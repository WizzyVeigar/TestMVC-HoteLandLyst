using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.Validators
{
    public class PriceValidation
    {
        /// <summary>
        /// Reduces the price if the days stayed is at/over 7 days
        /// </summary>
        /// <param name="currentRoom">the room currently being booked</param>
        public void CheckDiscount(BookingModel currentRoom)
        {
            if ((currentRoom.EndDate - currentRoom.StartDate).TotalDays >= 7)
            {
                decimal price = decimal.Parse(currentRoom.Room.ToString());
                price *= (decimal)0.9;
                currentRoom.ReservationPrice = price;
            }
        }
    }
}
