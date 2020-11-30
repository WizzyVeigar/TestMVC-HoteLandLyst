using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Extensions;
using TestMVC_HoteLandLyst.Models;
using TestMVC_HoteLandLyst.DalClasses;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class CheckOutController : Controller
    {
        public IActionResult Index()
        {
            FullReservationModel fullReservation = new FullReservationModel() { RoomsToBook = GetReservations() };

            return View(fullReservation);
        }
        
        public void MakeReservation(Customer customerValues)
        {
            List<BookingModel> reservations = GetReservations();
            //Make in factory
            FullReservationModel reservationModel = new FullReservationModel { Customer = customerValues, RoomsToBook = reservations };

            MsSqlConnection.Instance.MakeReservation(reservationModel);
        }


        public List<BookingModel> GetReservations()
        {
            return HttpContext.Session.GetObjectFromJson<List<BookingModel>>("UserBookings");
        }
    }
}
