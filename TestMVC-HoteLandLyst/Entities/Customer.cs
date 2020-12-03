using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC_HoteLandLyst.Models
{
    public class Customer
    {
        [DataType(DataType.Text)]
        public string FName { get; set; }

        [DataType(DataType.Text)]
        public string LName { get; set; }
        
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PostalCode)]
        public int CityAreaCode { get; set; }
    }
}
