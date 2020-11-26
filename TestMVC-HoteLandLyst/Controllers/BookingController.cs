using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Models;
using TestMVC_HoteLandLyst.Extensions;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public void AddToBooking(BookingModel booking)
        {
            if (HttpContext.Session.Keys.Any(key => key == "UserBookings"))
            {
                FullReservationModel fullReservation = HttpContext.Session.GetObjectFromJson<FullReservationModel>("UserBookings");
                fullReservation.roomsToBook.Add(booking);
            }
            else
            {
                
            }
        }
    }
}
