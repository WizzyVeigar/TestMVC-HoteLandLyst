using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Factories;
using TestMVC_HoteLandLyst.Models;
using TestMVC_HoteLandLyst.Extensions;
using System.Diagnostics;
using TestMVC_HoteLandLyst.Interfaces;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class RoomDetailsController : Controller
    {
        private ICreateMultiple<Room> createMultiple;

        public RoomDetailsController(ICreateMultiple<Room> createMultiple)
        {
            this.createMultiple = createMultiple;
        }

        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BookingModel bookingModel = BookingModelFactory.Instance.CreateSingle();

            try
            {
                bookingModel.Room = ((RoomFactory)createMultiple).GetSingle((int)id);

                HttpContext.Session.SetObjectAsJson("currentRoom", bookingModel);
                HttpContext.Response.Cookies.Append("currentRoom", bookingModel.Room.RoomNumber.ToString());

                return View(bookingModel);
            }
            catch (Exception e)
            {
                //Room could not be found in instance.rooms
         
                throw e;
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
            string roomNumber = HttpContext.Request.Cookies["currentRoom"];

            currentRoom.Room = ((RoomFactory)createMultiple).GetSingle(int.Parse(roomNumber));
            //Temporary addition of hours
            currentRoom.StartDate.AddHours(10);

            if (HttpContext.Session.Keys.Any(key => key == "UserBookings"))
            {
                List<BookingModel> reservations = HttpContext.Session.GetObjectFromJson<List<BookingModel>>("UserBookings");
                reservations.Add(currentRoom);
            }
            else
            {
                List<BookingModel> bookingModels = new List<BookingModel>();
                bookingModels.Add(currentRoom);
                //new List<BookingModel>() { bookingModels }
                HttpContext.Session.SetObjectAsJson("UserBookings", bookingModels);
            }

            return RedirectToAction("Index", "Rooms");
        }
    }
}
