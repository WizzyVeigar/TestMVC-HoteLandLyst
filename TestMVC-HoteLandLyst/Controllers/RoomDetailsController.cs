using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Factories;
using TestMVC_HoteLandLyst.Models;
using TestMVC_HoteLandLyst.Extensions;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class RoomDetailsController : Controller
    {
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BookingModel bookingModel = BookingModelFactory.Instance.CreateSingle();

            try
            {
                bookingModel.Room = RoomFactory.Instance.Rooms.Where(room => room.RoomNumber == id).FirstOrDefault();
                HttpContext.Session.SetObjectAsJson("currentRoom", bookingModel);

                return View(bookingModel);
            }
            catch (Exception)
            {
                //Room could not be found in instance.rooms
                throw;
            }

            //How To do this properly?

        }

        /// <summary>
        /// Add the chosen room to the local list of booked rooms
        /// </summary>
        /// <param name="currentRoom">The current room you want to book</param>
        
        [HttpPost]
        public IActionResult AddToBooking(BookingModel currentRoom)
        {

            //Temporary addition of hours
            currentRoom.StartDate.AddHours(10);

            if (HttpContext.Session.Keys.Any(key => key == "UserBookings"))
            {
                List<BookingModel> reservations = HttpContext.Session.GetObjectFromJson<List<BookingModel>>("UserBookings");
                reservations.Add(currentRoom);
            }
            else
            {
                HttpContext.Session.SetObjectAsJson("UserBookings", new List<BookingModel>() { currentRoom });
            }

            return RedirectToAction("Index", "Rooms");
        }
    }
}
