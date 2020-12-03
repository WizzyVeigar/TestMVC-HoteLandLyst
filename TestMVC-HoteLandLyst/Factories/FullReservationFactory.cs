using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.Factories
{
    public class FullReservationFactory
    {
        private static FullReservationFactory instance;

        public static FullReservationFactory Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new FullReservationFactory();
                }
                return instance;
            }
        }


        public FullReservationModel CreateSingle()
        {
            return new FullReservationModel();
        }
        public FullReservationModel CreateSingle(List<BookingModel> models)
        {
            return new FullReservationModel(models);
        }
    }
}
