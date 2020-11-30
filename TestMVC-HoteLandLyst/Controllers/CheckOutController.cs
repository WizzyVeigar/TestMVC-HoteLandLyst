using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Extensions;
using TestMVC_HoteLandLyst.Models;
using TestMVC_HoteLandLyst.Managers;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class CheckOutController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
