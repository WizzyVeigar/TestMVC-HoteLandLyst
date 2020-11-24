using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.Factories
{
    public class CustomerFactory : IFactory<Customer>
    {
        private static CustomerFactory instance;
        public static CustomerFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerFactory();
                }
                return instance;
            }
        }

        public IList<Customer> CreateAll()
        {
            throw new NotImplementedException();
        }
    }
}
