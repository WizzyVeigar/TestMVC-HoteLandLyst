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
using System.Data;
using TestMVC_HoteLandLyst.Validators;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class RoomDetailsController : Controller
    {
        private ICreateMultiple<Room> createMultiple;
        private ISqlServerAccess sqlServer;

        public RoomDetailsController(ICreateMultiple<Room> createMultiple, ISqlServerAccess sqlServer)
        {
            this.createMultiple = createMultiple;
            this.sqlServer = sqlServer;
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
                ValidatorFactory.Instance.GetDateValidation().CheckValidEndDate(currentRoom);
                ValidatorFactory.Instance.GetPriceValidation().CheckDiscount(currentRoom);
                AddRoomToSession(currentRoom);

                return RedirectToAction("Index", "Rooms");
            }
            catch (InvalidCastException e)
            {
                throw e;
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

        [HttpGet]
        public DateTime[] GetUnavailableDates(int roomNumber = 101)
        {
            return ValidatorFactory.Instance.GetDateValidation().GetUnavailableDates(roomNumber, sqlServer);
        }
    }
}
