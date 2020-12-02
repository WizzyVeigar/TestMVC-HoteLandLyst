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
using TestMVC_HoteLandLyst.Factories;

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

        public IActionResult Index()
        {
            try
            {
                //Make in factory
                fullReservation = new FullReservationModel(GetReservations());
                return View(fullReservation);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel());
            }

        }

        public void MakeReservation(Customer customerValues)
        {
            //Problematic singleton?
            fullReservation.RoomsToBook = GetReservations();
            if (fullReservation.RoomsToBook == null)
            {
                throw new ArgumentNullException();
            }
            fullReservation.Customer = customerValues;

            ((MsSqlConnection)DataAccess).MakeReservation(fullReservation);
        }

        private List<BookingModel> GetReservations()
        {
            return BookingModelFactory.Instance.GetSessionReservations(HttpContext.Session);
        }
    }
}
