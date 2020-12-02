using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using TestMVC_HoteLandLyst.Extensions;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.Factories
{
    public class BookingModelFactory
    {
        private static BookingModelFactory instance;
        public static BookingModelFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookingModelFactory();
                }
                return instance;
            }
        }

        public BookingModel CreateBookingModel()
        {
            return new BookingModel();
        }
        public List<BookingModel> GetSessionReservations(ISession currentSession)
        {
            List<BookingModel> bookingModels = currentSession.GetObjectFromJson<List<BookingModel>>("UserBookings");
            if (bookingModels == null || bookingModels.Count == 0)
            {
                return null;
            }

            return bookingModels;
            
        }
    }
}
