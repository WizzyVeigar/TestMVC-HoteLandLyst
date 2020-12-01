using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Extensions;
using TestMVC_HoteLandLyst.Models;
using System.Diagnostics;
using TestMVC_HoteLandLyst.DalClasses;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class CheckOutController : Controller
    {
        FullReservationModel fullReservation;

        public IActionResult Index(List<BookingModel> userBookings)
        {
            try
            {
                fullReservation = new FullReservationModel() { RoomsToBook = userBookings };
                return View(fullReservation);
            }
            catch (Exception)
            {
                return View(new ErrorViewModel() { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

        }

        public void MakeReservation(Customer customerValues)
        {
            //List<BookingModel> reservations = GetReservations();

            //Make in factory
            fullReservation.Customer = customerValues;

            MsSqlConnection.Instance.MakeReservation(fullReservation);
        }


        public List<BookingModel> GetReservations()
        {
            return HttpContext.Session.GetObjectFromJson<List<BookingModel>>("UserBookings");
        }
    }
}
