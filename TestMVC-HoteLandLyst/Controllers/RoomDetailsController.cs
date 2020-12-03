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

            //Using singleton here, perhaps consider injecting an interface, like below.
            BookingModel bookingModel = BookingModelFactory.Instance.CreateBookingModel();

            try
            {
                bookingModel.Room = ((RoomFactory)createMultiple).GetSingle((int)id);

                HttpContext.Session.SetObjectAsJson("currentRoom", bookingModel);
                HttpContext.Response.Cookies.Append("currentRoom", bookingModel.Room.RoomNumber.ToString());

                return View(bookingModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Add the chosen room to the local list of booked rooms
        /// </summary>
        /// <param name="currentRoom">The current room you want to book</param>
        [HttpPost]
        public IActionResult AddToBooking(BookingModel currentRoom)
        {
            //There is a chance the user can have messed with the cookie....
            string roomNumber = HttpContext.Request.Cookies["currentRoom"];
            try
            {
                currentRoom.Room = ((RoomFactory)createMultiple).GetSingle(int.Parse(roomNumber));
                CheckValidEndDate(currentRoom);
                CheckDiscount(currentRoom);
                AddRoomToSession(currentRoom);

                return RedirectToAction("Index", "Rooms");
            }
            catch (InvalidCastException e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Reduces the price if the days stayed is at/over 7 days
        /// </summary>
        /// <param name="currentRoom">the room currently being booked</param>
        private void CheckDiscount(BookingModel currentRoom)
        {
            if ((currentRoom.EndDate - currentRoom.StartDate).TotalDays >= 7)
            {
                decimal price = decimal.Parse(currentRoom.Room.ToString());
                price *= (decimal)0.9;
                currentRoom.ReservationPrice = price;
            }
        }

        /// <summary>
        /// Adds the booking model to the session with UserBookings as key
        /// </summary>
        /// <param name="booking">The current booking you want to add to the "UserBookings" session</param>
        private void AddRoomToSession(BookingModel booking)
        {
            List<BookingModel> bookingModels;

            if (HttpContext.Session.Keys.Any(key => key == "UserBookings"))
            {
                bookingModels = HttpContext.Session.GetObjectFromJson<List<BookingModel>>("UserBookings");
            }
            else
            {
                //Maybe make somewhere else
                bookingModels = new List<BookingModel>();
            }

            bookingModels.Add(booking);
            HttpContext.Session.SetObjectAsJson("UserBookings", bookingModels);
        }

        /// <summary>
        /// Checks the EndDate for <paramref name="booking"/> and sets it if needed
        /// </summary>
        /// <param name="booking">The room which you want to check the end date</param>
        private void CheckValidEndDate(BookingModel booking)
        {
            if (booking.EndDate.Hour >= 10 && booking.EndDate.Minute > 0)
            {
                booking.EndDate = new DateTime(booking.EndDate.Year, booking.EndDate.Month, booking.EndDate.Day, 10, 0, booking.EndDate.Second);
            }
        }
    }
}
