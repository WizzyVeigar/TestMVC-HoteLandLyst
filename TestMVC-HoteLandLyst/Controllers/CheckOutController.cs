using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Extensions;
using TestMVC_HoteLandLyst.Models;
using System.Diagnostics;
using TestMVC_HoteLandLyst.DalClasses;
using TestMVC_HoteLandLyst.Interfaces;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class CheckOutController : Controller
    {
        FullReservationModel fullReservation;

        private IDataAccess DataAccess { get; set; }

        public CheckOutController(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }

        public IActionResult Index(List<BookingModel> userBookings)
        {
            try
            {
                //Make in factory
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

            fullReservation.Customer = customerValues;

            ((MsSqlConnection)DataAccess).MakeReservation(fullReservation);
        }


        public List<BookingModel> GetReservations()
        {
            return HttpContext.Session.GetObjectFromJson<List<BookingModel>>("UserBookings");
        }
    }
}
