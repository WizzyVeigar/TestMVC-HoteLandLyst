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
                fullReservation = FullReservationFactory.Instance.CreateSingle(GetReservations());
                return View(fullReservation);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel());
            }

        }

        public void MakeReservation(Customer customerValues)
        {
            fullReservation = FullReservationFactory.Instance.CreateSingle(GetReservations());
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

        [HttpPost]
        public void doStuff()
        {
            string model = @"{
   'RoomsToBook':[
      {
                'Room':{
                    'RoomNumber':101,
            'DayPrice':695.00,
            'RoomAccessories':[
               {
                        'AccessoryName':'Eget k\u00F8kken',
                  'ExtraCharge':350.00
               }
            ]
         },
         'StartDate':'2020-12-04T12:19:00',
         'EndDate':'2020-12-16T10:00:00',
         'ReservationPrice':940.500
      }
   ],
   'Customer':{
                'FName':'bo',
      'LName':'larse',
      'Address':'Somewhere in the between',
      'PhoneNumber':'28199319',
      'Email':'Somewhereinthebetween@email.hello',
      'CityAreaCode':4130
   }
        }";
            FullReservationModel thing = JsonSerializer.Deserialize<FullReservationModel>(model);
            ((MsSqlConnection)DataAccess).MakeReservation(thing);
        }
    }
}
