using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Validators;

namespace TestMVC_HoteLandLyst.Factories
{
    public class ValidatorFactory
    {
        private static ValidatorFactory instance;

        public static ValidatorFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ValidatorFactory();
                }
                return instance;
            }
        }


        public DateValidation GetDateValidation()
        {
            return new DateValidation();
        }

        public PriceValidation GetPriceValidation()
        {
            return new PriceValidation();
        }
    }
}
