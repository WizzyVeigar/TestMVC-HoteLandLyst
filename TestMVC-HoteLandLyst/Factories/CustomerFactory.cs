using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Managers;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.Factories
{
    public class CustomerFactory : ICreateSingle<Customer>
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

        public Customer CreateSingle(DataRow row)
        {
            Customer cust = new Customer();
            cust.PhoneNumber = row["customerPhone"].ToString();
            cust.FName = row["customerFName"].ToString();
            cust.LName = row["customerLName"].ToString();
            cust.Address = row["customerAddress"].ToString();
            cust.CityAreaCode = Convert.ToInt32(row["CityAreaCode"]);
            cust.Email = row["customerEmail"].ToString();
            return cust;
        }
    }
}
