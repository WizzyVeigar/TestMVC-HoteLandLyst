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
using System.Text.Json;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class CheckOutController : Controller
    {
        FullReservationModel fullReservation;

        private ISqlServerAccess DataAccess { get; set; }

        public CheckOutController(ISqlServerAccess dataAccess)
        {
            DataAccess = dataAccess;
        }

        public IActionResult Index()
        {
            try
            {
                //Make in factory
                fullReservation = FullReservationFactory.Instance.CreateSingle(GetReservations());
                return View(fullReservation);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public IActionResult MakeReservation(Customer customerValues)
        {
            fullReservation = FullReservationFactory.Instance.CreateSingle(GetReservations());
            if (fullReservation.RoomsToBook == null)
            {
                throw new ArgumentNullException();
            }
            fullReservation.Customer = customerValues;
            if (!((CustomerAccess)DataAccess).FindCustomer(fullReservation.Customer.PhoneNumber))
            {
                ((CustomerAccess)DataAccess).CreateCustomer(fullReservation.Customer);
            }
            ((ReservationAccess)DataAccess).CreateReservation(fullReservation);

            return RedirectToAction("Index", "Rooms");
        }

        private List<BookingModel> GetReservations()
        {
            return BookingModelFactory.Instance.GetSessionReservations(HttpContext.Session);
        }
    }
}
