using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

        public BookingModel CreateSingle()
        {
            return new BookingModel();
        }
    }
}
